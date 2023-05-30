using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGrids : MonoBehaviour
{
    [Header("Spawn Grids Setting")]
    [SerializeField] private int _col = 10;
    [SerializeField] private int _row = 10;
    [SerializeField] private float _xSpace = 10f;
    [SerializeField] private float _zSpace = 10f;
    [SerializeField] private GameObject _grid;
    [SerializeField] private GameObject _key;
    [SerializeField] private GameObject _player;
    
    [HideInInspector] public List<GameObject> Grids = new List<GameObject>();
    [HideInInspector] public List<int> Numbers = new List<int>();

    [HideInInspector] public int Count = 0;

    private void Start()
    {
        // spawn grids
        for(int i = 0; i < _col * _row; i++)
        {
            float xPosition = _xSpace + (_xSpace * (i % _col));
            float zPosition = _zSpace + (_zSpace * (i / _col));
            GameObject grid = Instantiate(_grid, new Vector3(xPosition, 0, zPosition), Quaternion.identity) as GameObject;
            Grids.Add(grid);
        }

        // spawn the player
        SpawnPlayer();

        // spawn the key
        SpawnKey();

    }

    private void SpawnPlayer()
    {
        int number = Random.Range(0, Grids.Count);
        Vector3 position = new Vector3(Grids[number].transform.position.x, 3, Grids[number].transform.position.z);
        Instantiate(_player, position, Quaternion.identity);
        Grids.Remove(Grids[number]);
    }

    private void SpawnKey()
    {
        // spawn the key
        int number = Random.Range(0, Grids.Count);
        Vector3 position = new Vector3(Grids[number].transform.position.x, 5, Grids[number].transform.position.z);
        Instantiate(_key, position, Quaternion.identity);
        Grids[number].tag = "Key";
        Grids.Remove(Grids[number]);
    }
}
