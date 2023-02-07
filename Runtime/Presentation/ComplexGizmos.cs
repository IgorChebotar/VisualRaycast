using UnityEngine;

namespace SimpleMan.VisualRaycast.Presentation
{
    public static class ComplexGizmos
    {
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

        public static void DrawHitSphere(Vector3 center, float radius, Color color)
        {
            Color previousColor = Gizmos.color;
            Gizmos.color = color;
            Gizmos.DrawSphere(center, radius);
            Gizmos.color = previousColor;
        }

        public static void DrawSphere(Vector3 center, float radius, Color color)
        {
            Color previousColor = Gizmos.color;
            Gizmos.color = color;
            Gizmos.DrawWireSphere(center, radius);
            Gizmos.color = previousColor;
        }

        public static void DrawBox(Vector3 center, Vector3 size, Quaternion rotation, Color color)
        {

        }

        public static void DrawBoxOLD(Vector3 center, Vector3 size, Quaternion rotation, Color color)
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
    }
}
