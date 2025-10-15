using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MainCamera : MonoBehaviour
{
    private void Awake()
    {
        CameraManager.SetCamera(GetComponent<Camera>());
    }
}