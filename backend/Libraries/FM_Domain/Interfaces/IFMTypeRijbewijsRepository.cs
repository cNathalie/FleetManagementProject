using FM_Domain;

namespace FM_Domain.Interfaces
{
    public interface IFMTypeRijbewijsRepository
    {
        public Task<List<TypeRijbewijs>> GetTypesRijbewijsAsync();



        public List<TypeRijbewijs> TypesRijbewijs { get; }
        public List<TypeRijbewijs> RefreshTypesRijbewijs();
        public void Insert(TypeRijbewijs typeRijbewijs);
        public void Update(TypeRijbewijs typeRijbewijs);
        public void Delete(TypeRijbewijs typeRijbewijs);
        public bool Exists(TypeRijbewijs typeRijbewijs);
    }
}
