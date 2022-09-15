using System;
using System.Collections.Generic;

public class SceneConfigGame : SceneConfig
{
    public const string SCENE_NAME = "GameTest";

    public override string sceneName => SCENE_NAME;

    public override Dictionary<Type, Interactor> CreateAllInteractors()
    {
        var interactorsMap = new Dictionary<Type, Interactor>();

        CreateInteractor<GameControllerInteractor>(interactorsMap);
        CreateInteractor<PlayerInteractor>(interactorsMap);
        CreateInteractor<WorldInteractor>(interactorsMap);
        CreateInteractor<CameraInteractor>(interactorsMap);
        CreateInteractor<UIControllerInteractor>(interactorsMap);
        CreateInteractor<ScoreInteractor>(interactorsMap);
        CreateInteractor<BankInteractor>(interactorsMap);
        CreateInteractor<HatInteractor>(interactorsMap);

        return interactorsMap;
    }

    public override Dictionary<Type, Repository> CreateAllRepositories()
    {
        var repositoriesMap = new Dictionary<Type, Repository>();

        CreateRepository<ScoreRepository>(repositoriesMap);
        CreateRepository<BankRepository>(repositoriesMap);
        CreateRepository<HatRepository>(repositoriesMap);

        return repositoriesMap;
    }
}
