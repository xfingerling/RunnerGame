using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameStateBase _currentState;
    private Dictionary<Type, GameStateBase> _statesMap;
    private Player _player;

    private void Start()
    {
        Game.OnGameInitializedEvent += OnGameInitialized;
        InitGameState();
    }

    private void OnGameInitialized()
    {
        Game.OnGameInitializedEvent -= OnGameInitialized;

        var playerInteractor = Game.GetInteractor<PlayerInteractor>();
        _player = playerInteractor.player;
        _player.OnPlayerDeathEvent += OnPlayerDeath;

        SetStateByDefault();
    }

    private void OnPlayerDeath()
    {
        SetStateDeath();
    }

    private void Update()
    {
        _currentState?.UpdateState();
    }

    #region STATE
    public void SetStateInit()
    {
        var state = GetGameState<GameStateInit>();
        SetState(state);
    }
    public void SetStateGame()
    {
        var state = GetGameState<GameStateGame>();
        SetState(state);
    }
    public void SetStateShop()
    {
        var state = GetGameState<GameStateShop>();
        SetState(state);
    }
    public void SetStateDeath()
    {
        var state = GetGameState<GameStateDeath>();
        SetState(state);
    }

    private void InitGameState()
    {
        _statesMap = new Dictionary<Type, GameStateBase>();

        CreateState<GameStateInit>();
        CreateState<GameStateGame>();
        CreateState<GameStateDeath>();
        CreateState<GameStateShop>();
    }

    private void SetState(GameStateBase newState)
    {
        if (_currentState != null)
            _currentState.Destruct();

        _currentState = newState;
        _currentState.Construct();
    }

    private void SetStateByDefault()
    {
        SetStateInit();
    }

    private GameStateBase GetGameState<T>() where T : GameStateBase
    {
        var type = typeof(T);
        return _statesMap[type];
    }

    private void CreateState<T>() where T : GameStateBase, new()
    {
        var state = new T();
        var type = typeof(T);
        _statesMap[type] = state;
    }
    #endregion
}
