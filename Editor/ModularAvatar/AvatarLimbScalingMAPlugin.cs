using UnityEngine;
using nadena.dev.ndmf;
using nadena.dev.ndmf.fluent;
using VRC.SDK3.Dynamics.Constraint.Components;
using Nanochip.AvatarLimbScaling.ModularAvatar.Runtime;

[assembly: ExportsPlugin(typeof(Nanochip.AvatarLimbScaling.ModularAvatar.Editor.AvatarLimbScalingMAPlugin))]
namespace Nanochip.AvatarLimbScaling.ModularAvatar.Editor
{
    /// <summary>
    /// The plugin context, also validates if the avatar is humanoid
    /// </summary>
    public class AvatarLimbScalingMAPluginContext : IExtensionContext
    {
        internal bool activated = false;
        internal bool validateSuccess = false;
        internal Transform avatarTransform;
        internal Animator avatarAnimator;

        public void OnActivate(BuildContext context)
        {
            if (activated) return;
            activated = true;
            // Validate the avatar, check if the avatar is humanoid
            avatarTransform = context.AvatarRootTransform;
            avatarAnimator = avatarTransform.GetComponent<Animator>();
            validateSuccess = avatarAnimator && avatarAnimator.avatar && avatarAnimator.avatar.isHuman;
        }

        public void OnDeactivate(BuildContext context)
        {
            avatarTransform = null;
            avatarAnimator = null;
            validateSuccess = false;
            activated = false;
        }
    }

    /// <summary>
    /// The Avatar Limb Scaling NDMF plugin defination
    /// </summary>
    public class AvatarLimbScalingMAPlugin : Plugin<AvatarLimbScalingMAPlugin>
    {
        public override string DisplayName => "Avatar Limb Scaling Modular Avatar Plugin";
        public override string QualifiedName => "Nanochip.AvatarLimbScaling.ModularAvatar";
        public override Color? ThemeColor => new Color(0xcd / 255f, 0xd0 / 255f, 0xd1 / 255f, 1);

        protected override void Configure()
        {
            Sequence seq = InPhase(BuildPhase.Generating);

            seq.WithRequiredExtension(
                typeof(AvatarLimbScalingMAPluginContext),
                (_seq) =>
                {
                    // Convert given clip and parameter name to a blendtree and
                    // add MA Merge Motion with the blendtree onto the gameobject.
                    _seq.Run(GenerateMAMergeMotionPass.Instance);
                }
            );

            seq = InPhase(BuildPhase.Transforming);

            seq.WithRequiredExtension(
                typeof(AvatarLimbScalingMAPluginContext),
                (_seq) =>
                {
                    // Retarget scale constraint sources
                    _seq.Run(ScaleConstraintRetargetPass.Instance);
                    // Turn on game objects during upload
                    _seq.Run(TurnOnDuringUploadPass.Instance);
                }
            );
            seq.Run(
                "Purge Avatar Limb Scaling Components",
                (ctx) =>
                {
                    foreach (var component in ctx.AvatarRootTransform.GetComponentsInChildren<AvatarLimbScalingMAComponent>())
                    {
                        Object.DestroyImmediate(component);
                    }
                }
            );
        }
    }
}