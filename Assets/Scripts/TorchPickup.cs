using UnityEngine;

public class TorchPickup : MonoBehaviour {

    public TorchType torchType;
    public GameObject heldTorchModel;

    private void OnTriggerEnter(Collider other) {

        if (!other.CompareTag("Player")) return;

        PuzzleController puzzle = FindObjectOfType<PuzzleController>();
        if (puzzle == null || !puzzle.CanPickupTorch(torchType)) return;

        PlayerTorch playerTorch = other.GetComponent<PlayerTorch>();
        if (playerTorch != null) {

            playerTorch.EquipUnlitTorch(torchType, heldTorchModel);
        }

        gameObject.SetActive(false);
    }
}