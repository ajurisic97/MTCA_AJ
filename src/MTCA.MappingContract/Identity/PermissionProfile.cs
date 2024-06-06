using AutoMapper;
using MTCA.Domain.Models;
using MTCA.Shared.DtoModels.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.MappingContract.Profiles.Identity;
public class PermissionProfile : Profile
{
    public PermissionProfile()
    {
        CreateMap<List<Permission>, List<PermissionDto>>()
           .ConvertUsing<PermissionConverter>();
    }

    public class PermissionConverter : ITypeConverter<List<Permission>, List<PermissionDto>>
    {
        public List<PermissionDto> Convert(List<Permission> source, List<PermissionDto> destination, ResolutionContext context)
        {
            var result = new List<PermissionDto>();
            foreach (var item in source)
            {
                var onePermission = new PermissionDto
                {
                    Id = item.Id,
                    Name = item.Name,
                };
                result.Add(onePermission);
            }
            return result;
        }
    }
}
