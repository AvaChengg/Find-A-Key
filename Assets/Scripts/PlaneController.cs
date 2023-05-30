using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [SerializeField] private Material _dissolve;

    private float _dissolveRate = 0.0125f;
    private float _refreshRate = 0.025f;
    private GameObject _grid;
    private SpawnGrids _spawnGrids;
    private bool _isCleared;

    private void Start()
    {
        _isCleared = true;
        _grid = GameObject.Find("Grid");
        _spawnGrids = _grid.GetComponent<SpawnGrids>();
    }

    public void RemoveGrid()
    {
        _spawnGrids.Count++;

        if (_isCleared)
        {
            _isCleared = false;
            for (int i = 0; i < _spawnGrids.Count; i++)
            {
                int number = Random.Range(0, _spawnGrids.Grids.Count);
                _spawnGrids.Numbers.Add(number);
                _spawnGrids.Grids[number].GetComponent<Renderer>().material = _dissolve;
                _spawnGrids.Grids[number].tag = "Dissolved";
            }
            StartCoroutine(DissolveGrid(_spawnGrids.Grids[_spawnGrids.Numbers[0]].GetComponent<Renderer>().material));
        }
    }

    public void ClearGrids()
    {
        if (_spawnGrids.Numbers != null)
        {
            for (int i = 0; i < _spawnGrids.Grids.Count; i++)
            {
                if (_spawnGrids.Grids[i].tag == "Dissolved")
                {
                    _spawnGrids.Grids[i].SetActive(false);
                    _spawnGrids.Grids.Remove(_spawnGrids.Grids[i]);
                }
            }
            _spawnGrids.Numbers.Clear();
            _isCleared = true;
        }
    }

    private IEnumerator DissolveGrid(Material material)
    {
        float elapseTime = 0;

        while (material.GetFloat("_DissolveAmount") < 1)
        {
            for (int i = 0; i < _spawnGrids.Numbers.Count; i++)
            {
                elapseTime += _dissolveRate;
                _spawnGrids.Grids[_spawnGrids.Numbers[i]].GetComponent<Renderer>().material.SetFloat("_DissolveAmount", elapseTime);
            }
            yield return new WaitForSeconds(_refreshRate);
        }
    }
}
