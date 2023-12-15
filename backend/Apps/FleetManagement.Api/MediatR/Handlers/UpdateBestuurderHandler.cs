using FleetManagement.Api.MediatR.Commands;
using FM.Domain.Interfaces;
using MediatR;

namespace FleetManagement.Api.MediatR.Handlers
{
    public class UpdateBestuurderHandler : IRequestHandler<UpdateBestuurderCommand>
    {
        private readonly IBestuurderRepository _bestuurdersRepository;

        public UpdateBestuurderHandler(IBestuurderRepository bestuurdersRepository)
        {
            _bestuurdersRepository = bestuurdersRepository;
        }

        public async Task Handle(UpdateBestuurderCommand request, CancellationToken cancellationToken)
        {
            await _bestuurdersRepository.UpdateAsync(request.Bestuurder);
        }
    }
}
