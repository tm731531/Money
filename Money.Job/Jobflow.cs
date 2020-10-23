using Money.JobService.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Money.JobService
{
    public abstract class JobFlow : IJob
    {
        public bool DoJob()
        {
            try
            {
                if (GetData())
                {
                    if (ParseData())
                    {
                        if (SaveData())
                        {
                            Console.WriteLine("Job Done");
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Save Error");
                            return false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Parse Error");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Get Error");
                    return false;
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 爬取資料
        /// </summary>
        /// <returns></returns>
        public abstract bool GetData();
        /// <summary>
        /// 驗證並且準備資料
        /// </summary>
        /// <returns></returns>
        public abstract bool ParseData();
        /// <summary>
        /// 儲存資料
        /// </summary>
        /// <returns></returns>
        public abstract bool SaveData();
    }
}
