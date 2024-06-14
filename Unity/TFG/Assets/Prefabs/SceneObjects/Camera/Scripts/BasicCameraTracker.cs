﻿using UnityEngine;
using System.Collections;
//--------------------------------------------------------------------
//Follows the player along the 2d plane, using a continuous lerp
//--------------------------------------------------------------------
public class BasicCameraTracker : MonoBehaviour {
    [SerializeField] GameObject m_Target = null;
    [SerializeField] float m_InterpolationFactor = 0.0f;
    [SerializeField] bool m_UseFixedUpdate = false;
    [SerializeField] public float m_ZDistance = 10.0f;
    [SerializeField] public float m_YOffset = 2.0f;
    public int targetFrameRate;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }
    void FixedUpdate () 
	{
        if (m_UseFixedUpdate)
        {
            Interpolate(Time.fixedDeltaTime);
        }
	}

    void LateUpdate()
    {
        if (!m_UseFixedUpdate)
        {
            Interpolate(Time.deltaTime);
        }
    }

    void Interpolate(float a_DeltaTime)
    {
        if (m_Target == null)
        {
            return;
        }
        //Vector3 diff = m_Target.transform.position + Vector3.back * m_ZDistance - transform.position;
        //transform.position += diff * m_InterpolationFactor * a_DeltaTime;
        Vector3 targetPosition = m_Target.transform.position;
        targetPosition.y += m_YOffset; // Ajusta la posición en el eje Y
        Vector3 diff = targetPosition + Vector3.back * m_ZDistance - transform.position;
        transform.position += diff * m_InterpolationFactor * a_DeltaTime;
    
    }
}
