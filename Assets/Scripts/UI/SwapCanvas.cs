using UnityEngine;

public class SwapCanvas : MonoBehaviour {
    public GameObject m_canvas;
    
    private void Update() {
        if (Input.anyKeyDown) {
            m_canvas.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
