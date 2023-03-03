using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FuzzPhyte.Pool;
namespace FuzzPhyte.Pool.Examples
{
    public class FPBullet : MonoBehaviour, IFPPoolable
    {
        [SerializeField]
        private FPComponentPooler ThePooler;
        [SerializeField]
        private Vector3 _startPos;
        public void OnObjectActivated()
        {
            //start my personal timer
            _startPos = this.transform.position;
            StartCoroutine(DelayBeforeSelfRelease());
        }

        public void OnObjectFirstGenerated(Transform poolManager)
        {
            //throw new System.NotImplementedException();
            if (poolManager.gameObject.GetComponent<FPComponentPooler>())
            {
                ThePooler = poolManager.gameObject.GetComponent<FPComponentPooler>();
            }
            _startPos = this.transform.position;
        }

        public void OnObjectReleased()
        {
            //this method is ideal for resetting things like physics or other timers you might be utilizing
            if (this.GetComponent<Rigidbody>())
            {
                var rb = this.GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            this.transform.rotation = Quaternion.identity;
        }

        public IEnumerator DelayBeforeSelfRelease()
        {
            yield return new WaitForSecondsRealtime(5f);
            if (ThePooler != null)
            {
                //assuming we are on the parent object
                ThePooler.ReleaseObject(this, _startPos);
            }
            else
            {
                Debug.LogError($"Missing a reference to my Pooler");
            }
        }
    }

}
