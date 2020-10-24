using Dapper;
using Money.Model.DB;
using Money.Model.WebView;
using Money.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Money.Repository
{
    public class StockDividendRepository : BulkInsert<StockDividendDTO>, IDisposable, ICRUD<StockDividendDTO>
    {
        private IDatabaseConnection _DatabaseConnection;

        private bool disposedValue = false;
        public StockDividendRepository(IDatabaseConnection databaseConnection)
        {
            _DatabaseConnection = databaseConnection;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    return;
                }

                disposedValue = true;
            }
        }

        ~StockDividendRepository()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool BulkInsertData(List<StockDividendDTO> ls)
        {
            try
            {
                BulkInsertRecords(ref ls, "stock_dividend", _DatabaseConnection.Create().ConnectionString);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertDataDetail(StockDividendDTO dataDto)
        {
            try
            {
                string sqlCommand = $@"
                                    INSERT INTO [dbo].[stock_dividend]
                                                       ([record_date]
                                                       ,[code]
                                                       ,[code_name]
                                                       ,[dividend_yield]
                                                       ,[dividend_year]
                                                       ,[pe_ratio]
                                                       ,[price_net_ratio]
                                                       ,[financial_year]
                                                       ,[financial_season])
                                                 VALUES
                                                       (@record_date
                                                       ,@code
                                                       ,@code_name
                                                       ,@dividend_yield
                                                       ,@dividend_year
                                                       ,@pe_ratio
                                                       ,@price_net_ratio
                                                       ,@financial_year
                                                       ,@financial_season)
                                    ";
                using (var conn = _DatabaseConnection.Create())
                {
                    var result = conn.Execute(sqlCommand, dataDto);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<StockDividendDTO> SearchDataByDaysAndCode(GetDataByDaysRequest value)
        {
            try
            {
                string sqlCommand = $@"SELECT top (@days) *
                                              FROM [dbo].[stock_dividend] sd (nowait)
                                              inner join  f_date_without_public_holiday() fd  on  sd.record_date =  fd.current_dates
                                              where code = @code
                                              order by record_date desc";
                using (var conn = _DatabaseConnection.Create())
                {
                    var result = conn.Query<StockDividendDTO>(sqlCommand, new { days = value.days, code = value.code }).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StockDividendDTO> SearchPERatioByDay(GetPERatioByDayRequest value)
        {
            try
            {
                string sqlCommand = $@"SELECT top (@topN) *
                                              FROM [dbo].[stock_dividend] sd (nowait)
                                              inner join  f_date_without_public_holiday() fd  on  sd.record_date =  fd.current_dates
                                              where record_date = @record_date
                                              order by pe_ratio desc";
                using (var conn = _DatabaseConnection.Create())
                {
                    var result = conn.Query<StockDividendDTO>(sqlCommand, new { topN = value.topN, record_date = value.record_date }).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StockDividendDTO> SearchYieldRateInfoByDays(GetYieldRateInfoByDaysRequest value)
        {
            try
            {
                string sqlCommand = $@"SELECT  *
                                               FROM [dbo].[stock_dividend] sd (nowait)
                                               inner join  f_date_without_public_holiday() fd  on  sd.record_date =  fd.current_dates
                                               where record_date  between @record_date_start and @record_date_end
                                               and code =@code 
                                               order by record_date";
                using (var conn = _DatabaseConnection.Create())
                {
                    var result = conn.Query<StockDividendDTO>(sqlCommand, new
                    {
                        code = value.code,
                        record_date_start = value.record_date_start,
                        record_date_end = value.record_date_end
                    }).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
