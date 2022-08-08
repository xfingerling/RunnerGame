using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return _instance; } }
    private static GameManager _instance;

    [SerializeField] private PlayerMotor _motor;

    public PlayerMotor Motor => _motor;

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
}
