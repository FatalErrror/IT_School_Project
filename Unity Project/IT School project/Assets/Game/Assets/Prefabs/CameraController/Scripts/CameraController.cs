using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    Transform Camera;
    Transform Player;
    PlayerController playerController;

    public Interface Interface;

    Gyroscope gyro;
    Quaternion rot, attitude;
    bool Android, isPlaying = true;
    public float Angle, RotateSpeed = 0.5f;

    bool ISMINE;

    public void setISMINE(bool value, PlayerController player)
    {
        Camera = GetComponentInChildren<Camera>().transform;
        Interface = GetComponentInChildren<Interface>();
        playerController = player;
        Player = player.transform;
        ISMINE = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        Android = Application.platform == RuntimePlatform.Android;
        /*
        if (!Android)
        {
            Quaternion Q = new Quaternion();
            Q.eulerAngles = new Vector3(0, 90, -90);
            Camera.localRotation = Q;
        }*/
        gyro = Input.gyro;
        gyro.enabled = true;
        transform.rotation = Quaternion.Euler(90, 90, 0);
        rot = new Quaternion(0, 0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (ISMINE)
        {
            if (isPlaying)
            {
                inPlay();
            }
            else
            {

            }
        }

    }


    void inPlay()
    {
        if (Android)
        {
            attitude = gyro.attitude;
            Camera.localRotation = attitude * rot;
            attitude.eulerAngles = Camera.rotation.eulerAngles;
            Camera.rotation = attitude;
        }
        else
        {
            if (Input.GetKey(KeyCode.A)) Camera.Rotate(0, -RotateSpeed, 0, Space.World);
            if (Input.GetKey(KeyCode.D)) Camera.Rotate(0, RotateSpeed, 0, Space.World);

            if (Input.GetKey(KeyCode.S)) Camera.Rotate( RotateSpeed, 0, 0, Space.Self);
            if (Input.GetKey(KeyCode.W)) Camera.Rotate( -RotateSpeed, 0, 0, Space.Self);
        }
        transform.position = playerController.Head.position;
        Angle = Camera.transform.rotation.eulerAngles.y;
    }

    public void ChangeCameraSensitive(float value)
    {
        RotateSpeed = value;
    }

    public void ChangeShift(float value)
    {
        Vector3 vector = transform.rotation.eulerAngles;
        vector.y = value;
        Quaternion Q = new Quaternion();
        Q.eulerAngles = vector;
        transform.rotation = Q;
    }
    private void OnApplicationPause(bool pause)
    {
        //gyro.enabled = !pause;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "InventoryObject") other.GetComponent<InventoryObject>().ShowName(playerController.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "InventoryObject") other.GetComponent<InventoryObject>().HideName();
    }
}
