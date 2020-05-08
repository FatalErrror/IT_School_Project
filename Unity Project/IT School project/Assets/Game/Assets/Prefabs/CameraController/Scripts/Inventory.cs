using UnityEngine;
using UnityEngine.UI;

public class Inventory
{
    public InventoryInformation[] inventories;
    public int[] Counts;
    public Sprite defoultSprite;
    public Inventory(int size, Sprite defoultSprite)
    {
        inventories = new InventoryInformation[size];
        Counts = new int[size];
        this.defoultSprite = defoultSprite;
    }
    public void setItem(int position, InventoryInformation inventoryInformation)
    {
        if (inventories.Length < position || position < 0) return;
        inventories[position] = inventoryInformation;
        Counts[position]++;
    }

    public InventoryInformation removeItem(int position)
    {
        if (inventories.Length < position || position < 0) return null;
        Counts[position]--;
        if (Counts[position] > 0) return inventories[position];
        Counts[position] = 0;
        var Out = inventories[position];
        inventories[position] = null;
        return Out;
    }
        
    public string GetDiscription(int position)
    {
        if (inventories.Length < position || position < 0) return "Error index don't inside array";
        InventoryInformation temp = inventories[position];
        if (temp == null) return "пусто";
        string value = temp.Name + "  x" + Counts[position] + "\n" + temp.Discription + "\n\nЧто с этим делать:\n" + temp.ActionType.ToString() + "\n\n";
        if (temp.Characteristics != null)
            for (int i = 0; i < temp.Characteristics.Length; i++)
            {
                value += "--" + temp.Characteristics[i].DiscriptionName + ": " + temp.Characteristics[i].value;
            }
        return value;
    }

    public void Render(Image[] items, Text[] counts)
    {
        if (inventories.Length < items.Length || inventories.Length < counts.Length) return;
        for (int i = 0; i < inventories.Length; i++)
        {
            if (inventories[i] != null)
                items[i].sprite = inventories[i].InventoryImage;
            else
                items[i].sprite = defoultSprite;
            counts[i].text = "x" + Counts[i];
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
    public bool isFull(InventoryInformation information)
    {
        for (int i = 0; i < inventories.Length; i++)
        {
            if (inventories[i] == null)
                return false;
            if (inventories[i].Prefab.name.Equals(information.Prefab.name))
                if (Counts[i] < information.CountInOneSlote)
                    return false;
        }
        return true;
    }

    public bool isFull(InventoryInformation information, out int freePosition)
    {
        for (int i = 0; i < inventories.Length; i++)
        {
            if (inventories[i] != null)
                if (inventories[i].Prefab.name.Equals(information.Prefab.name))
                    if (Counts[i] < information.CountInOneSlote)
                    {
                        freePosition = i;
                        return false;
                    }   
        }
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
