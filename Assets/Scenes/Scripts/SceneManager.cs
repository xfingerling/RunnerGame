public sealed class SceneManager : SceneManagerBase
{
    public override void InitScenesMap()
    {
        sceneConfigMap[SceneConfigGame.SCENE_NAME] = new SceneConfigGame();
    }
}
