using AutoMapper;
using MTCA.Domain.Models;
using MTCA.Shared.DtoModels.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.MappingContract.Profiles.Identity;
public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<List<Role>, List<RoleDto>>()
            .ConvertUsing<RoleConverter>();
    }

    public class RoleConverter : ITypeConverter<List<Role>, List<RoleDto>>
    {
        public List<RoleDto> Convert(List<Role> source, List<RoleDto> destination, ResolutionContext context)
        {
            var result = new List<RoleDto>();
            foreach (var item in source)
            {
                var oneRole = new RoleDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                };
                result.Add(oneRole);
            }
            return result;
        }
    }
}
