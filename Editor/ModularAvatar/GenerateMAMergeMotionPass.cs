using System.Linq;
using nadena.dev.modular_avatar.core;
using nadena.dev.ndmf;
using Nanochip.AvatarLimbScaling.ModularAvatar.Runtime;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Nanochip.AvatarLimbScaling.ModularAvatar.Editor
{
    public class GenerateMAMergeMotionPass : Pass<GenerateMAMergeMotionPass>
    {
        public override string DisplayName => "Generate MA Merge Motion";
        public override string QualifiedName => "Nanochip.AvatarLimbScaling.ModularAvatar.Passes.GenerateMAMergeMotion";

        protected override void Execute(BuildContext context)
        {
            AvatarLimbScalingMAPluginContext alsCtx = context.Extension<AvatarLimbScalingMAPluginContext>();
            if (!alsCtx.validateSuccess) return;

            foreach (MergeBlendTreeFromClip comp in alsCtx.avatarTransform.GetComponentsInChildren<MergeBlendTreeFromClip>(true))
            {
                if (!comp.Clip || string.IsNullOrWhiteSpace(comp.ParameterName)) continue;

                // Generate clips by the first keyframe and the last keyframe.
                var (clip0, clip1) = GenerateClip01(context, comp.Clip);

                // Generate a blendtree with given parameter name and generated clips,
                // and save it into generated assets.
                BlendTree blendTree = new()
                {
                    blendParameter = comp.ParameterName,
                    blendType = BlendTreeType.Simple1D,
                    name = $"{comp.Clip.name}_BlendTree",
                    children = new ChildMotion[]
                    {
                        new()
                        {
                            motion = clip0,
                            threshold = 0,
                            timeScale = 1,
                        },
                        new ()
                        {
                            motion = clip1,
                            threshold = 1,
                            timeScale = 1,
                        }
                    },
                    useAutomaticThresholds = true
                };
                context.AssetSaver.SaveAsset(blendTree);

                // Add a MA Merge Motion component on the same gameobject,
                // and append the generated blend tree to it
                ModularAvatarMergeBlendTree mergeBlendTree = comp.gameObject.AddComponent<ModularAvatarMergeBlendTree>();
                mergeBlendTree.Motion = blendTree;
                mergeBlendTree.PathMode = MergeAnimatorPathMode.Relative;
            }
        }

        /// <summary>
        /// Generate a animation clip and save it into generated assets
        /// </summary>
        /// <param name="context"></param>
        /// <param name="name"></param>
        /// <param name="bindings"></param>
        /// <param name="curves"></param>
        /// <returns></returns>
        private AnimationClip GenerateAnimationClip(BuildContext context, string name, EditorCurveBinding[] bindings, AnimationCurve[] curves)
        {
            AnimationClip clip = new()
            {
                name = name
            };
            AnimationUtility.SetEditorCurves(clip, bindings, curves);
            context.AssetSaver.SaveAsset(clip);
            return clip;
        }

        /// <summary>
        /// Generate animation clips from first and last frames,
        /// 0 stands for parameter value 0, so does 1
        /// /// </summary>
        /// <param name="context"></param>
        /// <param name="originalClip"></param>
        /// <returns></returns>
        private (AnimationClip, AnimationClip) GenerateClip01(BuildContext context, AnimationClip originalClip)
        {
            EditorCurveBinding[] bindings = AnimationUtility.GetCurveBindings(originalClip);
            AnimationCurve[] originalCurves = bindings.Select(
                binding => AnimationUtility.GetEditorCurve(originalClip, binding)
            ).ToArray();

            // Generate clip0 with the first keyframe from the original clip
            AnimationCurve[] curves = originalCurves.Select(
                originalCurve =>
                {
                    AnimationCurve curve = new();
                    curve.AddKey(originalCurve.keys[0]);
                    return curve;
                }
            ).ToArray();

            AnimationClip clip0 = GenerateAnimationClip(context, $"{originalClip.name}_0", bindings, curves);

            // Generate clip1 with the last keyframe from the original clip
            curves = originalCurves.Select(
                originalCurve =>
                {
                    AnimationCurve curve = new();
                    curve.AddKey(originalCurve.keys[originalCurve.length - 1]);
                    return curve;
                }
            ).ToArray();

            AnimationClip clip1 = GenerateAnimationClip(context, $"{originalClip.name}_1", bindings, curves);

            return (clip0, clip1);
        }
    }
}