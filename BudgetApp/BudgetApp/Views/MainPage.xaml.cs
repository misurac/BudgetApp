﻿using BudgetApp.Model;
using BudgetApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BudgetApp
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Expense> expenses;
        public float budget = ExpenseManager.ReadBudget();
        public float remainingBudget = ExpenseManager.RemainingBudget();
        public List<Month> MonthsList { get; set; }
        public List<string> monthsStrings { get; set; }
        public MainPage()
        {
            InitializeComponent();
            expenses = new ObservableCollection<Expense>();
            Budget.Text = budget.ToString();
            RemainingAmount.Text = remainingBudget.ToString();

            MonthsList = new List<Month>();
            MonthsList.Add(Month.AllMonths);
            MonthsList.Add(Month.January);
            MonthsList.Add(Month.February);
            MonthsList.Add(Month.March);
            MonthsList.Add(Month.April);
            MonthsList.Add(Month.May);
            MonthsList.Add(Month.June);
            MonthsList.Add(Month.July);
            MonthsList.Add(Month.August);
            MonthsList.Add(Month.September);
            MonthsList.Add(Month.October);
            MonthsList.Add(Month.November);
            MonthsList.Add(Month.December);

            monthsStrings = new List<string>();
            monthsStrings = MonthsList.ConvertAll(m => m.ToString());

            mainpicker.ItemsSource = monthsStrings;

        }

        protected async override void OnAppearing()
        {
            budget = ExpenseManager.ReadBudget();
            if (budget <= 0) {
                await Navigation.PushModalAsync(new SaveBudgetPage
                {
                    BindingContext = null
                });
            }
            

            var ExpensesFromFile = ExpenseManager.GetExpenses();
            //ExpenseManager.DeleteAllExpenses();
            //ExpenseManager.SaveBudget(500);
            Budget.Text = budget.ToString();
            RemainingAmount.Text = ExpenseManager.RemainingBudget().ToString();
            if(budget > 0)
            {
               AddNewExpense.IsEnabled = true;
            }
            //clearing the observableCollection list to get the new observableCollection list
            expenses.Clear();
            foreach (var expense in ExpensesFromFile)
            {
                expenses.Add(expense);
            }
            ExpenseRecords.ItemsSource = expenses.OrderBy(n => n.Date).ToList();
            
        }

        private void HamburgerButton_Clicked(object sender, EventArgs e)
        {
        }

        private void EditBudgetButtonClick(object sender, EventArgs e)
        {
        }

        private async void AddNewExpense_Clicked(object sender, EventArgs e)
        {
            //It will navivate to expense page upon add expense button click
            await Navigation.PushModalAsync(new ExpensePage
            {
                BindingContext = null
            });
        }
        private async void ExpenseRecord_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Navigate to expense page on selection of record
            if (e.SelectedItem != null)
            {
                await Navigation.PushModalAsync(new ExpensePage
                {
                    BindingContext = (Expense)e.SelectedItem
                });
            }
        }
    }
}
