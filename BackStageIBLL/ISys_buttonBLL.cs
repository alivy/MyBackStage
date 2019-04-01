﻿using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackStageIBLL
{
    public interface ISys_ButtonBLL<T> : IBaseBLL<T> where T : class, new()
    {
        int GetButtonCount();
    }
}
