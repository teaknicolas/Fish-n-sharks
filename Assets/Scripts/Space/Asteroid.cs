using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Asteroid : MonoBehaviour
{
    public Transform _player;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    private void Awake()
    {
        // When the object is instanciated
        rb = this.GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Collision behaviour with Player
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
            GameController.instance.GameOver();
            //Reset();
            this.gameObject.SetActive(false);
            //Destroy(explosion, 3f);
        }

        // Functionality of sharks cancelling each other
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            GameController.Points += 1;
            

            //Reset();
            this.gameObject.SetActive(false);
            //Destroy(cannibal, 1f);

        }
    }
}
