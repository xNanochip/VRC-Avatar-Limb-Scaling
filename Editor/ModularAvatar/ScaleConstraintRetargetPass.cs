using nadena.dev.ndmf;
using Nanochip.AvatarLimbScaling.ModularAvatar.Runtime;
using UnityEngine;
using VRC.SDK3.Dynamics.Constraint.Components;

namespace Nanochip.AvatarLimbScaling.ModularAvatar.Editor
{
    public class ScaleConstraintRetargetPass : Pass<ScaleConstraintRetargetPass>
    {
        public override string DisplayName => "Scale Constraint Retarget Pass";
        public override string QualifiedName => "Nanochip.AvatarLimbScaling.ModularAvatar.Passes.ScaleConstraintRetarget";

        protected override void Execute(BuildContext context)
        {
            AvatarLimbScalingMAPluginContext alsCtx = context.Extension<AvatarLimbScalingMAPluginContext>();
            if (!alsCtx.validateSuccess) return;

            // Retarget the scale constraint source on the same gameobject to the given bone
            foreach (ScaleConstraintRetarget comp in alsCtx.avatarTransform.GetComponentsInChildren<ScaleConstraintRetarget>(true))
            {
                HumanBodyBones bone = comp.TargetBone;
                Transform boneTransform = alsCtx.avatarAnimator.GetBoneTransform(bone);

                if (boneTransform == null) continue;

                VRCScaleConstraint[] constraints = comp.GetComponents<VRCScaleConstraint>();
                foreach (VRCScaleConstraint constraint in constraints)
                {
                    constraint.TargetTransform = boneTransform;
                }
            }
        }
    }
}
