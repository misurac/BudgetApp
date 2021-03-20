using BudgetApp.Model;
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
        public Budget budget = ExpenseManager.ReadBudget((Month)DateTime.Now.Month);
        public float remainingBudget = ExpenseManager.RemainingBudget((Month)DateTime.Now.Month);
        public List<Month> MonthsList { get; set; }
        public List<string> monthsStrings { get; set; }
        public static string selectedMonth { get; set; }
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

            selectedMonth = DateTime.Now.ToString("MMMM");

            mainpicker.SelectedItem = selectedMonth;

        }

        protected async override void OnAppearing()
        {
            //ExpenseManager.DeleteAllExpenses();
            //ExpenseManager.DeleteBudget();
            var allExpenses = ExpenseManager.GetExpenses();
            foreach(var expense in allExpenses)
            {
                expenses.Add(expense);
            }
            budget = ExpenseManager.ReadBudget((Month)DateTime.Now.Month);
            if (budget.BudgetAmount <= 0) {
                await Navigation.PushModalAsync(new SaveBudgetPage
                {
                    BindingContext = null
                });
            }                                
            Budget.Text = budget.BudgetAmount.ToString();
            RemainingAmount.Text = ExpenseManager.RemainingBudget((Month)DateTime.Now.Month).ToString();
            if(budget.BudgetAmount > 0)
            {
               AddNewExpense.IsEnabled = true;
            }
            //clearing the observableCollection list to get the new observableCollection list
            expenses.Clear();
            var expensesByMonth = ExpenseManager.GetExpensesByMonth((Month)DateTime.Now.Month);
            foreach (var expense in expensesByMonth)
            {
                expenses.Add(expense);
            }

            var orderedExpenses = expenses.OrderBy(n => n.Date).ToList() ;
            expenses.Clear();
            foreach(var expense in orderedExpenses)
            {
                expenses.Add(expense);
            }
            ExpenseRecords.ItemsSource =  expenses;
        }

        private void HamburgerButton_Clicked(object sender, EventArgs e)
        {
        }

        private async void EditBudgetButtonClick(object sender, EventArgs e)
        {

            var SelectedMonth = mainpicker.Items[mainpicker.SelectedIndex];            
            Enum.TryParse(SelectedMonth.ToString(), out Month month);          
            await Navigation.PushModalAsync(new SaveBudgetPage
            {
                budget = budget,
                
            }) ; 
        }

        private void ItemSelectedFromPicker(object sender, EventArgs e)
        {
            //Getting the selected item from the picker
            var SelectedMonth = mainpicker.Items[mainpicker.SelectedIndex];

            selectedMonth = SelectedMonth;
            mainpicker.Title = selectedMonth;
            //checking if the item is AllMonths. if yes, clearing the observable collection and adding all expenses to it
            if (SelectedMonth == "AllMonths")
            {
                EditBudget.IsEnabled = false;
                Enum.TryParse(SelectedMonth, out Month month);
                Budget.Text = ExpenseManager.ReadBudget(month).BudgetAmount.ToString();
                RemainingAmount.Text = ExpenseManager.RemainingBudget(month).ToString();
                var ExpensesFromFile = ExpenseManager.GetExpenses();
                expenses.Clear();
                foreach (var expense in ExpensesFromFile)
                {
                    expenses.Add(expense);
                }
                var orderedExpenses = expenses.OrderBy(n => n.Date).ToList();
                expenses.Clear();
                foreach (var expense in orderedExpenses)
                {
                    expenses.Add(expense);
                }
            }
            else
            {
                EditBudget.IsEnabled = true;
                Enum.TryParse(SelectedMonth, out Month month);
                budget = ExpenseManager.ReadBudget(month);
                Budget.Text = budget.BudgetAmount.ToString();
                remainingBudget = ExpenseManager.RemainingBudget(month);
                RemainingAmount.Text = remainingBudget.ToString();
                var expensesList = ExpenseManager.GetExpensesByMonth(month);
                //Clearing the observable collection.
                expenses.Clear();
                foreach(var expense in expensesList)
                {
                    expenses.Add(expense);
                }
            }
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

        private async void GraphAnalysis_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushModalAsync(new GraphAnalysis());
               
            }

        }
    }

