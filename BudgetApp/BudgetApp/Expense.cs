using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BudgetApp
{
    public enum ExpenseCategory
    {
        Housing,
        Transit,
        Groceries,
        Exercise,
        Travel,
        Other
    }
    public class Expense /*: INotifyPropertyChanged*/
    {

        public string Name { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }

        //Research how to get selected date here: https://forums.xamarin.com/discussion/151957/how-to-save-selected-date-in-datepicker
        //and here: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.inotifypropertychanged?view=net-5.0
        public ExpenseCategory Category { get; set; }



    }
}
