using FleetManagement.Api.MediatR.Commands;
using FM.Domain.Interfaces;
using FM.Domain.Models;
using MediatR;

namespace FleetManagement.Api.MediatR.Handlers
{
    public class CreateBestuurderHandler : IRequestHandler<CreateBestuurderCommand, DBestuurder>
    {
        private readonly IBestuurderRepository _bestuurderRepository;

        public CreateBestuurderHandler(IBestuurderRepository bestuurderRepo)
        {
            _bestuurderRepository = bestuurderRepo;
        }

        public async Task<DBestuurder> Handle(CreateBestuurderCommand request, CancellationToken cancellationToken)
        {
            var newBestuurder = await _bestuurderRepository.InsertAsync(request.Bestuurder);
            return newBestuurder;
        }
    }
}
