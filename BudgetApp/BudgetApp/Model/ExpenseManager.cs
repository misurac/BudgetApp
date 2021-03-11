using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BudgetApp.Model
{
    public static class ExpenseManager
    {
        public static List<Expense> GetExpenses()
        {
            // It is a placeholder method
            //TODO read all the expenses stored in a folder and return to the UI
            var expenses = new List<Expense>();
            expenses.Add(new Expense("Costco Groceries",78.50f,ExpenseCategory.Food));
            expenses.Add(new Expense("House Rent", 450.00f, ExpenseCategory.Housing));
            expenses.Add(new Expense("Wallgreens pharmacy", 18.50f, ExpenseCategory.Medical));
            return expenses;
        }
        /**
         * Returns true when expense is saved to a file
         * Returns false when something went wrong,UI can inform it to the user
         * */
        public static bool SaveExpense(Expense e) {
            //These are placeholders method
            //TODO Save this expense to a file
            return true;
        }
    }
}
