// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
using System;
using UnityEngine;

[Serializable]
public class AxisReference
{
    public bool UseConstant = true;
    public string ConstantValue;
    public AxisVariable Variable;

    public AxisReference()
    { }

    public AxisReference(string value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    public string Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }

    /// <summary>
    /// Return axis with smoothing filter from reference value
    /// </summary>
    /// <returns></returns>
    public float Axis() { return Input.GetAxis(Value); }

    public static implicit operator string(AxisReference reference)
    {
        return reference.Value;
    }

    /// <summary>
    /// Return axis with no smoothing filter
    /// </summary>
    /// <param name="r"></param>
    public static implicit operator float (AxisReference r)
    { return Input.GetAxisRaw(r.Value); }
}