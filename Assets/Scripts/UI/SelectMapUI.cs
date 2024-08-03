

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectMapUI : MonoBehaviour
{

    public void ChooseMap(int map)
    {
        VariableGlobal.MapIndex = map;
        SceneManager.LoadScene("GamePlay");
        SoundManager.Instance.PlaySound(SoundManager.Instance.click);
    }

    public void GoToMainMenu()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.click);
        SceneManager.LoadScene("GameMenu");
    }
}
