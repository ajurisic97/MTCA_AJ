using AutoMapper;
using MTCA.Domain.Models;
using MTCA.Shared.DtoModels.Catalog.City;
using MTCA.Shared.DtoModels.Catalog.Region;
using MTCA.Shared.DtoModels.Catalog.Street;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.MappingContract.Catalog;
public class StreetProfile : Profile
{
    public StreetProfile()
    {
        CreateMap<List<Street>, List<StreetDto>>()
            .ConvertUsing<StreetConverter>();
        CreateMap<List<Street>, List<StreetExtendedDto>>()
    .ConvertUsing<StreetExtendedConverter>();
    }
    public class StreetConverter : ITypeConverter<List<Street>, List<StreetDto>>
    {
        public List<StreetDto> Convert(List<Street> source, List<StreetDto> destination, ResolutionContext context)
        {
            var result = new List<StreetDto>();
            foreach (var item in source)
            {
                var dto = new StreetDto()
                {
                    RegionId = item.RegionId,
                    CityId = item.CityId,
                    Latitude = item.Latitude,
                    Longitude = item.Longitude,
                    Id = item.Id,
                    Name = item.Name,
                };
                result.Add(dto);
            }
            return result;
        }
    }

    public class StreetExtendedConverter : ITypeConverter<List<Street>, List<StreetExtendedDto>>
    {
        public List<StreetExtendedDto> Convert(List<Street> source, List<StreetExtendedDto> destination, ResolutionContext context)
        {
            var result = new List<StreetExtendedDto>();
            foreach (var item in source)
            {
                var dto = new StreetExtendedDto()
                {
                    Latitude = item.Latitude,
                    Longitude = item.Longitude,
                    Id = item.Id,
                    Name = item.Name,
                };
                var dbCity = item.City;
                var dbRegion = item.Region;
                var cityDto = new CityDto()
                {
                    Name = dbCity.Name,
                    Id = dbCity.Id,
                    CountryId = dbCity.CountryId,
                    Latitude = dbCity.Latitude,
                    Longitude = dbCity.Longitude,
                    RegionId = dbCity.RegionId
                };
                dto.City = cityDto;

                if (dbRegion != null)
                {
                    var regionDto = new RegionDto()
                    {
                        CustomRegionName = dbRegion.CustomRegionName,
                        Id = dbRegion.Id,
                        Latitude = dbRegion.Latitude,
                        Longitude = dbRegion.Longitude,
                        Name = dbRegion.Name,
                        ParentId = dbRegion.ParentId,
                        RegionType = dbRegion.RegionType,
                    };
                    dto.Region = regionDto;
                }
                result.Add(dto);
            }
            return result;
        }
    }
}