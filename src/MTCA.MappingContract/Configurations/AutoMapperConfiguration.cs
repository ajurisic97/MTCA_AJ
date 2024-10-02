using AutoMapper;
using MTCA.MappingContract.Catalog;
using MTCA.MappingContract.Profiles.Catalog;
using MTCA.MappingContract.Profiles.Identity;

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

            #region Catalog

            cfg.AddProfile<PersonProfile>();
            cfg.AddProfile<RegionProfile>();
            cfg.AddProfile<StreetProfile>();
            cfg.AddProfile<CityProfile>();
            cfg.AddProfile<CountryProfile>();

            #endregion

            #region Data

            #endregion

            #region Identity

            cfg.AddProfile<PermissionProfile>();
            cfg.AddProfile<RolePermissionProfile>();
            cfg.AddProfile<RoleProfile>();
            cfg.AddProfile<UserProfile>();

            #endregion

        });
        return config;
    }
}
