using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beamsaberspin : MonoBehaviour
{
    public float spinSpeed;

    void FixedUpdate()
    {
        transform.rotation *= Quaternion.Euler(Vector3.forward * spinSpeed * Time.deltaTime);
    }
}
