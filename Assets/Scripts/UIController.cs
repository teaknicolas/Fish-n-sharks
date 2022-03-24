using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    [SerializeField] private Image chargeImage = null;
    [SerializeField] private GameObject emptyImage = null;
    [SerializeField] private float timeOffset = 2.0f;
    [SerializeField] private float timeMod = 4.0f;
    [SerializeField] private TextMeshProUGUI distanceText = null;

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

        GameController.Distance += Time.deltaTime * timeMod;
        distanceText.text = String.Format("{0:0m}", GameController.Distance);


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
            emptyImage.SetActive(true);
        }
        else
        {
            PlayerMovement.instance.emptyCharge = false;
            emptyImage.SetActive(false);
        }
    }
}
