using UnityEngine;
using UnityEngine.UI;

public class InventoryObject : MonoBehaviour
{
    [Header("Discription")]
    public string Name;
    public string Discription;
    public Sprite InventoryImage;

    [Header("Сharacteristics")]
    public TypeOfAction ActionType;
    public Characteristic[] Characteristics;

    [Header("Controle")]
    public GameObject Prefab;


    Transform Controle;
    GameObject NameText;
    GameObject TakeButton;
    GameObject PutButton;
    GameObject UseButton;
    GameObject PickButton;

    
    Transform Player, startParent;
    Interface Interface;

    protected delegate void use();
    protected event use Using;

    public virtual void Initialize()
    {
        
    }

    
    private void Start()
    {
        Controle = transform.GetChild(0).GetChild(0);

        NameText = Controle.GetChild(0).gameObject;
        TakeButton = Controle.GetChild(1).gameObject;
        UseButton = Controle.GetChild(2).gameObject;
        PutButton = Controle.GetChild(3).gameObject;
        PickButton = Controle.GetChild(4).gameObject;

        Controle = Controle.parent;

        Button.ButtonClickedEvent button = new Button.ButtonClickedEvent();
        button.AddListener(TakeObj);
        TakeButton.GetComponent<Button>().onClick = button;
        Button.ButtonClickedEvent button1 = new Button.ButtonClickedEvent();
        button1.AddListener(PutObj);
        PutButton.GetComponent<Button>().onClick = button1;
        Button.ButtonClickedEvent button2 = new Button.ButtonClickedEvent();
        button2.AddListener(Use);
        UseButton.GetComponent<Button>().onClick = button2;
        Button.ButtonClickedEvent button3 = new Button.ButtonClickedEvent();
        button3.AddListener(PickUp);
        NameText.GetComponent<Button>().onClick = button3;
        PickButton.GetComponent<Button>().onClick = button3;

        NameText.transform.GetChild(0).GetComponent<Text>().text = Name;
        TakeButton.SetActive(false);
        PutButton.SetActive(false);
        UseButton.SetActive(false);
        NameText.SetActive(false);
        PickButton.SetActive(false);
        startParent = transform.parent;

        Using += U;
    }

    public InventoryInformation GetInventoryInformation()
    {
        return new InventoryInformation(Name, Discription, InventoryImage, Prefab, ActionType, Characteristics);
    }

    public void setInterface(Interface Interface)
    {
        PickButton.SetActive(Interface != null);
        this.Interface = Interface;
    }

    public void PickUp()
    {
        if (Interface != null && Interface.PlaceForObjects.childCount == 0)
        {
            Rigidbody rigidbody;
            if (TryGetComponent(out rigidbody)) Destroy(rigidbody);
            transform.parent = Interface.PlaceForObjects;
            transform.localPosition = Vector3.zero;
            if (Interface.CanPickedUp()) TakeButton.SetActive(true);
            if (!(ActionType == (TypeOfAction.Nothing | TypeOfAction.Empty | TypeOfAction.Ingredient))) UseButton.SetActive(true);
            PutButton.SetActive(true);
            PickButton.SetActive(false);
            ResMainScript.OnUpdate -= OnUpdate;
        }

    }

    public void TakeObj()
    {
        Interface.PickedUp(GetInventoryInformation());
        ResMainScript.OnUpdate -= OnUpdate;
        ResMainScript.OnUpdate -= OnUpdate;
        Destroy(gameObject);
    }

    public void PutObj()
    {
        TakeButton.SetActive(false);
        PutButton.SetActive(false);
        UseButton.SetActive(false);
        transform.parent = startParent;
        gameObject.AddComponent<Rigidbody>();
        ResMainScript.OnUpdate -= OnUpdate;
    }

    public void ShowName(Transform Player)
    {
        if (transform.parent == startParent) this.Player = Player;
        else this.Player = Player.GetComponent<PlayerController>().Head;
        NameText.SetActive(true);
        ResMainScript.OnUpdate += OnUpdate;
    }

    public void HideName()
    {
        NameText.SetActive(false);
        ResMainScript.OnUpdate -= OnUpdate;
    }

    public void OnUpdate()
    {
        Controle.LookAt(Player);
    }

    public void Use()
    {
        Using();
    }

    private void U()
    {
        
    }

}
public enum TypeOfAction
{
    Empty,
    Nothing,
    JustUse,
    UseOnYourself,
    UseOnAliveCreater,
    Ingredient
}

public class InventoryInformation
{
    public string Name, Discription;
    public Sprite InventoryImage;
    public TypeOfAction ActionType;
    public Characteristic[] Characteristics;
    public GameObject Prefab;

    public InventoryInformation(string Name, string Discription, Sprite InventoryImage, GameObject Prefab, TypeOfAction ActionType, Characteristic[] Characteristics)
    {
        this.Name = Name;
        this.Discription = Discription;
        this.InventoryImage = InventoryImage;
        this.ActionType = ActionType;
        this.Characteristics = Characteristics;
        this.Prefab = Prefab;
    }
}




