using UnityEngine;
using System.Collections;

public class FlickeringFlashlight : MonoBehaviour {

    public Light flashlightLight;

    public float minTimeBetweenFlickers = 2f;

    public float maxTimeBetweenFlickers = 10f;

    public float flickerDuration = 0.2f;

    private bool isFlickering = false;

    private void Start() {

        if (flashlightLight != null) {

            flashlightLight.enabled = true;
        }

        StartCoroutine(FlickerRoutine());
    }

    private IEnumerator FlickerRoutine() {

        while (true)
        {
            float waitTime = Random.Range(minTimeBetweenFlickers, maxTimeBetweenFlickers);
            yield return new WaitForSeconds(waitTime);

            if (!isFlickering && flashlightLight != null) {

                isFlickering = true;

                flashlightLight.enabled = false;

                yield return new WaitForSeconds(flickerDuration);

                flashlightLight.enabled = true;

                isFlickering = false;
            }
        }
    }

    public void StopFlickering() {

        StopCoroutine(FlickerRoutine());

        if (flashlightLight != null) {

            flashlightLight.enabled = true;
        }
    }
}