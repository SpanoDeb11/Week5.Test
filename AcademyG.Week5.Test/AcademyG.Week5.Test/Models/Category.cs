using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyG.Week5.Test.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Type { get; set; } //Categoria

        public List<Expense> Expenses { get; set; }
    }
}
