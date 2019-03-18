using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Asteroid : MonoBehaviour {
    public float m_speed;
    public uint m_asteroidsSpawnOnDestroy;
    public AsteroidType m_astroidTypeOnDestroy;
    Vector2 m_direction;
    Rigidbody2D m_rb;
    
    private void Awake() {
        m_rb = GetComponent<Rigidbody2D>();
    }
    
    private void OnEnable() {
        AsteroidManager.m_instance.RegisterAsteroid();
        m_direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        m_direction = m_direction.normalized; 
    }
    
    private void FixedUpdate() {
        m_rb.MovePosition(m_rb.position + m_direction * m_speed * Time.fixedDeltaTime); 
    }
    
    public void Explode() {
        for (int i = 0; i < m_asteroidsSpawnOnDestroy; ++i){
            GameObject go = m_astroidTypeOnDestroy?.Request();
            go.transform.position = transform.position;
            Quaternion randomRotation = Quaternion.identity;
            randomRotation.eulerAngles = new Vector3(0f, 0f, Random.Range(0f, 360f));
            go.transform.rotation = randomRotation;
        }
        AsteroidManager.m_instance.NotifyDestruction();
        Destroy(gameObject);
    }
}
