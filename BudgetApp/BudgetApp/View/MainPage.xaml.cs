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
        private void AddNewExpense_Clicked(object sender, EventArgs e)
        {

        }



        private void ExpenseRecord_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}