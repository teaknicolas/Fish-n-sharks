using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public float horizontal_speed = 0.2f;

    public float vertical_speed = 0.2f;

    private Renderer rend;

    private ScrollBackground instance;

    public Renderer Rend { get => rend; set => rend = value; }

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Rend = GetComponent<Renderer>();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
