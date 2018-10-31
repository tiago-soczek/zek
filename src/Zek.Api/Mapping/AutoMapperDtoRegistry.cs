using AutoMapper;
using AutoMapper.Mappers;
using System;
using System.Linq;

namespace Zek.Api.Mapping
{
    public class AutoMapperDtoRegistry : Profile
    {
        public AutoMapperDtoRegistry()
        {
            AddConditionalObjectMapper().Where(DtoMapping);
        }

        private bool DtoMapping(Type source, Type destination)
        {
            // Course > CourseDto
            return source.Name == destination.Name + "Dto" ||
                   source.Name + "Dto" == destination.Name;
        }
    }
}
