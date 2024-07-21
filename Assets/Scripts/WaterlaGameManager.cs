using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterlaGameManager : MonoBehaviour
{
    public static WaterlaGameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;

    [SerializeField] private int EnemiesDeadOnDemand;

    private enum State
    {
        WaitingForStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    [SerializeField] private State state;
    [SerializeField] private float waitingForStartTimer = 2f;
    [SerializeField] private float countdownToStartTimer = 3f;
    [SerializeField] private float gamePlayingTimer = 10f;

    private bool isGamePaused;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("There's more than one WaterlaGameManager" + transform + "-" + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingForStart:
                waitingForStartTimer -= Time.deltaTime;
                if (waitingForStartTimer < 0f)
                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
               // OnStateChanged?.Invoke(this, EventArgs.Empty);
                break;
        }

        if (InputManager.Instance.GetPauseGameThisFrame())
        {
            TogglePauseGame();
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStart()
    {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartAmount()
    {
        return countdownToStartTimer;
    }
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }
    public int GetEnemiesDeadOnDemand()
    {
        return EnemiesDeadOnDemand;
    }
    public float GetGamePlayingTimer()
    {
        return gamePlayingTimer;
    }
    public bool IsGamePaused()
    {
        return isGamePaused;
    }
    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;    
        if (isGamePaused )
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnPaused?.Invoke(this, EventArgs.Empty);

        }
    }
}
