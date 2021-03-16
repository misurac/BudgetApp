using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace BudgetApp.Model
{
    public static class ExpenseManager
    {
        public static string saveBudgetFileName = "bud.txt";
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
                e.FileName = Path.Combine
                    (Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    $"{Path.GetRandomFileName()}.exp.txt");
                File.WriteAllText(e.FileName, expenseProperties);
            }
            return true;
        }

        public static bool SaveBudget(float e)
        {
            
            if (e > 0)
            {
                var budget = e;
                /*since there will be only one value for save budget, made it as static variable and given a name
                /to the file "saveBudgetFileName"*/
                var budgetFilename = Path.Combine
                    (Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                   saveBudgetFileName);
                File.WriteAllText(budgetFilename, budget.ToString());
            }
            return true;
        }

        public static float ReadBudget()
        {

            var filename = Path.Combine
                    (Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    saveBudgetFileName);
            try
            {
                var budgetAmount = float.Parse(File.ReadAllText(filename));
                return budgetAmount;
            }
            catch
            {
                return 0;
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

        public static float RemainingBudget()
        {
            float sumOfExpensesAmount = 0;
            var budget = ReadBudget();
            if (budget != 0)
            {
                var expenses = GetExpenses();
                for (var i = 0; i < expenses.Count; i++)
                {
                    sumOfExpensesAmount += expenses[i].Amount;
                }
                var remainingBudget = budget - sumOfExpensesAmount;
                return remainingBudget;
            }
            else
                return 0;
        }


    }

}

