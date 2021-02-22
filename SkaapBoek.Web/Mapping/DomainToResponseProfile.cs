using AutoMapper;
using SkaapBoek.Core;
using SkaapBoek.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.Mapping
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<MilsPhase, MilsPhaseCreateViewModel>();
            CreateMap<MilsPhaseCreateViewModel, MilsPhase>();
        }
    }
}
