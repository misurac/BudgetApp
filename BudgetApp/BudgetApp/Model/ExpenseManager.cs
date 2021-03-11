using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetApp.Model
{
  public  class ExpenseManager
    {
        private static List<Expense> getExpenses()
        {
            var expenses = new List<Expense>();
            expenses.Add(new Expense(ExpenseCategory.Exercise));
            expenses.Add(new Expense(ExpenseCategory.Groceries));
            expenses.Add(new Expense(ExpenseCategory.Housing));
            expenses.Add(new Expense(ExpenseCategory.Transit));
            expenses.Add(new Expense(ExpenseCategory.Travel));
            expenses.Add(new Expense(ExpenseCategory.Other));


            return expenses;
        }
    }
}
