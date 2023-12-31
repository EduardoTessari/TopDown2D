using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// library for camera access.
using Cinemachine;

public class CameraController : Singleton <CameraController>
{
    private CinemachineVirtualCamera _cinemachineVirtualCamera;

    public void SetPlayerCameraFollow()
    {
        _cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _cinemachineVirtualCamera.Follow = PlayerControler.Instance.transform;
    }
}
