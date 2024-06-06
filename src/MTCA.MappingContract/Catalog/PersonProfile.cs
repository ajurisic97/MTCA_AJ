

using AutoMapper;
using MTCA.Domain.Models;
using MTCA.Shared.DtoModels;
using MTCA.Shared.DtoModels.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.MappingContract.Profiles.Catalog;
public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<List<Person>, List<PersonDto>>()
            .ConvertUsing<PersonConverter>();
    }

    public class PersonConverter : ITypeConverter<List<Person>, List<PersonDto>>
    {
        public List<PersonDto> Convert(List<Person> source, List<PersonDto> destination, ResolutionContext context)
        {

            var result = new List<PersonDto>();

            foreach (var item in source)
            {
                var onePerson = new PersonDto()
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    CreatedBy= item.CreatedBy,
                    CreatedOn= item.CreatedOn,
                    LastModifiedBy= item.LastModifiedBy,
                    LastModifiedOn= item.LastModifiedOn,
                };

                result.Add(onePerson);
            }

            return result;
        }
    }
}

