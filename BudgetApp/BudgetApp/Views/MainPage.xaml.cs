using BudgetApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
            TestList = new List<Foo>();
            TestList.Add(new Foo() { Akshay = "This" });
            TestList.Add(new Foo() { Akshay = "is" });
            TestList.Add(new Foo() { Akshay = "my" });
            TestList.Add(new Foo() { Akshay = "list" });
            picker.ItemsSource = TestList;
        }

        public string selectedCategory { get; set; }
        public string expenseDescription { get; set; }
        public float expenseAmount { get; set; }
        public DateTime expenseDate { get; set; }
        public List<Foo> TestList { get; set; }


        //This is the event handler for the Category Picker
        private void SelectedCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            var SelectedCategory = picker.Items[picker.SelectedIndex];
            selectedCategory = SelectedCategory;
        }

        //This is the event handler for the ExpenseDescription Entry box
        private void ExpenseDescriptionTextChanged(object sender, TextChangedEventArgs e)
        {
            var oldText = e.OldTextValue;
            var newText = e.NewTextValue;
            expenseDescription = newText;
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
        private void SaveButtonClicked(object sender, EventArgs e)
        {
            Debug.WriteLine(selectedCategory);
            Debug.WriteLine(expenseDescription);
            Debug.WriteLine(expenseAmount);
            Debug.WriteLine(expenseDate);
        }

        private void DeleteButtonClicked(object sender, EventArgs e)
        {

        }
    }
}
