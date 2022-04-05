using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bonus : MonoBehaviour
{

    //public Transform _player;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    //public GameObject explosionEffect;
    private void Awake()
    {
       
        rb = this.GetComponent<Rigidbody2D>();
        


    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameController.Bonuspoints ++;
            //var explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
            //Debug.Log("Points " + GameController.BonusCount);
           
            PlayerMovement.instance.PlayerSpeed += 0.5f;

            GameController.BonusCount--;
            this.gameObject.SetActive(false);
            //Destroy(explosion, 3f);
        }

       
    }
}
