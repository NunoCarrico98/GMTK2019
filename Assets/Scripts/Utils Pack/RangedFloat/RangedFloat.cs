using System;

[Serializable]
public struct RangedFloat
{
	public float minValue;
	public float maxValue;

    public float GetRandom() { return UnityEngine.Random.Range(minValue, maxValue); }
}