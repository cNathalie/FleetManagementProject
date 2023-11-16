namespace FM_Domain.Interfaces
{
    public interface IFMTypeWagenRepository
    {
        public List<TypeWagen> TypeWagens { get; }
        public List<TypeWagen> RefreshTypeWagens();
        public void Insert(TypeWagen typeWagen);
        public void Update(TypeWagen typeWagen);
        public void Delete(TypeWagen typeWagen);
        public bool Exists(TypeWagen typeWagen);
    }
}

