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
| 3     | Webapi專案開發     | 


# 議題

1. 1 最近n天 如果遇到周六日跳過，遇到像是10/1那周跳過  
   2 表設計每日一張表或是使用儲存體的建立分割區  
   3 同樣一分的資料第二次塞入應該無效  
2. 1 job應該以量少支援多個爬蟲為主  
3. 1 嚴格遞增的邏輯運算以及檢察
4. 1 DBJOB取消單筆INSERT，使用大量的BCP(加速並且減低LOCK發生
5. 1 讀取資料時 不使用nolock  



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
[
  {
    "code": "2330",
    "record_date": "109/10/19",
    "dividend_yield": 1.23,
    "dividend_year": 108,
    "pe_ratio": 9.54,
    "price_net_ratio": 1.25,
    "financial_year": 109,
    "financial_season": 2
  }
]

```

指定特定日期 顯示當天本益比前n名  
/GetPERatioByDay  



```
input
{
  "record_date": "109/10/22",
  "topN": 10
}
output 
[
  {
    "code": "2330",
    "record_date": "109/10/22",
    "dividend_yield": 1.23,
    "dividend_year": 108,
    "pe_ratio": 9.54,
    "price_net_ratio": 1.25,
    "financial_year": 109,
    "financial_season": 2
  }
]

```

指定日期範圍、證券代號 顯示這段時間內殖利率 為嚴格遞增的最長天數並顯示開始、結束日期  
/GetYieldRateInfoByDays  



```
input
{
  "code": "2330",
  "record_date_start": "109/10/22",
  "record_date_end": "109/10/23"
}
output 
{
  "total_days":2,
  "min_pe_ratio_date": "109/10/22",
  "max_pe_ratio_date": "109/10/23"
}

```