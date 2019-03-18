using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager m_instance;
    public uint m_scorePerAsteroid;
    public NewHighScore m_newHighScore;
    public UnityEvent m_scoreChanged;
    Life m_ship;
    uint m_score = 0;
    bool m_gameOver;
    
    private void Awake() {
        if (GameManager.m_instance != null) {
            Destroy(gameObject);
        } else {
            GameManager.m_instance = this;
            m_gameOver = false;
        }
    }
    
    private void Start() {
        m_ship = FindObjectOfType<Life>();
        m_ship.m_damageTaken.AddListener(GameOver);
        AsteroidManager.m_instance.m_anAsteroidWasDestroyed.AddListener(AddScore);
    }
    
    private void Update() {
        if (m_gameOver && Input.anyKeyDown) {
            SceneManager.LoadScene("Main Menu");
        }
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
            m_gameOver = true;
            uint pos;
            if (HighScoreManager.m_instance.RegisterScore(m_score, out pos)) {
                m_newHighScore.SetTextScore(m_score, pos + 1);
                AsteroidManager.m_instance.enabled = false;
            } else {
                SceneManager.LoadScene("Main Menu");
            }
        }
    }
    
}
