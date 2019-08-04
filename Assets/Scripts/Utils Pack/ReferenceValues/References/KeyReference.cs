// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
using System;
using UnityEngine;

[Serializable]
public class KeyReference
{
    public bool UseConstant = true;
    public KeyCode ConstantValue;
    public KeyVariable Variable;

    public KeyReference()
    { }

    public KeyReference(KeyCode value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    public KeyCode Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }

    public static implicit operator KeyCode(KeyReference reference)
    {
        return reference.Value;
    }

    public static implicit operator bool(KeyReference reference)
    {
        return Input.GetKeyDown(reference.Value);
    }
}