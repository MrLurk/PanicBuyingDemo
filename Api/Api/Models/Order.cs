using CoCoSql.Attributer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models {
    [Table("T_Order")]
    public class Order {
        [InsertExclusion]
        public int Id { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal SumAmount { get; set; }
    }
}
