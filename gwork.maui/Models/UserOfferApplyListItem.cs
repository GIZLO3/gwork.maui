namespace gwork.maui.Models
{
    public class UserOfferApplyListItem
    {
        public User? User { get; set; }
        public Offer? Offer { get; set; }
        public UserOfferApplyStatusEnum Status { get; set; }
    }
}