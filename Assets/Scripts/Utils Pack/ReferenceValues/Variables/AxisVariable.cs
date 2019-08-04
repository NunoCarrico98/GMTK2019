using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Scriptable/Input/Axis"), fileName = "Input State")]
public class AxisVariable : ScriptableObject
{
    [SerializeField] private string value = "";

#if UNITY_EDITOR
    [TextArea]
    public string DeveloperDescription = "";
#endif

    public string Value { get => value; set => this.value = value; }

    public float Axis() { return Input.GetAxis(Value); }

    public float AxisRaw() { return Input.GetAxisRaw(Value); }

    public void SetValue(string value) { Value = value; }

    public void SetValue(AxisVariable value) { Value = value.Value; }
}
