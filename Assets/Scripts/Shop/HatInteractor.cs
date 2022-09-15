using System.Collections.Generic;
using UnityEngine;

public class HatInteractor : Interactor
{
    public int currentHatIndex => _repository.currentHatIndex;
    public byte[] unlockedHatFlag => _repository.unlockedHatFlag;
    public Hat[] hats;

    private HatRepository _repository;
    private List<GameObject> _hatModels;
    private Transform _hatContainer;

    public override void OnCreate()
    {
        base.OnCreate();

        _repository = Game.GetRepository<HatRepository>();

        _hatModels = new List<GameObject>();
        hats = Resources.LoadAll<Hat>("Hats");

        var playerInteractor = Game.GetInteractor<PlayerInteractor>();
        _hatContainer = playerInteractor.player.hatCointainer;
    }

    public override void OnStart()
    {
        base.OnStart();

        SpawnHats();
        SelectHat(currentHatIndex);
    }

    public void SelectHat(int index)
    {
        DisableAllHats();

        _hatModels[index].SetActive(true);
        _repository.unlockedHatFlag[index] = 1;
        _repository.currentHatIndex = index;
        _repository.Save();
    }

    private void DisableAllHats()
    {
        foreach (var hat in _hatModels)
            hat.SetActive(false);
    }

    private void SpawnHats()
    {
        for (int i = 0; i < hats.Length; i++)
        {
            _hatModels.Add(Object.Instantiate(hats[i].Model, _hatContainer));
        }
    }
}
