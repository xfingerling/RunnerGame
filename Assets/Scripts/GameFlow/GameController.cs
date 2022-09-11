using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private IGameState _currentState;
    private Dictionary<Type, IGameState> _statesMap;

    private void Start()
    {
        InitGameState();
        SetStateByDefault();
    }

    private void Update()
    {
        _currentState.UpdateState(this);
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
        _statesMap = new Dictionary<Type, IGameState>();

        CreateState<GameStateInit>();
        CreateState<GameStateGame>();
        CreateState<GameStateDeath>();
        CreateState<GameStateShop>();
    }

    private void SetState(IGameState newState)
    {
        if (_currentState != null)
            _currentState.Destruct(this);

        _currentState = newState;
        _currentState.Construct(this);
    }

    private void SetStateByDefault()
    {
        SetStateInit();
    }

    private IGameState GetGameState<T>() where T : IGameState
    {
        var type = typeof(T);
        return _statesMap[type];
    }

    private void CreateState<T>() where T : IGameState, new()
    {
        var state = new T();
        var type = typeof(T);
        _statesMap[type] = state;
    }
    #endregion
}
