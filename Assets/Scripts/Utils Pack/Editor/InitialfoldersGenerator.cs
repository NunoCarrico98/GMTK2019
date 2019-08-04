//Script created by Roberto Gomes

using System.IO;
using UnityEditor;
using UnityEngine;

public static class InitialfoldersGenerator
{
    /// <summary>
    /// simplify the creation of subfolders
    /// </summary>
    private struct Folder
    {
        public string name;
        public Folder[] subFolders;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Folder name</param>
        /// <param name="subFolders">Subfolders names (can be set using strings)</param>
        public Folder(string name, params Folder[] subFolders)
        {
            this.name = name;
            this.subFolders = subFolders;
        }

        /// <summary>
        /// Recursive method to create subfolders and subfolders of it subfolders on and on
        /// </summary>
        /// <param name="selfPath"></param>
        public void CreateSubFolders(string selfPath)
        {
            foreach (var sub in subFolders)
            {
                string subDir = Path.Combine(selfPath, sub);
                Directory.CreateDirectory(subDir);
                sub.CreateSubFolders(subDir);
            }
        }

        public static implicit operator Folder(string name) { return new Folder(name); }
        public static implicit operator string(Folder folder) { return folder.name; }
    }

    /// <summary>
    /// folders can be setted as strings, but to add subfolders to it you need to create using new Folder
    /// </summary>
    private static readonly Folder[] folders = new Folder[] {
        "Scripts",
        "Prefabs",
        "Materials",
        "Models",
        "Resources",
        "Scenes",
        "Sprites",
        new Folder("Settings",
                        "Post Process",
                                new Folder("Reference Data",
                                    "Manager",
                                    "Player")),
        };

    /// <summary>
    /// Create all folders listed on folders variable on application data path
    /// </summary>
    [MenuItem("Edit/Generate Initial Folders %&b")] //% = ctrl, & = alt
    private static void CreateInitialFolders()
    {
        string path = Application.dataPath;

        foreach (var folder in folders)
        {
            //create main folders
            string dir = Path.Combine(path, folder);
            Directory.CreateDirectory(dir); //do not need to check the existence of the directory :D
            folder.CreateSubFolders(dir); // will create all subfolders
        }

        //update to show folders
        AssetDatabase.Refresh();
    }
}
