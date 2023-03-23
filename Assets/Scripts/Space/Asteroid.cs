using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Asteroid : MonoBehaviour
{
    public Transform _player;
    private Rigidbody2D rb;
    public float MinSpeed = 1f;
    public float MaxSpeed = 10f;
    public float speed;

    [SerializeField]
    private float mass;
    [SerializeField]
    private Vector2 velocity;

    public float MinMass = 1f;
    public float MaxMass = 10f;

    public float MinSize = 1f;
    public float MaxSize = 5f;

    public float MinDensity = 1f;
    public float MaxDensity = 10f;

    public float screenHeight = 200f;
    public float screenWidth = 200f;
    private BoxCollider2D worldBounds; // reference to the world bounds collider

    public float repulsionForce = 10f;

    public GameObject explosionEffect;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        // get the reference to the world bounds collider
        worldBounds = GameObject.FindGameObjectWithTag("WorldBounds").GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        // Generate a random size between MinSize and MaxSize
        float size = Random.Range(MinSize, MaxSize);
        this.transform.localScale = new Vector3(size, size, size);

        // Generate a random density between MinDensity and MaxDensity
        float density = Random.Range(MinDensity, MaxDensity);

        // Generate a random density between MinDensity and MaxDensity
         speed = Random.Range(MinSpeed, MaxSpeed);

        // Calculate the mass of the asteroid based on its size and density
        if (size > 0 && density > 0)
        {
            mass = (4f / 3f) * Mathf.PI * Mathf.Pow(size / 2f, 3f) * density;
        }
        else
        {
            Debug.LogWarning("Invalid size or density generated for asteroid.");
        }

        // Set a random initial velocity
        velocity = Random.insideUnitSphere * MaxSpeed;
    }

    void Update()
    {


        // update the position based on the velocity
        this.transform.position += (Vector3)velocity * Time.deltaTime;


        // Wrap around the screen
        WrapAround();


        // apply a random force to the asteroid to change its direction
        Vector2 force = Random.onUnitSphere * MaxSpeed / 10f;
        velocity += force * Time.deltaTime;

        // clamp the velocity to the maximum speed
        velocity = Vector2.ClampMagnitude(velocity, MaxSpeed);
    }

    void FixedUpdate()
    {
        // Freeze the z-axis
       // rb.position = new Vector3(rb.position.x, rb.position.y, 0f);
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.CompareTag("Asteroid"))
        {
            
            Object go =  Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(go, 2f);
            Destroy(this.gameObject);
        }

       

        Debug.Log("Collision of " + this.gameObject.name + " and " + collision.gameObject.name);
        // Get the other asteroid's rigidbody
        Rigidbody2D otherRb = collision.gameObject.GetComponent<Rigidbody2D>();

        // Calculate the direction from this asteroid to the other asteroid
        Vector2 repulsionDirection = otherRb.transform.position - rb.transform.position;

        // Apply the repulsion force in the opposite direction
        otherRb.AddForce(repulsionDirection.normalized * repulsionForce, ForceMode2D.Impulse);
        rb.AddForce(-repulsionDirection.normalized * repulsionForce, ForceMode2D.Impulse);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
            GameController.instance.GameOver();
            //Reset();
            this.gameObject.SetActive(false);
            Destroy(explosion, 3f);
        }
    }

    void WrapAround()
    {
        Vector3 position = transform.position;
        float halfWidth = screenWidth / 2f;
        float halfHeight = screenHeight / 2f;

        if (position.x < -halfWidth)
        {
            position.x = halfWidth;
        }
        else if (position.x > halfWidth)
        {
            position.x = -halfWidth;
        }

        if (position.y < -halfHeight)
        {
            position.y = halfHeight;
        }
        else if (position.y > halfHeight)
        {
            position.y = -halfHeight;
        }

        transform.position = position;
    }
}






