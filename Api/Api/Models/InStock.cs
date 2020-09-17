using CoCoSql.Attributer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models {
    [Table("T_InStock")]
    public class InStock {
        [InsertExclusion]
        public int Id { get; set; }
        /// <summary>
        /// 库存数量
        /// </summary>
        public int StockNumber { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }
    }
}
