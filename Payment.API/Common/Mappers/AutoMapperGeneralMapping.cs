using AutoMapper;
using Payment.API.Application.Commands.Authorize;
using Payment.API.Application.Queries.Transactions.List;
using Payment.Domain.Model.Transaction;

namespace Payment.API.Common.Mappers
{
    public class AutoMapperGeneralMapping : Profile
    {
        public AutoMapperGeneralMapping()
        {
            CreateMap<AuthorizeCommand, Transaction>().ReverseMap();

            CreateMap<TransactionsListQueryResponse, Transaction>()
                .ReverseMap()
                .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Uid));
        }
    }
}
