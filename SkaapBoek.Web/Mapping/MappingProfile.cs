using AutoMapper;
using SkaapBoek.Core;
using SkaapBoek.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MilsPhase, MilsPhaseDto>().ReverseMap();
            CreateMap<MilsPhase, MilsPhaseEditViewModel>()
                .ForMember(dest => dest.MilsPhaseId, opt => 
                    opt.MapFrom(src => src.Id));
            CreateMap<MilsPhaseEditViewModel, MilsPhase>();
            CreateMap<MilsTask, MilsPhaseEditViewModel>().ReverseMap();
        }
    }
}
