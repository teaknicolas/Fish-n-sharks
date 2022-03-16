using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Image chargeImage = null;
    [SerializeField] private GameObject emptyImage = null;
    [SerializeField] private float timeOffset = 2.0f;
    [SerializeField] private float timeMod = 4.0f;

    private float chargeValue = 1f;

    public bool usingCharge;

    public static UIController instance;

    void Awake()
    {
        instance = this; 
    }
    // Update is called once per frame
    void Update()
    {
        if (usingCharge)
        {
            chargeValue = Mathf.Clamp01(chargeValue - (timeOffset * Time.deltaTime));
            chargeImage.fillAmount = chargeValue;
        }
        else
        {
            chargeValue = Mathf.Clamp01(chargeValue + (timeOffset * Time.deltaTime));
            chargeImage.fillAmount = chargeValue;
        }
        if(chargeValue <= 0)
        {
            PlayerMovement.instance.emptyCharge = true;
        }
    }
}
