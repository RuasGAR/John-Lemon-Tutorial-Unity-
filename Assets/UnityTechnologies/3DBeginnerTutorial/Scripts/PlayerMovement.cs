using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float turnSpeed =20f; //radianos
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //static methods -> related to the class, and not specific instances
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize(); //necessary to normalize the magnitude of the resulting vector;

        // If the horizontal variable is not near to 0, than hasHorizontal Input will be true
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);

        bool isWalking = hasHorizontalInput || hasVerticalInput;

        m_Animator.SetBool("IsWalking", isWalking);

        // Audio Stuff
        if (isWalking)
        {
            if(!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        } 
        else 
        {
            m_AudioSource.Stop();
        }

        // O deltaTime é o tempo existente entre cada frame. Multiplicar por ele é importante porque 
        // torna a biblioteca adaptável aos cenários de frame hate. Como exemplo, podemos imaginar um movimento
        // levando um segundo numa taxa, e o dobro do tempo numa taxa cortada pela metade - por exemplo.
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);

        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    private void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        // The Animator’s deltaPosition is the change in position due
        // to root motion that would have been applied to this frame. 
        m_Rigidbody.MoveRotation(m_Rotation);

    }
}
