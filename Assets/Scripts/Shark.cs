using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{

    public GameObject _player;
    public Rigidbody2D _rigidbody2D;

    [SerializeField]
    private float powerOfShark = 1;
    private void Awake()
    {
        //_player = _player.GetComponent<GameObject>();
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

        Vector2 dir = _player.transform.position - this.transform.position;
        if(_player.tag == "Player")
        {
            _rigidbody2D.AddForce(dir * powerOfShark) ;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
