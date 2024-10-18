using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] public static GameManager instance;

    [Header("POOLING")]
    [SerializeField] private GameObject ObjectToPool;
    [SerializeField] List<GameObject> pooledObjects;
    [SerializeField] int poolAmount;

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
        for(int i = 0; i < pooledObjects.Capacity; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}
