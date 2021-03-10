using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetApp
{
    public enum ExpenseCategory
    {
        Housing,
        Transit,
        Groceries,
        Exercise,
        Travel,
        Other
    }
    public class Expense
    {

        public string Name { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }
        public ExpenseCategory Category { get; set; }
    }
}
