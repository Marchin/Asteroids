using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Shot : MonoBehaviour {
    public float m_speed;
    public float m_duration;
    Rigidbody2D m_rb;
    float m_durationLeft;
    
    protected virtual void Awake() {
        m_rb = GetComponent<Rigidbody2D>();
    }
    
    private void OnEnable() {
        m_durationLeft = m_duration;
    }
    
    private void Update() {
        m_durationLeft -= Time.deltaTime;
        if (m_durationLeft <= 0f) {
            gameObject.SetActive(false);
        }
    }
    
    private void FixedUpdate() {
        Vector2 up = transform.up;
        m_rb.MovePosition(m_rb.position + up * m_speed * Time.fixedDeltaTime); 
    }
    
    protected virtual void OnCollisionEnter2D (Collision2D collision) {
        gameObject.SetActive(false);
    }
}
