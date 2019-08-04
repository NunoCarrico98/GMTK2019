// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using RoboRyanTron.Unite2017.Sets;
using UnityEngine;
using UnityEngine.Events;



namespace RoboRyanTron.Unite2017.Sets
{
    [CreateAssetMenu]
    public class ThingRuntimeSet : RuntimeSet<Thing>
    {
        [System.Serializable]
        public class ThingEvent : UnityEvent<Thing> { }

        public ThingEvent onAdd;
        public ThingEvent onRemove;
        public ThingEvent onUpdate;

        public override void OnAdd(Thing thing)
        {
            onAdd.Invoke(thing);
        }

        public override void OnRemove(Thing thing)
        {
            onRemove.Invoke(thing);
        }

        public override void OnUpdate(Thing thing)
        {
            onUpdate.Invoke(thing);
        }
    }
}