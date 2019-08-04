using System;
using UnityEngine;

public class DebugOnBuild : MonoBehaviour
{
    private static DebugOnBuild instance;

    private bool isVisible = false;
    private string log;

    private void Awake()
    {
        if (!instance)
            instance = this;
    }

    public static void Show()
    { 
        if(!Debug.isDebugBuild)
            return;

        if (!instance)
        {
            GameObject obj = new GameObject();
            obj.hideFlags = HideFlags.HideInHierarchy;
            instance = obj.AddComponent<DebugOnBuild>();
        }

        instance.isVisible = true;
    }

    public static void Hide()
    { if (instance) instance.isVisible = false; }

    public static void Log(string log, bool showOnConsole = true)
    {
        if (!Debug.isDebugBuild || Application.isEditor)
        {
            if (showOnConsole)
                Debug.Log(log);
            return;
        }

        if (!instance)
            Show();

        instance.log = log + "\n" + instance.log;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
            isVisible = !isVisible;
    }

    private void OnGUI()
    {
        if (!isVisible)
            return;

        Rect rect = new Rect(Vector2.zero, new Vector2(Screen.width / 2, Screen.height));
        GUI.TextArea(rect, log, 10000);
    }
}
