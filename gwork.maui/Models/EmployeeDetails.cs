using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gwork.maui.Models
{
    public class EmployeeDetails
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? Education { get; set; }
    }
}
