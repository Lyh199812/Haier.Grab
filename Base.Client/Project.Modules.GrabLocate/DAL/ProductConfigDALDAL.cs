﻿using Base.Client.DAL;
using Base.Client.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Modules.GrabLocate
{
    public class ProductConfigDAL : BaseDBService
    {
        public ProductConfigDAL(GrabDBConfig dbConfig) :base(dbConfig)
        {
            
        }
    }
}
