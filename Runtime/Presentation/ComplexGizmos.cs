using SimpleMan.Utilities;
using UnityEditor;
using UnityEngine;

namespace SimpleMan.VisibleRaycast
{
    internal static class ComplexGizmos
    {
#if UNITY_EDITOR
        private const float MAX_DRAW_DISTANCE = 100000;




        public static void DrawLine(Vector3 from, Vector3 to, Color color)
        {
            Color previousColor = Gizmos.color;
            Gizmos.color = color;
            Gizmos.DrawLine(from, to);
            Gizmos.color = previousColor;
        }

        public static void DrawRay(Vector3 from, Vector3 direction, float distance, Color color)
        {
            if (distance > MAX_DRAW_DISTANCE)
                distance = MAX_DRAW_DISTANCE;

            Vector3 endPoint = from + direction * distance;
            DrawLine(from, endPoint, color);
        }

        public static void DrawSphere(Vector3 center, float radius, Color color)
        {
            Color previousColor = Gizmos.color;
            Gizmos.color = color;

            Mesh mesh = MeshesFactory.SphereMesh;
            Gizmos.DrawMesh(mesh, center, Quaternion.identity, Vector3.one * radius * 2);

            Gizmos.color = previousColor;
        }

        public static void DrawWireSphere(Vector3 center, float radius, Color color)
        {
            Color previousColor = Gizmos.color;
            Gizmos.color = color;
            Gizmos.DrawWireSphere(center, radius);
            Gizmos.color = previousColor;
        }

