using UnityEngine;

public class ShipShot : Shot {
    Shield m_shield;
    
    public void InitShield(Shield shield) {
        m_shield = shield;
    }
    
    protected override void OnCollisionEnter2D(Collision2D collision)  {
        if (collision.gameObject.GetComponent<Ovni>() != null) {
            m_shield.Enable();
            collision.gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
