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
        //Gen circle around player
        //pick closest point on circle
        //go to that circle
        if(Vector3.Distance(transform.position, Player.transform.position) < 25)
        {
            Nav.SetDestination(transform.GetChild(0).position);
        }
        else
        {
            Nav.SetDestination(transform.position);
        }
    }
}
