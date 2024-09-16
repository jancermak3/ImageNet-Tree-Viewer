using AutoMapper;
using ImageNet.Core.Entities;

namespace ImageNet.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ImageNetItem, ImageNetItemDto>();
        }
    }
}