﻿using Dapper;
using Money.Model.DB;
using Money.Repository.Interface;
using System;
using System.Collections.Generic;
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
            catch(Exception ex)
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
    }
}
