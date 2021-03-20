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
        public Budget budget;      
        public SaveBudgetPage()
        {
            InitializeComponent();
            
        }

        protected override void OnAppearing()
        {
            /*
            var files = Directory.EnumerateFiles(Environment.GetFolderPath
                (Environment.SpecialFolder.LocalApplicationData), "*.bud.txt");
            foreach (string filePath in files)
                File.Delete(filePath); */
            if (budget != null)
            {
                BudgetAmount.Text = budget.BudgetAmount.ToString();
                int month = (int)budget.MonthInYear;
                var date = new DateTime(2021,month,1);
                MonthPicker.Date = date;
            }
            else {
                BudgetAmount.Text = "0";
            }                     
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
            var fileName = "";
            Month updatedMonth = (Month)month;
            if (budget != null && budget.MonthInYear == updatedMonth) {
                fileName = budget.FileName;
            }
            //converting the enum value to enum
            Budget b = new Budget(amount, (Month)month)
            {
                FileName = fileName
            };
            ExpenseManager.SaveBudget(b);
            await Navigation.PushModalAsync(new MainPage
            {
                BindingContext = null
            }) ;



        }

        private async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage
            {
                BindingContext = null
            });
        }
    }
}