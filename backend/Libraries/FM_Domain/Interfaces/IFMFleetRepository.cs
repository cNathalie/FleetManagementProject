using FM_Domain;

namespace FM_Domain.Interfaces
{
    public interface IFMFleetRepository
    {
        public List<FleetMember> Fleet { get; }
        public List<FleetMember> RefreshFleet();
        public void Insert(FleetMember fleetMember);
        public void Update(FleetMember fleetMember);
        public void Delete(FleetMember fleetMember);
        public bool Exists(FleetMember fleetMember);
    }
}
