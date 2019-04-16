using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public static class KeyPoseExtension
{
    public static bool IsFist(this MLHandKeyPose keyPose) => keyPose.ToString() == "Fist";

    public static bool IsOpenHandBack(this MLHandKeyPose keyPose) => keyPose.ToString() == "OpenHandBack";

}
