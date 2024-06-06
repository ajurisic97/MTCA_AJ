using AutoMapper;
using MTCA.Domain.Models;
using MTCA.Shared.DtoModels.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.MappingContract.Profiles.Identity;
public class RolePermissionProfile : Profile
{
    public RolePermissionProfile()
    {
        CreateMap<List<Role>, List<RolePermissionDto>>()
            .ConvertUsing<RolePermissionConverter>();
    }
    public class RolePermissionConverter : ITypeConverter<List<Role>, List<RolePermissionDto>>
    {
        public List<RolePermissionDto> Convert(List<Role> source, List<RolePermissionDto> destination, ResolutionContext context)
        {
            var result = new List<RolePermissionDto>();
            foreach (var role in source)
            {
                var oneRole = new RolePermissionDto
                {
                    Id = role.Id,
                    Name = role.Name,
                };
                oneRole.Permissions = new List<PermissionDto>();
                foreach (var permission in role.Permissions)
                {
                    var onePermission = new PermissionDto
                    {
                        Id = permission.Id,
                        Name = permission.Name,
                    };
                    oneRole.Permissions.Add(onePermission);
                }

                result.Add(oneRole);
            }
            return result;
        }
    }
}
