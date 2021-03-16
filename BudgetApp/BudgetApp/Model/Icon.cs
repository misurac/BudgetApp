using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetApp.Model
{
    public class Icon
    {
        public string IconFile { get; set; }
        public ExpenseCategory Category { get; set; }

        private static List<Icon> GetIcons()
        {
            var icons = new List<Icon>();
            icons.Add(new Icon { IconFile = "Assets/ Category Icons/Food-Icon.png", Category = ExpenseCategory.Food });
            icons.Add(new Icon { IconFile = "Assets/ Category Icons/Housing-icon.png", Category = ExpenseCategory.Housing });
            icons.Add(new Icon { IconFile = "Assets/ Category Icons/Insurance-Icon.png", Category = ExpenseCategory.Insuarance });
            icons.Add(new Icon { IconFile = "Assets/ Category Icons/Medical-Icon.png", Category = ExpenseCategory.Medical });
            icons.Add(new Icon { IconFile = "Assets/ Category Icons/Other-Icon.png", Category = ExpenseCategory.Other });
            icons.Add(new Icon { IconFile = "Assets/ Category Icons/saving-Icon.png", Category = ExpenseCategory.Saving });
            icons.Add(new Icon { IconFile = "Assets/ Category Icons/Transportation-Icon.png", Category = ExpenseCategory.Transportation });
            icons.Add(new Icon { IconFile = "Assets/ Category Icons/Utolities-Icon.png", Category = ExpenseCategory.Utilities });
            return icons;
        }
    }
}
