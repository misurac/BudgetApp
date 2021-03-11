using BudgetApp.Model;
using BudgetApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void HamburgerButton_Clicked(object sender, EventArgs e)
        {

        }
        private  async void AddNewExpense_Clicked(object sender, EventArgs e)
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