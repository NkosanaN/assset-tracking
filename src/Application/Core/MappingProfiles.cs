using Application.Items;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Item, Item>(); //this mapper is used for posting & edit   
            CreateMap<Item, ItemDto>(); //this mapper is used by GetItem to map to ItemDto 

            CreateMap<AppUser, Profiles.Profile>()
                .ForMember(d =>
                    d.Image, o => o.MapFrom(s => 
                    s.UserPhotos.FirstOrDefault(x => x.IsMain).Url));
        }
    }
}
