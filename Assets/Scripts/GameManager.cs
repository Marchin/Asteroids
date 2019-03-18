using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager m_instance;
    public uint m_scorePerAsteroid;
    public UnityEvent m_scoreChanged;
    Life m_ship;
    uint m_score = 0;
    
    private void Awake() {
        if (GameManager.m_instance != null) {
            Destroy(gameObject);
        } else {
            GameManager.m_instance = this;
        }
    }
    
    void Start() {
        m_ship = FindObjectOfType<Life>();
        m_ship.m_damageTaken.AddListener(GameOver);
        AsteroidManager.m_instance.m_anAsteroidWasDestroyed.AddListener(AddScore);
    }
    
    public uint GetCurrentScore() {
        return m_score;
    }
    
    public void AddScore() {
        m_score += m_scorePerAsteroid;
        m_scoreChanged.Invoke();
    }
    
    void GameOver() {
        if (m_ship.GetLifes() <= 0) {
            HighScoreManager.m_instance.RegisterScore(m_score);
            SceneManager.LoadScene("Main Menu");
        }
    }
    
}
