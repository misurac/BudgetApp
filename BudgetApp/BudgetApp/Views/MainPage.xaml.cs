using BudgetApp.Model;
using BudgetApp.Views;
using System;
using System.Collections.Generic;
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
        public MainPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            var expenses = new List<Expense>();
            var records = Directory.EnumerateFiles(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "*.expense.txt");
            foreach (var record in records)
            {
                var expense = new Expense
                {
                   
                    
                };
                expenses.Add(expense);
            }
            ExpenseRecords.ItemsSource = expenses.OrderBy(n => n.Date).ToList();



        }

        private void HamburgerButton_Clicked(object sender, EventArgs e)
        {

        }

        private async void AddNewExpense_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ExpensePage
            {
                BindingContext = new Expense()
            });
        }



        private async void ExpenseRecord_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
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
