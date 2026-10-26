using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MushroomSpawner : MonoBehaviour
{
    [SerializeField] private Tilemap _grassTilemap;
    [SerializeField] private Tilemap _mushroomTilemap;
    [SerializeField] private TileBase _mushroomTile;

    [SerializeField] private int _maxMushrooms = 50;
    [SerializeField] private float _spawnInterval = 5f;

    //[SerializeField] private GameObject _mushroomPrefab;

    private List<Vector3Int> _spawnPositions = new();

    private void Start()
    {
        foreach (Vector3Int cellPosition in _grassTilemap.cellBounds.allPositionsWithin)
        {
            if(_grassTilemap.HasTile(cellPosition))
            {
                //Vector3Int worldPosition = _grassTilemap.GetCellCenterWorld(cellPosition);
                _spawnPositions.Add(cellPosition);
            }
        }

        //UnityEngine.Debug.Log($"Найдено клеток травы: {_spawnPositions.Count}");
        InvokeRepeating(nameof(SpawnMushroom), 0f, _spawnInterval);
    }
    private void SpawnMushroom()
    {
        if (_spawnPositions.Count == 0)
            return;

        //if (transform.childCount >= _maxMushrooms)
        //    return;
        if (GetMushroomCount() >= _maxMushrooms)
            return;


        Vector3Int randomCell = _spawnPositions[UnityEngine.Random.Range(0, _spawnPositions.Count)];
        if (_mushroomTilemap.HasTile(randomCell))
            return;
        _mushroomTilemap.SetTile(randomCell, _mushroomTile);

        //int randomIndex = UnityEngine.Random.Range(0, _spawnPositions.Count);

        //Vector3 spawnPosition = _spawnPositions[randomIndex];

        //Instantiate(_mushroomPrefab, spawnPosition, Quaternion.identity, transform);
    }
    private int GetMushroomCount()
    {
        int count = 0;

        foreach (Vector3Int cellPosition in _mushroomTilemap.cellBounds.allPositionsWithin)
        {
            if (_mushroomTilemap.HasTile(cellPosition))
            {
                count++;
            }
        }

        return count;
    }
}
