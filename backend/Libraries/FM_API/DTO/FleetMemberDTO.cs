﻿namespace FM_API.DTO
{
    public class FleetMemberDTO
    {
        public int FleetMemberId { get; set; }
        public string BestuurderNaam { get; set; }
        public string BestuurderVoornaam { get; set; }
        public int TankkaartId { get; set; }
        public string VoertuigMerkModel { get; set; }
        public string VoertuigNummerplaat { get; set; }
        public string VoertuigChassisnummer { get; set; }
    }
}
