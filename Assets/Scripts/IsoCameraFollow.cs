using UnityEngine;

/// <summary>
/// This component is used to make the camera follow the player simulating an isometric view
/// </summary>
public class IsometricCameraTracker : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        transform.position = desiredPosition; 

        transform.LookAt(target.position);
    }
}