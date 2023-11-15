using FM_Domain;

namespace FM_Domain.Interfaces
{
    public interface IFMBrandstoftypeRepository
    {
        public List<BrandstofType> Brandstoffen { get; }
        public List<BrandstofType> RefreshBrandstoffen();
        public void Insert(BrandstofType brandstofType);
        public void Update(BrandstofType brandstofType);
        public void Delete(BrandstofType brandstofType);
        public bool Exists(BrandstofType brandstofType);
    }
}
