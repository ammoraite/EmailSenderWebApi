﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmmoraiteCollections;

namespace Interfases
{
    public interface ICatalog<ICategory>
    {
        public ConcurrentList<ICategory> Catergories { get; set; }
    }
}
