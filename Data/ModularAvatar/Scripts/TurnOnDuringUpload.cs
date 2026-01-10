using UnityEngine;
using VRC.SDKBase;

namespace Nanochip.AvatarLimbScaling.ModularAvatar.Runtime
{
    [AddComponentMenu("Avatar Limb Scaling MA/Turn On During Upload")]
    /// <summary>
    /// Partial implementation of VRCFury "Apply During Upload" (turn-on only).
    /// </summary>
    public class TurnOnDuringUpload : AvatarLimbScalingMAComponent
    {
        public Transform[] Transforms;
    }
}
