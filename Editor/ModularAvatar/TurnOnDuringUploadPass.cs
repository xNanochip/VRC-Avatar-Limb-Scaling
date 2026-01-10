using nadena.dev.ndmf;
using Nanochip.AvatarLimbScaling.ModularAvatar.Runtime;
using UnityEngine;

namespace Nanochip.AvatarLimbScaling.ModularAvatar.Editor
{
    public class TurnOnDuringUploadPass : Pass<TurnOnDuringUploadPass>
    {
        public override string DisplayName => "Turn On During Upload";
        public override string QualifiedName => "Nanochip.AvatarLimbScaling.ModularAvatar.Passes.TurnOnDuringUpload";

        protected override void Execute(BuildContext context)
        {
            AvatarLimbScalingMAPluginContext alsCtx = context.Extension<AvatarLimbScalingMAPluginContext>();
            if (!alsCtx.validateSuccess) return;

            // Turn on gameobjects
            foreach (TurnOnDuringUpload comp in alsCtx.avatarTransform.GetComponentsInChildren<TurnOnDuringUpload>(true))
            {
                foreach (Transform transform in comp.Transforms)
                {
                    if (!transform) continue;
                    transform.gameObject.SetActive(true);
                }
            }
        }
    }
}