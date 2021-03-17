using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetApp.Model
{
    public class Icon
    {
        public string IconFile { get; set; }
        public ExpenseCategory Category { get; set; }

        private  List<Icon> GetIcons()
        {
            var icons = new List<Icon>();
            icons.Add(new Icon { IconFile = "Assets/Category Icons/food.png", Category = ExpenseCategory.Food });
            icons.Add(new Icon { IconFile = "Assets/ Category Icons/housing.png", Category = ExpenseCategory.Housing });
            icons.Add(new Icon { IconFile = "Assets/ Category Icons/insurance.png", Category = ExpenseCategory.Insurance });
            icons.Add(new Icon { IconFile = "Assets/ Category Icons/medical.png", Category = ExpenseCategory.Medical });
            icons.Add(new Icon { IconFile = "Assets/ Category Icons/other.png", Category = ExpenseCategory.Other });
            icons.Add(new Icon { IconFile = "Assets/ Category Icons/saving.png", Category = ExpenseCategory.Saving });
            icons.Add(new Icon { IconFile = "Assets/ Category Icons/transportation.png", Category = ExpenseCategory.Transportation });
            icons.Add(new Icon { IconFile = "Assets/ Category Icons/utilities.png", Category = ExpenseCategory.Utilities });
            return icons;
        }
    }
}

