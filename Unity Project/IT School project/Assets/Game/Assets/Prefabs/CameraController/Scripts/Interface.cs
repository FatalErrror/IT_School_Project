using SimpleInputNamespace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour, ISerealizable
{
    [Header("Inventory panel")]
    public Text Discription;
    public GameObject UseBt;
    public Sprite defoultSprite;
    public Transform Buttons;

    public GameObject prefab; // remove this

    private string discription { get { return Discription.text; } set { Discription.text = value; } }

    [Header("Settings panel")]
    public Slider shift;
    public Slider jumpSensetive;
    public Toggle aim;
    public Toggle dynemickJoystick;

    [Header("Main interface")]
    public GameObject forUnrender;
    public GameObject Hit;
    public GameObject Aim;
    public GameObject Hub;
    public Joystick joystic;
    public Transform PlaceForObjects;


    
    //CameraController
    Inventory inventory;
    int SelectedItem;
    Image[] items;
    Text[] counts;
    Color dark, standart;
    float HubAngle;
    // Start is called before the first frame update
    void Start()
    {
        Hit.SetActive(false);
        Hub.SetActive(false);
        items = new Image[Buttons.childCount];
        counts = new Text[Buttons.childCount];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = Buttons.GetChild(i).GetChild(0).GetComponent<Image>();
            counts[i] = Buttons.GetChild(i).GetChild(1).GetComponent<Text>();
        }
        inventory = new Inventory(items.Length, defoultSprite);
        standart = Buttons.GetChild(0).GetComponent<Image>().color;
        dark = new Color(1, 1, 1, 1);//  dark( 29 / 255, 29 / 255, 29 / 255, 1)


        Serealizer.LoadData(new ISerealizable[] { this });
        Debug.Log("Data loaded");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //=================== public =====================
    public void Back()
    {
        HubAngle = Hub.transform.rotation.eulerAngles.y - transform.parent.parent.rotation.eulerAngles.y;
        Time.timeScale = 1;
        forUnrender.SetActive(true);
        Hub.SetActive(false);
        Buttons.GetChild(SelectedItem).GetComponent<Image>().color = standart;
    }

    public void Settings()
    {
        Time.timeScale = 0;
        forUnrender.SetActive(false);
        Hub.SetActive(true);
        Quaternion quaternion = new Quaternion();
        quaternion.eulerAngles = new Vector3(0, transform.parent.parent.rotation.eulerAngles.y + HubAngle, 0);
        Hub.transform.rotation = quaternion;
        inventory.Render(items, counts);
        discription = "Инвентарь";
    }

    //=============== Inventory panel ================
    public void SelectItem(Button button)
    {
        Buttons.GetChild(SelectedItem).GetComponent<Image>().color = standart;
        int num = System.Convert.ToInt32(button.name.Substring(4, button.name.Length - 5));
        SelectedItem = num-1;
        button.GetComponent<Image>().color = dark;
        discription = inventory.GetDiscription(SelectedItem);

    }

    public void ThrowOut()
    {
        prefab = inventory.inventories[SelectedItem].Prefab;
        GameObject temp = Instantiate(
            inventory.inventories[SelectedItem].Prefab,
            Buttons.GetChild(SelectedItem).position,
            Quaternion.identity, inventory.inventories[SelectedItem].Parent)
            .AddComponent<Rigidbody>().gameObject;
        temp.transform.LookAt(transform);
        temp.transform.Translate(0,0,-1,Space.Self);
        inventory.removeItem(SelectedItem);
        inventory.Render(items, counts);
        discription = inventory.GetDiscription(SelectedItem);
        temp.GetComponent<InventoryObject>().StartDestroy();
    }

    public void Use()
    {

    }
    
    //================ Settings panel ================
    public void changeShift()
    {
        transform.parent.parent.parent.GetComponent<CameraController>().ChangeShift(shift.value);
    }
    public void changeAim()
    {
        Aim.SetActive(aim.isOn);
    }
    public void changeDynemicJoystic()
    {
         joystic.IsDynamicJoystick(dynemickJoystick.isOn);
    }

    public void SaveAndOut()
    {
        Serealizer.SavaData(new ISerealizable[] {this });
        Debug.Log("Data saved");
        Application.Quit();
        Debug.Log("Quited");
    }


    //================ Main interface ================
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "InventoryObject") other.GetComponent<InventoryObject>().setInterface(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "InventoryObject") other.GetComponent<InventoryObject>().setInterface(null);
    }

    public bool CanPickedUp(InventoryInformation information)
    {
        if (inventory.isFull(information)) return false;
        else return true;
    }

    public void PickedUp(InventoryInformation information)
    {
        int i;
        inventory.isFull(information, out i);
        inventory.setItem(i, information);
    }



    public string getName()
    {
        return "interface";
    }

    public string[] Serealization()
    {
        string[] data = new string[5+items.Length*2];
        data[0] = shift.value.ToString();
        data[1] = jumpSensetive.value.ToString();
        data[2] = aim.isOn.ToString();
        data[3] = dynemickJoystick.isOn.ToString();
        data[4] = HubAngle.ToString();
        for (int i = 5; i < items.Length+5; i++)
        {
            data[i] = inventory.inventories[i-5]!=null?inventory.inventories[i-5].Prefab.name:"\n";
        }
        for (int i = items.Length + 5; i < items.Length*2+5; i++)
        {
            data[i] = inventory.Counts[i-items.Length - 5].ToString();
        }

        return data;
    }

    public void Deserealization(string[] data)
    {
        shift.value = (float)Convert.ToDouble(data[0]);
        jumpSensetive.value = (float)Convert.ToDouble(data[1]);
        aim.isOn = Convert.ToBoolean(data[2]);
        dynemickJoystick.isOn = Convert.ToBoolean(data[3]);
        HubAngle = (float)Convert.ToDouble(data[4]);
        for (int i = 5; i < items.Length + 5; i++)
        {
            inventory.inventories[i - 5] = data[i].Equals("\n") ? null : ResMainScript.getPrefab(data[i]).GetComponent<InventoryObject>().GetInventoryInformation();
        }
        for (int i = items.Length + 5; i < items.Length * 2 + 5; i++)
        {
            inventory.Counts[i - items.Length - 5] = Convert.ToInt32(data[i]);
        }
    }
}
