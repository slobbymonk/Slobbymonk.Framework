using UnityEngine;
using UnityEngine.Events;
using System;

public class VisionCone : MonoBehaviour
{
    [Header("Vision Settings")]
    [Range(0f, 180f)] public float horizontalFOV = 90f;
    [Range(0f, 180f)] public float verticalFOV = 60f;  
    public float viewDistance = 10f;                   
    public LayerMask detectionMask;                    

    [Header("Callbacks")]
    public UnityEvent<GameObject> onTargetSeen;
    public UnityEvent<GameObject> onTargetLost;
    public Action<GameObject> OnTargetSeenAction;
    public Action<GameObject> OnTargetLostAction;

    [Header("Debug")]
    public Transform target;
    public bool canSeeTarget;

    private bool lastSeenState;

    void Update()
    {
        if (target != null)
        {
            bool isSeen = IsInSight(target);

            if (isSeen && !lastSeenState)
            {
                onTargetSeen?.Invoke(target.gameObject);
                OnTargetSeenAction?.Invoke(target.gameObject);
            }
            else if (!isSeen && lastSeenState)
            {
                onTargetLost?.Invoke(target.gameObject);
                OnTargetLostAction?.Invoke(target.gameObject);
            }

            canSeeTarget = isSeen;
            lastSeenState = isSeen;
        }
    }

    /// <summary>
    /// Checks if a given target is inside the vision cone and visible.
    /// </summary>
    public bool IsInSight(Transform target)
    {
        Vector3 directionToTarget = target.position - transform.position;
        float distance = directionToTarget.magnitude;

        if (distance > viewDistance)
            return false;

        Vector3 localDir = transform.InverseTransformDirection(directionToTarget.normalized);

        float horizontalAngle = Mathf.Atan2(localDir.x, localDir.z) * Mathf.Rad2Deg;
        if (Mathf.Abs(horizontalAngle) > horizontalFOV * 0.5f)
            return false;

        float verticalAngle = Mathf.Atan2(localDir.y, localDir.z) * Mathf.Rad2Deg;
        if (Mathf.Abs(verticalAngle) > verticalFOV * 0.5f)
            return false;

        if (Physics.Raycast(transform.position, directionToTarget.normalized, out RaycastHit hit, viewDistance, detectionMask))
        {
            if (hit.transform == target)
                return true;
        }

        return false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Vector3 forward = transform.forward;
        Vector3 up = transform.up;
        Vector3 right = transform.right;

        Quaternion leftRot = Quaternion.AngleAxis(-horizontalFOV * 0.5f, up);
        Quaternion rightRot = Quaternion.AngleAxis(horizontalFOV * 0.5f, up);
        Quaternion upRot = Quaternion.AngleAxis(-verticalFOV * 0.5f, right);
        Quaternion downRot = Quaternion.AngleAxis(verticalFOV * 0.5f, right);

        Vector3 leftDir = leftRot * forward;
        Vector3 rightDir = rightRot * forward;
        Vector3 upDir = upRot * forward;
        Vector3 downDir = downRot * forward;

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + leftDir * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + rightDir * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + upDir * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + downDir * viewDistance);

        if (target != null && canSeeTarget)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}