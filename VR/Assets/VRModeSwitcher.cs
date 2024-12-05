using UnityEngine;
using UnityEngine.XR;

public class VRModeSwitcher : MonoBehaviour
{
    void Start()
    {
        // Detecta si hay un visor conectado
        if (!XRSettings.isDeviceActive)
        {
            Debug.Log("No VR headset detected. Switching to desktop mode.");
            XRSettings.enabled = false;
        }
        else
        {
            Debug.Log("VR headset detected. Switching to VR mode.");
            XRSettings.enabled = true;
        }
    }
}
