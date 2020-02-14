using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    private Vector3 m_vecDiff     = Vector3.zero;

    public float m_fFollowSpeed   = 0.0f;
    public GameObject m_objTarget = null;

    void Start()
    {
        m_vecDiff = m_objTarget.transform.position - transform.position;    
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp
        (
            transform.position,
            m_objTarget.transform.position - m_vecDiff,
            Time.deltaTime * m_fFollowSpeed
        );
    }
}
