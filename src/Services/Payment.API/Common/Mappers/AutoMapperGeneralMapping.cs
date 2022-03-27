using AutoMapper;
using Payment.API.Application.Commands.Authorize;
using Payment.API.Application.Queries.Transactions.List;
using Payment.Domain.Model.Transaction;
using System.Text;

namespace Payment.API.Common.Mappers
{
    public class AutoMapperGeneralMapping : Profile
    {
        public AutoMapperGeneralMapping()
        {
            CreateMap<AuthorizeCommand, Transaction>().ReverseMap();

            CreateMap<Transaction, TransactionsListQueryResponse>()
                .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Uid));
        }
    }
}
