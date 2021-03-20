using BudgetApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using Entry = Microcharts.ChartEntry;

namespace BudgetApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GraphAnalysis : ContentPage
    {
        public static string selectedMonth { get; set; }
        
        

        //public float budget = ExpenseManager.ReadBudget(month);
 
        public List<Entry> expensesList = new List<Entry>();
        public GraphAnalysis()
        {
            InitializeComponent();
            selectedMonth = DateTime.Now.ToString("MMMM");
            Enum.TryParse(selectedMonth, out Month month);                                   
            var ExpensesFromFile = ExpenseManager.GetExpenses();


            foreach (var expense in ExpensesFromFile)
            {
                Entry Entry = new Entry(expense.Amount)
                {

                    Label = expense.Category.ToString(),
                    ValueLabel = expense.Amount.ToString()
                };
                expensesList.Add(Entry);

            }


            foreach (var entry in expensesList)
            {
                if (entry.Label == ExpenseCategory.Food.ToString())
                {

                    entry.Color = SKColor.Parse("#00ced1");
                    entry.ValueLabelColor = SKColor.Parse("#00ced1");

                }
                if (entry.Label == ExpenseCategory.Insurance.ToString())
                {
                    entry.Color = SKColor.Parse("#d100ce");
                    entry.ValueLabelColor = SKColor.Parse("#d100ce");
                }
                if (entry.Label == ExpenseCategory.Medical.ToString())
                {
                    entry.Color = SKColor.Parse("#ced100");
                    entry.ValueLabelColor = SKColor.Parse("#ced100");
                }
                if (entry.Label == ExpenseCategory.Utilities.ToString())
                {
                    entry.Color = SKColor.Parse("#d10300");
                    entry.ValueLabelColor = SKColor.Parse("#d10300");
                }
                if (entry.Label == ExpenseCategory.Transportation.ToString())
                {
                    entry.Color = SKColor.Parse("#cc0000");
                    entry.ValueLabelColor = SKColor.Parse("#cc0000");
                }
                if (entry.Label == ExpenseCategory.Saving.ToString())
                {
                    entry.Color = SKColor.Parse("#00a5e6");
                    entry.ValueLabelColor = SKColor.Parse("#00a5e6");
                }
                if (entry.Label == ExpenseCategory.Other.ToString())
                {
                    entry.Color = SKColor.Parse("#008000");
                    entry.ValueLabelColor = SKColor.Parse("#008000");
                }

            }


            Chart1.Chart = new RadialGaugeChart() { Entries = (IEnumerable<ChartEntry>)expensesList };
            Chart2.Chart = new BarChart() { Entries = (IEnumerable<ChartEntry>)expensesList };
            Chart3.Chart = new DonutChart() { Entries = (IEnumerable<ChartEntry>)expensesList };

        }
    }
}