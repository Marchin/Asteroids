using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour {
    public uint m_scorePerAsteroid;
    TMP_Text m_scoreText;
    
    private void Start() {
        m_scoreText = GetComponent<TMP_Text>();
        GameManager.m_instance.m_scoreChanged.AddListener(RefreshScoreText);
        RefreshScoreText();
    }
    
    private void RefreshScoreText() {
        m_scoreText.text = string.Format("Score: {0}", 
                                         GameManager.m_instance.GetCurrentScore());
    }
}
