﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pathfinding : MonoBehaviour {
    public Transform[] points;
    private NavMeshAgent nav;
    private int destPoint;
    // Use this for initialization
    void Start () {

        nav = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Updatepdate () {
        if (!nav.pathPending && nav.remainingDistance < 0.5f)
            GoToNextPoint();
    }

    void GoToNextPoint()
    {
        if (points.Length == 0)
        {
            return;
        }
        nav.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }
}
