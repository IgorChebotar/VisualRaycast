using SimpleMan.Utilities;
using UnityEngine;

namespace SimpleMan.VisualRaycast
{
    [CreateAssetMenu(fileName = "RAR", menuName = "VR/Config")]
    public class DAConfig : ScriptableObject 
    {
        [Range(128, 8192)]
        [Tooltip("Sets the maximum number of draw tasks that can be executed simultaneously")]
        [SerializeField] private int _maxDrawTasksCount = 2048;

        [Range(0, 5)]
        [Tooltip("Life time of gizmos in seconds")]
        [SerializeField] private float _gizmoLifeTime = 0.6f;

        [Range(0, 2)]
        [Tooltip("Radius of hit indicators")]
        [SerializeField] private float _hitPointRadius = 0.2f;

        [ColorUsage(false)]
        [Tooltip("The color for representing a hit")]
        [SerializeField] private Color _hitColor = Color.green;

        [ColorUsage(false)]
        [Tooltip("Color for representing a miss")]
        [SerializeField] private Color _missColor = Color.red;




        public int MaxDrawTasksCount
        {
            get => _maxDrawTasksCount;
            set => _maxDrawTasksCount = value.ClampPositive();
        }

        public float GizmoLifeTime
        {
            get => _gizmoLifeTime;
            set => _gizmoLifeTime = value.ClampPositive();
        }

        public float HitPointRadius
        {
            get => _hitPointRadius;
            set => _hitPointRadius = value.ClampPositive();
        }

        public Color HitColor
        {
            get => _hitColor;
            set => _hitColor = value;
        }

        public Color MissColor
        {
            get => _missColor;
            set => _missColor = value;
        }




        public void ResetToDefault()
        {
            _maxDrawTasksCount = 2048;
            _gizmoLifeTime = 0.6f;
            _hitPointRadius = 0.2f;
            _hitColor = Color.green;
            _missColor = Color.red;
        }
    }
}

