using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour, IInitListener, IDeinitListener
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    public static CameraManager Instance;

    public void OnInitialize()
    {
        Instance = this;
    }

    public void OnDeinitialize()
    {
        Instance = null;
    }

    public void SetCameraTarget(Transform target)
    {
        virtualCamera.m_Follow = target;    
    }
}
