﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using BudgetApp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

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
            ExpenseManager.SaveBudget(500);
            var readBudgetAmount=ExpenseManager.ReadBudget();
            Assert.AreEqual(500, readBudgetAmount);

            ExpenseManager.SaveBudget(600);
             readBudgetAmount = ExpenseManager.ReadBudget();
            Assert.AreEqual(600, readBudgetAmount);
        }
    }
}