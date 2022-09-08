using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpsideDownDetector : MonoBehaviour
{
    public bool useNormalLighting = true;
    public bool canChangeLighting = false;
    public GameObject normalLight;
    public GameObject raveLight;
    public GameObject sunlight;


    private void Start()
    {
        InvokeRepeating("changeColor", 1f, 1f);
    }


    void changeColor()
    {
        raveLight.GetComponent<Light>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }


    void FixedUpdate()
    {
        if (!canChangeLighting && Vector3.Dot(transform.up, Vector3.down) > 0.75) // cube is upside down
            canChangeLighting = true;


        if (canChangeLighting && Vector3.Dot(transform.up, Vector3.up) > 0.75) // cube is right side up
        {
            canChangeLighting = false;                   
            useNormalLighting = !useNormalLighting;

            normalLight.GetComponent<Light>().enabled = useNormalLighting;
            raveLight.GetComponent<Light>().enabled = !useNormalLighting;

            if (useNormalLighting)
                sunlight.GetComponent<Light>().intensity = 0.5f;
            else
                sunlight.GetComponent<Light>().intensity = 0f;
        }
    }
}
