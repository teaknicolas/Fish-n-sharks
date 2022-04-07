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
        if (!GameController.GamePaused)
        {


            transform.position += transform.up * PlayerSpeed * Time.deltaTime;
        if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
               

           
            }
            


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
    }

    
}
