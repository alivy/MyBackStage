﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backStageIDal
{
    public interface ISys_ButtonDal<T> : IBaseDal<T> where T : class, new()
    {
    }
}