        public static void DrawWireBox(Vector3 center, Vector3 size, Quaternion rotation, Color color)
        {
            Color previousColor = Gizmos.color;
            Gizmos.color = color;

            Vector3 lineOrigin, lineEnd;

            //Top front line
            lineOrigin = lineEnd = center;
            lineOrigin += rotation * Vector3.up * size.y * 0.5f +
                          rotation * Vector3.left * size.x * 0.5f +
                          rotation * Vector3.back * size.z * 0.5f;

            lineEnd += rotation * Vector3.up * size.y * 0.5f +
                       rotation * Vector3.right * size.x * 0.5f +
                       rotation * Vector3.back * size.z * 0.5f;

            Gizmos.DrawLine(lineOrigin, lineEnd);




            //Top back line
            lineOrigin = lineEnd = center;
            lineOrigin += rotation * Vector3.up * size.y * 0.5f +
                          rotation * Vector3.left * size.x * 0.5f +
                          rotation * Vector3.forward * size.z * 0.5f;

            lineEnd += rotation * Vector3.up * size.y * 0.5f +
                       rotation * Vector3.right * size.x * 0.5f +
                       rotation * Vector3.forward * size.z * 0.5f;

            Gizmos.DrawLine(lineOrigin, lineEnd);



            //Bottom front line
            lineOrigin = lineEnd = center;
            lineOrigin += rotation * Vector3.down * size.y * 0.5f +
                          rotation * Vector3.left * size.x * 0.5f +
                          rotation * Vector3.forward * size.z * 0.5f;

            lineEnd += rotation * Vector3.down * size.y * 0.5f +
                       rotation * Vector3.right * size.x * 0.5f +
                       rotation * Vector3.forward * size.z * 0.5f;

            Gizmos.DrawLine(lineOrigin, lineEnd);




            //Bottom back line
            lineOrigin = lineEnd = center;
            lineOrigin += rotation * Vector3.down * size.y * 0.5f +
                          rotation * Vector3.left * size.x * 0.5f +
                          rotation * Vector3.back * size.z * 0.5f;

            lineEnd += rotation * Vector3.down * size.y * 0.5f +
                       rotation * Vector3.right * size.x * 0.5f +
                       rotation * Vector3.back * size.z * 0.5f;

            Gizmos.DrawLine(lineOrigin, lineEnd);



            //Front left line
            lineOrigin = lineEnd = center;
            lineOrigin += rotation * Vector3.up * size.y * 0.5f +
                          rotation * Vector3.left * size.x * 0.5f +
                          rotation * Vector3.back * size.z * 0.5f;

            lineEnd += rotation * Vector3.down * size.y * 0.5f +
                       rotation * Vector3.left * size.x * 0.5f +
                       rotation * Vector3.back * size.z * 0.5f;

            Gizmos.DrawLine(lineOrigin, lineEnd);




            //Back left line
            lineOrigin = lineEnd = center;
            lineOrigin += rotation * Vector3.up * size.y * 0.5f +
                          rotation * Vector3.left * size.x * 0.5f +
                          rotation * Vector3.forward * size.z * 0.5f;

            lineEnd += rotation * Vector3.down * size.y * 0.5f +
                       rotation * Vector3.left * size.x * 0.5f +
                       rotation * Vector3.forward * size.z * 0.5f;

            Gizmos.DrawLine(lineOrigin, lineEnd);




            //Front right line
            lineOrigin = lineEnd = center;
            lineOrigin += rotation * Vector3.up * size.y * 0.5f +
                          rotation * Vector3.right * size.x * 0.5f +
                          rotation * Vector3.forward * size.z * 0.5f;

            lineEnd += rotation * Vector3.down * size.y * 0.5f +
                       rotation * Vector3.right * size.x * 0.5f +
                       rotation * Vector3.forward * size.z * 0.5f;

            Gizmos.DrawLine(lineOrigin, lineEnd);




            //Back right line
            lineOrigin = lineEnd = center;
            lineOrigin += rotation * Vector3.up * size.y * 0.5f +
                          rotation * Vector3.right * size.x * 0.5f +
                          rotation * Vector3.back * size.z * 0.5f;

            lineEnd += rotation * Vector3.down * size.y * 0.5f +
                       rotation * Vector3.right * size.x * 0.5f +
                       rotation * Vector3.back * size.z * 0.5f;

            Gizmos.DrawLine(lineOrigin, lineEnd);




            //Top left line
            lineOrigin = lineEnd = center;
            lineOrigin += rotation * Vector3.up * size.y * 0.5f +
                          rotation * Vector3.left * size.x * 0.5f +
                          rotation * Vector3.back * size.z * 0.5f;

            lineEnd += rotation * Vector3.up * size.y * 0.5f +
                       rotation * Vector3.left * size.x * 0.5f +
                       rotation * Vector3.forward * size.z * 0.5f;

            Gizmos.DrawLine(lineOrigin, lineEnd);




            //Bottom left line
            lineOrigin = lineEnd = center;
            lineOrigin += rotation * Vector3.down * size.y * 0.5f +
                          rotation * Vector3.left * size.x * 0.5f +
                          rotation * Vector3.back * size.z * 0.5f;

            lineEnd += rotation * Vector3.down * size.y * 0.5f +
                       rotation * Vector3.left * size.x * 0.5f +
                       rotation * Vector3.forward * size.z * 0.5f;

            Gizmos.DrawLine(lineOrigin, lineEnd);




            //Top right line
            lineOrigin = lineEnd = center;
            lineOrigin += rotation * Vector3.up * size.y * 0.5f +
                          rotation * Vector3.right * size.x * 0.5f +
                          rotation * Vector3.back * size.z * 0.5f;

            lineEnd += rotation * Vector3.up * size.y * 0.5f +
                       rotation * Vector3.right * size.x * 0.5f +
                       rotation * Vector3.forward * size.z * 0.5f;

            Gizmos.DrawLine(lineOrigin, lineEnd);




            //Bottom right line
            lineOrigin = lineEnd = center;
            lineOrigin += rotation * Vector3.down * size.y * 0.5f +
                          rotation * Vector3.right * size.x * 0.5f +
                          rotation * Vector3.back * size.z * 0.5f;

            lineEnd += rotation * Vector3.down * size.y * 0.5f +
                       rotation * Vector3.right * size.x * 0.5f +
                       rotation * Vector3.forward * size.z * 0.5f;

            Gizmos.DrawLine(lineOrigin, lineEnd);
            Gizmos.color = previousColor;
        }

