using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Money.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockDividendController : ControllerBase
    {
        // GET: api/StockDividend
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [Route("GetDataByDays")]
        public void GetDataByDays([FromBody] string value)
        {
        }

        /// <summary>
        /// 指定特定日期 顯示當天本益比前n名
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [Route("GetPERatioByDay")]
        public void GetPERatioByDay([FromBody] string value)
        {
        }


        /// <summary>
        /// 指定日期範圍、證券代號 顯示這段時間內殖利率 為嚴格遞增的最長天數並顯示開始、結束日期
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [Route("GetYieldRateInfoByDays")]
        public void GetYieldRateInfoByDays([FromBody] string value)
        {
        }

    }
}
