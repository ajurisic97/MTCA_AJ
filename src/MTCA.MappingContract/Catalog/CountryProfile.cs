using AutoMapper;
using MTCA.Domain.Models;
using MTCA.Shared.DtoModels.Catalog.City;
using MTCA.Shared.DtoModels.Catalog.Country;
using MTCA.Shared.DtoModels.Catalog.Region;
using MTCA.Shared.DtoModels.Catalog.Street;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.MappingContract.Catalog;
public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<List<Country>, List<CountryDto>>()
            .ConvertUsing<CountryConverter>();
        CreateMap<List<Country>, List<CountryExtendedDto>>()
    .ConvertUsing<CountryExtendedConverter>();
    }
    public class CountryConverter : ITypeConverter<List<Country>, List<CountryDto>>
    {
        public List<CountryDto> Convert(List<Country> source, List<CountryDto> destination, ResolutionContext context)
        {
            var result = new List<CountryDto>();
            foreach (var item in source)
            {
                var dto = new CountryDto()
                {
                    RegionId = item.RegionId,
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

    public class CountryExtendedConverter : ITypeConverter<List<Country>, List<CountryExtendedDto>>
    {
        public List<CountryExtendedDto> Convert(List<Country> source, List<CountryExtendedDto> destination, ResolutionContext context)
        {
            var result = new List<CountryExtendedDto>();
            foreach (var item in source)
            {
                var dto = new CountryExtendedDto()
                {
                    Latitude = item.Latitude,
                    Longitude = item.Longitude,
                    Id = item.Id,
                    Name = item.Name,
                };

                var dbRegion = item.Region;
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

                foreach (var city in item.Cities)
                {
                    dto.Cities.Add(new CityDto()
                    {
                        CountryId = city.CountryId,
                        Id = city.Id,
                        Latitude = city.Latitude,
                        Longitude = city.Longitude,
                        Name = city.Name,
                        RegionId = city.RegionId
                    });
                }
                result.Add(dto);
            }
            return result;
        }
    }
}