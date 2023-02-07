using System;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine;
using SimpleMan.Utilities;

namespace SimpleMan.VisualRaycast.Editor
{
    internal static class ProjectSettingsTab
    {
        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider()
        {
            var config = GetConfigAsset();
            var visualTreeInstance = GetVisualTreeInstance();

            BindToConfig(visualTreeInstance, config);
            BindResetButton(visualTreeInstance, config);


            var provider = new SettingsProvider("Project/Visual Raycast", SettingsScope.Project)
            {
                label = "Visual Raycast",

                keywords = new HashSet<string>(new[] { "Visual", "Physics", "Raycast" }),

                
                activateHandler = (searchContext, rootElement) =>
                {
                    rootElement.Add(visualTreeInstance);
                    
                }
            };

            return provider;
        }

        private static void BindResetButton(TemplateContainer treeInstance, DAConfig config)
        {
            treeInstance.Q<Button>("ResetButton").clicked += config.ResetToDefault;
        }

        private static void BindToConfig(TemplateContainer treeInstance, DAConfig config)
        {
            var serializedConfig = new SerializedObject(config);

            treeInstance.Q<SliderInt>("MaxDrawTasksCount").bindingPath = "_maxDrawTasksCount";
            treeInstance.Q<Slider>("GizmoLifeTime").bindingPath = "_gizmoLifeTime";
            treeInstance.Q<Slider>("HitPointRadius").bindingPath = "_hitPointRadius";
            treeInstance.Q<ColorField>("HitColor").bindingPath = "_hitColor";
            treeInstance.Q<ColorField>("MissColor").bindingPath = "_missColor";
            treeInstance.Bind(serializedConfig);
        }

        private static TemplateContainer GetVisualTreeInstance()
        {
            VisualTreeAsset visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(Constants.PROJECT_SETTINGS_TAB_UXML_PATH);
            if(visualTreeAsset.NotExist())
            {
                throw new NullReferenceException(
                    "UXML file was not found. Reimport this plugin to fix issue");
            }
            return visualTreeAsset.CloneTree();
        }

        private static DAConfig GetConfigAsset()
        {
            var asset = AssetDatabase.LoadAssetAtPath<DAConfig>(Constants.FULL_CONFIG_PATH);
            if (asset == null)
            {
                asset = ScriptableObject.CreateInstance<DAConfig>();
                AssetDatabase.CreateAsset(asset, Constants.FULL_CONFIG_PATH);
                AssetDatabase.SaveAssets();

                Debug.Log($"<b>Visual Raycast:</b> Config asset created at path: {Constants.FULL_CONFIG_PATH}");
            }

            return asset;
        }
    }
}
