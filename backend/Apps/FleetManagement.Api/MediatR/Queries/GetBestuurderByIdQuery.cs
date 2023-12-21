using FM.Domain.Models;
using MediatR;

namespace FleetManagement.Api.MediatR.Queries
{
    public record GetBestuurderByIdQuery(int Id) : IRequest<DBestuurder>;


}
