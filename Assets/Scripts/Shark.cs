using System.Collections;
using System.Collections.Generic;

using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Shark : MonoBehaviour
{

    public Transform _player;
    private Rigidbody2D rb;

    public GameObject explosionEffect;
    public GameObject cannibalEffect; // for shar k killing each other
    [SerializeField]
    private float maxSpeed = 9f;
    [SerializeField]
    private float minSpeed ; // toujours plus rapide que Player
    [SerializeField]
    private float slowingSpeed = 5f;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float speedRespawn ;

    [SerializeField]
    private float rotateSpeed = 200f;

    [SerializeField]
    private float radius = 2f;

    [SerializeField]
    private bool isSpeeding = false;
    private void Awake()
    {
        //_player = _player.GetComponent<GameObject>();
        rb = this.GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        minSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().PlayerSpeed + 1;

        speedRespawn = speed;
    }

    // Start is called before the first frame update
    void Start()
    {

        //Vector2 dir = _player.position - this.transform.position;
        //if (_player.tag == "Player")
        //{
        //    _rigidbody2D.AddForce(dir * powerOfShark);
        //}

    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 dir = _player.transform.position - this.transform.position;
        //this.transform.position = this.transform.position * dir * Time.deltaTime * powerOfShark;
    }

    private void FixedUpdate()
    {
        // Gestion de trajectoire requin 0.1.0

        //if (!GameController.GamePaused)  
        //{
        //    Vector2 direction = (Vector2)_player.position - rb.position;

        //    direction.Normalize();

        //    float rotateAmount = Vector3.Cross(direction, transform.up).z;

        //    rb.angularVelocity = rotateSpeed * -rotateAmount;
        //    rb.velocity = transform.up * speed;

        //    if (isSpeeding == false)
        //    {
        //        StartCoroutine(AugmentSpeed(0.5f));
        //    }
        //}
        //else { }

        // Gestion de trajectoire requin 0.2.0


        //if (!GameController.GamePaused)
        //{
        //   // Vector2 direction = (Vector2)_player.position - rb.position;

        //   //// direction.Normalize();  // Pas nécessaire 

        //   // float rotateAmount = Vector3.Cross(direction, transform.up).z;

        //   // rb.angularVelocity = rotateSpeed * -rotateAmount;
        //   // rb.velocity = transform.up * speed;

        //   // if (isSpeeding == false)
        //   // {
        //   //     StartCoroutine(AugmentSpeed(0.5f));
        //   // }
        //}



        Arrive();

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var explosion =  Instantiate(explosionEffect, transform.position, transform.rotation);
            GameController.instance.GameOver();
            Reset();
            this.gameObject.SetActive(false);
            Destroy(explosion, 3f);
        }

        // Functionality of sharks cancelling each other
        if (collision.gameObject.CompareTag("Shark"))
        {
            GameController.Points += 1;
            var cannibal = Instantiate(cannibalEffect, transform.position, transform.rotation);
            
            Reset();
            this.gameObject.SetActive(false);
            Destroy(cannibal, 1f);
            
        }
    }

    private IEnumerator AugmentSpeed(float time)
    {
        isSpeeding = true;
        yield return new WaitForSeconds(time);

        float speedtemp = speed;
        if (speed < maxSpeed)
        {
            speed = speed + GameController.DifficultyMultiplier;
        }

        //Debug.Log("Speed of ennemy" + speed);
        isSpeeding = false;
    }

    public void Reset()
    {
        this.speed = speedRespawn;
    }

    public void Arrive() // Implementation of Arrive behviour 
    {
        if (!GameController.GamePaused)
        {
            Vector2 direction = (Vector2)_player.position - rb.position;

            float distance = Vector2.Distance((Vector2)_player.position, rb.position);

            rb.AddForce(direction);

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            rb.angularVelocity = rotateSpeed * -rotateAmount;
            Debug.Log("Shark speed + " + speed);

            //if (isSpeeding == false)
            //{
            //    StartCoroutine(AugmentSpeed(0.5f));
            //}


            if (distance < radius) // Quand le requin arrive dans le rayon 'radius' du joueur qu'on définie
            {
                
                float desiredSpeed =Mathf.Clamp(speed / distance, minSpeed , maxSpeed); //(maxSpeed * distance, 0, maxSpeed)->formule buggé

                rb.velocity = transform.up * desiredSpeed ;
                //Debug.Log("  desiredSpeed : " + desiredSpeed + "velocity " + rb.velocity);

            }
            else
            {
                if(speed < maxSpeed)
                {
                    speed = maxSpeed;
                }
                rb.velocity = transform.up * speed;
                
                //Debug.Log("OUT   distavce : "  + distance + " maxSpeed : " + maxSpeed + " VELOCITY : " + rb.velocity);
                //Debug.Log("  speed : " + speed + "velocity "  + rb.velocity);
            }
        }
    }



}
