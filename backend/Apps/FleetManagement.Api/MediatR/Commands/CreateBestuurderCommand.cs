using FM.Domain.Models;
using MediatR;

namespace FleetManagement.Api.MediatR.Commands
{
    public record CreateBestuurderCommand(DBestuurder Bestuurder) : IRequest<DBestuurder>;
}
