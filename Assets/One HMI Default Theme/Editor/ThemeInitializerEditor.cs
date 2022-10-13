#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using TMPro;
using System.Reflection;
namespace Sean21.OneHMI.Theme.Default
{
    using static Sean21.OneHMI.EditorGenerics;
    [CustomEditor(typeof(ThemeInitializer))]
    public class ThemeInitializerEditor : OneHMIEditor
    {
        ThemeInitializer initializer;
        void OnEnable()
        {
            initializer = target as ThemeInitializer;
            Modify("fontAsset").Disable(() => !initializer.setupTMP).Indent();
        }
    }
    public partial class ThemeInitializer
    {
        (string url, string displayName) tmp = ("com.unity.textmeshpro", "TextMeshPro");
        (string url, string displayName) urp = ("com.unity.render-pipelines.universal", "Universal Render Pipeline 12.0.0 or later");
        public bool checkDependency = true;
        public bool checkXChartsCompatibility = true;
        public bool prebakeColliders = true;
        public bool setupTMP = true;
        public TMP_FontAsset fontAsset;
        private const string fontAssetPath = "Assets/One HMI Default Theme/Fonts/FangZhengHei SDF.asset";
        partial void ed_OnEnable()
        {
            //Missing XCharts Template TMP Font
            // Debug.Log("TMP Template TMP_Font Path:" + AssetDatabase.GUIDToAssetPath("ef7b1281966e3c143be5c8b7c175e8ff"));
            //Existing "FangZhengHei SDF.asset" in repository
            // Debug.Log("FangZhengHei SDF.asset path:" + AssetDatabase.GUIDToAssetPath("3b9107e219405264b9974c7ab87c3da8"));
            if (Application.isPlaying) return;
            if (checkDependency)
            {
#if USE_TMP
                DependencyManagerWindow.installed[tmp.url] = tmp.displayName;
#else
                DependencyManagerWindow.absent[tmp.url] = tmp.displayName;
#endif

#if USE_UNIVERSAL_RP
                DependencyManagerWindow.installed[urp.url] = urp.displayName;
#else
                DependencyManagerWindow.absent[urp.url] = urp.displayName;
#endif
                DependencyManagerWindow.CheckDependency();
            }
            if (prebakeColliders) PreBakeColliders();
            if (setupTMP) SetupTMP();

#if !ENABLE_LEGACY_INPUT_MANAGER
            if (checkXChartsCompatibility) CheckXChartsCompatibility();
#endif
        }
        void CheckXChartsCompatibility()
        {
            if (EditorUtility.DisplayDialog(
                "XCharts Compatibility Check",
                "Default Theme Depends on XCharts, thus Input Manager need to be enabled!\n" +
                "Go to Player Settings -> Other -> Configuration, Set Active Input Handeling to [Input Manager (Old)] or [Both]",
                "OK",
                "Cancle"
            ))
            {
                SettingsService.OpenProjectSettings("Project/Player");
            }
        }
        void PreBakeColliders()
        {
            if (!PlayerSettings.bakeCollisionMeshes)
            {
                PlayerSettings.bakeCollisionMeshes = true;
                Debug.Log("Setting changed: Player Settings -> Other -> Optimization -> Prebake Collision Meshes from false to true. To avoid this behaviour, disable Theme Default Canvas(GameObject) -> Theme Initializer(Component) -> Prebake Colliders");
            }
        }
        void SetupTMP()
        {
            if (!LoadAssetIfNull(fontAssetPath, ref fontAsset)) return;
            if (TMP_Settings.defaultFontAsset == fontAsset) return;
            var field = typeof(TMP_Settings).GetField("m_defaultFontAsset", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null) return;
            field.SetValue(TMP_Settings.instance, fontAsset);
            Debug.Log($"Setting changed: Project Settings -> TextMeshPro -> DefaultFontAsset to {fontAsset}. To avoid this behaviour, disable Theme Default Canvas(GameObject) -> Theme Initializer(Component) -> Set TMP Defalut Font Asset");
        }

    }
}
#endif