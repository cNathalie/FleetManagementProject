using FM.Domain.Models;
using MediatR;

namespace FleetManagement.Api.MediatR.Queries
{
    public record GetBestuurdersQuery() : IRequest<IEnumerable<DBestuurder>>;
}
