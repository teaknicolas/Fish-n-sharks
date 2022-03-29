using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Shark : MonoBehaviour
{

    public Transform _player;
    private Rigidbody2D rb;



    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private float rotateSpeed = 200f;
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
        Vector2 direction = (Vector2)_player.position - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = rotateSpeed *  - rotateAmount;
        rb.velocity = transform.up * speed; 
    }
}
