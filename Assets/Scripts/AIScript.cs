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
    [SerializeField] Vector3 Offset;
    float passedtime = 0;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<Player>().transform;
        Nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(Player.position.x, transform.position.y, Player.position.z));
        if(Vector3.Distance(transform.position, Player.transform.position) < 10)
        {
            Nav.SetDestination(transform.position - transform.forward);
        }
        else
        {
            Nav.SetDestination(transform.position + transform.forward);
        }

        passedtime += Time.deltaTime;

        if (BulletCount > 0) 
        {
            if (GameManager.instance.GetPooledObject() & passedtime > GameManager.instance.AICoolDown)
            {
                switch (GameManager.instance.difficulty) 
                {
                    case GameManager.Difficulty.EASY:
                        Offset = new Vector3(Random.Range(0, 3), Random.Range(-3, 3), Random.Range(-3, 3));
                        break;
                    case GameManager.Difficulty.MEDIUM:
                        Offset = new Vector3(Random.Range(0, 2), Random.Range(-2, 2), Random.Range(-2, 2));
                        break;
                    case GameManager.Difficulty.HARD:
                        Offset = new Vector3(Random.Range(0, 1), Random.Range(-1, 1), Random.Range(-1, 1));
                        break;
                    default:
                        break;
                }
                GameObject bullet = GameManager.instance.GetPooledObject();
                bullet.SetActive(true);
                bullet.transform.position = transform.position + transform.forward;
                bullet.GetComponent<Rigidbody>().AddForce((transform.forward * 40) + Offset, ForceMode.VelocityChange);
                passedtime = 0;
                BulletCount--;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            BulletCount += Random.Range(1, 3);
        }
    }
}
