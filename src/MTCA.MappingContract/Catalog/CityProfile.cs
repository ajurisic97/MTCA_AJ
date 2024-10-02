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
public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<List<City>, List<CityDto>>()
            .ConvertUsing<CityConverter>();
        CreateMap<List<City>, List<CityExtendedDto>>()
    .ConvertUsing<CityExtendedConverter>();
    }
    public class CityConverter : ITypeConverter<List<City>, List<CityDto>>
    {
        public List<CityDto> Convert(List<City> source, List<CityDto> destination, ResolutionContext context)
        {
            var result = new List<CityDto>();
            foreach (var item in source)
            {
                var dto = new CityDto()
                {
                    RegionId = item.RegionId,
                    CountryId = item.CountryId,
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

    public class CityExtendedConverter : ITypeConverter<List<City>, List<CityExtendedDto>>
    {
        public List<CityExtendedDto> Convert(List<City> source, List<CityExtendedDto> destination, ResolutionContext context)
        {
            var result = new List<CityExtendedDto>();
            foreach (var item in source)
            {
                var dto = new CityExtendedDto()
                {
                    Latitude = item.Latitude,
                    Longitude = item.Longitude,
                    Id = item.Id,
                    Name = item.Name,
                };
                var dbCountry = item.Country;
                var countryDto = new CountryDto()
                {
                    Name = dbCountry.Name,
                    Id = dbCountry.Id,
                    Latitude = dbCountry.Latitude,
                    Longitude = dbCountry.Longitude,
                    RegionId = dbCountry.RegionId
                };
                dto.Country = countryDto;

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

                foreach (var street in item.Streets)
                {
                    dto.Streets.Add(new StreetDto()
                    {
                        CityId = street.CityId,
                        Id = street.Id,
                        Latitude = street.Latitude,
                        Longitude = street.Longitude,
                        Name = street.Name,
                        RegionId = street.RegionId
                    });
                }
                result.Add(dto);
            }
            return result;
        }
    }
}