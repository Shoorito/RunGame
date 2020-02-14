using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePanel : MonoBehaviour
{
    public GameObject[] m_arrayIcons = { };

    public void updateLife(int nLife)
    {
        for(int nIndex = 0; nIndex < m_arrayIcons.Length; nIndex++)
        {
            if (nIndex < nLife)
                m_arrayIcons[nIndex].SetActive(true);
            else
                m_arrayIcons[nIndex].SetActive(false);
        }
    }
}
