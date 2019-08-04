// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UtilsPack.CustomEvents
{
    [System.Serializable]
    public class EventVector3 : UnityEvent<Vector3>
    {

    }

    [System.Serializable]
    public class EventTransform : UnityEvent<Transform>
    {

    }
}