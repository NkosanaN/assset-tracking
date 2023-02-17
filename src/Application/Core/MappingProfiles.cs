using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Item, Item>();

            CreateMap<AppUser, Profiles.Profile>()
                .ForMember(d =>
                    d.Image, o => o.MapFrom(s => 
                    s.UserPhotos.FirstOrDefault(x => x.IsMain).Url));
        }
    }
}
