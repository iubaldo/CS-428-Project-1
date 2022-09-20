using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpsideDownDetector : MonoBehaviour
{
    public bool useNormalLighting = true;
    public bool canChangeLighting = false;
    public bool tracking = false;
    public GameObject normalLight;
    public GameObject raveLight;
    public GameObject sunlight;
    public AudioSource jam;


    private void Start()
    {
        InvokeRepeating("changeColor", 1f, 1f);
    }


    public void setTracking(bool status) { tracking = status; }


    void changeColor()
    {
        raveLight.GetComponent<Light>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }


    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        float waitTime = 8f;

        while (elapsedTime < waitTime)
        {
            jam.volume = Mathf.Lerp(jam.volume, 1f, elapsedTime / waitTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        yield return null;
    }


    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        float waitTime = 5f;

        while (elapsedTime < waitTime)
        {
            jam.volume = Mathf.Lerp(jam.volume, 0f, elapsedTime / waitTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        jam.Pause();
        yield return null;
    }


    void FixedUpdate()
    {
        if (!canChangeLighting && Vector3.Dot(transform.up, Vector3.down) > 0.75 && tracking) // cube is upside down
            canChangeLighting = true;


        if (canChangeLighting && Vector3.Dot(transform.up, Vector3.up) > 0.75 && tracking) // cube is right side up
        {
            canChangeLighting = false;                   
            useNormalLighting = !useNormalLighting;

            normalLight.GetComponent<Light>().enabled = useNormalLighting;
            raveLight.GetComponent<Light>().enabled = !useNormalLighting;

            if (useNormalLighting)
            {
                sunlight.GetComponent<Light>().intensity = 0.5f;
                StartCoroutine(FadeOut());
            }
                
            else
            {
                sunlight.GetComponent<Light>().intensity = 0f;
                jam.Play();
                StartCoroutine(FadeIn());
            }
                
        }
    }
}
