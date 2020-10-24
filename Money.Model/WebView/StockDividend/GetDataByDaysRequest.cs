using System;
using System.Collections.Generic;
using System.Text;

namespace Money.Model.WebView
{
    public class GetDataByDaysRequest
    {
        /// <summary>
        /// 證券代號
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 最近n天
        /// </summary>
        public int days { get; set; }
    }

}
