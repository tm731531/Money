﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Money.Model.WebView;
using Money.Repository;
using Money.WebService.WorkClass;

namespace Money.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockDividendController : ControllerBase
    {
        StockDividendService _stockDividendService;
        public StockDividendController()
        {
            _stockDividendService = new StockDividendService(new AzureDbConnectionHelper());
        }
        // GET: api/StockDividend
        [HttpGet]
        [Route("Get")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        
        /// <summary>
        /// 依照證券代號 搜尋最近n天的資料
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [Route("GetDataByDays")]
        public ActionResult<GetDataByDaysResponse> GetDataByDays(GetDataByDaysRequest value)
        {
            if (!CheckGetDataByDaysModel(value)) { return BadRequest(new { message="資料有誤"});
            }
            GetDataByDaysResponse getDataByDaysResponse = 
                _stockDividendService.GetDataByDays(value);
            
            return Ok(getDataByDaysResponse);
        }

        

        /// <summary>
        /// 指定特定日期 顯示當天本益比前n名
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [Route("GetPERatioByDay")]
        public ActionResult<GetPERatioByDayResponse> GetPERatioByDay(GetPERatioByDayRequest value)
        {
            
            GetPERatioByDayResponse getPERatioByDayResponse = new GetPERatioByDayResponse();
            getPERatioByDayResponse.data = new List<Model.DB.StockDividendDTO>();
            getPERatioByDayResponse.data.Add(new Model.DB.StockDividendDTO() { code = "2331" });
            throw new Exception("未完成 ");
            return Ok(getPERatioByDayResponse);
        }


        /// <summary>
        /// 指定日期範圍、證券代號 顯示這段時間內殖利率 為嚴格遞增的最長天數並顯示開始、結束日期
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [Route("GetYieldRateInfoByDays")]
        public ActionResult<GetDataByDaysResponse> GetYieldRateInfoByDays(GetDataByDaysRequest value)
        {
            GetYieldRateInfoByDaysResponse getYieldRateInfoByDaysResponse = new GetYieldRateInfoByDaysResponse();
            getYieldRateInfoByDaysResponse.total_days = 5;
            getYieldRateInfoByDaysResponse.max_pe_ratio_date = DateTime.Now.AddDays(1);
            getYieldRateInfoByDaysResponse.min_pe_ratio_date = DateTime.Now.AddDays(-1);
            throw new Exception("未完成 ");

            return Ok(getYieldRateInfoByDaysResponse);
        }
        private bool CheckGetDataByDaysModel(GetDataByDaysRequest value)
        {
            bool result = true;
            if (string.IsNullOrEmpty(value.code)) { result = false; };
            if (value.days < 0) { result = false; };
            return result;
        }
    }
}