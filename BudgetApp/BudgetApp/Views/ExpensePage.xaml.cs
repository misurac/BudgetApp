using BudgetApp.Model;
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

        //public string selectedCategory { get; set; }
        public string expenseDescription { get; set; }
        //public float expenseAmount { get; set; }
        //public DateTime expenseDate { get; set; }
        //public List<ExpenseCategory> ExpenseCategoriesList { get; set; }
        //public List<string> expenseCategoryStrings { get; set; }

        //This is the event handler for the ExpenseDescription Entry box
        private void ExpenseDescriptionTextChanged(object sender, TextChangedEventArgs e)
        {
            var oldText = e.OldTextValue;
            var newText = e.NewTextValue;
            expenseDescription = newText;
        }
    }
}