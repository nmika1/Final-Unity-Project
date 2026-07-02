using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject blurBackground;
    public GameObject menuPanel;
    public GameObject gameUI;

    void Start()
    {
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;

        if (blurBackground != null) blurBackground.SetActive(false);
        if (menuPanel != null) menuPanel.SetActive(false);
        if (gameUI != null) gameUI.SetActive(true);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}