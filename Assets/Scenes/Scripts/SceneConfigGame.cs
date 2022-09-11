using System;
using System.Collections.Generic;

public class SceneConfigGame : SceneConfig
{
    public const string SCENE_NAME = "GameTest";

    public override string sceneName => SCENE_NAME;

    public override Dictionary<Type, Interactor> CreateAllInteractors()
    {
        var interactorsMap = new Dictionary<Type, Interactor>();

        CreateInteractor<GameFlowInteractor>(interactorsMap);
        CreateInteractor<PlayerInteractor>(interactorsMap);
        CreateInteractor<WorldInteractor>(interactorsMap);
        CreateInteractor<CameraInteractor>(interactorsMap);
        CreateInteractor<UIControllerInteractor>(interactorsMap);

        return interactorsMap;
    }

    public override Dictionary<Type, Repository> CreateAllRepositories()
    {
        var repositoriesMap = new Dictionary<Type, Repository>();



        return repositoriesMap;
    }
}
