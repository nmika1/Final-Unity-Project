using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject difficultyMenu;

    public void ToggleSettings()
    {
        bool isActive = !settingsPanel.activeSelf;
        settingsPanel.SetActive(isActive);
        if (isActive) difficultyMenu.SetActive(false);
    }

    public void OpenDifficulty()
    {
        settingsPanel.SetActive(false);
        difficultyMenu.SetActive(true);
    }

    public void BackToSettings()
    {
        difficultyMenu.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseAll()
    {
        settingsPanel.SetActive(false);
        difficultyMenu.SetActive(false);
    }

    public void SetDifficulty(string level)
    {
        if (GameSettings.instance != null)
        {
            GameSettings.instance.ApplyDifficulty(level);

            EnemyStats[] allEnemies = FindObjectsByType<EnemyStats>(FindObjectsSortMode.None);
            foreach (EnemyStats enemy in allEnemies)
            {
                enemy.UpdateStats();
            }
        }
    }
}