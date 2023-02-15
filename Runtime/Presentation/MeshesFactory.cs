using SimpleMan.Utilities;
using System;
using UnityEngine;
using static SimpleMan.VisibleRaycast.Constants;

namespace SimpleMan.VisibleRaycast
{
    internal static class MeshesFactory
    {
        private static Mesh _sphereMesh;
        private static Mesh _halfsphereMesh;
        private static Mesh _boxMesh;
        private static Mesh _cylinderMesh;




        public static Mesh SphereMesh
        {
            get
            {
                if (_sphereMesh.Exist())
                    return _sphereMesh;

                _sphereMesh = Load(SPHERE_MESH_RESOURCES_PATH);
                return _sphereMesh;
            }
        }
        public static Mesh Halfsphere
        {
            get
            {
                if (_halfsphereMesh.Exist())
                    return _halfsphereMesh;

                _halfsphereMesh = Load(HALFSPHERE_MESH_RESOURCES_PATH);
                return _halfsphereMesh;
            }
        }
        public static Mesh BoxMesh
        {
            get
            {
                if (_boxMesh.Exist())
                    return _boxMesh;

                _boxMesh = Load(BOX_MESH_RESOURCES_PATH);
                return _boxMesh;
            }
        }
        public static Mesh CylinderMesh
        {
            get
            {
                if (_cylinderMesh.Exist())
                    return _cylinderMesh;

                _cylinderMesh = Load(CYLINDER_MESH_RESOURCES_PATH);
                return _cylinderMesh;
            }
        }




        private static Mesh Load(string path)
        {
            Mesh result = Resources.Load<Mesh>(path);
            if (result.NotExist())
            {
                throw new NullReferenceException(
                    $"{Constants.PLUGIN_DISPLAYED_NAME}: Mesh was not found at path: '{path}'. Reimport this " +
                    $"plugin to fix the issue");
            }

            return result;
        }
    }
}
