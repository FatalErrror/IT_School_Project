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


    [Header("Main interface")]
    public Transform forUnrender;
    public Joystick joystic;


    Inventory inventory = new Inventory(12);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //=================== public =====================


    //=============== Inventory panel ================

    
    //================ Settings panel ================


    //================ Main interface ================


}
