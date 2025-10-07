using UnityEngine;

public class Lantern : MonoBehaviour {

    public TorchType requiredTorchType;
    public int lanternIndex;
    public GameObject lanternLight;

    public AudioClip successSound;
    public AudioClip failureSound;
    public AudioSource audioSource;

    private bool isLit = false;

    private void Start() {

        if (lanternLight != null) lanternLight.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {

        if (!other.CompareTag("Player")) return;

        PlayerTorch playerTorch = other.GetComponent<PlayerTorch>();
        if (playerTorch == null || !playerTorch.HasTorch || !playerTorch.IsTorchLit) return;

        TryLight(playerTorch);
    }

    public void TryLight(PlayerTorch playerTorch) {

        TorchType playerTorchType = playerTorch.CurrentTorchType;

        if (isLit || playerTorchType != requiredTorchType) {

            if (failureSound != null && audioSource != null) {

                audioSource.PlayOneShot(failureSound);
            }
            return;
        }

        PuzzleController puzzle = FindObjectOfType<PuzzleController>();
        if (puzzle != null && !puzzle.CanLightLantern(lanternIndex)) {

            if (failureSound != null && audioSource != null) {

                audioSource.PlayOneShot(failureSound);
            }
            return;
        }

        LightLantern();
        isLit = true;
        playerTorch.DequipTorch();

        if (successSound != null && audioSource != null) {

            audioSource.PlayOneShot(successSound);
        }

        if (puzzle != null) {

            puzzle.OnLanternLit(lanternIndex);
        }
    }

    private void LightLantern() {

        if (lanternLight != null) lanternLight.SetActive(true);
    }
}
