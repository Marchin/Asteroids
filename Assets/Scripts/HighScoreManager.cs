using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HighScoreManager : MonoBehaviour {
    public static HighScoreManager m_instance;
    public uint m_maxHighScores = 10;
    HighScoreData m_data;
    string m_dataPath;
    string m_json;
    
    private void Awake() {
        if (HighScoreManager.m_instance != null) {
            Destroy(gameObject);
        } else {
            HighScoreManager.m_instance = this;
            m_dataPath = Application.persistentDataPath + "/scores.json";
            if (File.Exists(m_dataPath)) {
                m_json = File.ReadAllText(m_dataPath);
                m_data = JsonUtility.FromJson<HighScoreData>(m_json);
                if (m_data.m_highScores.Length != m_maxHighScores) {
                    m_data.m_highScores = new uint[m_maxHighScores];
                }
            } else {
                CreateFile();
            }
        }
        DontDestroyOnLoad(gameObject);
    }
    
    void CreateFile() {
        m_data = new HighScoreData();
        m_data.m_highScores = new uint[m_maxHighScores];
        m_json = JsonUtility.ToJson(m_data);
        File.WriteAllText(m_dataPath, m_json);
    }
    
    
    public bool RegisterScore(uint score) {
        bool isHighScore = false;
        for (uint iScore = 0; iScore < m_maxHighScores; ++iScore) {
            if (score > m_data.m_highScores[iScore]) {
                for (uint j = iScore + 1; j < m_maxHighScores; ++j) {
                    m_data.m_highScores[j] = m_data.m_highScores[j - 1];
                }
                m_data.m_highScores[iScore] = score;
                m_json = JsonUtility.ToJson(m_data);
                File.WriteAllText(m_dataPath, m_json);
                isHighScore = true;
                break;
            }
        }
        
        return isHighScore;
    }
}
