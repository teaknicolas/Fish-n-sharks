using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    //[SerializeField] private Image chargeImage = null;
    //[SerializeField] private GameObject emptyImage = null;
    //[SerializeField] private float timeOffset = 2.0f;
    [SerializeField] private float timeMod = 4.0f;
    //[SerializeField] private TextMeshProUGUI distanceText = null;

    [SerializeField] private TextMeshProUGUI distanceText = null;
    [SerializeField] public TextMeshProUGUI pointsText = null;

    [SerializeField] private TextMeshProUGUI highScoreText = null;

    [SerializeField] private TextMeshProUGUI pointsBonusText = null;


    // Start Game over , Restart
    [SerializeField] private GameObject startPanel = null;

    [SerializeField] private GameObject restartPanel = null;
    //private float chargeValue = 1f;

    //public bool usingCharge;

    public static UIController instance;

    void Awake()
    {
        instance = this;
        highScoreText.text = "Best: " +PlayerPrefs.GetInt("maxScore", 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameController.GamePaused)
        {
            GameController.Distance += Time.deltaTime * timeMod;
            distanceText.text = String.Format("{0:0m}", GameController.Distance);
            pointsText.text = "Score : " + GameController.Points;
            pointsBonusText.text = "" + GameController.Bonuspoints;
            //Debug.Log("BONUS " + GameController.BonusCount);

            //if (usingCharge)
            //{
            //    chargeValue = Mathf.Clamp01(chargeValue - (timeOffset * Time.deltaTime));
            //    chargeImage.fillAmount = chargeValue;
            //}
            //else
            //{
            //    chargeValue = Mathf.Clamp01(chargeValue + (timeOffset * Time.deltaTime));
            //    chargeImage.fillAmount = chargeValue;
            //}
            //if(chargeValue <= 0)
            //{
            //    PlayerMovement.instance.emptyCharge = true;
            //    emptyImage.SetActive(true);
            //}
            //else
            //{
            //    PlayerMovement.instance.emptyCharge = false;
            //    emptyImage.SetActive(false);
            //}
        }

    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        GameController.GamePaused = false;

    }

    public void RestartGame()
    {
        int currentScore = int.Parse(pointsText.text);
        //int maxScore = PlayerPrefs.GetInt("maxScore", 0);
        
        //if(currentScore > maxScore)
        //{
        //    PlayerPrefs.SetInt("maxScore", currentScore);
        //}

      
        restartPanel.SetActive(false);
        GameController.GamePaused = false;
    }

    public void EndGame()
    {
        highScoreText.text = "Score: " + pointsText.text;

        restartPanel.SetActive(true);
        GameController.GamePaused = true;
        GameController.Distance = 0;
        GameController.Points = 0;
        GameController.EnnemyCount = 0;
        GameController.BonusCount = 0;
        GameController.Bonuspoints = 0;
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Shark"))
        {
            enemy.GetComponent<Shark>().Reset();
            enemy.SetActive(false);
        }
    }
}
