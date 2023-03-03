using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FuzzPhyte.Pool.Examples
{
    /// <summary>
    /// example demonstrating how you need to manage the release of said object
    /// </summary>
    public class FPPoolShooter : MonoBehaviour
    {
        public FPComponentPooler MyBulletPooler;
        public float BulletForce = 100;
        public Transform BulletSpawnPt;
        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var aBullet = MyBulletPooler.GetObject(BulletSpawnPt);
                aBullet.transform.position = BulletSpawnPt.position;
                //aBullet.transform.rotation = BulletSpawnPt.rotation;
                if (aBullet.GetComponent<Rigidbody>())
                {
                    var rb = aBullet.GetComponent<Rigidbody>();
                    rb.AddForce(BulletSpawnPt.forward * BulletForce);
                }
                else
                {
                   var rb= aBullet.AddComponent<Rigidbody>();
                    rb.AddForce(BulletSpawnPt.forward * BulletForce);
                }
            }
        }
    }

}
