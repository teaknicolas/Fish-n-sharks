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
        if (collision.gameObject.CompareTag("Player"))
        {
            GameController.BonusCount += 1;
            //var explosion = Instantiate(explosionEffect, transform.position, transform.rotation);

            PlayerMovement.instance.PlayerSpeed += 0.1f;
            this.gameObject.SetActive(false);
            //Destroy(explosion, 3f);
        }

       
    }
}
