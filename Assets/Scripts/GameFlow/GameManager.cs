using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return _instance; } }
    private static GameManager _instance;

    [SerializeField] private PlayerMotor _motor;
    [SerializeField] private WorldGeneration _worldGeneration;
    [SerializeField] private SceneryChunkGeneration _sceneryChunkGeneration;
    [SerializeField] private GameObject[] _cameras;

    public PlayerMotor Motor => _motor;
    public WorldGeneration WorldGeneration => _worldGeneration;
    public SceneryChunkGeneration SceneryChunkGeneration => _sceneryChunkGeneration;

    private GameState _state;

    private void Awake()
    {
        _instance = this;
        _state = GetComponent<GameStateInit>();
        _state.Construct();
    }

    private void Update()
    {
        _state.UpdateState();
    }

    public void ChangeState(GameState state)
    {
        state.Construct();
        _state = state;
        state.Destruct();
    }

    public void ChangeCamera(GameCamera camera)
    {
        foreach (var go in _cameras)
        {
            go.SetActive(false);

            _cameras[(int)camera].SetActive(true);
        }
    }
}
