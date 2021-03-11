using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetApp.Model
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
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public ExpenseCategory Category { get; set; }
        public string Icon { get; set; }
        public Expense(string name, decimal amount, ExpenseCategory category)
        {
            Name = name;
            Amount = amount;
            Category = category;
            Date = DateTime.Now;

        }
        public Expense(ExpenseCategory category)
        {

        }
        public Expense()
        {

        }

    }
}

 
