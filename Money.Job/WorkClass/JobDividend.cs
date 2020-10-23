using Money.Helper;
using Money.Model.DB;
using Money.Repository;
using Money.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Money.JobService.WorkClass
{
    public class JobDividend : JobFlow
    {
        private string _httpData = string.Empty;
        private DateTime _targetDate;
        private List<StockDividendDTO> _ls;
        private IDatabaseConnection _connection;
        public JobDividend(IDatabaseConnection databaseConnection)
        {
            _connection = databaseConnection;
        }
        public override bool GetData()
        {
            try
            {
                HttpHelper httpHelper = new HttpHelper();
                _targetDate = DateTime.Now;
                _httpData = httpHelper.GetData($"https://www.twse.com.tw/exchangeReport/BWIBBU_d?response=csv&date={_targetDate.ToString("yyyyMMdd")}&selectType=ALL", is950Encode: true);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override bool ParseData()
        {
            try
            {
                string[] spl = _httpData.Split("\r\n");
                _ls = new List<StockDividendDTO>();
                foreach (var data in spl)
                {
                    try
                    {
                        StockDividendDTO stockDividend = new StockDividendDTO();
                        var spd = data.Replace("\"", "").Split(",");
                        stockDividend.record_date = _targetDate;
                        stockDividend.code = (spd[0]);
                        stockDividend.code_name = (spd[1]);
                        stockDividend.dividend_yield = Convert.ToDecimal(spd[2]);
                        stockDividend.dividend_year = Convert.ToInt32(spd[3]);
                        stockDividend.pe_ratio = Convert.ToDecimal(spd[4]);
                        stockDividend.price_net_ratio = Convert.ToDecimal(spd[5]);
                        stockDividend.financial_year = Convert.ToInt32(spd[6].Split("/")[0]);
                        stockDividend.financial_season = Convert.ToInt32(spd[6].Split("/")[1]);
                        _ls.Add(stockDividend);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override bool SaveData()
        {
            try
            {
                new StockDividendRepository(_connection).BulkInsertData(_ls);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
