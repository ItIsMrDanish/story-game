using UnityEngine;

public class TriggerWalls : MonoBehaviour {

    public GameObject[] objectsToActivate = new GameObject[1];
    public GameObject[] objectsToDeactivate = new GameObject[1];
    public AudioClip triggerSound;

    public float soundVolume = 0.8f;

    public AudioSource audioSource;

    public bool loopSound = false;

    private void OnTriggerEnter(Collider other) {

        if (!other.CompareTag("Player")) return;
        for (int i = 0; i < objectsToActivate.Length; i++) {

            if (objectsToActivate[i] != null) {
                objectsToActivate[i].SetActive(true);
            }
        }

        for (int i = 0; i < objectsToDeactivate.Length; i++) {

            if (objectsToDeactivate[i] != null) {

                objectsToDeactivate[i].SetActive(false);
            }
        }

        if (triggerSound != null && audioSource != null) {

            audioSource.volume = soundVolume;

            if (loopSound) {

                audioSource.clip = triggerSound;
                audioSource.loop = true;
                audioSource.Play();
            } else {

                audioSource.PlayOneShot(triggerSound, soundVolume);
            }
        }
    }
}