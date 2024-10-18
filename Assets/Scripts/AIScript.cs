using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIScript : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] private Transform Player;
    [SerializeField] NavMeshAgent Nav;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<Player>().transform;
        Nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Draw Line from player to object
        //extend line until out of range
        //go to end of line
        transform.LookAt(Player);
        if(Vector3.Distance(transform.position, Player.transform.position) < 25)
        {
            Debug.Log("current player pos: " + Player.transform.position);
            Nav.SetDestination(transform.position-transform.forward);
        }
        else
        {
            Nav.SetDestination(transform.position);
        }
    }
}
