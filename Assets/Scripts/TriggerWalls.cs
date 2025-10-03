using UnityEngine;

public class TriggerWalls : MonoBehaviour {

    public GameObject[] objectsToActivate = new GameObject[1];
    public GameObject[] objectsToDeactivate = new GameObject[1];

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
    }
}