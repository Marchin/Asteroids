using UnityEngine;

public class Shield : MonoBehaviour {
    SpriteRenderer m_sprite;
    
    private void Awake() {
        m_sprite = GetComponent<SpriteRenderer>();
        m_sprite.enabled = false;
    }
    
    public void Enable() {
        m_sprite.enabled = true;
    }
    
    public void Disable() {
        m_sprite.enabled = false;
    }
    
    public bool IsActive() {
        return m_sprite.enabled;
    }
}
