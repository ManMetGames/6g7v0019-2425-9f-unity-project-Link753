using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIScript : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] private Transform Player;
    [SerializeField] NavMeshAgent Nav;
    [SerializeField] int BulletCount;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<Player>().transform;
        Nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player);
        if(Vector3.Distance(transform.position, Player.transform.position) < 10)
        {
            Nav.SetDestination(transform.position - transform.forward);
        }
        else
        {
            Nav.SetDestination(transform.position + transform.forward);
        }

        if (BulletCount > 0) 
        {
            GameObject bullet = GameManager.instance.GetPooledObject();
            bullet.SetActive(true);
            bullet.transform.position = transform.position;
            bullet.GetComponent<Rigidbody>().AddForce(transform.position + transform.forward);
        }
    }
}
