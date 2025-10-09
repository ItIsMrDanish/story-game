using UnityEngine;

public class FireSource : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {

        if (!other.CompareTag("Player")) return;

        PlayerTorch playerTorch = other.GetComponent<PlayerTorch>();
        if (playerTorch == null || !playerTorch.HasTorch || playerTorch.IsTorchLit) return;

        playerTorch.IgniteTorch();
    }
}