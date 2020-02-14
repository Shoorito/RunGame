using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    private int         m_nCurrentTipIndex = 0;
    private const int   m_nStageTipSize    = 30;

    public int m_nStartTipIndex                      = 0;
    public int m_nPreInstantiate                     = 0;
    public Transform    m_transCharacter             = null;
    public GameObject[] m_arStageTips                = { };
    public List<GameObject> m_listGeneratedStageList = new List<GameObject>();

    void Start()
    {
        m_nCurrentTipIndex = m_nStartTipIndex - 1;

        updateStage(m_nPreInstantiate);
    }

    void Update()
    {
        int nCharPositionIndex = (int)(m_transCharacter.position.z / m_nStageTipSize);

        if(nCharPositionIndex + m_nPreInstantiate > m_nCurrentTipIndex)
        {
            updateStage(nCharPositionIndex + m_nPreInstantiate);
        }
    }

    void updateStage(int nToTipIndex)
    {
        if (nToTipIndex <= m_nCurrentTipIndex)
            return;

        for (int nIndex = m_nCurrentTipIndex + 1; nIndex <= nToTipIndex; nIndex++)
        {
            GameObject objStageObject = generateStage(nIndex);

            m_listGeneratedStageList.Add(objStageObject);
        }

        while (m_listGeneratedStageList.Count > m_nPreInstantiate + 2)
            DestroyOldestStage();

        m_nCurrentTipIndex = nToTipIndex;
    }

    GameObject generateStage(int nIndex)
    {
        int        nNextStageTip  = 0;
        Vector3    vecStageObject = Vector3.zero;
        GameObject objStageObject = null;

        nNextStageTip    = Random.Range(0, m_arStageTips.Length);
        vecStageObject.z = nIndex * m_nStageTipSize;
        objStageObject   = (GameObject)Instantiate(m_arStageTips[nNextStageTip], vecStageObject, Quaternion.identity);

        return objStageObject;
    }

    void DestroyOldestStage()
    {
        GameObject objOldStage = m_listGeneratedStageList[0];

        m_listGeneratedStageList.RemoveAt(0);

        Destroy(objOldStage);
    }
}
