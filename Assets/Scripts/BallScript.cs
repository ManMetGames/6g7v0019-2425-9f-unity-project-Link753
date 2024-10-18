using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] float DespawnTime;
    float passedTime;
    // Start is called before the first frame update
    void OnEnable()
    {
        passedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        passedTime += Time.deltaTime;
        if (passedTime > DespawnTime) 
        {
            passedTime = 0;
            gameObject.SetActive(false);
        }
    }
}
