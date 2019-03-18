using UnityEngine;

public class ScreenWarping : MonoBehaviour {
    Camera m_camera;
    
    private void Awake() {
        m_camera = Camera.main;
    }
    private void Update() {
        float z = transform.position.z;
        Vector2 positionInScreen = m_camera.WorldToScreenPoint(transform.position);
        if (positionInScreen.x < 0f) {
            positionInScreen.x = m_camera.pixelWidth;
        } else if (positionInScreen.x > m_camera.pixelWidth) {
            positionInScreen.x = 0f;
        }
        if (positionInScreen.y < 0f) {
            positionInScreen.y = m_camera.pixelHeight;
        } else if (positionInScreen.y > m_camera.pixelHeight) {
            positionInScreen.y = 0f;
        }
        Vector3 newPos = m_camera.ScreenToWorldPoint(positionInScreen);
        newPos.z = z;
        transform.position = newPos; 
    }
    
}
