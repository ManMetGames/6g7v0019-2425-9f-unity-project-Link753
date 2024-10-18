using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] float DespawnTime;
    float passedTime, CollisionIgnoreTime;
    // Start is called before the first frame update
    void OnEnable()
    {
        passedTime = 0;
        GetComponent<Collider>().enabled = false;
    }

    private void Awake()
    {
        CollisionIgnoreTime = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        passedTime += Time.deltaTime;
        if(passedTime > CollisionIgnoreTime)
        {
            GetComponent<Collider>().enabled = true;
        }
        if (passedTime > DespawnTime) 
        {
            passedTime = 0;
            gameObject.SetActive(false);
        }
    }
}
