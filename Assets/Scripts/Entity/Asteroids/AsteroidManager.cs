using UnityEngine;
using UnityEngine.Events;

public class AsteroidManager : MonoBehaviour {
    public static AsteroidManager m_instance;
    public AsteroidType m_asteroidType;
    public uint m_initCount;
    public uint m_safeDistance;
    public AudioSource m_explosionSound;
    public UnityEvent m_anAsteroidWasDestroyed;
    Transform m_ship;
    uint m_count;
    
    private void Awake() {
        m_ship = FindObjectOfType<ShipMovement>().transform;
        if (AsteroidManager.m_instance != null) {
            Destroy(gameObject);
        } else {
            AsteroidManager.m_instance = this;
        }
    }
    
    private void Start() {
        SetupLevel();
    }
    
    private void SetupLevel() {
        for (uint i = 0; i < m_initCount; ++i) {
            Camera camera = Camera.main;
            GameObject go = m_asteroidType.Request();
            Vector2 randomScreenPoint = new Vector2(Random.Range(0f, camera.pixelWidth),
                                                    Random.Range(0f, camera.pixelHeight));
            Vector3 randomPoint = camera.ScreenToWorldPoint(randomScreenPoint);
            randomPoint.z = 0f;
            Vector3 safeDistanceVector = randomPoint - m_ship.position;
            if (safeDistanceVector.magnitude < m_safeDistance) {
                safeDistanceVector = safeDistanceVector.normalized;
                randomPoint = m_ship.position + safeDistanceVector * m_safeDistance;
            }
            Quaternion randomRotation = Quaternion.identity;
            randomRotation.eulerAngles = new Vector3(0f, 0f, Random.Range(0f, 360f));
            go.transform.position = randomPoint;
            go.transform.rotation = randomRotation;
        }
    }
    
    public void RegisterAsteroid() {
        ++m_count;
    }
    
    public void NotifyDestruction() {
        m_anAsteroidWasDestroyed.Invoke();
        --m_count;
        m_explosionSound.Play();
        if (m_count <= 0) {
            ++m_initCount;
            SetupLevel();
        }
    }
}
