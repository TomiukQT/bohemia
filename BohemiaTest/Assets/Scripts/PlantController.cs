using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlantState {Base, Burning, Burnt};

public class PlantController : MonoBehaviour
{

    public Material m_StartedBurningMaterial;
    public Material m_BurningMaterial;
    public Material m_BurntMaterial;
    public PlantState m_State = PlantState.Base;

    private float m_CurrentFire = 0f;
    private float m_MaxFire = 5f;

    private bool m_IsBurning = false;
    private int m_CurrentBurning = 0;
    public int m_BurningDuration = 10;

    private float m_BurningRadius = 25f;
    private Vector3 m_BurningCenter;

    private Wind m_WindManager;
    private void Awake()
    {
        TimeTickSystem.OnTick += TimeTickSystem_OnTick;
        m_BurningCenter = transform.position;

        m_WindManager = GameObject.Find("GameManager").GetComponent<Wind>();
    }

    private void Start()
    {
        
    }

    private void TimeTickSystem_OnTick(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        //Debug.Log("TICK: " + e.ticks);
        UpdateState();
        WindCorrection();
        SpreadFire();
    }

    public void StartBurning(float amount = 1f)
    {
        if (m_State != PlantState.Base)
            return;
        m_CurrentFire+= amount;
        gameObject.GetComponent<Renderer>().material = m_StartedBurningMaterial;
        if (m_CurrentFire < m_MaxFire)
            return;
        m_IsBurning = true;
        gameObject.GetComponent<Renderer>().material = m_BurningMaterial;
        m_State = PlantState.Burning;
    }

    void UpdateState()
    {
        if(m_IsBurning)
        {
            m_CurrentBurning++;
            if(m_CurrentBurning >= m_BurningDuration)
            {
                m_IsBurning = false;
                m_State = PlantState.Burnt;
                gameObject.GetComponent<Renderer>().material = m_BurntMaterial;
            }
        }
    }

    void WindCorrection()
    {
        Vector3 correction = new Vector3(Mathf.Sin((m_WindManager.m_Rotation+90) * Mathf.Deg2Rad), 
                                         0,
                                         Mathf.Cos((m_WindManager.m_Rotation+90) * Mathf.Deg2Rad));
        m_BurningCenter = transform.position + (correction * m_WindManager.m_Strength);
    }

    void SpreadFire()
    {
        if (m_State != PlantState.Burning)
            return;
        Collider[] colliders = Physics.OverlapSphere(m_BurningCenter, m_BurningRadius, LayerMask.GetMask("Plants"));
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].gameObject.GetComponent<PlantController>().StartBurning();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_BurningCenter, m_BurningRadius);
    }

    private void OnMouseDown()
    {
        StartBurning(m_MaxFire);
    }
}
