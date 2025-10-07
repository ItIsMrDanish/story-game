using UnityEngine;

public class FireSource : MonoBehaviour {

    public AudioClip igniteSound;
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other) {

        if (!other.CompareTag("Player")) return;

        PlayerTorch playerTorch = other.GetComponent<PlayerTorch>();
        if (playerTorch == null || !playerTorch.HasTorch || playerTorch.IsTorchLit) return;

        playerTorch.IgniteTorch();

        if (igniteSound != null && audioSource != null) {

            audioSource.PlayOneShot(igniteSound);
        }
    }
}