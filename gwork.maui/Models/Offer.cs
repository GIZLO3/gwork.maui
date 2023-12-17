using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gwork.maui.Models
{
    public class Offer
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? PositionName { get; set; }
        public ConcractType ConcractType { get; set; }
        public DateTime ExpireDate { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public decimal? SalaryLowest { get; set; }
        public decimal? SalaryHighest { get; set; }
        //public Firm? Firm { get; set; }
    }
}
