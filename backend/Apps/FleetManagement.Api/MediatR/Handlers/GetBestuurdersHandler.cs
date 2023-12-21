using FleetManagement.Api.MediatR.Queries;
using FM.Domain.Interfaces;
using FM.Domain.Models;
using MediatR;

namespace FleetManagement.Api.MediatR.Handlers
{
    public class GetBestuurdersHandler: IRequestHandler<GetBestuurdersQuery, IEnumerable<DBestuurder>>
    {
        private readonly IBestuurderRepository _bestuurdersRepository;

        public GetBestuurdersHandler(IBestuurderRepository bestuurdersRepository)
        {
            _bestuurdersRepository = bestuurdersRepository;
        }   

        public async Task<IEnumerable<DBestuurder>> Handle(GetBestuurdersQuery request, CancellationToken cancellationToken)
        => await _bestuurdersRepository.GetAllAsync();

    }
}
