using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Touch touch;

    private Vector2 touchPos;

    private Quaternion rotZ;
    [SerializeField] private float speedMod = 0.4f;

    [SerializeField] private GameObject lightObj = null;

    public bool emptyCharge = false;

    public static PlayerMovement instance;

    // Player movements
    [SerializeField] private float playerSpeed;


    private void Awake()
    {
        instance = this;
    }



    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (!emptyCharge)
            {
                lightObj.SetActive(true);
                
            }
            else
            {
                lightObj.SetActive(false);
                
            }

            UIController.instance.usingCharge = true;

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
        else
        {
            lightObj.SetActive(false);
            UIController.instance.usingCharge = false;
        }



    }

    private void FixedUpdate()
    {
        transform.position += transform.up * playerSpeed  * Time.deltaTime;

        if (touch.phase == TouchPhase.Moved)
        {

            if (Input.GetTouch(0).position.y > Screen.height / 2)
            {
                rotZ = Quaternion.Euler(0f, 0f, -touch.deltaPosition.x * speedMod);
                transform.rotation = rotZ * transform.rotation;
            }
            else
            {
                rotZ = Quaternion.Euler(0f, 0f, touch.deltaPosition.x * speedMod);
                transform.rotation = rotZ * transform.rotation;
                Debug.Log(Input.GetTouch(0).position.y + " hieght : " + Screen.height / 2);
            }

        }
    }
}
