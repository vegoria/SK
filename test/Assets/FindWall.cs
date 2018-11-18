using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindWall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        FindClosestWall();
    }

    void FindClosestWall()
    {
        int index = 0;
        float distanceToClosestCube = Mathf.Infinity;
        float distanceToClosestCubeTwo = Mathf.Infinity;
        Cuber closestCube = null;
        Cuber closestCubeTwo = null;
        Cuber[] allCube = GameObject.FindObjectsOfType<Cuber>();

        UnityEngine.AI.NavMeshAgent _navMeshAgent;
        _navMeshAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();


        int i = 0;
        foreach (Cuber currentCube in allCube)
        {
            float distanceToCube = (currentCube.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToCube < distanceToClosestCube)
            {
                distanceToClosestCube = distanceToCube;
                closestCube = currentCube;
                index = i;
            }
            i++;
        };
        double radius = (distanceToClosestCube * 0.01)/1.5;
        float radiusRound = Mathf.Round((float)radius * 10.0f) * 0.1f;
        Debug.Log(radiusRound);
        _navMeshAgent.radius = radiusRound;


        Cuber cubeOne = closestCube;
        /*i = 0;
        foreach (Cuber currentCube in allCube)
        {
            if (i != index)
            {
                float distanceToCube = (currentCube.transform.position - cubeOne.transform.position).sqrMagnitude;
                if (distanceToCube < distanceToClosestCubeTwo)
                {
                    distanceToClosestCubeTwo = distanceToCube;
                    closestCubeTwo = currentCube;
                }
            }
            i++;
        }
        Debug.Log(distanceToClosestCubeTwo);
        Cuber cubeTwo = closestCubeTwo;*/




        Debug.DrawLine(cubeOne.transform.position, this.transform.position);
    }
}
