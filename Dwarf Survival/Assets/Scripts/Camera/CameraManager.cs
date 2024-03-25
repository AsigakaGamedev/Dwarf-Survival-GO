using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    public void SetCameraTarget(Transform target)
    {
        virtualCamera.m_Follow = target;    
    }
}
