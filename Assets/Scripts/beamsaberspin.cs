using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beamsaberspin : MonoBehaviour
{
    public GameObject target;
    public float spinSpeed;

    void FixedUpdate()
    {
        target.transform.rotation *= Quaternion.Euler(Vector3.forward * spinSpeed * Time.deltaTime);
        Debug.Log("changing rotation");
    }
}
