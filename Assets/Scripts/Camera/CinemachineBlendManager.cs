using Cinemachine;
using UnityEngine;

public class CinemachineBlendManager
{
    private static CinemachineBlendDefinition? nextBlend;

    static CinemachineBlendManager()
    {
        CinemachineCore.GetBlendOverride = GetBlendOverrideDelegate;
    }

    public static void ClearNextBlend()
    {
        nextBlend = null;
    }

    public static void SetNextBlend(CinemachineBlendDefinition blend)
    {
        nextBlend = blend;
    }

    public static CinemachineBlendDefinition GetBlendOverrideDelegate(ICinemachineCamera fromVcam, ICinemachineCamera toVcam, CinemachineBlendDefinition defaultBlend, MonoBehaviour owner)
    {
        if (nextBlend.HasValue)
        {
            var blend = nextBlend.Value;
            nextBlend = null;
            return blend;
        }
        return defaultBlend;
    }
}
