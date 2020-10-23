
using Money.JobService;
using Money.Repository;
using System;

namespace Money
{
    class Program
    {
        /// <summary>
        /// 爬蟲資料主程式
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            try
            {
                if (args.Length < 1)
                {
                    Console.WriteLine("Need Job Name");
                    return;
                }
                //"Dividend"  
                new JobFactory(new AzureDbConnectionHelper()).GetJob(args[0]).DoJob();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                Console.Read();
            }
            
        }
    }
}
