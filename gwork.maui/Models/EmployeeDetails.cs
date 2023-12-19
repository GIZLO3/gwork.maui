using SQLite;

namespace gwork.maui.Models
{
    public class EmployeeDetails
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public EducationEnum Education { get; set; }
        public string? CurrentJob { get; set; }
        public string? ProfessionSummary { get; set; }
        public string? Languages { get; set; }
        public string? Courses { get; set; }
    }
}