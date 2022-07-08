using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    //Controls Canvas
    public CanvasGroup controlsPanel;

    private void Start() {
        controlsPanel.gameObject.SetActive(false);
    }

    public static void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public static void LoadSampleScene() {
        SceneManager.LoadScene("SampleScene");
    }

    public static void LoadGameOver() {
        SceneManager.LoadScene("GameOver");
    }

    public static void QuitGame() {
        Application.Quit();
    }

    public void ShowControlsPanel() {
        controlsPanel.gameObject.SetActive(true);
    }

    public void HideControlsPanel() {
        controlsPanel.gameObject.SetActive(false);
    }
}
