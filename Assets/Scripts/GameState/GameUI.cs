using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Timer gameTimer;

    public EndGame endGamePanel;
    public float timeRemaining;
    private bool isGameOver = false;
    
    public IEnumerator UpdateTimerCoroutine()
    {
        while (GameManager.Instance.isGameStarted)
        {
            UpdateTimerText();
            yield return new WaitForSeconds(1f);
            timeRemaining--;
        }
    }
    public void StartGame()
    {
        StartCoroutine(UpdateTimerCoroutine());
        gameTimer.gameObject.SetActive(true);
    }
    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        gameTimer.timerText.text = $"{minutes:D2}:{seconds:D2}";

        if (timeRemaining == 0)
        {
            TriggerVictory();
        }
    }
    public void TriggerVictory()
    {
        isGameOver = true;
        ShowUI(endGamePanel, true);
        Time.timeScale = 0; 
    }

    public void TriggerDefeat()
    {
        isGameOver = true;
        ShowUI(endGamePanel, false);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    private void ShowUI(EndGame endGamePanel, bool winCondition)
    {
        endGamePanel.gameObject.SetActive(true);
        if (winCondition)
        {
            endGamePanel.endGameText.text = "You Win!";
        }
        else
        {
            endGamePanel.endGameText.text = "You Lose!";
        }
    }

    private void HideUI(EndGame endGamePanel)
    {
        endGamePanel.gameObject.SetActive(false);
    }
 
}
