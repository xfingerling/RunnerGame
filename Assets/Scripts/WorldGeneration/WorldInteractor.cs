using UnityEngine;

public class WorldInteractor : Interactor
{
    private LevelChunkGeneration _levelChunkGeneration;
    private SceneryChunkGeneration _sceneryChunkGeneration;

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

        _levelChunkGeneration = levelGeneration.GetComponent<LevelChunkGeneration>();
        _sceneryChunkGeneration = levelGeneration.GetComponent<SceneryChunkGeneration>();
    }

    public void UpdateLevel()
    {
        _levelChunkGeneration.UpdateLevel();
        _sceneryChunkGeneration.UpdateLevel();
    }
    public void ResetWorld()
    {
        _levelChunkGeneration.ResetWorld();
        _sceneryChunkGeneration.ResetWorld();
    }
}
