using Money.JobService.Interface;
using Money.JobService.WorkClass;
using Money.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Money.JobService
{

    /// <summary>
    /// Job工廠 如有後續新增 就從GetJob+ workclass裡面擴充
    /// </summary>
    public class JobFactory
    {
        private IDatabaseConnection _connection;
        public JobFactory(IDatabaseConnection databaseConnection)
        {
            _connection = databaseConnection;
        }


        public IJob GetJob(string targetJob)
        {
            var jobName = new Helper.StringHelper.JobName();

            if (targetJob.ToLower() == jobName.Dividend)
            {
                return new JobDividend(_connection);
            }
            else
            {
                return null;
            }
        }
    }
}
