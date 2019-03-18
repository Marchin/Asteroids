using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ovni : MonoBehaviour {    
    public GameObject m_shotPrefab;
    public float m_speed;
    public float m_shootInterval;
    public int m_maxShotsAtTheSameTime;
    public float m_offsetToEdge = 0.75f;
    public AudioSource m_shootSound;
    Rigidbody2D m_rb;
    Camera m_camera;
    Vector2 m_right;
    GameObject[] m_shots;
    int m_orientation;
    float m_shootTimer;
    
    private void Awake() {
        m_rb = GetComponent<Rigidbody2D>();
        m_right = transform.right;
        m_camera = Camera.main;
        m_shootTimer = m_shootInterval;
        m_shots = new GameObject[m_maxShotsAtTheSameTime];
        for (int i = 0; i < m_maxShotsAtTheSameTime; i++) {
            GameObject go = Instantiate(m_shotPrefab);
            go.SetActive(false);
            m_shots[i] = go;                                        
        }
    }
    
    private void OnEnable() {
        m_orientation = Random.Range(0, 2); //It starts with 0 or 1 
        //to decide which side of the screen to appear on
        Vector2 randomScreenPoint = 
            new Vector2((m_orientation <= 0)? m_camera.pixelWidth : 0f,
                        Random.Range(0f, m_camera.pixelHeight));
        m_orientation = m_orientation * 2 - 1; //We convert it from being either 0 or 1 to -1 or 1
        Vector3 randomPoint = m_camera.ScreenToWorldPoint(randomScreenPoint);
        randomPoint.z = 0f;
        transform.position = randomPoint;
    }
    
    private void Update() {
        m_shootTimer -= Time.deltaTime;
        if (m_shootTimer <= 0f) {
            foreach (GameObject shot in m_shots) {
                if (!shot.activeInHierarchy) {
                    shot.SetActive(true);
                    Vector3 direction = new Vector3(Random.Range(-1f, 1f), 
                                                    Random.Range(-1f, 1f),
                                                    0f);
                    direction = direction.normalized;
                    shot.transform.position = 
                        transform.position + direction * m_offsetToEdge;
                    shot.transform.up = direction;
                    m_shootSound.Play();
                    break;
                }
            }
            m_shootTimer = m_shootInterval;
        }
    }
    
    private void FixedUpdate() {
        m_rb.MovePosition(m_rb.position + m_right*m_orientation*m_speed*Time.fixedDeltaTime); 
        Vector2 positionInScreen = m_camera.WorldToScreenPoint(transform.position);
        if (positionInScreen.x < 0f || positionInScreen.x > m_camera.pixelWidth) {
            gameObject.SetActive(false);
        }
    }
    
}
