using BudgetApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetApp.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SaveBudgetPage : ContentPage
    {
        public SaveBudgetPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var files = Directory.EnumerateFiles(Environment.GetFolderPath
                (Environment.SpecialFolder.LocalApplicationData), "*.bud.txt");
            foreach (string filePath in files)
                File.Delete(filePath);
            try
            {
                if (float.Parse(BudgetAmount.Text) > 0)
                {
                    SaveButton.IsEnabled = true;
                }
            }
            catch
            {
                BudgetAmount.Text = "0";
            }
            var budget = ExpenseManager.ReadBudget();

            //commented out this to allow navigation to savebudget page from mainpage

            //if (budget > 0)
            //{
            //    await Navigation.PushModalAsync(new MainPage
            //    {
            //        BindingContext = null
            //    });
            //}
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            float amount;
            int month;
            try
            {
                amount = float.Parse(BudgetAmount.Text);
                var m = MonthPicker.Date;
                month = m.Month;

            }
            catch
            {
                amount = 0;
                month = DateTime.Now.Month;
            }
            //converting the enum value to enum
            //Budget budget = new Budget(amount, (Month)month);
            ExpenseManager.SaveBudget(amount);
            await Navigation.PushModalAsync(new MainPage
            {
                BindingContext = null
            });



        }

    }
}