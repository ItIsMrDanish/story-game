using UnityEngine;

public class EnemyPath : MonoBehaviour {

    public Transform[] waypoints;

    public float moveSpeed = 2f;
    public float waypointThreshold = 0.1f;

    public bool loopPath = true;

    [SerializeField] private int currentWaypointIndex = 0;

    private void Start() {

        if (waypoints == null || waypoints.Length == 0) {

            enabled = false;
            return;
        }

    }
    private void Update() {

        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];

        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWaypoint.position) <= waypointThreshold) {

            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Length) {

                if (loopPath) {

                    currentWaypointIndex = 0;
                } else {

                    currentWaypointIndex--;
                    enabled = false;
                    return;
                }
            }
        }
    }
}