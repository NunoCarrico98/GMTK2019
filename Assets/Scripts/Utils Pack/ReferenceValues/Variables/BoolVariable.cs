using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Variables/Bool")]
public class BoolVariable : ScriptableObject
{
    [SerializeField]
    private bool value = false;

#if UNITY_EDITOR
    [TextArea]
    public string DeveloperDescription = "";
#endif

    public bool Value
    {
        get { return value; }
        set { this.value = value; }
    }

    public void SetValue(bool value)
    {
        Value = value;
    }

    public void SetValue(BoolVariable value)
    {
        Value = value.Value;
    }
}