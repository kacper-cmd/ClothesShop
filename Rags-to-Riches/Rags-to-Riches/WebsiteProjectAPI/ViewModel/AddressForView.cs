namespace WebsiteProjectAPI.ViewModel
{
    public class AddressForView
    {
        public int HomeNumber { get; set; }

        public string Street { get; set; } = null!;

        public string City { get; set; } = null!;

        public string? ZipCode { get; set; }

        public string? Country { get; set; }

        public int AddressesId { get; set; }

        //public virtual ICollection<UserForView> Users { get; set; } = new List<UserForView>(); //change to UserForView
    }
}
