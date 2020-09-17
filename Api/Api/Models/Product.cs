using CoCoSql.Attributer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models {
    [Table("T_Product")]
    public class Product {
        [InsertExclusion]
        public int Id { get; set; }
        /// <summary>
        /// 商品名
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Price { get; set; }
    }
}
