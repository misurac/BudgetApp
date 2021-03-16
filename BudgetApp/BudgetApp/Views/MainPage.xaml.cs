using BudgetApp.Model;
using BudgetApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public MainPage()
        {
            InitializeComponent();
            expenses = new ObservableCollection<Expense>();
            Budget.Text = budget.ToString();
            RemainingAmount.Text = remainingBudget.ToString();
        }

        protected override void OnAppearing()
        {
            var Expenses = ExpenseManager.GetExpenses();
            ExpenseManager.SaveBudget(500);
            Budget.Text = budget.ToString();
            RemainingAmount.Text = ExpenseManager.RemainingBudget().ToString();
            if(budget > 0)
            {
               AddNewExpense.IsEnabled = true;
            }
            foreach (var expense in Expenses)
            {
                expenses.Add(expense);
            }
            ExpenseRecords.ItemsSource = expenses.OrderBy(n => n.Date).ToList();
            
        }

        private void HamburgerButton_Clicked(object sender, EventArgs e)
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
