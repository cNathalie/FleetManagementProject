namespace FleetManagement.Api.DTOs.Tankkaart
{
    public class TankkaartOutgoingDTO
    {
        public int TankkaartId {  get; set; }
        public int Kaartnummer {  get; set; }
        public DateOnly Geldigheidsdatum { get; set; }
        public string? Brandstoftype { get; set; }
        public int Pincode { get; set; }
        public bool? isActief { get; set; }
    }
}
