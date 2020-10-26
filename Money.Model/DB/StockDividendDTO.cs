using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Money.Model.DB
{
    [Table("stock_dividend")]
   
    public class StockDividendDTO 
    {
        /// <summary>
        /// 編號
        /// </summary>
        public long id { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime record_date { get; set; }

        /// <summary>
        /// 證券代號
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 證券名稱
        /// </summary>
        public string code_name { get; set; }

        /// <summary>
        /// 殖利率(%)
        /// </summary>
        public decimal? dividend_yield { get; set; }

        /// <summary>
        /// 股利年度
        /// </summary>
        public int? dividend_year { get; set; }

        /// <summary>
        /// 本益比
        /// </summary>
        public decimal? pe_ratio { get; set; }

        /// <summary>
        /// 股價淨值比
        /// </summary>
        public decimal? price_net_ratio { get; set; }

        /// <summary>
        /// 財報年
        /// </summary>
        public int? financial_year { get; set; }

        /// <summary>
        /// 財報季
        /// </summary>
        public int? financial_season { get; set; }

    }

}
