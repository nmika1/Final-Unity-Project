using UnityEngine;
using TMPro;
using System.Collections;

public class MissionManager : MonoBehaviour
{
    private TextMeshProUGUI missionText;
    public string message = "Hello Agent! You were sent back in time to complete a mission to rescue the princess from the enemy and come out alive with her.";

    void Awake()
    {
        missionText = GetComponent<TextMeshProUGUI>();
        missionText.enabled = false;
    }

    public void StartMission()
    {
        StartCoroutine(PlayTypewriter());
    }

    IEnumerator PlayTypewriter()
    {
        missionText.enabled = true;
        missionText.text = "";
        foreach (char c in message.ToCharArray())
        {
            missionText.text += c;
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(2f);
        missionText.enabled = false;
    }
}