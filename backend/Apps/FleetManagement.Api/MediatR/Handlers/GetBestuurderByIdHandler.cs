using FleetManagement.Api.MediatR.Queries;
using FM.Domain.Interfaces;
using FM.Domain.Models;

namespace FleetManagement.Api.MediatR.Handlers
{
    public class GetBestuurderByIdHandler 
    {
        private readonly IBestuurderRepository _bestuurdersRepository;

        public GetBestuurderByIdHandler(IBestuurderRepository bestuurdersRepository)
        {
            _bestuurdersRepository = bestuurdersRepository;
        }

        public async Task<DBestuurder> Handle(GetBestuurderByIdQuery request, CancellationToken cancellationToken)
        => await _bestuurdersRepository.GetByIdAsync(request.Id);
    }
}
