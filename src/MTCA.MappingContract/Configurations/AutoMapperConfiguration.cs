using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.MappingContract.Configurations;
public class AutoMapperConfiguration
{
    public MapperConfiguration Configure()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapMethod = (m => false);

        });
        return config;
    }
}
