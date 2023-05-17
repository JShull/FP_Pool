using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace FuzzPhyte.Pool
{
    public class FP_ObjectPool<T> where T : Component
    {
        private T prefab;
        private Transform parent;
        private Transform poolMgr;
        private int poolSize;
        private float idleTime;
        private Queue<T> pool;
        private HashSet<T> activeObjects;

        /// <summary>
        /// we are going to spawn the items under the parent object passed in
        /// we then set our local position/rotation to that parent item
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="poolSize"></param>
        /// <param name="PoolManager"></param>
        /// <param name="idleTime"></param>
        /// <param name="parent"></param>
        public FP_ObjectPool(T prefab, int poolSize, Transform PoolManager, float idleTime = 5f, Transform parent = null)
        {
            this.prefab = prefab;
            this.poolSize = poolSize;
            this.idleTime = idleTime;
            this.parent = parent;
            this.poolMgr = PoolManager;

            pool = new Queue<T>(poolSize);
            activeObjects = new HashSet<T>();

            for (int i = 0; i < poolSize; i++)
            {
                T obj = Object.Instantiate(prefab, parent);

                // if there's anything we need to do to the object when we first generate it
                SetupTransformObject(obj);
                if (obj is IFPPoolable)
                {
                   obj.GetComponent<IFPPoolable>().OnObjectFirstGenerated(PoolManager);
                }
                obj.gameObject.SetActive(false);
                pool.Enqueue(obj);
            }
            
            // Start the cleanup coroutine
            MonoBehaviour behaviour = PoolManager != null ? PoolManager.GetComponent<MonoBehaviour>() : null;
            if (behaviour != null)
            {
                behaviour.StartCoroutine(CleanupCoroutine());
            }
            else
            {
                Debug.LogError("Object pooler parent object must have a MonoBehaviour component so we can run our cleanup coroutine. Suggest making the parent contain the Mono behaviour class that's doing the work");
            }
        }

        public T GetObject()
        {
            T obj;

            if (pool.Count > 0)
            {
                obj = pool.Dequeue();
            }
            else
            {
                obj = Object.Instantiate(prefab, parent);
                SetupTransformObject(obj);
                if (obj is IFPPoolable)
                {
                    obj.GetComponent<IFPPoolable>().OnObjectFirstGenerated(poolMgr);
                }
            }

            obj.gameObject.SetActive(true);

            activeObjects.Add(obj);

            // Call the interface method if we have it
            if (obj is IFPPoolable)
            {
                obj.GetComponent<IFPPoolable>().OnObjectActivated();
            }

            return obj;
        }
        public T GetObject(Vector3 position, Quaternion rotation)
        {
            T obj;

            if (pool.Count > 0)
            {
                obj = pool.Dequeue();
            }
            else
            {
                obj = Object.Instantiate(prefab, parent);
                SetupTransformObject(obj);
                if (obj is IFPPoolable)
                {
                    obj.GetComponent<IFPPoolable>().OnObjectFirstGenerated(poolMgr);
                }
            }

            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.gameObject.SetActive(true);

            activeObjects.Add(obj);

            // Call the interface method if we have it
            if (obj is IFPPoolable)
            {
                obj.GetComponent<IFPPoolable>().OnObjectActivated();
            }

            return obj;
        }

        private void SetupTransformObject(T obj)
        {
            obj.gameObject.transform.localPosition = Vector3.zero;
            obj.gameObject.transform.localRotation = Quaternion.identity;
        }
        
        public void ReleaseObject(T obj)
        {
            obj.gameObject.SetActive(false);
            activeObjects.Remove(obj);
            
            //if we have our interface we can reference it here
            if (obj is IFPPoolable)
            {
                obj.GetComponent<IFPPoolable>().OnObjectReleased();
            }
            pool.Enqueue(obj);
        }
        /// <summary>
        /// if we want to reset the object at a certain position after releasing back to the pool
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="position"></param>
        public void ReleaseObject(T obj, Vector3 position)
        {
            ReleaseObject(obj);
            obj.gameObject.transform.position = position;
        }

        private System.Collections.IEnumerator CleanupCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(idleTime);

                if (activeObjects.Count == 0 && pool.Count > poolSize)
                {
                    int count = pool.Count - poolSize;

                    for (int i = 0; i < count; i++)
                    {
                        T obj = pool.Dequeue();
                        Object.Destroy(obj.gameObject);
                    }
                }
            }
        }
    }

}
