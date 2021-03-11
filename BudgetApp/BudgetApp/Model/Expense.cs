using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetApp.Model
{
    public enum ExpenseCategory
    {
        Food,
        Housing,
        Insuarance,
        Medical,
        Other,
        Saving,
        Transportation,
        Utilities
    }
    public class Expense
    {
        public string Name { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }
        public ExpenseCategory Category { get; set; }

        public Expense(String name, float amount, ExpenseCategory category= ExpenseCategory.Other)
        {
            Name = name;
            Amount = amount;
        }
    }

    
}
