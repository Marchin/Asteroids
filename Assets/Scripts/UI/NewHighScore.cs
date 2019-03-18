using UnityEngine;
using TMPro;

public class NewHighScore : MonoBehaviour {
    TMP_Text m_text;
    
    private void Awake() {
        m_text = GetComponent<TMP_Text>();
    }
    
    public void SetTextScore(uint score, uint pos) {
        transform.parent.gameObject.SetActive(true);
        m_text.text = string.Format("{1}. {0}", score, pos);
    }
    
}
