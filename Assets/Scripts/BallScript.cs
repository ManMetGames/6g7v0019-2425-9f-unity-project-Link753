using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.GetComponent<AIScript>())
        {
            GameObject.Find("Crosshair").GetComponent<TMP_Text>().color = Color.red;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.GetComponent<AIScript>())
        {
            GameObject.Find("Crosshair").GetComponent<TMP_Text>().color = Color.black;
        }
    }
}
