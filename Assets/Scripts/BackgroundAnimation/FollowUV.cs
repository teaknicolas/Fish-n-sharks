using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class FollowUV : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();

        // personal copy of the shared material
        Material mat = mr.material;

        Vector2 offset = mat.mainTextureOffset;

        offset.x =  transform.position.x / transform.localScale.x;
        offset.y =  transform.position.y / transform.localScale.y;
        mat.mainTextureOffset = offset;
    }
}