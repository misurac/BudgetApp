﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace BudgetApp.Model
{
    public static class ExpenseManager
    {
        public static List<Expense> GetExpenses()
        {
            
            
            var expenses = new List<Expense>();
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
        public static bool SaveExpense(Expense e) {
                    
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
            if(e > 0)
            {
                var budget = e;
                var budgetFilename = Path.Combine
                    (Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    $"{Path.GetRandomFileName()}.bud.txt");
                File.WriteAllText(budgetFilename, budget.ToString());
            }
            return true;
        }
            
    }


}

