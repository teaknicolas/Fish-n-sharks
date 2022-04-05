using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    private Touch touch;

    private Vector2 touchPos;

    private Quaternion rotZ;
    [SerializeField] private float speedMod = 0.4f;

    //[SerializeField] private GameObject lightObj = null;

    public bool emptyCharge = false;

    public static PlayerMovement instance;

    // Player movements
    [SerializeField] private float playerSpeed;



    // Joystick system

    [SerializeField] private Joystick joystick;

    Vector2 movement_Indicator;

    private CircleCollider2D circle_collider;

    Vector2 direction;

    public GameObject indicator;

    public float PlayerSpeed { get => playerSpeed; set => playerSpeed = value; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        circle_collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            //if (!emptyCharge)
            //{
            //    lightObj.SetActive(true);
                
            //}
            //else
            //{
            //    lightObj.SetActive(false);
                
            //}

            //UIController.instance.usingCharge = true;

            //if (touch.phase == TouchPhase.Moved)
            //{
            //    if (Input.GetTouch(0).position.y > Screen.height / 2)
            //    {
            //        rotZ = Quaternion.Euler(0f, 0f, -touch.deltaPosition.x * speedMod);
            //        transform.rotation = rotZ * transform.rotation;
            //    }
            //    else
            //    {
            //        rotZ = Quaternion.Euler(0f, 0f, touch.deltaPosition.x * speedMod);
            //        transform.rotation = rotZ * transform.rotation;
            //        Debug.Log(Input.GetTouch(0).position.y + " hieght : " + Screen.height / 2);
            //    }


            //}

           
        }
        //else
        //{
        //    lightObj.SetActive(false);
        //    UIController.instance.usingCharge = false;
        //}


        movement_Indicator.x = this.transform.position.x + joystick.Horizontal * circle_collider.radius; // movement of the joystick

        movement_Indicator.y = this.transform.position.y + joystick.Vertical * circle_collider.radius;

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {


            indicator.SetActive(true);
            indicator.transform.position = movement_Indicator;


            direction = movement_Indicator - (Vector2)this.transform.position;
            direction = direction.normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //set the angle into a quaternion + sprite offset depending on initial sprite facing direction
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            //Roatate current game object to face the target using a slerp function which adds some smoothing to the move
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speedMod );
        }
        else
        {
            indicator.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        transform.position += transform.up * PlayerSpeed  * Time.deltaTime;

        if (touch.phase == TouchPhase.Moved)
        {

            //if (Input.GetTouch(0).position.y > Screen.height / 2)
            //{
            //    rotZ = Quaternion.Euler(0f, 0f, -touch.deltaPosition.x * speedMod);
            //    transform.rotation = rotZ * transform.rotation;
            //}
            //else
            //{
            //    rotZ = Quaternion.Euler(0f, 0f,  joystick.Horizontal * speedMod);
            //    transform.rotation = rotZ * transform.rotation;
            //    Debug.Log(Input.GetTouch(0).position.y + " hieght : " + Screen.height / 2);
            //}
            //rotZ = Quaternion.Euler(0f, 0f, (joystick.Horizontal + joystick.Vertical) * speedMod);
            //transform.rotation =  transform.rotation * rotZ;

        }


        //if (touch.phase == TouchPhase.Moved)
        //{

        //    //if (Input.GetTouch(0).position.y > Screen.height / 2)
        //    //{
        //    //    rotZ = Quaternion.Euler(0f, 0f, -touch.deltaPosition.x * speedMod);
        //    //    transform.rotation = rotZ * transform.rotation;
        //    //}
        //    //else
        //    //{
        //    //    rotZ = Quaternion.Euler(0f, 0f, touch.deltaPosition.x * speedMod);
        //    //    transform.rotation = rotZ * transform.rotation;
        //    //    Debug.Log(Input.GetTouch(0).position.y + " hieght : " + Screen.height / 2);
        //    //}

        //    Vector2 directionTouch;
        //    directionTouch = Input.GetTouch(0).position - (Vector2)transform.position;
        //    float angle = Mathf.Atan2(directionTouch.y, directionTouch.x) * Mathf.Rad2Deg;
        //    Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speedMod * Time.deltaTime); 
        //}

        
    }
}
