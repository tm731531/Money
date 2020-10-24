using System;
using System.Collections.Generic;
using System.Text;

namespace Money.Model.WebView
{
    public class GetYieldRateInfoByDaysRequest
    {

        /// <summary>
        /// 證券代號
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 日期範圍-頭
        /// </summary>
        public DateTime record_date_start { get; set; }
        /// <summary>
        /// 日期範圍-尾
        /// </summary>
        public DateTime record_date_end { get; set; }
    }

}
