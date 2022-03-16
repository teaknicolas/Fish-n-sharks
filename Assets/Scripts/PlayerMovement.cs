using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Touch touch;

    private Vector2 touchPos;

    private Quaternion rotY;
    private float speedMod = 0.3f;

    [SerializeField] private GameObject lightObj = null;

    public bool emptyCharge = false;

    public static PlayerMovement instance;

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

            if(touch.phase == TouchPhase.Moved)
            {
                rotY = Quaternion.Euler(0f, touch.deltaPosition.x * speedMod, 0f);
                transform.rotation = rotY * transform.rotation;
            }
        }
        else
        {
            lightObj.SetActive(false);
        }
    }
}
