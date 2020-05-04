using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResMainScript : MonoBehaviour
{
    public delegate void Update();
    public static event Update OnUpdate;
    // Start is called before the first frame update

    private void Start()
    {
        OnUpdate += onUpdate;
        
    }

    private void FixedUpdate()
    {
       OnUpdate();
    }

    void onUpdate()
    {

    }
}
