using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonFlick : Button  {
    public float m_flickInterval;
    TMP_Text m_text;
    BaseEventData m_baseEvent;
    float m_flickTimer;
    
    protected override void Awake() {
        m_baseEvent = null; //To stop warnigns
        base.Awake();
        m_text = GetComponentInChildren<TMP_Text>();
        m_flickTimer = m_flickInterval;
    }
    
    private void Update() {
        if (IsHighlighted(m_baseEvent)) {
            m_flickTimer -= Time.deltaTime;
            if (m_flickTimer <= 0f) {
                if (m_text.color == Color.green) {
                    m_text.color = Color.black;
                } else {
                    m_text.color = Color.green;
                }
                m_flickTimer = m_flickInterval;
            }
        } else {
            if (m_text.color == Color.black) {
                m_text.color = Color.green;
            }
        }
    }
}
