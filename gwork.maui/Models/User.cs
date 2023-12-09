using SQLite;

namespace gwork.maui.Models
{
    public class User : ICloneable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public byte[]? Salt { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get ; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
