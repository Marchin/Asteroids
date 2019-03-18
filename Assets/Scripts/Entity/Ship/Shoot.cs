using UnityEngine;

public class Shoot : MonoBehaviour {
    public GameObject m_shotPrefab;
    public int m_maxShotsAtTheSameTime;
    public AudioSource m_shootSound;
    GameObject[] m_shots;
    float m_frontOffset;
    
    private void Awake() {
        m_shots = new GameObject[m_maxShotsAtTheSameTime];
        Shield shield = GetComponentInChildren<Shield>();
        for (int i = 0; i < m_maxShotsAtTheSameTime; i++) {
            GameObject go = Instantiate(m_shotPrefab);
            go.GetComponent<ShipShot>().InitShield(shield);
            go.SetActive(false);
            m_shots[i] = go;                                        
        }        
        Camera camera = Camera.main;
        m_frontOffset = GetComponent<SpriteRenderer>().bounds.extents.y;
    }
    
    private void Update() {
        if (Input.GetButtonDown("Fire")) {
            foreach (GameObject shot in m_shots) {
                if (!shot.activeInHierarchy) {
                    shot.SetActive(true);
                    shot.transform.position = 
                        transform.position + transform.up * m_frontOffset;
                    shot.transform.rotation = transform.rotation;
                    m_shootSound.Play();
                    break;
                }
            }
        }
    }
}
