using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        var newPos = target.transform.position;
        newPos.z = transform.position.z;
        transform.position = newPos;
    }
}
