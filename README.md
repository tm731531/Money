# Money

# 需求
+ 1.依照證券代號 搜尋最近n天的資料  
+ 2.指定特定日期 顯示當天本益比前n名  
+ 3.指定日期範圍、證券代號 顯示這段時間內殖利率 為嚴格遞增的最長天數並顯示開始、結束日期   

# 資料來源
https://www.twse.com.tw/zh/page/trading/exchange/BWIBBU_d.html  
https://www.twse.com.tw/exchangeReport/BWIBBU_d?response=csv&date=20201021&selectType=ALL



# 工作項目
|          | 工作      | 是否完成
| -------- | -------- | -------- 
| 1     | DB設計     |V  
| 2     | Job塞資料     |V  
| 3     | Webapi專案開發     | V


# 專案
|          | 名稱      | 功用
| -------- | -------- | -------- 
| 1     | Money     |job層  
| 2     | Money.Webapi     |webapi層  
| 3     | Money.Helper     |通用輔助
| 4     | Money.Repository     | orm操作層
| 5     | Money.Model     | 所有的模型跟viewmodel存放地
| 6     | Money.JobService     | 輔助job
| 7     | Money.WebService     | 輔助webapi
| 7     | Money.Test     | 測試


# 議題

1. 1 最近n天 如果遇到周六日跳過，遇到像是10/1那周跳過  
   2 表設計每日一張表或是使用儲存體的建立分割區  
   3 同樣一分的資料第二次塞入應該無效  
2. 1 job應該以量少支援多個爬蟲為主  
3. 1 嚴格遞增的邏輯運算以及檢察(Test)
4. 1 DBJOB取消單筆INSERT，使用大量的BCP(加速並且減低LOCK發生  
   2 讀取資料時 不使用nolock，避免髒讀   
   3 避免讀取過大量的資料後回來用linq收縮，因此Repository裡面多寫幾個function 等日後有共同方向去做。
 



# ApiSpec
依照證券代號 搜尋最近n天的資料  
/GetDataByDays  



```
input
{
  "code": "2330",
  "days": 1
}
output 
{
    "data": [
        {
            "id": 4513,
            "record_date": "2020-10-23T00:00:00",
            "code": "2330",
            "code_name": "台積電",
            "dividend_yield": 2.1,
            "dividend_year": 108,
            "pe_ratio": 25.77,
            "price_net_ratio": 6.81,
            "financial_year": 109,
            "financial_season": 2
        }
    ]
}

```

指定特定日期 顯示當天本益比前n名  
/GetPERatioByDay  



```
input
{
  "record_date": "0109-10-21",
  "topN": 3
}
output 
{
    "data": [
        {
            "id": 1340,
            "record_date": "2020-10-21T00:00:00",
            "code": "6505",
            "code_name": "台塑化",
            "dividend_yield": 3.56,
            "dividend_year": 108,
            "pe_ratio": 904.44,
            "price_net_ratio": 2.91,
            "financial_year": 109,
            "financial_season": 2
        },
        {
            "id": 1144,
            "record_date": "2020-10-21T00:00:00",
            "code": "3189",
            "code_name": "景碩",
            "dividend_yield": 1.39,
            "dividend_year": 108,
            "pe_ratio": 797.78,
            "price_net_ratio": 1.27,
            "financial_year": 109,
            "financial_season": 2
        },
        {
            "id": 1138,
            "record_date": "2020-10-21T00:00:00",
            "code": "3062",
            "code_name": "建漢",
            "dividend_yield": 1.04,
            "dividend_year": 108,
            "pe_ratio": 722.5,
            "price_net_ratio": 0.89,
            "financial_year": 109,
            "financial_season": 2
        }
    ]
}

```

指定日期範圍、證券代號 顯示這段時間內殖利率 為嚴格遞增的最長天數並顯示開始、結束日期  
/GetYieldRateInfoByDays  



```
input
{
  "code": "2330",
  "record_date_start": "0109-10-19",
  "record_date_end": "0109-10-23"
}
output 
{
    "total_days": 2,
    "min_pe_ratio_date": "2020-10-19T00:00:00",
    "max_pe_ratio_date": "2020-10-20T00:00:00"
}

```

# sql  
```

--Description : 避開假日的日期
Create FUNCTION [dbo].[f_date_without_public_holiday]()
returns @T table(  current_dates date,
	    rank_day int,rank_week int,rank_year int)
AS
begin 
declare @dt  int,@twodt int
set @dt =20
set @twodt =1
declare @dt_date  date
set @dt_date = GETDATE()
	WHILE (@twodt <= @dt)
	BEGIN
         if( DATEPART(dw, @dt_date) !=1 and DATEPART(dw, @dt_date) !=7 and  not exists(select 1 from [public_holiday] where target_date = @dt_date))
		 BEGIN
		 insert into  @T select @dt_date , @twodt,DATEPART(ww, @dt_date),DATEPART(yyyy, @dt_date)
		 set  @twodt=@twodt+1
		 END

    set @dt_date= DATEADD(day ,-1 ,@dt_date)
	END
  
  RETURN

END
```