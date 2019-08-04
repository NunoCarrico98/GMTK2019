using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Variables/Int")]
public class IntVariable : ScriptableObject
{
    [SerializeField]
    private int value = 0;

#if UNITY_EDITOR
    [TextArea]
    public string DeveloperDescription = "";
#endif

    public int Value
    {
        get { return value; }
        set { this.value = value; }
    }

    public void SetValue(int value)
    {
        Value = value;
    }

    public void SetValue(IntVariable value)
    {
        Value = value.Value;
    }
}