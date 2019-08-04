using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Variables/Vector3")]
public class Vector3Variable : ScriptableObject
{
    [SerializeField]
    private Vector3 value = Vector3.zero;

#if UNITY_EDITOR
    [TextArea]
    public string DeveloperDescription = "";
#endif

    public Vector3 Value
    {
        get { return value; }
        set { this.value = value; }
    }

    public void SetValue(Vector3 value)
    {
        Value = value;
    }

    public void SetValue(Vector3Variable value)
    {
        Value = value.Value;
    }
}