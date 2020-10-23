using System;
using System.Collections.Generic;
using System.Text;

namespace Money.Repository.Interface
{
    interface ICRUD<T>
    {
        int InsertDataDetail(T dataDto);
    }
}
