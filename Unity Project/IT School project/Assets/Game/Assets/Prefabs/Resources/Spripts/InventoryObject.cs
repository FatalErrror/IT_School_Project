using UnityEngine;
using UnityEngine.UI;

public class InventoryObject : MonoBehaviour
{
    public static InventoryObject Empty { get { 
            InventoryObject empty = new InventoryObject(); 
            empty.ActionType = TypeOfAction.Empty;
            return empty; } } 
    
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




