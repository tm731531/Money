using Money.Model.WebView;
using Money.Repository;
using Money.Repository.Interface;

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
    }
}
