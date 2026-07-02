using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject SettingsPanel;

    public void ToggleMenu()
    {
        SettingsPanel.SetActive(!SettingsPanel.activeSelf);
    }
}