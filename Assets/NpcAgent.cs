using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NpcAgent : MonoBehaviour {
    private NavMeshAgent agent;
    private bool caught = false;
    public GameObject mas;
    public GameObject gameManager;
    private OverallGameManager ogm;
    public int count = 26;
    // Use this for initialization
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        mas.GetComponent<NavMeshAgent>().speed = 0.7f;
        ogm = gameManager.GetComponent<OverallGameManager>();
        Debug.Log("faulty OGM name " + ogm.name);
    }

    void Update()
    {
       
        if (caught)
        {
            StopCoroutine(MoveToLocation(mas.transform.position));
        }
        else{
            StartCoroutine(MoveToLocation(mas.transform.position));
        }

    }

    private IEnumerator MoveToLocation(Vector3 targetLocation)
    {
        Vector3 dir = targetLocation - agent.transform.position;
        Quaternion turn = Quaternion.LookRotation(dir);
        for (int i = 8; i > 0; i--)
        {
            Vector3 rotation = Quaternion.Slerp(agent.transform.rotation, turn, Time.deltaTime).eulerAngles;
            agent.gameObject.transform.rotation = Quaternion.Euler(0f, rotation.y * Time.deltaTime / 8, 0f);
            agent.gameObject.transform.Rotate(0f, rotation.y, 0f);
            yield return new WaitForFixedUpdate();
        }
        agent.SetDestination(targetLocation);
        //xmasAnimation.SetMovingState(1);
        //Debug.Log("move to location is called");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "Mas1") { 
            agent.isStopped = true;
            caught = true;
            ogm.SendMessage("Result", false);
            mas.GetComponent<NavMeshAgent>().speed = 0;
        }
    }
}
