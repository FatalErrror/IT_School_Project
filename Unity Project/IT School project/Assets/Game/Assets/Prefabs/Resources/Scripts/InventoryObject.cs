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
    public int CountInOneSlote = 16;
    public bool DestroyAfterPut;
    public float DealayBeforeDestroy;
    public Characteristic[] Characteristics;

    [Header("Controle")]
    public string Prefab;

    Transform Controle;
    GameObject NameText;
    GameObject TakeButton;
    GameObject PutButton;
    GameObject UseButton;
    GameObject PickButton;

    
    Transform Player, startParent;
    Interface Interface;
    Coroutine DestroyCoroutine;


    public virtual void Initialize()
    {
        
    }

    public virtual void Use()
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
        //startParent = transform.parent;   
    }

    public InventoryInformation GetInventoryInformation()
    {
        return new InventoryInformation(
            Name, 
            Discription,
            InventoryImage,
            ResMainScript.getPrefab(Prefab),
            ActionType,
            CountInOneSlote,
            Characteristics,
            startParent);
    }

    public void setInterface(Interface Interface)
    {
        PickButton.SetActive(Interface != null && Interface.PlaceForObjects.childCount == 0);
        this.Interface = Interface;
    }

    public void PickUp()
    {
        if (Interface != null && Interface.PlaceForObjects.childCount == 0)
        {
            gameObject.layer = 5;
            CancelDestroy();

            Rigidbody rigidbody;
            if (TryGetComponent(out rigidbody)) Destroy(rigidbody);
            transform.SetParent(Interface.PlaceForObjects);
            transform.localPosition = Vector3.zero;
            Vector3 position = transform.position - Controle.position;
            transform.Translate(position, Space.World);
            

            if (Interface.CanPickedUp(GetInventoryInformation())) TakeButton.SetActive(true);
            if (!(ActionType == (TypeOfAction.Nothing | TypeOfAction.Empty | TypeOfAction.Ingredient))) UseButton.SetActive(true);
            PutButton.SetActive(true);
            PickButton.SetActive(false);
            //ResMainScript.OnUpdate -= OnUpdate;
        }

    }

    public void TakeObj()
    {
        Interface.PickedUp(GetInventoryInformation());
        DestroyThis();
    }

    public void PutObj()
    {
        gameObject.layer = 0;
        StartDestroy();

        TakeButton.SetActive(false);
        PutButton.SetActive(false);
        UseButton.SetActive(false);
        transform.SetParent(startParent);
        gameObject.AddComponent<Rigidbody>();
        ResMainScript.OnUpdate -= OnUpdate;
    }

    public void ShowName(Transform Player)
    {
        if (transform.parent == startParent) this.Player = Player;
        else this.Player = Player.GetComponent<PlayerController>().Head;
        NameText.SetActive(true);
        ResMainScript.OnUpdate -= OnUpdate;
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

    public void StartDestroy()
    {
        if (DestroyAfterPut)
            DestroyCoroutine = Timer.DelayCall(DealayBeforeDestroy, DestroyThis, false);
    }

    public void CancelDestroy()
    {
        if (DestroyCoroutine != null)
            StopCoroutine(DestroyCoroutine);
    }

    public void DestroyThis()
    {
        CancelDestroy();
        ResMainScript.OnUpdate -= OnUpdate;
        ResMainScript.OnUpdate -= OnUpdate;
        Destroy(gameObject);
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
    public int CountInOneSlote;
    public Characteristic[] Characteristics;
    public GameObject Prefab;
    public Transform Parent;

    public InventoryInformation(string Name,
        string Discription, 
        Sprite InventoryImage, 
        GameObject Prefab,
        TypeOfAction ActionType,
        int CountInOneSlote,
        Characteristic[] Characteristics,
        Transform Parent)
    {
        this.Name = Name;
        this.Discription = Discription;
        this.InventoryImage = InventoryImage;
        this.ActionType = ActionType;
        this.CountInOneSlote = CountInOneSlote;
        this.Characteristics = Characteristics;
        this.Prefab = Prefab;
        this.Parent = Parent;
    }
}




