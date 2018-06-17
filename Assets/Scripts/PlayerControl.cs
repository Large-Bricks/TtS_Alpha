using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControl : MonoBehaviour {

    /* Fields */
    [Header("Movement")]
    [SerializeField]
    private float acceleration = 1.0f;
    [SerializeField]
    private float friction = 0.9f;
    [SerializeField]
    private float maxSpeed = 2.0f;

    [SerializeField]
    private bool arrowKeys = false;

    [SerializeField]
    private Animator characterAnimator;

    /* Internal variables */
    private Vector3 velocity = Vector3.zero;

    /* Components */
    private Rigidbody m_rigidbody;
    
    // Use this for initialization
    void Start () {
        m_rigidbody = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
        float horizontal = 0;
        float vertical = 0;

        if (arrowKeys)
        {
            horizontal = Input.GetAxisRaw("HorizontalLR");
            vertical = Input.GetAxisRaw("VerticalUD");
        } else
        {
            horizontal = Input.GetAxisRaw("HorizontalAD");
            vertical = Input.GetAxisRaw("VerticalWS");
        }

        Vector3 input = new Vector3(horizontal, 0, vertical);
        
        velocity = Vector3.ClampMagnitude(m_rigidbody.velocity + input.normalized * acceleration, maxSpeed);
        velocity *= friction;

        m_rigidbody.velocity = new Vector3(velocity.x, m_rigidbody.velocity.y, velocity.z);
        transform.LookAt(new Vector3(transform.position.x + velocity.x, transform.position.y, transform.position.z + velocity.z));
        //m_rigidbody.rotation = Quaternion.Euler(velocity.x, 0, velocity.z);
        if (m_rigidbody.velocity.magnitude > 0.1f)
        {
            characterAnimator.SetBool("Running", true);
        } else
        {
            characterAnimator.SetBool("Running", false);
        }

	}
}
