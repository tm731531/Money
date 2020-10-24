using Money.Model.WebView;
using Money.Repository;
using Money.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Money.WebService.WorkClass
{
     public class StockDividendService
    {
        private IDatabaseConnection _connection;

        public StockDividendService(IDatabaseConnection databaseConnection)
        {
            _connection = databaseConnection;
        }
        public GetDataByDaysResponse GetDataByDays(GetDataByDaysRequest value) {
            GetDataByDaysResponse getDataByDaysResponse = new GetDataByDaysResponse();
            getDataByDaysResponse.data= new StockDividendRepository(_connection).SearchDataByDaysAndCode(value);
            return getDataByDaysResponse;
        }

        public GetPERatioByDayResponse GetPERatioByDay(GetPERatioByDayRequest value)
        {
            GetPERatioByDayResponse getPERatioByDayResponse = new GetPERatioByDayResponse();
            getPERatioByDayResponse.data = new StockDividendRepository(_connection).SearchPERatioByDay(value);
            return getPERatioByDayResponse;
        }

        public GetYieldRateInfoByDaysResponse GetYieldRateInfoByDays(GetYieldRateInfoByDaysRequest value)
        {
            var originData = new StockDividendRepository(_connection).SearchYieldRateInfoByDays(value);
            var getStrictlyIncreasing = StrictlyIncreasing(originData.Select(x=>x.dividend_yield).ToList());
            GetYieldRateInfoByDaysResponse getYieldRateInfoByDaysResponse = new GetYieldRateInfoByDaysResponse();
            getYieldRateInfoByDaysResponse.max_pe_ratio_date = originData[getStrictlyIncreasing.highPoint].record_date;
            getYieldRateInfoByDaysResponse.min_pe_ratio_date = originData[getStrictlyIncreasing.lowPoint].record_date;
            getYieldRateInfoByDaysResponse.total_days = getStrictlyIncreasing.highPoint - getStrictlyIncreasing.lowPoint + 1;
            return getYieldRateInfoByDaysResponse;
        }
        public (int lowPoint, int highPoint) StrictlyIncreasing(List<decimal?> targetData)
        {

            int tempLowPoint = 0;
            int tempHighPoint = 0;
            int lowPoint = 0;
            int highPoint = 0;
            if (targetData.Count() == 0) { return (0, 0); };
            for (int i = 1; i < targetData.Count(); ++i)
            {
                if (targetData[i] > targetData[i - 1]) tempHighPoint = i;
                if (targetData[i] <= targetData[i - 1] || i == targetData.Count() - 1)
                {
                    if (tempHighPoint - tempLowPoint > highPoint - lowPoint)
                    {
                        lowPoint = tempLowPoint;
                        highPoint = tempHighPoint;
                    }
                    tempLowPoint = i;
                }
            }

            return (lowPoint, highPoint);
        }
    }
}
