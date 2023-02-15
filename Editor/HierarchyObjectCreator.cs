using SimpleMan.VisibleRaycast.Presentation;
using SimpleMan.Utilities;
using UnityEditor;
using UnityEngine;

namespace SimpleMan.VisibleRaycast.Editor
{
    public static class HierarchyObjectCreator
    {
        [MenuItem("GameObject/Visual Raycast Drawer", false)]
        public static void CreateVisualDrawer(MenuCommand menuCommand)
        {
            VisualRaycastDrawer visualizer = Object.FindObjectOfType<VisualRaycastDrawer>();
            if (visualizer.Exist())
            {
                if (EditorUtility.DisplayDialog(
                    "Invalid operation",
                    "Visual raycast drawer already exist on current scene",
                    "Select it", "Ok"))
                {
                    Selection.activeGameObject = visualizer.gameObject;
                }

                return;
            }

            GameObject drawer = new GameObject("VisualRaycastDrawer", typeof(VisualRaycastDrawer));
            GameObjectUtility.SetParentAndAlign(drawer.gameObject, menuCommand.context as GameObject);
            Undo.RegisterCreatedObjectUndo(drawer, "Create " + drawer.name);

            Selection.activeObject = drawer;
        }     
    }
}
