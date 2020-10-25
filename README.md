# Money


# [UML](https://drive.google.com/file/d/1Qt7_ceeinz1Oqpda9JL0xaFZLHRUo3Ib/view?usp=sharing)


# version
|  | api | 功能 |
| -------- | -------- | -------- |
| 1.0     | StockDividend/GetDataByDays|依照證券代號 搜尋最近n天的資料|
||StockDividend/GetPERatioByDay|指定特定日期 顯示當天本益比前n名|
||StockDividend/GetYieldRateInfoByDays|指定日期範圍、證券代號 顯示這段時間內殖利率 為嚴格遞增的最長天數並顯示開始、結束日期|


# Api
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