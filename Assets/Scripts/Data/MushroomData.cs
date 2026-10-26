using UnityEngine;

[CreateAssetMenu(fileName = "MushroomData", menuName = "Scriptable Objects/MushroomData")]
public class MushroomData : ScriptableObject
{
    [field: SerializeField] public string DisplayName { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    //[field: SerializeField] public TileBase[] GrowthStageTiles { get; private set; } // 0 - росток, последний - зрелый
    [field: SerializeField] public float SecondsPerStage { get; private set; } = 10f;
    [field: SerializeField] public int HarvestValue { get; private set; } = 1;
}
