using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour {
    public uint m_initialLifes;
    public float m_damageInvulnerabilyDuration;
    public float m_flickInterval;
    public AudioSource m_deathSound;
    public UnityEvent m_damageTaken;
    SpriteRenderer m_sprite;
    Collider2D m_collider;
    Shield m_shield;
    uint m_currLifes;
    float m_damageInvulnerabily;
    float m_timeForFlick;
    
    private void Awake() {
        m_currLifes = m_initialLifes;
        m_collider = GetComponent<Collider2D>();
        m_sprite = GetComponent<SpriteRenderer>();
        m_shield = GetComponentInChildren<Shield>();
    }
    
    private void Update() {
        if (m_damageInvulnerabily > 0f) {
            m_damageInvulnerabily -= Time.deltaTime;
            if (m_damageInvulnerabily <= 0f) {
                Color color = m_sprite.color;
                color.a = 1f;
                m_sprite.color = color;
                m_collider.enabled = true;
            } else if (m_timeForFlick <= 0f) {
                Color color = m_sprite.color;
                color.a = (m_sprite.color.a == 0f)? 1f : 0f;
                m_sprite.color = color;
                m_timeForFlick = m_flickInterval;
            }
            m_timeForFlick -= Time.deltaTime;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision) {
        if (m_shield.IsActive()) {
            m_shield.Disable();
        } else {
            --m_currLifes;
            m_damageTaken.Invoke();
        }
        m_deathSound.Play();
        m_damageInvulnerabily = m_damageInvulnerabilyDuration;
        m_collider.enabled = false;
        if (m_currLifes <= 0) {
            Time.timeScale = 0f;
        }
    }
}
