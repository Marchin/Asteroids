using UnityEngine;
using TMPro;

public class HighScoresText : MonoBehaviour {
    TMP_Text m_text;
    uint[] m_highScores;
    
    private void Awake() {
        m_text = GetComponent<TMP_Text>();
        m_text.text = "";
    }
    
    private void Start() {
        m_highScores = HighScoreManager.m_instance.GetScores();
        for (uint iScore = 0; iScore < m_highScores.Length; ++iScore) {
            m_text.text += string.Format("{0}. {1}\n", (iScore + 1), m_highScores[iScore]);
        }
    }
}
