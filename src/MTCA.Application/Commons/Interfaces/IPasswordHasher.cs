﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Commons.Interfaces;
public interface IPasswordHasher
{
    bool Verify(string passwordHash, string inputPassword);
    string Hash(string password);
}
