using FM_Domain;

namespace FM_Domain.Interfaces
{
    public interface IFMTankkaartRepository
    {
        public List<Tankkaart> Tankkaarten { get; }
        public List<Tankkaart> RefreshTankkaarten();
        public void Insert(Tankkaart tankkaart);
        public void Update(Tankkaart tankkaart);
        public void Delete(Tankkaart tankkaart);
        public bool Exists(Tankkaart tankkaart);
    }
}
