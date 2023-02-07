﻿using SimpleMan.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleMan.VisualRaycast.Presentation
{
    [AddComponentMenu("Simple Man/Visual Raycast/Physics Visualizer")]
    public class VisualRaycastDrawer : MonoBehaviour
    {
        private List<DrawTask> _drawTasks;
        private List<DrawTask> _drawTasksToRemove;
        private DAConfig _config;




        private DAConfig Config
        {
            get
            {
                if(_config.Exist())
                    return _config;

                _config = Resources.Load<DAConfig>(Constants.RESOURCES_CONFIG_PATH);
                if (_config.NotExist())
                {
                    throw new NullReferenceException(
                        "Config doesn't exist in project. Reimport/Reinstall this plugin to fix" +
                        "this issue");
                }

                return _config;
            }
        }
        private Color HitColor => Config.HitColor;
        private Color MissColor => Config.MissColor;
        private int MaxDrawTasksCount => Config.MaxDrawTasksCount;
        private float HitPointRadius => Config.HitPointRadius;
        private float GizmoLifeTime => Config.GizmoLifeTime;




        private void OnEnable()
        {
            _drawTasks = new List<DrawTask>(MaxDrawTasksCount);
            _drawTasksToRemove = new List<DrawTask>(MaxDrawTasksCount);

            InternalPhysicsCast.OnRaycast += TryAddTask;
            InternalPhysicsCast.OnSphereOverlap += AddTask;
            InternalPhysicsCast.OnBoxOverlap += AddTask;
        }

        private void OnDisable()
        {
            InternalPhysicsCast.OnRaycast -= TryAddTask;
            InternalPhysicsCast.OnSphereOverlap -= AddTask;
            InternalPhysicsCast.OnBoxOverlap -= AddTask;
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;

            for (int i = 0; i < _drawTasks.Count; i++)
            {
                _drawTasks[i].Draw();
                _drawTasks[i].TimeLeft -= Time.deltaTime;

                if (_drawTasks[i].TimeLeft <= 0)
                    _drawTasksToRemove.Add(_drawTasks[i]);
            }

            foreach (var task in _drawTasksToRemove)
            {
                _drawTasks.Remove(task);
            }
        }

        private void AddTask(RaycastInfo info)
        {
            DrawTask task;
            switch (info.castType)
            {
                case RaycastInfo.ECastType.Raycast:
                    if (info.isSingle)
                    {
                        task = new RaycastSingleDrawTask(
                        info.from,
                        info.direction,
                        info.distance,
                        GizmoLifeTime,
                        HitPointRadius,
                        HitColor,
                        MissColor,
                        info.result); break;
                    }
                    else
                    {
                        task = new RaycastMultiDrawTask(
                        info.from,
                        info.direction,
                        info.distance,
                        GizmoLifeTime,
                        HitPointRadius,
                        HitColor,
                        MissColor,
                        info.result); break;
                    }

                default:
                    throw new NotImplementedException();
            }

            _drawTasks.Add(task);
        }

        private void TryAddTask(RaycastInfo info)
        {
            RemoveOldTaskIfNeed();
            AddTask(info);

            void RemoveOldTaskIfNeed()
            {
                int difference = _drawTasks.Capacity - _drawTasks.Count;
                if (difference < 1)
                    _drawTasks.RemoveAt(0);
            }
        }

        private void AddTask(SphereOverlapInfo info)
        {
            DrawTask task = new SphereOverlapDrawTask(
                info.center,
                info.radius,
                info.result,
                HitPointRadius,
                GizmoLifeTime,
                HitColor,
                MissColor);

            _drawTasks.Add(task);
        }

        private void AddTask(BoxOverlapInfo info)
        {
            DrawTask task = new BoxOverlapDrawTask(
                info.center,
                info.size,
                info.rotation,
                info.result, HitPointRadius,
                GizmoLifeTime,
                HitColor,
                MissColor);

            _drawTasks.Add(task);
        }
    }
}