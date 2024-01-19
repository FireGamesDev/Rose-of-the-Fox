using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class CheckMobile : MonoBehaviour
{
    public bool IsMobile = true;
    public static CheckMobile Instance;

    private void Awake()
    {
        Instance = this;
    }

    [DllImport("__Internal")]
    private static extern bool isMobileWebGL();

    public bool CheckIfWebGLIsMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        return isMobileWebGL();
#endif
        return IsMobile;
    }
}
