using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory
{
    public InventoryObject[] inventories;
    public Inventory(int size)
    {
        inventories = new InventoryObject[size];
    }

    public string GetDiscription(int position)
    {
        if (inventories.Length < position) return "Error index don't inside array";
        InventoryObject temp = inventories[position];
        if (temp == null) return "пусто";
        string value = temp.Name +"\n" + temp.Discription + "\nAction: " + temp.ActionType.ToString() + "\n\n";
        for (int i = 0; i < temp.Сharacteristics.Length; i++)
        {
            value += "--" + temp.Сharacteristics[i].DiscriptionName + ": " + temp.Сharacteristics[i].value;
        }
        return value;
    }

    public void Render(Image[] items)
    {
        if (inventories.Length < items.Length) return;
        for (int i = 0; i < items.Length; i++)
        {
            if (inventories[i] != null)
                items[i].sprite = inventories[i].InventoryImage;
            else
                items[i].sprite = null;
        }
    }
}
