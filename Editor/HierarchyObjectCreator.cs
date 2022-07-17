using UnityEngine;
using UnityEditor;


namespace SimpleMan.VisualRaycast
{
    public static class HierarchyObjectCreator
    {
        //------METHODS
        [MenuItem("GameObject/Raycast Drawer", false, 10)]
        public static void CreateVisualDrawer(MenuCommand menuCommand)
        {
            // Create a custom game object
            GameObject drawer = new GameObject("RaycastDrawer", typeof(SimpleMan.VisualRaycast.VisualCastDrawer));

            // Ensure it gets reparented if this was a context click (otherwise does nothing)
            GameObjectUtility.SetParentAndAlign(drawer.gameObject, menuCommand.context as GameObject);

            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(drawer, "Create " + drawer.name);

            Selection.activeObject = drawer;
        }
    }
}
