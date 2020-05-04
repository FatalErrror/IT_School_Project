using SimpleInputNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CameraController cameraController;
    Joystick joystick;
    Interface Interface;

    public Transform Head;

    public float speed = 2, standartSpeed;

    Vector3 movement;
    float angle;

    bool IsGrounded, joysticMode = true;
    // Start is called before the first frame update
    void Start()
    {
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").transform.parent.GetComponent<CameraController>();
        cameraController.setISMINE(true, this);
        Interface = cameraController.Interface;
        joystick = Interface.joystic;
    }

    // Update is called once per frame
    void Update()
    {
        if (joysticMode) Move();
    }


    private void Move()
    {
        Quaternion Q = new Quaternion();
        Q.eulerAngles = new Vector3(0, cameraController.Angle, 0);
        transform.rotation = Q;
        angle = Q.y - 180;
        angle *= Mathf.Deg2Rad;
        movement = new Vector3(
            -joystick.xAxis.value * Mathf.Cos(angle) - joystick.yAxis.value * Mathf.Sin(angle),
            0,
            joystick.xAxis.value * Mathf.Sin(angle) - joystick.yAxis.value * Mathf.Cos(angle)
            );

        transform.Translate(movement * speed * Time.deltaTime);
        
        if (Input.acceleration.magnitude > Interface.jumpSensetive.value) jump(); 
        if (Input.GetKeyDown(KeyCode.Space)) jump();

    }


    public void jump()
    {
        if (IsGrounded)
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IsGrounded = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        IsGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        IsGrounded = false;
    }


    /*Old code
    //delegate() { } - анонимный метод

    public GameObject timer, Spel1, Spel2;
    public Joystick joystick;
    public Interface Interface;
    public float speed, angle, standartSpeed, Yspeed;
    public bool cooldown = false;
    public float HP = 300, MP = 200;
    Vector3 movement;
    CameraController cameraController;
    LineIndecator HPIndecator, MPIndecator;
    Animator anim;
    GameObject Spel;
    bool IsSpel1 = true;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(Application.version);
        speed = standartSpeed;

        Interface = GameObject.FindGameObjectWithTag("Interface").GetComponent<Interface>();

        transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        cameraController = GameObject.FindGameObjectWithTag("CameraController").GetComponent<CameraController>();
        anim = GetComponent<Animator>();
        HPIndecator = Interface.HPIndecator;
        MPIndecator = Interface.MPIndecator;
        HPIndecator.MaxValue = HP;
        MPIndecator.MaxValue = MP;
        HP = PlayerPrefs.GetFloat("HP", HP);
        MP = PlayerPrefs.GetFloat("MP", MP);
        HPIndecator.SetValue(HP);
        MPIndecator.SetValue(MP);
        Spel = Spel1;
        Timer.DelayCall(1, UpMP);
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("HP", HP);
        PlayerPrefs.SetFloat("MP", MP);
    }

    private void OnApplicationQuit()
    {
        Save();
    }
    public void Die()
    {
        PlayerPrefs.DeleteAll();
        Application.LoadLevel("Hub");
    }

    public void UpMP()
    {
        MPIndecator.SetValue(MP + 1);
        MP = MPIndecator.Value;
        Timer.DelayCall(1, UpMP);
    }

    public Sprite ChangeSpel()
    {
        if (IsSpel1)
        {
            Spel = Spel2;
        }
        else
        {
            Spel = Spel1;
        }
        IsSpel1 = !IsSpel1;
        return Spel.GetComponent<MoveBall>().Image;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0) Die();
        Quaternion Q = new Quaternion();
        Q.eulerAngles = new Vector3(0, cameraController.Angle, 0);
        transform.rotation = Q;
        angle = Q.y - 180;
        angle *= Mathf.Deg2Rad;
        movement = new Vector3(
            -joystick.xAxis.value * Mathf.Cos(angle) - joystick.yAxis.value * Mathf.Sin(angle),
            Yspeed,
            joystick.xAxis.value * Mathf.Sin(angle) - joystick.yAxis.value * Mathf.Cos(angle)
            );

        transform.Translate(movement * speed * Time.deltaTime);
        if (speed != 0 && movement.magnitude != 0)
        {
            anim.SetBool("Moving", true);
            //anim.SetFloat("Speed", speed * movement.magnitude * Time.deltaTime * 10);
        }
        else
        {
            anim.SetBool("Moving", false);
        }
    }

    public void hit(float damage)
    {
        HP -= damage;
        HPIndecator.SetValue(HP);
        Interface.Render(Interface.Hit);
        Timer.DelayCall(0.5f, delegate () { Interface.Unrender(Interface.Hit); });
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Hit")
        {
            hit(collision.transform.GetComponentInParent<OpponentController>().Damage);
        }
    }

    public void CreateSpel()
    {
        MP -= Spel1.GetComponent<MoveBall>().NeedMP;
        MPIndecator.SetValue(MP);
        GameObject erekiBall2 = Instantiate(Spel, cameraController.Camera);
        erekiBall2.transform.SetParent(transform.parent);
    }

    public void Attack()
    {
        if (!cooldown && MP - Spel.GetComponent<MoveBall>().NeedMP > 0)
        {
            anim.SetTrigger("Attack");
            Timer.DelayCall(0.54f, CreateSpel);
            speed = 0.2f * standartSpeed;
            cooldown = true;
            Timer.DelayCall(Spel.GetComponent<MoveBall>().Cooldown, delegate () { cooldown = false; });
            Timer.DelayCall(1f, delegate () { speed = standartSpeed; });
        }
    }*/
}
