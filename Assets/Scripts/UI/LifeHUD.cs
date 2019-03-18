using UnityEngine;
using UnityEngine.UI;

public class LifeHUD : MonoBehaviour {
	Image[] m_lifeImages;
    
	private void Awake() {
		m_lifeImages = GetComponentsInChildren<Image>();
		FindObjectOfType<Life>()?.m_damageTaken.AddListener(DamageTaken);
	}
	
	void DamageTaken() {
		foreach (Image img in m_lifeImages) {
			if (img.enabled) {
				img.enabled = false;
				break;
			}
		}
	}
}
