using BudgetApp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpensePage : ContentPage
    {
        public ExpensePage()
        {
            InitializeComponent();

            //Here I create a list of type ExpenseCategory, which has the values of the Enum (i.e. 1, 2, 3, etc.)
            ExpenseCategoriesList = new List<ExpenseCategory>();
            ExpenseCategoriesList.Add(ExpenseCategory.Food);
            ExpenseCategoriesList.Add(ExpenseCategory.Housing);
            ExpenseCategoriesList.Add(ExpenseCategory.Insurance);
            ExpenseCategoriesList.Add(ExpenseCategory.Medical);
            ExpenseCategoriesList.Add(ExpenseCategory.Other);
            ExpenseCategoriesList.Add(ExpenseCategory.Saving);
            ExpenseCategoriesList.Add(ExpenseCategory.Transportation);
            ExpenseCategoriesList.Add(ExpenseCategory.Utilities);

            //Here I convert the values of the Enum to the Names of the enum (i.e. Food, Housing, Insurance, etc.)
            expenseCategoryStrings = new List<string>();
            expenseCategoryStrings = ExpenseCategoriesList.ConvertAll(f => f.ToString());

            //Here I set the picker to display the expenseCategoryStrings list
            picker.ItemsSource = expenseCategoryStrings;

        }
        protected override void OnAppearing()
        {
            if (BindingContext != null)
            {
                var expense = (Expense)BindingContext;
                if (!string.IsNullOrEmpty(expense.FileName))
                {
                    Description.Text = expense.ExpenseName;
                    ExpenseDate.Date = expense.Date;
                    ExpenseAmount.Text = expense.Amount.ToString();
                    //Picker.SelectedItemProperty= myCategory;                                        
                    DeleteButton.IsEnabled = true;
                }  
            }
        }
        public List<ExpenseCategory> ExpenseCategoriesList { get; set; }
        public List<string> expenseCategoryStrings { get; set; }
    
        //The event handler for the Save Button currently writes the values of the page to Output

        private async void SaveButtonClicked(object sender, EventArgs e)
        {
            var expenseName = Description.Text;
            var expenseDate = ExpenseDate.Date;
            var expenseAmount = float.Parse(ExpenseAmount.Text);
            var category = (string)picker.SelectedItem;
            Enum.TryParse(category, out ExpenseCategory myCategory);

            //here we did binding context, to edit the values of the expense
            if (BindingContext != null)
            {
                var expense = (Expense)BindingContext;
                expense.ExpenseName = expenseName;
                expense.Date = File.GetCreationTime(expense.FileName);
                expense.Category = myCategory;
                expense.Amount = expenseAmount;
                ExpenseManager.SaveExpense(expense);
            }
            //if binding context is null then,        
            //Creating a new instance of currentExpense 
            else
            {
                Expense currentExpense = new Expense(expenseName, expenseAmount, expenseDate, myCategory);
                ExpenseManager.SaveExpense(currentExpense);
            }
            await Navigation.PopModalAsync();
               

        }

        private  async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void DeleteButtonClicked(object sender, EventArgs e)
        {
            var expenseName = Description.Text;
            //var expenseDate = 
            var expenseAmount = float.Parse(ExpenseAmount.Text);
            var category = (string)picker.SelectedItem;
            Enum.TryParse(category, out ExpenseCategory myCategory);

            //here we did binding context, to edit the values of the expense
            if (BindingContext != null)
            {
                var expense = (Expense)BindingContext;
                expense.ExpenseName = expenseName;
                expense.Amount = expenseAmount;
                expense.Date = File.GetCreationTime(expense.FileName); 
                expense.Category = myCategory;
                ExpenseManager.DeleteExpense(expense);
            }

            await Navigation.PopModalAsync();
        }
    }
}