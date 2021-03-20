using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace BudgetApp.Model
{
    public static class ExpenseManager
    {
        //public static string saveBudgetFileName = "bud.txt";
        public static List<Expense> GetExpenses()
        {
            

            var expenses = new List<Expense>();
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            //Getting all .exp.txt files from specialFolder.
            var files = Directory.EnumerateFiles(Environment.GetFolderPath
                (Environment.SpecialFolder.LocalApplicationData), "*.exp.txt");
            foreach (var filename in files)
            {
                var content = File.ReadAllText(filename);

                /*Each filename content is a string seperated with commas. So, splitting with comma, and the
                array will be store in splittedCOntent*/
                var splittedContent = content.Split(',');
                //coverting string in to enum. after converting, the value will be stored in myCategory.
                Enum.TryParse(splittedContent[3], out ExpenseCategory myCategory);
                //Parsing the parameters in to its type.
                var expense = new Expense(splittedContent[0], float.Parse(splittedContent[1]),
                    DateTime.Parse(splittedContent[2]), myCategory);
                //Getting the file name for Each expense
                expense.FileName = filename;
                //Adding to expenses list
                expenses.Add(expense);
            }
            return expenses;
        }
        /**
         * Returns true when expense is saved to a file
         * Returns false when something went wrong,UI can inform it to the user
         * */

        //Will save Expense in to a file 
        public static bool SaveExpense(Expense e)
        {

            if ((e != null) && (e.Amount > 0))
            {
                //converting the object in to string and saving it to the file.
                var expenseProperties = e.ExpenseName + "," + e.Amount + "," + e.Date + "," + e.Category;
                if (string.IsNullOrEmpty(e.FileName))
                {
                    e.FileName = Path.Combine
                        (Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        $"{Path.GetRandomFileName()}.exp.txt");
                    
                }
                File.WriteAllText(e.FileName, expenseProperties);

            }
            return true;
        }

        public static bool SaveBudget(Budget e)
        {
            
            if (e !=null && e.BudgetAmount> 0)
            {
                //var budget = e;
                var expenseProperties = e.BudgetAmount + "," + (int)e.MonthInYear;
                if (string.IsNullOrEmpty(e.FileName))
                {
                    e.FileName = Path.Combine
                    (Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                   $"{Path.GetRandomFileName()}.bud.txt");

                }
                File.WriteAllText(e.FileName, expenseProperties);
            }
            return true;
        }

        public static bool DeleteBudget() {
            var files = Directory.EnumerateFiles(Environment.GetFolderPath
                (Environment.SpecialFolder.LocalApplicationData), "*.bud.txt");
            
            foreach (var file in files)
            {
                File.Delete(file);               
            }
            return true;
        }

        public static Budget ReadBudget(Month e)
        {
            if (e == Month.AllMonths)
            {
                var files = Directory.EnumerateFiles(Environment.GetFolderPath
                (Environment.SpecialFolder.LocalApplicationData), "*.bud.txt");
                var sumOfAllBudgets = 0;
                foreach (var file in files)
                {
                    var budget = File.ReadAllText(file);
                    var splittedContent = budget.Split(',');
                    sumOfAllBudgets += Convert.ToInt32(splittedContent[0]);

                }
                var b = new Budget(sumOfAllBudgets, Month.AllMonths);
                return b;
            }
            else
            {
                var files = Directory.EnumerateFiles(Environment.GetFolderPath
                (Environment.SpecialFolder.LocalApplicationData), "*.bud.txt");
                foreach (var file in files)
                {
                    var budget = File.ReadAllText(file);
                    var splittedContent = budget.Split(',');
                    var month = int.Parse(splittedContent[1]);
                    if (month == Convert.ToInt32(e))
                    {
                        var bu = new Budget(float.Parse(splittedContent[0]), e);
                        bu.FileName = file;
                        return bu;
                        
                    }
               }
                var b = new Budget(0, e);
                return b;
            }
            
        }

        public static float SumOfExpenses()
        {
            float sumOfExpensesAmount = 0;            
            var expenses = GetExpenses();
            for (var i = 0; i < expenses.Count; i++)
            {
                sumOfExpensesAmount += expenses[i].Amount;
            }
            return sumOfExpensesAmount;
        }

        public static float RemainingBudget(Month e)
        {
            float sumOfExpensesAmount = 0;
            var budget = ReadBudget(e);
            if (budget.BudgetAmount != 0 && budget.BudgetAmount > 0)
            {
                var expenses = GetExpensesByMonth(e);
                for (var i = 0; i < expenses.Count; i++)
                {
                    sumOfExpensesAmount += expenses[i].Amount;
                }
                var remainingBudget = budget.BudgetAmount - sumOfExpensesAmount;
                return remainingBudget;
            }
            else
                return 0;
        }

        public static void DeleteAllExpenses()
        {
            var files = Directory.EnumerateFiles(Environment.GetFolderPath
              (Environment.SpecialFolder.LocalApplicationData), "*.exp.txt");
            foreach (string filePath in files)
                File.Delete(filePath);
        }

        public static bool DeleteExpense(Expense e)
        {
            if (e.FileName != null && e.FileName != "") 
            {                
                File.Delete(e.FileName);
            }
            return true;
        }

        public static List<Expense> GetExpensesByMonth(Month e)
        {
            if (e == Month.AllMonths)
            {
                return GetExpenses();
            }
            else
            {
                var allExpenses = GetExpenses();
                var filteredExpenses = allExpenses.Where(expense => expense.Date.Month == Convert.ToInt32(e)).ToList();
                return filteredExpenses;
            }
        }





    }

}

