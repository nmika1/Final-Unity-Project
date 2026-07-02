using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings instance;
    public float enemySpeed = 3f;
    public int enemyHealth = 2;
    public float attackSpeed = 1.5f;
    public float attackRange = 20f;
    public float detectionRange = 5f;
    public int dropAmount = 5;
    public int coinAmount = 5;
    public string difficulty = "Easy";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            ApplyDifficulty(difficulty);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ApplyDifficulty(string level)
    {
        difficulty = level;

        if (level == "Easy")
        {
            enemySpeed = 2f;
            enemyHealth = 1;
            attackSpeed = 2f;
            attackRange = 35f;
            detectionRange = 8f;
            dropAmount = 2;
            coinAmount = 5;
        }
        else if (level == "Normal")
        {
            enemySpeed = 3f;
            enemyHealth = 1;
            attackSpeed = 1.5f;
            attackRange = 40f;
            detectionRange = 10f;
            dropAmount = 2;
            coinAmount = 4;
        }
        else if (level == "Hard")
        {
            enemySpeed = 5f;
            enemyHealth = 1;
            attackSpeed = 1f;
            attackRange = 45f;
            detectionRange = 12f;
            dropAmount = 3;
            coinAmount = 3;
        }
    }
}