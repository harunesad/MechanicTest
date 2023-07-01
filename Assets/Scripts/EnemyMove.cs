using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    GameObject player;
    NavMeshAgent enemy;
    GameObject portal;
    GameObject eye;
    bool portalMove;
    public LayerMask playerLayer;
    public Soldiers soldiers;

    public enum Soldiers
    {
        Patrol,
        Soldier
    }
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        enemy = GetComponent<NavMeshAgent>();
        portal = transform.GetChild(0).gameObject;
        eye = transform.GetChild(1).gameObject;
    }
    void Update()
    {
        RaycastHit hit;
        if (Vector3.Distance(player.transform.position, transform.position) < 4 && !portalMove)
        {
            enemy.SetDestination(player.transform.position);
        }
        else if(Physics.Raycast(eye.transform.position, eye.transform.forward, out hit, 10, playerLayer) && !portalMove)
        {
            enemy.SetDestination(player.transform.position);
        }
        else
        {
            switch (soldiers)
            {
                case Soldiers.Patrol:
                    break;
                case Soldiers.Soldier:
                    StartCoroutine(PortalMove());
                    portalMove = true;
                    break;
                default:
                    break;
            }
        }
        Debug.DrawRay(eye.transform.position, eye.transform.forward * 10, Color.red);
    }
    IEnumerator PortalMove()
    {
        yield return new WaitForSeconds(2);
        portal.SetActive(true);
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
        portalMove = false;
    }
}
