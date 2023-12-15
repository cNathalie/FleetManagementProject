using AutoMapper;

namespace FleetManagement.Api.AutoMapper
{
    public partial class MappingConfig
    {
        public class DateOnlyToDateTimeConverter : ITypeConverter<DateOnly, DateTime>
        {
            public DateTime Convert(DateOnly source, DateTime destination, ResolutionContext context)
            {
                return new DateTime(source.Year, source.Month, source.Day, 0, 0, 0);
            }
        }
    }
}
