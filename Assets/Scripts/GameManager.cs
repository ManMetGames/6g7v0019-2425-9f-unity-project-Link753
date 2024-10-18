using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] public static GameManager instance;

    [Header("GLOBAL VARIABLES")]
    [SerializeField] public float AICoolDown;
    public Difficulty difficulty;

    [Header("POOLING")]
    [SerializeField] private GameObject ObjectToPool;
    [SerializeField] List<GameObject> pooledObjects;
    [SerializeField] int poolAmount;

    public enum Difficulty
    {
        EASY,
        MEDIUM,
        HARD
    }

    // Start is called before the first frame update
    private void Start()
    {
        instance = FindObjectOfType<GameManager>();
        if (instance == null || instance == this)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if(AICoolDown == 0)
        {
            AICoolDown = 1f;
        }

        pooledObjects = new List<GameObject>();
        GameObject tmp;

        for(int i = 0; i < poolAmount; i++)
        {
            tmp = Instantiate(ObjectToPool,GameObject.Find("PooledObjects").transform);
            pooledObjects.Add(tmp);
            tmp.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}
