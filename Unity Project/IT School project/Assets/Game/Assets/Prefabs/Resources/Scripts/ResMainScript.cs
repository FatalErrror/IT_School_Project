using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResMainScript : MonoBehaviour
{
    public delegate void Update();
    public static event Update OnUpdate;

    public GameObject[] Prefabs;
    static GameObject[] prefabs;
    static string[] Names;
    // Start is called before the first frame update

    private void Start()
    {
        OnUpdate += onUpdate;
        Names = new string[Prefabs.Length];
        prefabs = new GameObject[Prefabs.Length];
        for (int i = 0; i < Names.Length; i++)
        {
            Names[i] = Prefabs[i].name;
            prefabs[i] = Prefabs[i];
        }
    }

    public static GameObject getPrefab(string Name)
    {
        int index = 0;
        for (int i=0;i < prefabs.Length;i++)
            if (Name.Equals(prefabs[i].name))
            {
                index = i;
                break;
            }
        return prefabs[index];
    }

    private void FixedUpdate()
    {
       OnUpdate();
    }

    void onUpdate()
    {

    }
}
