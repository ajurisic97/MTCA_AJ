using AutoMapper;
using MTCA.Domain.Models;
using MTCA.Shared.DtoModels.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.MappingContract.Profiles.Identity;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<List<User>, List<UserDto>>()
            .ConvertUsing<UserConverter>();
    }

    public class UserConverter : ITypeConverter<List<User>, List<UserDto>>
    {
        public List<UserDto> Convert(List<User> source, List<UserDto> destination, ResolutionContext context)
        {
            var result = new List<UserDto>();
            foreach (var item in source)
            {
                var oneUser = new UserDto
                {
                    Id = item.Id,
                    Username = item.Username,

                };
                result.Add(oneUser);
            }
            return result;
        }
    }
}
