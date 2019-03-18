using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {
    public EventSystem m_eventSystem;
    public GameObject m_firstSelected;
    bool m_selected;
    
    private void Start() {
        m_eventSystem.SetSelectedGameObject(m_firstSelected);
        m_selected = true;
    }
    
    private void Update() {
        if (!m_selected && Input.GetAxisRaw("Vertical") != 0) {
            m_eventSystem.SetSelectedGameObject(m_firstSelected);
            m_selected = true;
        }
    }
    
    private void OnDisable() {
        m_selected = false;
    }
}
