using System;
using System.Collections.Generic;
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
        }

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