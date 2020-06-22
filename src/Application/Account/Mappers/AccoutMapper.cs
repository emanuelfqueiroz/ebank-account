using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Account.Mappers
{
    public static class AccountMapper
    {
        public static IMapper mapper;
        static AccountMapper()
        {
            mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountResponse, Domain.Models.Account>().ReverseMap();
            }).CreateMapper();
        }

        public static AccountResponse Map(this Domain.Models.Account model)
        {
            return mapper.Map<AccountResponse>(model);
        }
    }
}
