using FleetManagement.Api.MediatR.Commands;
using FM.Domain.Interfaces;
using MediatR;

namespace FleetManagement.Api.MediatR.Handlers
{
    public class DeleteBestuurderHandler : IRequestHandler<DeleteBestuurderCommand>
    {
        private readonly IBestuurderRepository _bestuurdersRepository;

        public DeleteBestuurderHandler(IBestuurderRepository bestuurdersRepository)
        {
            _bestuurdersRepository = bestuurdersRepository;
        }

        public async Task Handle(DeleteBestuurderCommand request, CancellationToken cancellationToken)
        {
            await _bestuurdersRepository.DeleteByIdAsync(request.Id);
        }
    }
}
