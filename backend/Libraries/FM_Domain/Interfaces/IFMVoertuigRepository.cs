namespace FM_Domain.Interfaces
{
    public interface IFMVoertuigRepository
    {
        public List<Voertuig> Voertuigen { get; }
        public List<Voertuig> RefreshVoertuigen();
        public Voertuig Insert(Voertuig voertuig);
        public void Update(Voertuig voertuig);
        public void Delete(Voertuig voertuig);
        public bool Exists(Voertuig voertuig);
    }
}
