using UnityEngine;

public class WorldInteractor : Interactor
{
    private LevelChunkPlacer _levelChunkPlacer;
    private SceneryChunkPlacer _sceneryChunkPlacer;

    public override void Initialize()
    {
        base.Initialize();

        Transform container = new GameObject("[World]").transform;

        GameObject splashScreenPrefab = Resources.Load<GameObject>("World/SplashScreen");
        GameObject startScenePrefab = Resources.Load<GameObject>("World/StartScene");
        GameObject snowFloorPrefab = Resources.Load<GameObject>("World/SnowFloor");
        GameObject levelGenerationPrefab = Resources.Load<GameObject>("World/LevelGeneration");

        Object.Instantiate(splashScreenPrefab, container);
        Object.Instantiate(startScenePrefab, container);
        Object.Instantiate(snowFloorPrefab, container);
        GameObject levelGeneration = Object.Instantiate(levelGenerationPrefab, container);

        _levelChunkPlacer = levelGeneration.GetComponent<LevelChunkPlacer>();
        _sceneryChunkPlacer = levelGeneration.GetComponent<SceneryChunkPlacer>();
    }

    public void UpdateLevel()
    {
        _levelChunkPlacer.UpdateLevel();
        _sceneryChunkPlacer.UpdateLevel();
    }
    public void ResetWorld()
    {
        _levelChunkPlacer.ResetWorld();
        _sceneryChunkPlacer.ResetWorld();
    }
}
