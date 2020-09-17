using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Models;
using CoCoSql.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Get() {
            return Ok(1);
        }

        [HttpGet("CreateOrder")]
        public ActionResult CreateOrder() {
            // 订单数量
            var orderCount = new Random().Next(1, 3);
            // 获取商品
            var product = CoCoSqlContext.FirstOrDefault<Product>();
            if (product == null)
                return Ok("没有商品数据");

            var locked = RedisHelper.Get("Lock");
            if (!string.IsNullOrWhiteSpace(locked)) {
                return Ok("当前抢购人数过多!");
            }

            var isLock = RedisHelper.Set("Lock", DateTime.Now.ToString("G"), 10, CSRedis.RedisExistence.Nx);
            if (!isLock) {
                return Ok("加锁失败,当前抢购人数过多!");
            }

            var productId = product.Id;
            // 获取库存
            var inStock = CoCoSqlContext.FirstOrDefault<InStock>(x => x.ProductId == productId);
            if (inStock == null)
                return Ok("没有库存数据");

            // 库存不足
            if (orderCount > inStock.StockNumber)
                return Ok("库存不足");

            Order order = new Order() {
                Count = orderCount,
                ProductId = product.Id,
                ProductName = product.ProductName,
                SumAmount = orderCount * product.Price
            };
            CoCoSqlContext.Insert(order);
            CoCoSqlContext.Update<InStock>(x => x.ProductId == productId, new { StockNumber = inStock.StockNumber - orderCount });
            RedisHelper.Del("Lock");

            return Ok("创建订单成功");
        }
    }
}
