using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Scriptable/Input/Key"), fileName = "Key Reference")]
public class KeyVariable : ScriptableObject
{
    [SerializeField] private KeyCode value = 0;
    
#if UNITY_EDITOR
    [TextArea]
    public string DeveloperDescription = "";
#endif

    public KeyCode Value { get => value; set => this.value = value; }

    public bool GetKey() { return Input.GetKey(value); }

    public bool GetKeyDown() { return Input.GetKeyDown(value); }

    public bool GetKeyUp() { return Input.GetKeyUp(value); }

    public void SetValue(KeyCode value) { Value = value; }

    public void SetValue(KeyVariable value) { Value = value.Value; }
}
