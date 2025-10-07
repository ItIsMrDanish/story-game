using UnityEngine;
using System.Collections;

public class PuzzleController : MonoBehaviour {

    public Lantern[] lanterns;
    public TorchPickup[] torchPickups;
    public FireSource[] fireSources;

    public GameObject[] successActivateObjects;
    public GameObject[] successDeactivateObjects;
    public GameObject[] objectsToDeactivateAfter;

    public AudioClip successSound;
    public AudioClip victoryMusic;
    public AudioSource audioSource;

    public Renderer statueRenderer;

    public string emissionIntensityProperty = "_EmissionIntensity";

    public float startEmissionIntensity = -10f;
    public float endEmissionIntensity = 10f;
    public float emissionDuration = 13f;

    public Color baseEmissionColor = Color.white;

    private int currentStep = 0;

    private bool puzzleSolved = false;

    private void Start() {

        if (lanterns.Length != 3 || torchPickups.Length != 3) {

            enabled = false;
            return;
        }

        for (int i = 0; i < torchPickups.Length; i++) {

            if (torchPickups[i] != null) {

                torchPickups[i].gameObject.SetActive(i == 0);
            }
        }

        foreach (FireSource source in fireSources) {

            if (source != null) source.gameObject.SetActive(true);
        }
    }

    public bool CanPickupTorch(TorchType type) {

        if (puzzleSolved) return false;
        int index = (int)type;
        return index == currentStep && currentStep < 3;
    }

    public bool CanLightLantern(int index) {

        if (puzzleSolved) return false;
        return index == currentStep && currentStep < 3;
    }

    public void OnLanternLit(int index) {

        if (puzzleSolved || index != currentStep) return;

        currentStep++;

        if (currentStep < 3 && torchPickups[currentStep] != null) {

            torchPickups[currentStep].gameObject.SetActive(true);
        }

        if (currentStep >= 3) {

            SolvePuzzle();
        }
    }

    private void SolvePuzzle() {

        puzzleSolved = true;
        currentStep = 3;

        foreach (GameObject obj in successActivateObjects) {

            if (obj != null) obj.SetActive(true);
        }
        foreach (GameObject obj in successDeactivateObjects) {

            if (obj != null) obj.SetActive(false);
        }

        if (successSound != null && audioSource != null) {

            audioSource.PlayOneShot(successSound);
        }

        if (victoryMusic != null && audioSource != null) {

            audioSource.Stop();
            audioSource.clip = victoryMusic;
            audioSource.loop = true;
            audioSource.Play();
        }

        foreach (TorchPickup pickup in torchPickups) {

            if (pickup != null) pickup.gameObject.SetActive(false);
        }
        foreach (FireSource source in fireSources) {

            if (source != null) source.gameObject.SetActive(false);
        }
        StartCoroutine(EmissionAnimationAndDeactivate());
    }

    private IEnumerator EmissionAnimationAndDeactivate() {
        if (statueRenderer == null) {

            DeactivateObjects();
            yield break;
        }

        Material matInstance = statueRenderer.material;
        if (matInstance == null) {

            DeactivateObjects();
            yield break;
        }

        matInstance.EnableKeyword("_EMISSION");

        SetEmissionIntensity(matInstance, startEmissionIntensity);

        float elapsed = 0f;
        while (elapsed < emissionDuration) {

            elapsed += Time.deltaTime;
            float t = elapsed / emissionDuration;

            float currentIntensity = Mathf.Lerp(startEmissionIntensity, endEmissionIntensity, t);
            SetEmissionIntensity(matInstance, currentIntensity);

            yield return null;
        }
        SetEmissionIntensity(matInstance, endEmissionIntensity);

        DeactivateObjects();
    }

    private void SetEmissionIntensity(Material mat, float intensity) {

        if (mat.HasProperty(emissionIntensityProperty)) {

            mat.SetFloat(emissionIntensityProperty, intensity);
        } else {

            float multiplier = Mathf.Max(0f, (intensity + 10f) / 10f);
            Color emissionColor = baseEmissionColor * multiplier;
            mat.SetColor("_EmissionColor", emissionColor);
        }
    }

    private void DeactivateObjects() {

        foreach (GameObject obj in objectsToDeactivateAfter) {

            if (obj != null) {

                obj.SetActive(false);
            }
        }
    }

    private void OnLoadSolved() {

        foreach (GameObject obj in successActivateObjects) {

            if (obj != null) obj.SetActive(true);
        }
        foreach (GameObject obj in successDeactivateObjects) {

            if (obj != null) obj.SetActive(false);
        }

        if (victoryMusic != null && audioSource != null) {

            audioSource.Stop();
            audioSource.clip = victoryMusic;
            audioSource.loop = true;
            audioSource.Play();
        }

        foreach (TorchPickup pickup in torchPickups) {

            if (pickup != null) pickup.gameObject.SetActive(false);
        }
        foreach (FireSource source in fireSources) {

            if (source != null) source.gameObject.SetActive(false);
        }

        if (statueRenderer != null) {

            StartCoroutine(EmissionAnimationAndDeactivate());
        }
    }
}