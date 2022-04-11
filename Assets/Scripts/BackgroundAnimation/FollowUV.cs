using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class FollowUV : MonoBehaviour
{

    [SerializeField] private float parralax = 3f;
    // Update is called once per frame
    void Update()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();

        // personal copy of the shared material
        Material mat = mr.material;

        Vector2 offset = mat.mainTextureOffset;

        offset.x =  transform.position.x / transform.localScale.x / parralax;
        offset.y =  transform.position.y / transform.localScale.y / parralax;
        mat.mainTextureOffset = offset;
    }
}