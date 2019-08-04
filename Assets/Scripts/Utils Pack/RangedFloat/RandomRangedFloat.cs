
using UnityEngine;

namespace CustomExtension
{
    public static class RandomExtension
    {
        public static float Range(this Random r, RangedFloat rFloat)
        {
            return Random.Range(rFloat.minValue, rFloat.maxValue);
        }
    }
}
