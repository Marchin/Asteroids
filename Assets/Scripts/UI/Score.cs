using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour {
    public uint m_scorePerAsteroid;
    TMP_Text m_scoreText;
    uint m_score = 0;
    
    private void Awake() {
        m_scoreText = GetComponent<TMP_Text>();
        RefreshScoreText();
    }
    
    private void Start() {
        AsteroidManager.m_instance.m_anAsteroidWasDestroyed.AddListener(AddScore);
    }
    
    public void AddScore() {
        m_score += m_scorePerAsteroid;
        RefreshScoreText();
    }
    
    private void RefreshScoreText() {
        m_scoreText.text = string.Format("Score: {0}", m_score);
    }
}
