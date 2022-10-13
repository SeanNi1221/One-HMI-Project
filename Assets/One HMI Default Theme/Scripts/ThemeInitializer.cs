using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
namespace Sean21.OneHMI.Theme.Default
{
    [ExecuteInEditMode]
    public partial class ThemeInitializer : MonoBehaviour
    {
        [Tooltip("Enable HDR overriding global settings.")]
        public bool overrideEnableHDR = true;
        [Tooltip("Set MSAA to 4x overriding global settings.")]
        public bool overrideMSAA4x = true;
        void Start() {
            SetGrapihcsRuntime();
        }
        partial void ed_OnEnable();
        void OnEnable() {
            ed_OnEnable();
        }
        /// <summary>
        /// Some URP Settings. Yet to find achievements with editor scripting, currently done at runtime.
        /// </summary>
        private void SetGrapihcsRuntime() {
            if(!Application.isPlaying) return;
            var pipeline = GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
            if(!pipeline) return;
            if(overrideEnableHDR) pipeline.supportsHDR = true;
            if(overrideMSAA4x) pipeline.msaaSampleCount = 4;
        }
        private void PrintCurrentPipelineSettings() {
            Debug.Log("Default render pipeline:" + GraphicsSettings.defaultRenderPipeline.name);
            Debug.Log("Current render pipeline:" + GraphicsSettings.currentRenderPipeline);
            Debug.Log("Current quality level render pipeline:" + QualitySettings.renderPipeline.name);
            Debug.Log("Current quality level:" + QualitySettings.GetQualityLevel());
        }
    }

}
