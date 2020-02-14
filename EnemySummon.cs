using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySummon : MonoBehaviour
{
    public GameObject m_objEnemyObject = null;

    private void Start()
    {
        GameObject objEnemy = null;

        objEnemy = (GameObject)Instantiate(m_objEnemyObject, Vector3.zero, Quaternion.identity);

        objEnemy.transform.SetParent(transform, false);
    }

    private void OnDrawGizmos()
    {
        Vector3 vecOffset = new Vector3(0.0f, 0.5f, 0.0f);

        if (m_objEnemyObject != null)
            Gizmos.DrawIcon(transform.position + vecOffset, m_objEnemyObject.name, true);

        Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);

        Gizmos.DrawSphere(transform.position + vecOffset, 0.5f);
    }
}
