using SQLite;

namespace gwork.maui.Models
{
    public class UserOfferApply
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OfferId { get; set; }
        public UserOfferApplyStatusEnum Status { get; set; }
    }
}