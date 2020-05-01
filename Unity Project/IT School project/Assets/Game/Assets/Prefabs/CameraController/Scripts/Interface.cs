using SimpleInputNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    [Header("Inventory panel")]
    public Image[] items;

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

    //CameraController
    Inventory inventory = new Inventory(9);

    // Start is called before the first frame update
    void Start()
    {
        Hit.SetActive(false);
        Hub.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //=================== public =====================
    public void Back()
    {
        Time.timeScale = 1;
        forUnrender.SetActive(true);
        Hub.SetActive(false);
    }

    public void Settings()
    {
        Time.timeScale = 0;
        forUnrender.SetActive(false);
        Hub.SetActive(true);
        Quaternion quaternion = new Quaternion();
        quaternion.eulerAngles = new Vector3(0, transform.parent.parent.rotation.eulerAngles.y, 0);
        Hub.transform.rotation = quaternion;
    }

    //=============== Inventory panel ================

    
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


}
