using System.Collections.Generic;
using UnityEngine;

public class HatLogic : MonoBehaviour
{
    [SerializeField] private Transform _hatContainer;

    private Hat[] _hats;
    private List<GameObject> _hatModels = new List<GameObject>();

    private void Awake()
    {
        _hats = Resources.LoadAll<Hat>("Hats");
        SpawnHats();
        SelectHat(0);
    }

    public void SelectHat(int index)
    {
        DisableAllHats();

        _hatModels[index].SetActive(true);
    }

    private void SpawnHats()
    {
        for (int i = 0; i < _hats.Length; i++)
        {
            _hatModels.Add(Instantiate(_hats[i].Model, _hatContainer) as GameObject);
        }
    }

    private void DisableAllHats()
    {
        for (int i = 0; i < _hatModels.Count; i++)
        {
            _hatModels[i].SetActive(false);
        }
    }
}
