﻿using SimpleMan.AsyncOperations;
using SimpleMan.VisualRaycast;
using UnityEngine;

namespace SimpleMan.VisualRaycastDemo
{
    public class BoxOverlapExample : MonoBehaviour
    {
        [Range(0f, 1f)]
        [SerializeField] private float _raycastFrequency = 0.4f;

        [SerializeField] private Vector3 _size = new Vector3(3, 3, 3);

        private void Start()
        {
            this.RepeatForever(Tick, _raycastFrequency);
        }

        private void Tick()
        {
            VisualRaycast.VisualPhysics.
                BoxOverlap().
                FromGameObjectInWorld(gameObject).
                WithSize(_size).
                UseDefaultLayerMask().
                UseRotation(transform.rotation).
                DontIgnoreAnything();
        }
    }
}
