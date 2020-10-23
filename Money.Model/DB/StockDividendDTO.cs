using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Money.Model.DB
{
    [Table("stock_dividend")]
   
    public class StockDividendDTO 
    {
        public long id { get; set; }

        public DateTime record_date { get; set; }

        public string code { get; set; }

        public string code_name { get; set; }

        public decimal? dividend_yield { get; set; }

        public int? dividend_year { get; set; }

        public decimal? pe_ratio { get; set; }

        public decimal? price_net_ratio { get; set; }

        public int? financial_year { get; set; }

        public int? financial_season { get; set; }

    }

}
