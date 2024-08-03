using UnityEngine;
using UnityEngine.InputSystem;

public class ActionMapChanger : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void HandleGameStateChanged(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.UI:
            playerInput.SwitchCurrentActionMap("UI");
            break;

            case GameManager.GameState.GamePlay:
            playerInput.SwitchCurrentActionMap("GamePlay");
            break;
        }
    }

    private void OnEnable()
    {
        gameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        gameManager.OnGameStateChanged -= HandleGameStateChanged;
    }
}