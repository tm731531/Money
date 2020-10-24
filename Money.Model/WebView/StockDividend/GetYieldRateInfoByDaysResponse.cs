using Money.Model.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Money.Model.WebView
{
    public class GetYieldRateInfoByDaysResponse
    {
        public int total_days { get; set; }
        public DateTime min_pe_ratio_date { get; set; }
        public DateTime max_pe_ratio_date { get; set; }
    }

}
