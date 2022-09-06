using System.Collections;
using UnityEngine;

public class Scene
{
    private InteractorsBase _interactorsBase;
    private RepositoryBase _repositoriesBase;
    private SceneConfig _sceneConfig;

    public Scene(SceneConfig sceneConfig)
    {
        _sceneConfig = sceneConfig;
        _interactorsBase = new InteractorsBase(sceneConfig);
        _repositoriesBase = new RepositoryBase(sceneConfig);
    }

    public Coroutine InitializeAsync()
    {
        return Coroutines.StartRoutine(InitializeRoutine());
    }

    private IEnumerator InitializeRoutine()
    {
        _interactorsBase.CreateAllInteractors();
        _repositoriesBase.CreateAllRepositories();
        yield return null;

        _interactorsBase.SendOnCreateToAllInteractors();
        _repositoriesBase.SendOnCreateToAllRepositories();
        yield return null;

        _interactorsBase.InitializeAllInteractors();
        _repositoriesBase.InitializeAllRepositories();
        yield return null;

        _interactorsBase.SendOnStartToAllInteractors();
        _repositoriesBase.SendOnStartToAllRepositories();
    }

    public T GetRepository<T>() where T : Repository
    {
        return _repositoriesBase.GetRepository<T>();
    }

    public T GetInteractor<T>() where T : Interactor
    {
        return _interactorsBase.GetInteractor<T>();
    }
}
