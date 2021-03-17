using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetApp.Model
{
    public enum ExpenseCategory
    {
        Food,
        Housing,
        Insurance,
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
            //Assigning the category and telling to display the icon for the category
            if (Category.ToString() == "Food")
            {
                CategoryIconFile = "food.png";
            }
            if (Category.ToString() == "Housing")
            {
                CategoryIconFile = "housing.png";
            }
            if (Category.ToString() == "Insurance")
            {
                CategoryIconFile = "insurance.png";
            }
            if (Category.ToString() == "Medical")
            {
                CategoryIconFile = "medical.png";
            }
            if (Category.ToString() == "Other")
            {
                CategoryIconFile = "other.png";
            }
            if (Category.ToString() == "Saving")
            {
                CategoryIconFile = "saving.png";
            }
            if (Category.ToString() == "Transportation")
            {
                CategoryIconFile = "transportation.png";
            }
            if (Category.ToString() == "Utilities")
            {
                CategoryIconFile = "utilities.png";
            }

        }

    }

    
}
