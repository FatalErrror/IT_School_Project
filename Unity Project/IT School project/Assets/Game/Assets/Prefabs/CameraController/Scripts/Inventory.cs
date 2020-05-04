using UnityEngine;
using UnityEngine.UI;

public class Inventory
{
    public InventoryInformation[] inventories;
    public Sprite defoultSprite;
    public Inventory(int size, Sprite defoultSprite)
    {
        inventories = new InventoryInformation[size];
        this.defoultSprite = defoultSprite;
    }
    public void setItem(int position, InventoryInformation inventoryInformation)
    {
        if (inventories.Length < position || position < 0) return;
        inventories[position] = inventoryInformation;
    }
        
    public string GetDiscription(int position)
    {
        if (inventories.Length < position || position < 0) return "Error index don't inside array";
        InventoryInformation temp = inventories[position];
        if (temp == null) return "пусто";
        string value = temp.Name + "\n" + temp.Discription + "\n\nЧто с этим делать:\n" + temp.ActionType.ToString() + "\n\n";
        if (temp.Characteristics != null)
            for (int i = 0; i < temp.Characteristics.Length; i++)
            {
                value += "--" + temp.Characteristics[i].DiscriptionName + ": " + temp.Characteristics[i].value;
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
                items[i].sprite = defoultSprite;
        }
    }

    public bool isFull()
    {
        for (int i = 0; i < inventories.Length; i++)
        {
            if (inventories[i] == null) return false;
        }
        return true;
    }

    public bool isFull(out int freePosition)
    {
        for (int i = 0; i < inventories.Length; i++)
        {
            if (inventories[i] == null)
            {
                freePosition = i;
                return false;
            }

        }
        freePosition = -1;
        return true;
    }


}
