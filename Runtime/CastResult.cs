using UnityEngine;

namespace SimpleMan.VisualRaycast
{
    public struct CastResult
    {
        //******            PROPERTIES        	******\\
        public RaycastHit[] Hits { get; private set; }
        public RaycastHit FirstHit
        {
            get
            {
                if (Hits.Length > 0)
                    return Hits[0];

                else
                    return new RaycastHit();
            }
        }
        public RaycastHit LastHit
        {
            get
            {
                if (Hits.Length > 0)
                    return Hits[Hits.Length - 1];

                else
                    return new RaycastHit();
            }
        }





        //******           CONSTRUCTORS         ******\\
        public CastResult(params RaycastHit[] hits)
        {
            if (hits == null)
                Hits = new RaycastHit[] { };
            
            else
                Hits = hits;
        }



        public static bool operator true(CastResult _castResult) => _castResult.Hits.Length > 0;


        public static bool operator false(CastResult _castResult) => _castResult.Hits.Length == 0;


        public static bool operator !(CastResult _castResult) => _castResult.Hits.Length == 0;
    }

    public struct OverlapResult
    {
        //******            PROPERTIES        	******\\
        public Collider[] Hits { get; private set; }
        public Collider FirstHit
        {
            get => Hits.Length > 0 ? Hits[0] : null;
        }
        public Collider LastHit
        {
            get => Hits.Length > 0 ? Hits[Hits.Length - 1] : null;
        }





        //******           CONSTRUCTORS         ******\\

        public OverlapResult(params Collider[] hits)
        {
            Hits = hits;
        }



        public static bool operator true(OverlapResult _castResult) => _castResult.Hits.Length > 0;


        public static bool operator false(OverlapResult _castResult) => _castResult.Hits.Length == 0;


        public static bool operator !(OverlapResult _castResult) => _castResult.Hits.Length == 0;
    }
}
