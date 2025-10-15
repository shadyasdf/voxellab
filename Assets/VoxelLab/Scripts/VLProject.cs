using System;
using System.IO;
using SFB;
using UnityEngine;

public class VLProject : MonoBehaviour
{
    public string projectName { get; protected set; } = $"New{projectFriendlyName}";
    
    public const string projectFriendlyName = "Project";
    public const string defaultProjectDirectory = ""; // Set to "" to use no default
    public static ExtensionFilter[] fileExtensionFilters =
    {
        new($"VoxelLab {projectFriendlyName}s", "vl")
    };
    
    public static VLProject instance;
    

    protected void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
    }
    
    
    public bool TryLoadProjectFromFile(string _path)
    {
        if (string.IsNullOrEmpty(_path))
        {
            return false;
        }

        string content = File.ReadAllText(_path);
        Debug.Log($"Loaded project!\n{_path}\n{content}");
        
        return true;
    }

    public bool TrySaveProjectToFile(string _path)
    {
        if (string.IsNullOrEmpty(_path))
        {
            return false;
        }

        string content = "Testing contents";
        File.WriteAllText(_path, content);
        Debug.Log($"Saved project!\n{_path}\n{content}");
        
        return true;
    }
}
