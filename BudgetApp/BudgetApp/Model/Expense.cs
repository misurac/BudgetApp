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
        public string ExpenseName { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }
        public ExpenseCategory Category { get; set; }
        public string FileName { get; set; }
        public string CategoryIconFile { get; set; }

        public Expense(String expenseName, float amount, DateTime date, ExpenseCategory category= ExpenseCategory.Other)
        {
            ExpenseName = expenseName;
            Amount = amount;
            Category = category;
            if (date == null)
            {
                date = DateTime.Now;
            }           
            Date = date;
            CategoryIconFile = $"/Assets/CategoryIcons/{Category}/{ExpenseName}.png";
        }
    }

    
}
