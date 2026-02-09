using PRGAssignment;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class OrderedFoodItem
{
    // ===== attributes =====
    private FoodItem foodItem;
    private int qtyOrdered;
    private double subTotal;

    // ===== constructor =====
    public OrderedFoodItem(FoodItem foodItem, int qtyOrdered)
    {
        this.foodItem = foodItem;
        this.qtyOrdered = qtyOrdered;
    }

    // ===== methods =====
    public double CalculateSubtotal()
    {
        subTotal = foodItem.Price * qtyOrdered;
        return subTotal;
    }

    public override string ToString()
    {
        return $"{foodItem.ItemName} - {qtyOrdered}";
    }
}

