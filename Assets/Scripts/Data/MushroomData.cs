using UnityEngine;

[CreateAssetMenu(fileName = "MushroomData", menuName = "Scriptable Objects/MushroomData")]
public class MushroomData : ItemData
{   
    [field: SerializeField] public float SecondsPerStage { get; private set; } = 10f; 
    [field: SerializeField] public int HarvestValue { get; private set; } = 1;
}