        public static void DrawBox(Vector3 center, Vector3 size, Quaternion rotation, Color color)
        {
            Color previousColor = Gizmos.color;
            Gizmos.color = color;

            Mesh mesh = MeshesFactory.BoxMesh;
            DrawMesh(mesh, center, size, rotation, color);
            Gizmos.color = previousColor;
        }

        public static void DrawMesh(Mesh mesh, Vector3 center, Vector3 scale, Quaternion rotation, Color color)
        {
            Color previousColor = Gizmos.color;
            Gizmos.color = color;
            Gizmos.DrawMesh(mesh, center, rotation, scale);
            Gizmos.color = previousColor;
        }

        public static void DrawWireMesh(Mesh mesh, Vector3 center, Vector3 scale, Quaternion rotation, Color color)
        {
            Color previousColor = Gizmos.color;
            Gizmos.color = color;
            Gizmos.DrawWireMesh(mesh, center, rotation, scale);
            Gizmos.color = previousColor;
        }

        public static void DrawWireCapsule(Vector3 center, float radius, float height, Quaternion rotation, Color color)
        {
            Color previousColor = Handles.color;
            Handles.color = color;

            Matrix4x4 angleMatrix = Matrix4x4.TRS(center, rotation, Handles.matrix.lossyScale);
            using (new Handles.DrawingScope(angleMatrix))
            {
                var pointOffset = (height - radius * 2) / 2;

                Handles.DrawWireArc(Vector3.up * pointOffset, Vector3.left, Vector3.back, -180, radius);
                Handles.DrawLine(new Vector3(0, pointOffset, -radius), new Vector3(0, -pointOffset, -radius));
                Handles.DrawLine(new Vector3(0, pointOffset, radius), new Vector3(0, -pointOffset, radius));
                Handles.DrawWireArc(Vector3.down * pointOffset, Vector3.left, Vector3.back, 180, radius);

                Handles.DrawWireArc(Vector3.up * pointOffset, Vector3.back, Vector3.left, 180, radius);
                Handles.DrawLine(new Vector3(-radius, pointOffset, 0), new Vector3(-radius, -pointOffset, 0));
                Handles.DrawLine(new Vector3(radius, pointOffset, 0), new Vector3(radius, -pointOffset, 0));
                Handles.DrawWireArc(Vector3.down * pointOffset, Vector3.back, Vector3.left, -180, radius);

                Handles.DrawWireDisc(Vector3.up * pointOffset, Vector3.up, radius);
                Handles.DrawWireDisc(Vector3.down * pointOffset, Vector3.up, radius);
            }
            Handles.color = previousColor;
        }

        public static void DrawCapsule(Vector3 center, float radius, float height, Quaternion rotation, Color color)
        {
            Color previousColor = Gizmos.color;
            Gizmos.color = color;
            Mesh cylinderMesh = MeshesFactory.CylinderMesh;
            Mesh halfsphereMesh = MeshesFactory.Halfsphere;


            Gizmos.DrawMesh(cylinderMesh, center, rotation, GeCylindertSize());
            Gizmos.DrawMesh(halfsphereMesh, GetTopHalfspherePosition(), rotation, GetHalfsphereSize());
            Gizmos.DrawMesh(halfsphereMesh, GetBottomHalfspherePosition(), GetInverseRotation(), GetHalfsphereSize());
            Gizmos.color = previousColor;



            Vector3 GeCylindertSize()
            {
                return new Vector3(
                    radius * 2,
                    (height - radius * 2).ClampPositive(),
                    radius * 2);
            }
            Vector3 GetHalfsphereSize()
            {
                return Vector3.one * radius * 2;
            }
            Vector3 GetTopHalfspherePosition()
            {
                Vector3 direction = rotation * Vector3.up;
                float distance = (height * 0.5f - radius).ClampPositive();
                return center + direction * distance;
            }
            Vector3 GetBottomHalfspherePosition()
            {
                Vector3 direction = rotation * Vector3.down;
                float distance = (height * 0.5f - radius).ClampPositive();
                return center + direction * distance;
            }
            Quaternion GetInverseRotation()
            {
                Vector3 euler = rotation.eulerAngles;
                euler.x += 180;

                return Quaternion.Euler(euler);
            }
        }
#endif
    }
}
