namespace FM_API;

public class TankkaartDTO
{
    public int Id { get; set; }

    public int Kaartnummer { get; set; }

    public DateTime Geldigheidsdatum { get; set; }

    public int Pincode { get; set; }

    public int BrandstofTypeId { get; set; }

    public string Brandstof { get; set; }

    public bool? Actief { get; set; }
}
