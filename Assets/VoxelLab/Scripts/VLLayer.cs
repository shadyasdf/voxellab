using UnityEngine;

public static class VLLayer
{
    public const string layerIgnoreRaycast = "Ignore Raycast";
    public const int layerIdIgnoreRaycast = 2;
    public const string layerCharacter = "Character";
    public const int layerIdCharacter = 3;
    public const string layerUI = "UI";
    public const int layerIdUI = 5;


    public static bool ContainsLayerId(this LayerMask _layerMask, int _layerId)
    {
        return (_layerMask & (1 << _layerId)) != 0;
    }
}
