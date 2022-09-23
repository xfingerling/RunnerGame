using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    private Canvas _canvas;
    private void Awake()
    {
        _canvas = GetComponentInParent<Canvas>();
    }

    private void Start()
    {
        Game.sceneManager.OnSceneLoadedEvent += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene obj)
    {
        Game.sceneManager.OnSceneLoadedEvent -= OnSceneLoaded;

        _canvas.gameObject.SetActive(false);
    }
}
