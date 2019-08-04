using UnityEditor;
using UnityEngine;

public static class BuildUtil
{
    [MenuItem("Project Utils/Build/Run")]
    public static void RunBuild()
    {
        try
        {
            string path = EditorUserBuildSettings.GetBuildLocation(EditorUserBuildSettings.activeBuildTarget);
            System.Diagnostics.Process.Start(path);
        }
        catch
        {
            Debug.LogError("Any build found, you need to build at least once");
        }
    }
}
