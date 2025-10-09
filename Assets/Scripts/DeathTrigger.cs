using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {

        if (!other.CompareTag("Player")) return;

        SceneManager.LoadScene("MainMenu");
    }
}
