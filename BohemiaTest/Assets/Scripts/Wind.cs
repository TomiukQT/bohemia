using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public float m_Rotation;
    public float m_Strength;

    public Transform m_Arrow;

    private void Update()
    {
        UpdateArrow();
    }

    void UpdateArrow()
    {
        m_Arrow.eulerAngles = new Vector3(0, m_Rotation, 0);
    }


}
