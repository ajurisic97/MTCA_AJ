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
public class RegionProfile : Profile
{
    public RegionProfile()
    {
        CreateMap<List<Region>, List<RegionDto>>()
            .ConvertUsing<RegionConverter>();
        CreateMap<List<Region>, List<RegionExtendedDto>>()
    .ConvertUsing<RegionExtendedConverter>();
    }

    
    public class RegionConverter : ITypeConverter<List<Region>, List<RegionDto>>
    {
        public List<RegionDto> Convert(List<Region> source, List<RegionDto> destination, ResolutionContext context)
        {
            var result = new List<RegionDto>();
            foreach (var item in source)
            {
                var itemDto = new RegionDto
                {
                    Id = item.Id,
                    CustomRegionName = item.CustomRegionName,
                    Latitude = item.Latitude,
                    Longitude = item.Longitude,
                    Name = item.Name,
                    ParentId = item.ParentId,
                    RegionType = item.RegionType
                };
                result.Add(itemDto);
            }
            return result;
        }
    }

    public class RegionExtendedConverter : ITypeConverter<List<Region>, List<RegionExtendedDto>>
    {
        public List<RegionExtendedDto> Convert(List<Region> source, List<RegionExtendedDto> destination, ResolutionContext context)
        {
            var result = new List<RegionExtendedDto>();
            foreach (var item in source)
            {
                var itemDto = new RegionExtendedDto
                {
                    Id = item.Id,
                    CustomRegionName = item.CustomRegionName,
                    Latitude = item.Latitude,
                    Longitude = item.Longitude,
                    Name = item.Name,
                    RegionType = item.RegionType
                };
                var parent = item.Parent;
                var children = item.Children;
                if (parent != null)
                {
                    var subDto = new RegionDto()
                    {
                        Id = parent.Id,
                        CustomRegionName = parent.CustomRegionName,
                        Latitude = parent.Latitude,
                        Longitude = parent.Longitude,
                        Name = parent.Name,
                        RegionType = parent.RegionType
                    };
                    itemDto.Parent = subDto;
                }
                if (children.Count != 0)
                {
                    foreach (var child in children)
                    {
                        var subDto = new RegionDto()
                        {
                            Id = child.Id,
                            CustomRegionName = child.CustomRegionName,
                            Latitude = child.Latitude,
                            Longitude = child.Longitude,
                            Name = child.Name,
                            RegionType = child.RegionType
                        };
                        itemDto.Children.Add(subDto);
                    }
                }
                if (item.Streets.Count != 0)
                {
                    foreach(var r in item.Streets)
                    {
                        var streetDto = new StreetDto()
                        {
                            Id = r.Id,
                            CityId = r.CityId,
                            Latitude = r.Latitude,
                            Longitude = r.Longitude,
                            Name = r.Name,
                        };
                        itemDto.Streets.Add(streetDto);
                    }
                }
                if (item.Cities.Count != 0)
                {
                    foreach (var city in item.Cities)
                    {
                        var cityDto = new CityDto()
                        {
                            Id = city.Id,
                            CountryId = city.CountryId,
                            Latitude = city.Latitude,
                            Longitude = city.Longitude,
                            Name = city.Name,
                        };
                        itemDto.Cities.Add(cityDto);
                    }
                }
                if (item.Countries.Count != 0)
                {
                    foreach (var country in item.Countries)
                    {
                        var countryDto = new CountryDto()
                        {
                            Id = country.Id,
                            Latitude = country.Latitude,
                            Longitude = country.Longitude,
                            Name = country.Name,
                        };
                        itemDto.Countries.Add(countryDto);
                    }
                }

                result.Add(itemDto);
            }
            return result;
        }
    }
}
