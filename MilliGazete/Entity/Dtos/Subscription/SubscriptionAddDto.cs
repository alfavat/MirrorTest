namespace Entity.Dtos
{
    public class SubscriptionAddDto
    {
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
