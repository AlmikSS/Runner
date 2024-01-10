using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> _tiles = new List<GameObject>();
    [SerializeField] private GameObject _startTile;
    [SerializeField] private GameObject _endTile;
    [SerializeField] private int _worldSize;
    private List<GameObject> _spawnedTiles = new List<GameObject>();
    
    public void Regenarate()
    {
        if (_spawnedTiles.Count != 0)
        {
            for (int i = 0; i < _spawnedTiles.Count; i++)
            {
                Destroy(_spawnedTiles[i]);
            }
            _spawnedTiles?.Clear();
        }

        Vector3 pos = transform.position;
        GameObject startTile = Instantiate(_startTile, pos, Quaternion.identity, transform);
        _spawnedTiles.Add(startTile);
        
        for (int i = 0; i < _worldSize - 2; i++)
        {
            pos.z += 49;
            int y = Random.Range(0, _tiles.Count);
            GameObject obj = Instantiate(_tiles[y], pos, Quaternion.identity, transform);
            _spawnedTiles.Add(obj);
        }

        pos.z += 49;
        GameObject endTile = Instantiate(_endTile, pos, Quaternion.identity, transform);
        _spawnedTiles.Add(endTile);
    }
}