using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform Camera;
    Transform Player;
    PlayerController playerController;

    public Interface Interface;

    Gyroscope gyro;
    Quaternion rot, attitude;
    bool Android, isPlaying = true;
    public float Angle;

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
        if (!Android)
        {
            Quaternion Q = new Quaternion();
            Q.eulerAngles = new Vector3(0, 90, -90);
            Camera.localRotation = Q;
        }
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
            Camera.Rotate(0, Input.GetAxis("Horizontal"), 0, Space.World);
            Camera.Rotate(-Input.GetAxis("Vertical"), 0, 0, Space.Self);
        }
        transform.position = playerController.Head.position;
        Angle = Camera.transform.rotation.eulerAngles.y;
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
}
