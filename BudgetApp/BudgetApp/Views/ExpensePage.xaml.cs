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
            ExpenseCategoriesList.Add(ExpenseCategory.Insuarance);
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

        public string selectedCategory { get; set; }
        public string expenseDescription { get; set; }
        public float expenseAmount { get; set; }
        public DateTime expenseDate { get; set; }
        public List<ExpenseCategory> ExpenseCategoriesList { get; set; }
        public List<string> expenseCategoryStrings { get; set; }
        public Expense currentExpense { get; set; }

        //This is the event handler for the ExpenseDescription Entry box
        private void ExpenseDescriptionTextChanged(object sender, TextChangedEventArgs e)
        {
            var oldText = e.OldTextValue;
            var newText = e.NewTextValue;
            expenseDescription = newText;
        }
        /*protected override void OnAppearing()
        {
            var expense = (Expense)BindingContext;
            if (!string.IsNullOrEmpty(expense.FileName))
            {
                 File.ReadAllText(expense.FileName);
            }
        }*/

        //This is the event handler for the Category Picker
        private void SelectedCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var SelectedCategory = picker.Items[picker.SelectedIndex];
            var SelectedCategory = picker.Items[picker.SelectedIndex];
            selectedCategory = SelectedCategory;
            Debug.WriteLine(selectedCategory);
        }

        //This is the event handler for the ExpenseAmount Entry box
        private void ExpenseAmountTextChanged(object sender, TextChangedEventArgs e)
        {
            var oldText = e.OldTextValue;
            var newText = e.NewTextValue;
            expenseAmount = float.Parse(newText);
        }

        //This is the event handler for the DatePicker
        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            DateTime? date = picker.Date;
            expenseDate = (DateTime)date;
        }

        //The event handler for the Save Button currently writes the values of the page to Output

        private async void SaveButtonClicked(object sender, EventArgs e)
        {
          ExpenseCategory PickedCategory = ExpenseCategory.Other;
            //Finding the right ExpenseCategory to assign to currentExpense
            for (int i = 0; i < expenseCategoryStrings.Count(); i++)
            {
                if (expenseCategoryStrings[i] == selectedCategory)
                {
                    PickedCategory = ExpenseCategoriesList[i];
                }
            }
            //Creating a new instance of currentExpense
            Expense currentExpense = new Expense(expenseDescription, expenseAmount, expenseDate, PickedCategory);

            ExpenseManager.SaveExpense(currentExpense);
            await Navigation.PopModalAsync();
           

        }

        private void DeleteButtonClicked(object sender, EventArgs e)
        {

        }

    }
}