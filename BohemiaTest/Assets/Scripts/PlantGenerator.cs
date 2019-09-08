using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGenerator : MonoBehaviour
{
    public int m_AmountToGenerate = 1000;

    public GameObject m_Plant;

    private Terrain m_Terrain;

    private GameObject m_PlantsParent;

    private void Awake()
    {
        m_Terrain = GameObject.Find("Terrain").GetComponent<Terrain>();
    }

    private void Start()
    {
        Generate();
    }

    void Generate()
    {
        if (m_PlantsParent != null)
        {
            Destroy(m_PlantsParent);
        }
        m_PlantsParent = new GameObject("PlantsParent");
        for (int i = 0; i < m_AmountToGenerate; i++)
        {
            float x = Random.Range(0f, m_Terrain.terrainData.size.x);
            float z = Random.Range(0f, m_Terrain.terrainData.size.z);
            float y = m_Terrain.SampleHeight(new Vector3(x, 0, z));
            y += m_Plant.transform.localScale.y/2;
            GameObject spawned = Instantiate(m_Plant, new Vector3(x, y, z), Quaternion.identity);
            spawned.transform.SetParent(m_PlantsParent.transform);

        }

    }


}
