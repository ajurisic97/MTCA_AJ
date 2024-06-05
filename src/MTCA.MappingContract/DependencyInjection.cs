using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.MappingContract;

public static class DependencyInjection
{
    public static IServiceCollection AddContract(this IServiceCollection services)
    {
        var mapperConfig = new AutoMapperConfiguration().Configure();
        IMapper mapper = mapperConfig.CreateMapper();

        services.AddSingleton(mapper);

        return services;
    }
}

