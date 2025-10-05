using UnityEngine;

public class LookAtPLayer : MonoBehaviour {

    public Transform playerTarget;

    public bool yAxisOnly = true;

    [Range(0.1f, 5f)]
    public float rotationSpeed = 1f;

    void Start() {

        if (playerTarget == null) {

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) {

                playerTarget = player.transform;
            } else {

                enabled = false;
                return;
            }
        }

        void Update() {

            if (playerTarget == null) return;

            Vector3 directionToPlayer = playerTarget.position - transform.position;

            if (yAxisOnly) {

                directionToPlayer.y = 0f;

                if (directionToPlayer != Vector3.zero) {
                    Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
            } else {

                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}