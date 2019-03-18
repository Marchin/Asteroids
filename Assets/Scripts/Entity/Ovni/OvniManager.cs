using UnityEngine;

public class OvniManager : MonoBehaviour {
    public GameObject m_ovniPrefab;
    public float m_spawnInterval;
    [Range(0f, 100f)] public float m_spawnIntervalVariance;
    GameObject m_ovni;
    float m_intervalTimer;
    
    private void Awake() {
        m_ovni = Instantiate(m_ovniPrefab);
        m_ovni.SetActive(false);
        SetTimer();
    }
    
    private void Update() {
        m_intervalTimer -= Time.deltaTime;
        if (m_intervalTimer <= 0f) {
            if (!m_ovni.activeInHierarchy) {
                m_ovni.SetActive(true);
            } else {
                SetTimer();
            }
        }
    }
    
    private void SetTimer() {
        m_intervalTimer = 
            (1 + Random.Range(-m_spawnIntervalVariance, m_spawnIntervalVariance) * 0.01f) * m_spawnInterval;
    }
}
