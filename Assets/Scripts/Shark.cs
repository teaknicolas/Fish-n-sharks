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
    private float minSpeed = 2f;
    [SerializeField]
    private float slowingSpeed = 5f;
    [SerializeField]
    private float speed = 1f;

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

    //private IEnumerator AugmentSpeed(float time)
    //{
    //    isSpeeding = true;
    //    yield return new WaitForSeconds(time);

    //    float speedtemp = speed;
    //    if(speed < 13f)
    //    {
    //        speed = speed + GameController.DifficultyMultiplier;
    //    }
       
    //    //Debug.Log("Speed of ennemy" + speed);
    //    isSpeeding = false;
    //}

    public void Reset()
    {
        speed = 2f;
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


            if (distance < radius) // Quand le requin arrive dans le rayon 'radius' du joueur qu'on définie
            {
                
                float desiredSpeed =Mathf.Clamp( maxSpeed * distance, 0, maxSpeed);

                rb.velocity = transform.up * desiredSpeed ;
                Debug.Log(" CMA BITE ZEAEAZ : " + desiredSpeed + " NON JE RIGOLE :  " + distance + " maxSpeed : " + maxSpeed +" VELOCITY : " + rb.velocity );
            }
            else
            {
                rb.velocity = transform.up * maxSpeed;
            }
        }
    }



}
