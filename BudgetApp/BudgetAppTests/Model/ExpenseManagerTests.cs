using Microsoft.VisualStudio.TestTools.UnitTesting;
using BudgetApp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;


namespace BudgetApp.Model {
    [TestClass]
    public class Test1 {

        [TestMethod]
        public void TestGetExpenses() {
            var files = Directory.EnumerateFiles(Environment.GetFolderPath
                (Environment.SpecialFolder.LocalApplicationData), "*.exp.txt");
            foreach(var file in files)
            {
                File.Delete(file);
            }
            var count = 0;
            Expense e = new Expense("Costco", 12.5f, DateTime.Now);
            Expense f = new Expense("FredMayer", 15.5f, DateTime.Now);
            ExpenseManager.SaveExpense(e);
            count++;
            ExpenseManager.SaveExpense(f);
            count++;
            var Expenses = ExpenseManager.GetExpenses();
            Assert.AreEqual(count, Expenses.Count);
        }

        [TestMethod]
        //This will write and read the budget
        public void TestBudget()
        {
            ExpenseManager.DeleteBudget();
            var b = new Budget(500, Month.March);
            ExpenseManager.SaveBudget(b);
            var readBudgetAmount=ExpenseManager.ReadBudget(Month.March);
            Assert.AreEqual(500, readBudgetAmount);

            ExpenseManager.SaveBudget(b);
             readBudgetAmount = ExpenseManager.ReadBudget(Month.March);
            Assert.AreEqual(500, readBudgetAmount);
        }

        [TestMethod]

        public void TestRamainingBudget()
        {
            ExpenseManager.DeleteBudget();
            var b = new Budget(500, Month.March);
            ExpenseManager.SaveBudget(b);
            var remainingBudget = ExpenseManager.RemainingBudget(Month.March);
            var budget = ExpenseManager.ReadBudget(Month.March);
            var amountLeft = budget.BudgetAmount - remainingBudget;
            var expensesAmount = ExpenseManager.SumOfExpenses();
            Assert.AreEqual(amountLeft, expensesAmount);
        }
    }
}