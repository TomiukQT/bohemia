using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public float m_Rotation;
    public float m_Strength;

    public Transform m_Arrow;

    public Slider m_WindSpeed;
    public Slider m_WindRotation;

    private void Update()
    {
        UpdateValues();
        UpdateArrow();
    }

    void UpdateArrow()
    {
        m_Arrow.eulerAngles = new Vector3(0, m_Rotation, 0);
    }

    void UpdateValues()
    {
        m_Rotation = m_WindRotation.value;
        m_Strength = m_WindSpeed.value;
    }


}
