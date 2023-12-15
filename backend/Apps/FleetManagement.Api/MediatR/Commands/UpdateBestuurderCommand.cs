using FM.Domain.Models;
using MediatR;

namespace FleetManagement.Api.MediatR.Commands
{
    public record UpdateBestuurderCommand(DBestuurder Bestuurder) : IRequest;
}
