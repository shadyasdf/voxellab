using System;
using System.Collections.Generic;
using System.IO;
using SFB;
using Unity.Mathematics;
using UnityEngine;
using VoxelMesh;

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

        VMObject<VMVoxelInfo_Color> obj = VMDataReader.ReadObject_Color(_path);
        
        VMObjectRenderer objectRenderer = new GameObject("ObjectRenderer").AddComponent<VMObjectRenderer>();
        objectRenderer.SetVoxelObject(obj);
        
        return true;
    }

    public bool TrySaveProjectToFile(string _path)
    {
        if (string.IsNullOrEmpty(_path))
        {
            return false;
        }

        Dictionary<ushort, VMVoxelInfo_Color> infoByPalette = new()
        {
            { 1, new VMVoxelInfo_Color(Color.red) },
            { 2, new VMVoxelInfo_Color(Color.blue) }
        };
        Dictionary<int3, ushort> paletteByPosition = new()
        {
            { new int3(0, 0, 0), 1 },
            { new int3(0, 1, 0), 2 }
        };
        VMData<VMVoxelInfo_Color> data = new(infoByPalette, paletteByPosition);
        VMChunk<VMVoxelInfo_Color> chunk1 = new(data);
        VMChunk<VMVoxelInfo_Color> chunk2 = new(data);
        VMObject<VMVoxelInfo_Color> obj = new(new Dictionary<VMChunk<VMVoxelInfo_Color>, VMChunkProperties>()
        {
            { chunk1, new VMChunkProperties(int3.zero, int3.zero) },
            { chunk2, new VMChunkProperties(new int3(1, 0, 0), int3.zero) }
        });
        
        VMDataWriter.WriteObject_Color(_path, obj);
        
        Debug.Log($"Saved project!\n{_path}");
        
        return true;
    }
}
