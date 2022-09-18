using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpsideDownDetector2 : MonoBehaviour
{
    public bool useNormalLighting = true;
    public bool canChangeLighting = false;
    public GameObject normalLight;
    public GameObject raveLight;
    public GameObject sunlight;
    public GameObject warningOverlay;
    public AudioSource jam;


    private void Start()
    {
        InvokeRepeating("changeColor", 1f, 0.1f);
    }


    void changeColor()
    {
        if (!useNormalLighting)
        {
            if (Random.Range(0, 100) < 90)
                raveLight.GetComponent<Light>().enabled = true;
            else
                raveLight.GetComponent<Light>().enabled = false;
        }
    }


    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        float waitTime = 5f;

        while (elapsedTime < waitTime)
        {
            warningOverlay.transform.localScale = Vector3.Lerp(warningOverlay.transform.localScale, Vector3.one, elapsedTime / waitTime);
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
            warningOverlay.transform.localScale = Vector3.Lerp(warningOverlay.transform.localScale, new Vector3(1.2f, 1.2f, 1.2f), elapsedTime / waitTime);
            jam.volume = Mathf.Lerp(jam.volume, 0f, elapsedTime / waitTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        jam.Pause();
        yield return null;
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
