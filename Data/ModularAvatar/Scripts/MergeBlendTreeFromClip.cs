using UnityEngine;
using VRC.SDKBase;

namespace Nanochip.AvatarLimbScaling.ModularAvatar.Runtime
{
    [AddComponentMenu("Avatar Limb Scaling MA/Merge Blend Tree From Clip")]
    /// <summary>
    /// Convert clip to blendtree to implement VRCFury's toggle's slider function in this case.
    /// </summary>
    public class MergeBlendTreeFromClip : AvatarLimbScalingMAComponent
    {
        public AnimationClip Clip;
        public string ParameterName;
    }
}