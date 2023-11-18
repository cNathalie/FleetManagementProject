namespace FM_API;

public class TankkaartDTO
{
    public int TankkaartId { get; set; }

    public int Kaartnummer { get; set; }

    public DateTime Geldigheidsdatum { get; set; }

    public int Pincode { get; set; }

    public string BrandstofType { get; set; }

    public bool? IsActief { get; set; }
}
