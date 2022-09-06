using System;
using System.Collections.Generic;

public class RepositoryBase
{
    private Dictionary<Type, Repository> _repositoriesMap;
    private SceneConfig _sceneConfig;

    public RepositoryBase(SceneConfig sceneConfig)
    {
        _sceneConfig = sceneConfig;
    }

    public void CreateAllRepositories()
    {
        _repositoriesMap = _sceneConfig.CreateAllRepositories();
    }

    public void SendOnCreateToAllRepositories()
    {
        var allrepositories = _repositoriesMap.Values;

        foreach (var repository in allrepositories)
        {
            repository.OnCreate();
        }
    }

    public void InitializeAllRepositories()
    {
        var allrepositories = _repositoriesMap.Values;

        foreach (var repository in allrepositories)
        {
            repository.Initialize();
        }
    }

    public void SendOnStartToAllRepositories()
    {
        var allrepositories = _repositoriesMap.Values;

        foreach (var repository in allrepositories)
        {
            repository.OnStart();
        }
    }

    public T GetRepository<T>() where T : Repository
    {
        var type = typeof(T);
        return (T)_repositoriesMap[type];
    }
}
