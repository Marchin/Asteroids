using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipMovement : MonoBehaviour {
    public float m_acceleration;
    public float m_maxSpeed;
    public float m_drag;
    public float m_angularSpeed;
    public AudioSource m_turboSound;
    Rigidbody2D m_rb;
    Vector2 m_currSpeed;
    Animator m_anim;
    float m_currAngularSpeed;
    
    private void Awake() {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponentInChildren<Animator>();
    }
    
    private void OnEnable() {
        m_currSpeed = Vector3.zero;
    }
    
    private void FixedUpdate() {
        //TURN
        if (Input.GetAxisRaw("Turn") != 0f) {
            //subtraction because +Z turns left
            m_rb.MoveRotation(m_rb.rotation - m_angularSpeed * Input.GetAxisRaw("Turn")); 
        }
        
        //TURBO
        Vector2 forward = transform.up;
        float turbo; 
        if (Input.GetButton("Turbo")) {
            if (!m_turboSound.isPlaying) {
                m_turboSound.Play();
                m_anim.SetBool("Turbo", true);
            }
            turbo = 1f;
        } else {
            if (m_turboSound.isPlaying) {
                m_turboSound.Stop();
                m_anim.SetBool("Turbo", false);
            }
            turbo = 0f;
        }
        m_currSpeed += 
            m_acceleration*Time.fixedDeltaTime * forward * turbo;
        
        if (m_currSpeed.magnitude > m_maxSpeed) {
            m_currSpeed = m_currSpeed.normalized * m_maxSpeed;
        } else if (m_currSpeed.magnitude > 0f) {
            m_currSpeed -= m_drag * m_currSpeed;
        }
        m_rb.MovePosition(m_rb.position + m_currSpeed*Time.fixedDeltaTime);
    }
}
