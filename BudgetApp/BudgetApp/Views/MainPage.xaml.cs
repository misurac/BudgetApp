using BudgetApp.Model;
using BudgetApp.Views;
using Microcharts;
using Microcharts.Forms;
using SkiaSharp;
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
        public float budget = ExpenseManager.ReadBudget((Month)DateTime.Now.Month);
        public float remainingBudget = ExpenseManager.RemainingBudget((Month)DateTime.Now.Month);
        public static string selectedMonth { get; set; }
        public float amountSpent { get; set; }
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

            //MonthLabel.Text = selectedMonth;


            selectedMonth = DateTime.Now.ToString("MMMM");
            

            mainpicker.Title = selectedMonth;

            

        }

        protected async override void OnAppearing()
        {
            Debug.WriteLine($"In OnAppearing: {selectedMonth}");
            //ExpenseManager.DeleteAllExpenses();
            var allExpenses = ExpenseManager.GetExpenses();
            foreach(var expense in allExpenses)
            {
                expenses.Add(expense);
            }
            budget = ExpenseManager.ReadBudget((Month)DateTime.Now.Month);
            if (budget <= 0) {
                await Navigation.PushModalAsync(new SaveBudgetPage
                {
                    BindingContext = null
                });
            }                                
            Budget.Text = budget.ToString();
            RemainingAmount.Text = ExpenseManager.RemainingBudget((Month)DateTime.Now.Month).ToString();
            if(budget > 0)
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

            //work on chart below
            amountSpent = budget - remainingBudget;
            Debug.WriteLine(amountSpent);
            Debug.WriteLine(remainingBudget);
            var entries = new[]
            {
                    new ChartEntry(amountSpent)
                {
                    Label = "iOS",
                    ValueLabel = "hello",
                    Color = SKColor.Parse("#b455b6")
                },
                new ChartEntry(remainingBudget)
                {
                    Label = "Forms",
                    ValueLabel = "hi there",
                    Color = SKColor.Parse("#3498db")
                }
            };

            var chart = new PieChart { Entries = entries };
            chartView.Chart = chart;

            //work on chart above

            

        }

        private void HamburgerButton_Clicked(object sender, EventArgs e)
        {
        }

        private async void EditBudgetButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SaveBudgetPage
            {
                BindingContext = null
            });
        }

        private void ItemSelectedFromPicker(object sender, EventArgs e)
        {
            //Getting the selected item from the picker
            var SelectedMonth = mainpicker.Items[mainpicker.SelectedIndex];
            selectedMonth = SelectedMonth;
            //MonthLabel.Text = selectedMonth;
            mainpicker.Title = selectedMonth;

            Debug.WriteLine($"In ItemSelectedFromPicker: {selectedMonth}");
            //checking if the item is AllMonths. if yes, clearing the observable collection and adding all expenses to it
            if (SelectedMonth == "AllMonths")
            {
                Enum.TryParse(SelectedMonth, out Month month);
                Budget.Text = ExpenseManager.ReadBudget(month).ToString();
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
                Enum.TryParse(SelectedMonth, out Month month);
                budget = ExpenseManager.ReadBudget(month);
                Budget.Text = budget.ToString();
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
            
            mainpicker.Title = selectedMonth;
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
