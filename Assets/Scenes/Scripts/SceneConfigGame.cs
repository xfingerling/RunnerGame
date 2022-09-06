using System;
using System.Collections.Generic;

public class SceneConfigGame : SceneConfig
{
    public const string SCENE_NAME = "Game";

    public override string sceneName => SCENE_NAME;

    public override Dictionary<Type, Interactor> CreateAllInteractors()
    {
        var interactorsMap = new Dictionary<Type, Interactor>();

        CreateInteractor<BankInteractor>(interactorsMap);

        return interactorsMap;
    }

    public override Dictionary<Type, Repository> CreateAllRepositories()
    {
        var repositoriesMap = new Dictionary<Type, Repository>();

        CreateRepository<BankRepository>(repositoriesMap);

        return repositoriesMap;
    }
}
