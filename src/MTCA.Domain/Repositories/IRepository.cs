﻿using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Domain.Repositories;

public interface IRepository<T> : IRepositoryBase<T>
    where T : class
{
}
