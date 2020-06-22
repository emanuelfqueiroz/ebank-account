using Application.TransferCommands.Transfer;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Account.Mappers
{
    public static class TransferMapper
    {
        public static IMapper mapper;
        static TransferMapper()
        {
            mapper = new MapperConfiguration(cfg =>
            {
                
            }).CreateMapper();
        }

    }
}
