using UnityEngine;
using VRC.SDKBase;

namespace Nanochip.AvatarLimbScaling.ModularAvatar.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Limb Scaling MA/SC Retarget")]
    /// <summary>
    /// Replacement for VRCFury "SC Target".
    /// </summary>
    public class ScaleConstraintRetarget : AvatarLimbScalingMAComponent
    {
        public HumanBodyBones TargetBone;
    }
}