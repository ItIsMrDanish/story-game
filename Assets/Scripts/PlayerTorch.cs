using UnityEngine;

public class PlayerTorch : MonoBehaviour {

    private TorchType currentTorchType = TorchType.Yellow;
    private bool hasTorch = false;
    private bool isLit = false;
    private GameObject currentHeldModel;

    public string fireEffectChildName = "FireEffect";

    public void EquipUnlitTorch(TorchType type, GameObject baseModel) {

        DequipTorch();

        currentTorchType = type;
        currentHeldModel = baseModel;
        if (currentHeldModel != null) {

            currentHeldModel.SetActive(true);
            isLit = false;
            DeactivateFireEffect();
        }
        hasTorch = true;
    }

    public void IgniteTorch() {

        if (!hasTorch || isLit) return;

        if (currentHeldModel != null) {

            Transform fireChild = currentHeldModel.transform.Find(fireEffectChildName);
            if (fireChild != null) {
                fireChild.gameObject.SetActive(true);
                ParticleSystem fireParticles = fireChild.GetComponent<ParticleSystem>();
                if (fireParticles != null && !fireParticles.isPlaying) {

                    fireParticles.Play();
                }
                Light torchLight = fireChild.GetComponent<Light>();
                if (torchLight != null) {

                    torchLight.enabled = true;
                }
            }
        }
        isLit = true;

        Debug.Log($"Ignited {currentTorchType} torch!");
    }

    public void DequipTorch() {
        if (currentHeldModel != null) {

            if (isLit) {

                DeactivateFireEffect();
            }
            currentHeldModel.SetActive(false);
            currentHeldModel = null;
        }
        hasTorch = false;
        isLit = false;
        currentTorchType = TorchType.Yellow;
    }

    private void DeactivateFireEffect() {

        if (currentHeldModel != null) {

            Transform fireChild = currentHeldModel.transform.Find(fireEffectChildName);
            if (fireChild != null) {
                fireChild.gameObject.SetActive(false);
                ParticleSystem fireParticles = fireChild.GetComponent<ParticleSystem>();
                if (fireParticles != null) {

                    fireParticles.Stop();
                }

                Light torchLight = fireChild.GetComponent<Light>();
                if (torchLight != null) {

                    torchLight.enabled = false;
                }
            }
        }
    }
    public bool HasTorch { get { return hasTorch; } }
    public bool IsTorchLit { get { return isLit; } }
    public TorchType CurrentTorchType { get { return currentTorchType; } }
}
