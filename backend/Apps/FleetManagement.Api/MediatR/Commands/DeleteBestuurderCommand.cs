using FM.Domain.Models;
using MediatR;

namespace FleetManagement.Api.MediatR.Commands
{
    public record DeleteBestuurderCommand(int Id) : IRequest;
}
