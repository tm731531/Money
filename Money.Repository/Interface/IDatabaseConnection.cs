﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Money.Repository.Interface
{
   public interface IDatabaseConnection
    {
        IDbConnection Create();
    }
}
