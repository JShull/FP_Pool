using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FuzzPhyte.Pool;

namespace FuzzPhyte.Pool.Examples
{
    public class FPComponentPooler : MonoBehaviour
    {
        [SerializeField]
        public FPBullet prefab;
        [Tooltip("Starting pool size - this is how many we retain in the pool for the lifetime of the object pooler")]
        public int poolSize = 10;
        [Tooltip("What object should the pooled objects be parented to?")]
        public Transform PoolParent;
        [Tooltip("How long should the object be inactive before it is returned to the pool?")]
        public float PoolDelayTime = 60f;
        [Tooltip("How many max active objects can be out at once - these are eventually destroyed")]
        public int MaxActiveObjects=500;

        private FP_ObjectPool<Component> pool;

        private void Start()
        {
            pool = new FP_ObjectPool<Component>(prefab, poolSize,this.transform, PoolDelayTime, PoolParent,MaxActiveObjects);
        }

        public Component GetObject()
        {
            var anItem = pool.GetObject();
            if(anItem==null)
            {
                Debug.LogWarning($"Probably hit Active Object Limit of {MaxActiveObjects}");
                return null;
            }
            return anItem;
        }
        public Component GetObject(Transform atTransform)
        {
            var anItem = pool.GetObject(atTransform.position, atTransform.rotation);
            if(anItem==null)
            {
                Debug.LogWarning($"Probably hit Active Object Limit of {MaxActiveObjects}");
                return null;
            }
            return anItem;
        }

        public void ReleaseObject(Component obj)
        {
            pool.ReleaseObject(obj);
        }
        public void ReleaseObject(Component obj,Vector3 resetPoint)
        {
            pool.ReleaseObject(obj,resetPoint);
        }
    }
}
