using System;
using System.Collections.Generic;
using System.Text;

namespace Money.Model.WebView
{

    public class GetPERatioByDayRequest
    {
        /// <summary>
        /// 特定日期 
        /// </summary>
        public string record_date { get; set; }
        /// <summary>
        /// 本益比前n名
        /// </summary>
        public int topN { get; set; }
    }


}
