using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject instructions;

    public void OpenGameInstructions()
    {
        mainMenu.SetActive(false);
        instructions.SetActive(true);
        SoundManager.Instance.PlaySound(SoundManager.Instance.click);
    }

    public void OpenGameMenu()
    {
        mainMenu.SetActive(true);
        instructions.SetActive(false);
        SoundManager.Instance.PlaySound(SoundManager.Instance.click);
    }

    public void GotoSelectMapScene()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.click);
        SceneManager.LoadScene("SelectMap");

    }

    public void Exit()
    {

        SoundManager.Instance.PlaySound(SoundManager.Instance.click);
        Application.Quit();
    }
}
