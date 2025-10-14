using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;
    public int enemiesKilled = 0;
    public int currentLevel = 1;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
        enemiesKilled++;
        if (enemiesKilled >= 100)
        {
            currentLevel++;
            enemiesKilled = 0;
            // Cargar siguiente nivel, escena o subir dificultad
            Debug.Log("Subiendo a nivel " + currentLevel);
        }
    }
}