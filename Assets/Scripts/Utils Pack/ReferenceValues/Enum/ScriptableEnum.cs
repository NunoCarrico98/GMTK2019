using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Enum", fileName = "New scriptable enum")]
public class ScriptableEnum : ScriptableObject
{
    public bool Compare(ScriptableEnum obj) { return obj.Equals(this); }

#if UNITY_EDITOR
    [TextArea]
    public string DeveloperDescription = "Use the ''Compare'' function to compare two ScriptableEnums";
#endif

}
