﻿using SimpleInputNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    [Header("Inventory panel")]
    public Text Discription;
    public GameObject UseBt;
    public Sprite defoultSprite;
    public Transform Buttons;

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
    Color dark, standart;
    float HubAngle;
    // Start is called before the first frame update
    void Start()
    {
        Hit.SetActive(false);
        Hub.SetActive(false);
        items = new Image[Buttons.childCount];
        for (int i = 0; i < items.Length; i++) items[i] = Buttons.GetChild(i).GetChild(0).GetComponent<Image>();
        inventory = new Inventory(items.Length, defoultSprite);
        standart = Buttons.GetChild(0).GetComponent<Image>().color;
        dark = new Color(29 / 255, 29 / 255, 29 / 255, 1);
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
        inventory.Render(items);
        discription = "Инвентарь";
    }

    //=============== Inventory panel ================
    public void SelectItem(Button button)
    {
        Buttons.GetChild(SelectedItem).GetComponent<Image>().color = standart;
        SelectedItem = (button.name[4] - '0')-1;
        button.GetComponent<Image>().color = dark;
        discription = inventory.GetDiscription(SelectedItem);
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

    public bool CanPickedUp()
    {
        if (inventory.isFull()) return false;
        else return true;
    }

    public void PickedUp(InventoryInformation information)
    {
        int i;
        inventory.isFull(out i);
        inventory.setItem(i, information);
    }
}