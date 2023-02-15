using UnityEngine;
using SimpleMan.Utilities;

namespace SimpleMan.VisibleRaycast.Presentation
{
    internal abstract class DrawTask
    {
        protected readonly float _hitPointRadius;
        protected readonly float _lifeTime;
        protected Color _hitColor;
        protected Color _missColor;
        private float _timeLeft;

        public DrawTask(float hitPointRadius, float lifeTime, Color hitColor, Color missColor)
        {
            _hitPointRadius = hitPointRadius;
            _lifeTime = lifeTime;
            _hitColor = hitColor;
            _missColor = missColor;
            _timeLeft = lifeTime;
        }

        public float TimeLeft
        {
            get => _timeLeft;
            set
            {
                _timeLeft = value.ClampPositive();
                float alpha = Mathf.InverseLerp(0, _lifeTime, value);

                _hitColor = _hitColor.WithAlpha(alpha);
                _missColor = _missColor.WithAlpha(alpha);
            }
        }

        public abstract void Draw();
    }
}
