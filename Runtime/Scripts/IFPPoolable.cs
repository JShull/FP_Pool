using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FuzzPhyte.Pool
{
    public interface IFPPoolable
    {
        void OnObjectFirstGenerated(Transform poolManager);
        void OnObjectActivated();
        void OnObjectReleased();
    }
}
