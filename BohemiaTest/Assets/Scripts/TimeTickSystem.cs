using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeTickSystem : MonoBehaviour
{

    public class OnTickEventArgs : EventArgs
    {
        public int ticks;
    }

    public static event EventHandler<OnTickEventArgs> OnTick;
    private const float m_TICK_TIMER_MAX = 1f;
    private int m_Tick;
    private float m_TickTimer;


    void Awake()
    {
        m_Tick = 0;
    }

    void Update()
    {
        m_TickTimer += Time.deltaTime;
        if (m_TickTimer >= m_TICK_TIMER_MAX)
        {
            //m_TickTimer -= m_TICK_TIMER_MAX;
            m_TickTimer = 0;
            m_Tick++;
            // Debug.Log(m_Tick);
            if (OnTick != null)
            {
                OnTick(this, new OnTickEventArgs { ticks = m_Tick });
            }

        }
    }
    //TimeTickSystem.OnTick += Timeticksystem_onTick
    //privat void TimeTickSystem_onTick(object sender, TimeTickSystem.OnTickEventargs e )
}
