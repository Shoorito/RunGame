using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int         m_nNowLane      = 0;
    private const int   m_nMinLane      = -2;
    private const int   m_nMaxLane      = 2;
    private const float m_fLaneWidth    = 1.0f;
    private const float m_fStunDuration = 0.5f;

    private Vector3             m_vecMoveDirection    = Vector3.zero;
    private Animator            m_animator            = null;
    private CharacterController m_characterController = null;

    public int m_nPlayerLife      = 0;
    public float m_fSpeedX        = 0.0f;
    public float m_fSpeedZ        = 0.0f;
    public float m_fGravity       = 0.0f;
    public float m_fSpeedJump     = 0.0f;
    public float m_fAccelerationZ = 0.0f;
    public float m_fRecoverTime   = 0.0f;

    // Start is called before the first frame update
    private void Start()
    {
        m_animator            = GetComponent<Animator>();
        m_characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        float fRatioX               = 0.0f;
        float fAcceleratedZ         = 0.0f;
        Vector3 vecGlobalDirection  = Vector3.zero;

        if(isStun())
        {
            m_vecMoveDirection.x = 0.0f;
            m_vecMoveDirection.z = 0.0f;
            m_fRecoverTime      -= Time.deltaTime;
        }
        else
        {
            if (Input.GetKeyDown("left"))
                MoveToLeft();

            if (Input.GetKeyDown("right"))
                MoveToRight();

            if (Input.GetKeyDown("space"))
                Jump();

            fAcceleratedZ = m_vecMoveDirection.z + (m_fAccelerationZ * Time.deltaTime);
            m_vecMoveDirection.z = Mathf.Clamp(fAcceleratedZ, 0.0f, m_fSpeedZ);

            fRatioX = (m_nNowLane * m_fLaneWidth - transform.position.x) / m_fLaneWidth;
            m_vecMoveDirection.x = fRatioX * m_fSpeedX;
        }

        m_vecMoveDirection.y -= m_fGravity * Time.deltaTime;
        vecGlobalDirection    = transform.TransformDirection(m_vecMoveDirection);

        m_characterController.Move(vecGlobalDirection * Time.deltaTime);

        if (m_characterController.isGrounded) m_vecMoveDirection.y = 0.0f;

        m_animator.SetBool("run", m_vecMoveDirection.z > 0.0f);
    }

    private void OnControllerColliderHit(ControllerColliderHit hitTarget)
    {
        if (isStun())
            return;

        if(hitTarget.gameObject.tag == "Robo")
        {
            m_fRecoverTime = m_fStunDuration;

            m_animator.SetTrigger("damage");

            Destroy(hitTarget.gameObject);

            m_nPlayerLife--;
        }
    }

    public int getLife()
    {
        return m_nPlayerLife;
    }

    public bool isStun()
    {
        return m_fRecoverTime > 0.0f || m_nPlayerLife <= 0;
    }

    public void MoveToLeft()
    {
        if (m_characterController.isGrounded && m_nNowLane > m_nMinLane) m_nNowLane--;
    }

    public void MoveToRight()
    {
        if (m_characterController.isGrounded && m_nNowLane < m_nMaxLane) m_nNowLane++;
    }

    public void Jump()
    {
        if(m_characterController.isGrounded)
        {
            m_vecMoveDirection.y = m_fSpeedJump;

            m_animator.SetTrigger("jump");
        }
    }
}
