using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Asteroid : MonoBehaviour
{
    public Transform _player;
    private Rigidbody2D rb;

    public float MaxSpeed = 10f;

    [SerializeField]
    private float mass;
    [SerializeField]
    private Vector3 velocity;

    public float MinMass = 1f;
    public float MaxMass = 10f;

    public float MinSize = 1f;
    public float MaxSize = 10f;

    public float MinDensity = 1f;
    public float MaxDensity = 10f;

    public float screenHeight = 200f;
    public float screenWidth = 200f;
    private BoxCollider2D worldBounds; // reference to the world bounds collider

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
        this.transform.position += velocity * Time.deltaTime;


        // Wrap around the screen
        WrapAround();


        // apply a random force to the asteroid to change its direction
        Vector3 force = Random.onUnitSphere * MaxSpeed / 10f;
        velocity += force * Time.deltaTime;

        // clamp the velocity to the maximum speed
        velocity = Vector3.ClampMagnitude(velocity, MaxSpeed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Get the other asteroid's mass and velocity
        Asteroid otherAsteroid = collision.gameObject.GetComponent<Asteroid>();
        if (otherAsteroid != null)
        {
            float otherMass = otherAsteroid.mass;
            Vector3 otherVelocity = otherAsteroid.velocity;

            float totalMass = mass + otherMass;
            Vector3 totalMomentum = mass * velocity + otherMass * otherVelocity;

            // Calculate the velocity of the system after the collision
            Vector3 newVelocity = totalMomentum / totalMass;

            // Calculate a repulsive force
            Vector3 direction = transform.position - otherAsteroid.transform.position;
            float distance = direction.magnitude;
            float forceMagnitude = Mathf.Max(0f, (1f / distance) - (1f / (MaxSize * 2f)));
            Vector3 repulsiveForce = direction.normalized * forceMagnitude;

            // Update the velocities of the asteroids
            velocity = newVelocity + repulsiveForce;
            otherAsteroid.velocity = newVelocity - repulsiveForce;

            // Randomize the new direction of the asteroids
            velocity = Random.insideUnitSphere * MaxSpeed;
            otherAsteroid.velocity = Random.insideUnitSphere * MaxSpeed;

            // Apply different directions of repulsive force based on the relative positions of the colliding asteroids
            if (direction.x > 0f && direction.y > 0f)
            {
                // Top right collision
                velocity = newVelocity + new Vector3(repulsiveForce.x, -repulsiveForce.y, 0f);
                otherAsteroid.velocity = newVelocity - new Vector3(-repulsiveForce.x, repulsiveForce.y, 0f);
            }
            else if (direction.x < 0f && direction.y > 0f)
            {
                // Top left collision
                velocity = newVelocity + new Vector3(-repulsiveForce.x, -repulsiveForce.y, 0f);
                otherAsteroid.velocity = newVelocity - new Vector3(repulsiveForce.x, repulsiveForce.y, 0f);
            }
            else if (direction.x < 0f && direction.y < 0f)
            {
                // Bottom left collision
                velocity = newVelocity + new Vector3(-repulsiveForce.x, repulsiveForce.y, 0f);
                otherAsteroid.velocity = newVelocity - new Vector3(repulsiveForce.x, -repulsiveForce.y, 0f);
            }
            else if (direction.x > 0f && direction.y < 0f)
            {
                // Bottom right collision
                velocity = newVelocity + new Vector3(repulsiveForce.x, repulsiveForce.y, 0f);
                otherAsteroid.velocity = newVelocity - new Vector3(-repulsiveForce.x, -repulsiveForce.y, 0f);
            }
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






