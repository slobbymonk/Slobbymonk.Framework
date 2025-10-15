using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    public static Camera MainCamera;

    public static void SetCamera(Camera camera)
    {
        if(camera == null)
        {
            Debug.LogError("Camera cannot be null.");
            return;
        }
        MainCamera = camera;
    }
}
