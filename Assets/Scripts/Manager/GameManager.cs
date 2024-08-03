using UnityEngine;
using Cinemachine;
using System;
using Assets.Scripts.UI;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public event Action<GameState> OnGameStateChanged;
    private GameState currentGameState = GameState.GamePlay;
    public GameCompletedUI gameCompletedUI;
    public PauseGameUI pauseGameUI;

    private int turnCount = 1;
    private int[] playerWin;

    public Player player1;
    public Player player2;

    public Text KOText;
    public Text player1WinCount;
    public Text player2WinCount;
    public Text RoundText;

    private Map map;

    public enum GameState
    {
        UI,
        GamePlay
    }



    private void Awake()
    {
        playerWin = new int[2] { 0, 0 };
        StaticEvent.OnPlayerDie += OnPlayerDie;
        var prefab = Resources.Load<Map>("Maps/Map" + VariableGlobal.MapIndex.ToString());
        map = Instantiate(prefab);
    }

    private void Start()
    {
        SetPlayerPos();
        StartCoroutine(ShowRound());
    }

    private void OnDestroy()
    {
        StaticEvent.OnPlayerDie -= OnPlayerDie;
    }




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseGameUI.gameObject.activeSelf)
            {
                pauseGameUI.gameObject.SetActive(true);
            }
        }
    }

    private void OnPlayerDie(Player player)
    {
        if (player == null) return;
        turnCount++;
        if (player.name == "Player1")
        {
            playerWin[1]++;
            player2WinCount.text = playerWin[1].ToString();
        }
        else
        {
            playerWin[0]++;
            player1WinCount.text = playerWin[0].ToString();
        }
        StartCoroutine(ShowKOText());
        if (playerWin[0] == 2 || playerWin[1] == 2)
        {
            GameCompleted();
            return;
        }
        StartCoroutine(NextTurn());
    }

    private IEnumerator ShowRound()
    {
        RoundText.text = "Round " + turnCount.ToString();
        RoundText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        RoundText.gameObject.SetActive(false);
    }


    public IEnumerator ShowKOText()
    {
        yield return new WaitForSeconds(0.1f);
        KOText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        KOText.gameObject.SetActive(false);
    }

    public IEnumerator NextTurn()
    {
        yield return new WaitForSeconds(5f);
        player1.ResetPlayer();
        player2.ResetPlayer();
        SetPlayerPos();
        StartCoroutine(ShowRound());
    }

    private void SetPlayerPos()
    {
        player1.transform.position = map.player1SpawnPos.position;
        player2.transform.position = map.player2SpawnPos.position;
    }
    public void ChangeState(GameState state)
    {
        if (state == currentGameState) return;

        switch (state)
        {
            case GameState.UI:
            EnterUIState();
            break;
            case GameState.GamePlay:
            EnterGamePlayState();
            break;
        }

        currentGameState = state;
        OnGameStateChanged?.Invoke(currentGameState);
    }

    public void GameCompleted()
    {
        string winer = playerWin[0] > playerWin[1] ? "Player 1 Win" : "Player 2 win";
        gameCompletedUI.winer.text = winer;
        StartCoroutine(Open());
    }

    public IEnumerator Open()
    {
        yield return new WaitForSeconds(4);
        gameCompletedUI.gameObject.SetActive(true);
    }

    private void EnterUIState()
    {
        Time.timeScale = 0f;
    }

    private void EnterGamePlayState()
    {
        Time.timeScale = 1f;
    }



}
