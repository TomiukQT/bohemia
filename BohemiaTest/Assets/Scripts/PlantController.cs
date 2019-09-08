using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlantState {Base, Burning, Burnt};

public class PlantController : MonoBehaviour
{


    public Material m_BurningMaterial;
    public Material m_BurntMaterial;
    public PlantState m_State = PlantState.Base;

    private bool m_IsBurning = false;
    private int m_CurrentBurning = 0;
    public int m_BurningDuration = 10;

    private float m_BurningRadius = 10f;

    private void Awake()
    {
        TimeTickSystem.OnTick += TimeTickSystem_OnTick;
    }

    private void Start()
    {
        
    }

    private void TimeTickSystem_OnTick(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        //Debug.Log("TICK: " + e.ticks);
        UpdateState();
        SpreadFire();
    }

    public void StartBurning()
    {
        if (m_State != PlantState.Base)
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

    void SpreadFire()
    {
        if (m_State != PlantState.Burning)
            return;
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_BurningRadius, LayerMask.GetMask("Plants"));
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].gameObject.GetComponent<PlantController>().StartBurning();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_BurningRadius);
    }

    private void OnMouseDown()
    {
        StartBurning();
    }
}
