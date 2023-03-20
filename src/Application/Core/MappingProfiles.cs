using Application.ItemEmployeeAssignments.Contracts;
using Application.Items;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Item, Item>(); //this map is used for posting & edit   
            CreateMap<Item, ItemDto>(); //this mapper is used by GetItems to map to ItemDto 

            CreateMap<ItemEmployeeAssignment, ItemEmployeeAssignmentResponse>();//this map is used for posting & edit   
            CreateMap<ItemEmployeeAssignmentResponse, Item> (); //this map is used for posting & edit   
            //CreateMap<ItemEmployeeAssignment, ItemEmployeeAssignmentRequest>()
            //    .ForMember(dest => dest.ResonForNotReturn, expression => ); //this map is used for posting & edit   
            CreateMap<ItemEmployeeAssignmentRequest, ItemEmployeeAssignment>(); //this map is used for posting & edit   

            CreateMap<AppUser, Profiles.Profile>()
                .ForMember(d =>
                    d.Image, o => o.MapFrom(s => 
                    s.UserPhotos.FirstOrDefault(x => x.IsMain).Url));
        }
    }
}
