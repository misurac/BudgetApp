﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetApp.Model
{
    public enum Month
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }
    public class Budget
    {
        public float BudgetAmount { get; set; }
        public Month MonthInYear { get; set; }
        public string FileName { get; set; }
        public Budget(float budgetAmount, Month month)
        {
            BudgetAmount = budgetAmount;
            MonthInYear = month;
        }
    }
}
