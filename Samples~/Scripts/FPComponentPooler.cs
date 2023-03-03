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
        public int poolSize = 10;
        [Tooltip("What object should the pooled objects be parented to?")]
        public Transform PoolParent;
        public float PoolDelayTime = 60f;

        private FP_ObjectPool<Component> pool;

        private void Start()
        {
            pool = new FP_ObjectPool<Component>(prefab, poolSize,this.transform, PoolDelayTime, PoolParent);
        }

        public Component GetObject()
        {
            return pool.GetObject();
        }
        public Component GetObject(Transform atTransform)
        {
            return pool.GetObject(atTransform.position, atTransform.rotation);
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
