using UnityEngine;
using UnityEngine.UI;

public class InventoryObject : MonoBehaviour
{
    [Header("Discription")]
    public string Name, Discription;
    public Sprite InventoryImage;

    [Header("Сharacteristics")]
    public TypeOfAction ActionType;
    public Сharacteristic[] Сharacteristics;

    
    
}
public enum TypeOfAction
{
    Empty,
    Nothink,
    UseOnYourself,
    UseOnAliveCreater,
    Ingredient
}




