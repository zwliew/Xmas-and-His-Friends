﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class XmasAgent : MonoBehaviour
{

    private NavMeshAgent agent;
    private AnimationController xmasAnimation;
    public GameObject mas;
    // Use this for initialization
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        xmasAnimation = GetComponentInChildren<AnimationController>();

    }

    private void Update()
    {

        if ((agent.transform.position - agent.destination).magnitude < 0.3f && xmasAnimation)
        {
            xmasAnimation.SetMovingState(0);
        }
        else
        {
            xmasAnimation.SetMovingState(1);
        }

    }

    public IEnumerator MoveToLocation(Vector3 targetLocation)
    {
        Vector3 dir = targetLocation - mas.transform.position;
        Quaternion turn = Quaternion.LookRotation(dir);

        for (int i = 8; i > 0; i--)
        {
            Vector3 rotation = Quaternion.Slerp(mas.transform.rotation, turn, Time.deltaTime).eulerAngles;
            mas.transform.rotation = Quaternion.Euler(0f, rotation.y * Time.deltaTime / 8, 0f);
            mas.transform.Rotate(0f, rotation.y, 0f);
            yield return new WaitForFixedUpdate();
        }
        agent.SetDestination(targetLocation);
        //xmasAnimation.SetMovingState(1);
        agent.isStopped = false;
        //Debug.Log("move to location is called");

    }
}
