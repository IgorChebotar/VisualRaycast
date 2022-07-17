using UnityEngine;


namespace SimpleMan.VisualRaycast
{
    internal interface ICastInfo
    {
        //******            PROPERTIES        	******\\
        public Vector3 OriginPoint { get; }
        public Vector3 Direction { get; }
        public float MaxDistance { get; }
        public bool CastAll { get; }
        public CastResult CastResult { get; }
    }

    internal struct RaycastInfo : ICastInfo
    {
        public Vector3 OriginPoint { get; private set; }
        public Vector3 Direction { get; private set; }
        public float MaxDistance { get; private set; }
        public bool CastAll { get; private set; }
        public CastResult CastResult { get; private set; }
        public RaycastInfo(Ray ray, float maxDistance,  bool castAll, CastResult castResult)
        {
            OriginPoint = ray.origin;
            Direction = ray.direction;
            MaxDistance = maxDistance;
            CastAll = castAll;
            CastResult = castResult;
        }
    }

    internal struct BoxcastInfo : ICastInfo
    {
        public Vector3 OriginPoint { get; private set; }
        public Vector3 Direction { get; private set; }
        public float MaxDistance { get; private set; }
        public bool CastAll { get; private set; }
        public CastResult CastResult { get; private set; }
        public Vector3 Size { get; private set; }
        public Quaternion Rotation { get; private set; }
        public BoxcastInfo(Ray ray, Vector3 size, Quaternion rotation, float maxDistance, bool castAll, CastResult castResult)
        {
            OriginPoint = ray.origin;
            Direction = ray.direction;
            CastResult = castResult;
            Size = size;
            MaxDistance = maxDistance;
            CastAll = castAll;
            Rotation = rotation;
        }
    }

    internal struct SpherecastInfo : ICastInfo
    {
        public Vector3 OriginPoint { get; private set; }
        public Vector3 Direction { get; private set; }
        public float MaxDistance { get; private set; }
        public bool CastAll { get; private set; }
        public CastResult CastResult { get; private set; }
        public float Radius { get; private set; }

        public SpherecastInfo(Ray ray, float radius, float maxDistance, bool castAll, CastResult castResult)
        {
            OriginPoint = ray.origin;
            Direction = ray.direction;
            MaxDistance = maxDistance;
            CastResult = castResult;
            CastAll = castAll;
            Radius = radius;
        }
    }

    internal struct BoxOverlapInfo
    {
        public BoxOverlapInfo(Vector3 center, Vector3 boxSize, Quaternion rotation, Collider[] hits)
        {
            Center = center;
            Size = boxSize;
            Rotation = rotation;
            Hits = hits;
        }

        public Vector3 Center { get; private set; }
        public Vector3 Size { get; private set; }
        public Quaternion Rotation { get; private set; }
        public Collider[] Hits { get; private set; }

    }

    internal struct SphereOverlapInfo
    {
        public SphereOverlapInfo(Vector3 center, float radius, Collider[] hits)
        {
            Center = center;
            Radius = radius;
            Hits = hits;
        }

        public Vector3 Center { get; private set; }
        public float Radius { get; private set; }
        public Collider[] Hits { get; private set; }

    }
}