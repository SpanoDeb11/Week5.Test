using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyG.Week5.Test.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
        public decimal Amount { get; set; }
        public bool Approved { get; set; }

        //FK
        public Category Category { get; set; }
    }
}
