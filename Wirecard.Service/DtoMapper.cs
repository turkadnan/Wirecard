using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wirecard.Core.Dtos;
using Wirecard.Core.Models;

namespace Wirecard.Business
{
    class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<CountryDto, Country>().ReverseMap();
            CreateMap<UserAppDto, UserApp>().ReverseMap();
        }
    }
}
