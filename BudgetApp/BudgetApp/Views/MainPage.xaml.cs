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
        
        public MainPage()
        {
            InitializeComponent();
            expenses = new ObservableCollection<Expense>();
           

        }
        protected override void OnAppearing()
        {
            var expenses = new List<Expense>();
            var files = Directory.EnumerateFiles(Environment.GetFolderPath
                (Environment.SpecialFolder.LocalApplicationData), "*.expense.txt");
            foreach (var filename in files)
            {
                var content = File.ReadAllText(filename);
                var splittedContent = content.Split(',');
                Enum.TryParse(splittedContent[3], out ExpenseCategory category);
                var expense = new Expense(splittedContent[0], float.Parse(splittedContent[1]), 
                    DateTime.Parse(splittedContent[2]),category);
                expense.FileName = filename;
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
                BindingContext = new Expense()
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
