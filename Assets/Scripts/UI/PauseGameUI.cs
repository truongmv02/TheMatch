using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class PauseGameUI : MonoBehaviour
    {
        public GameManager gameManager;

        public void Resume()
        {

            SoundManager.Instance.PlaySound(SoundManager.Instance.click);
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            gameManager.ChangeState(GameManager.GameState.UI);
        }

        private void OnDisable()
        {
            gameManager.ChangeState(GameManager.GameState.GamePlay);
        }
        public void Replay()
        {

            SoundManager.Instance.PlaySound(SoundManager.Instance.click);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void BackToMenu()
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.click);
            SceneManager.LoadScene("GameMenu");
        }
    }
}