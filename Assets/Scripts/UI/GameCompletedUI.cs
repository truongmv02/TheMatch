using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class GameCompletedUI : MonoBehaviour
    {
        public Text winer;
        public GameManager gameManager;

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