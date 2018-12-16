using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class FindWall : MonoBehaviour {
	
	float fatigue;
	float rest;
	bool zmeczyl;
	// Use this for initialization
	void Start () {
        UnityEngine.AI.NavMeshAgent _navMeshAgent;
        _navMeshAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();

        _navMeshAgent.speed = Random.Range(3.0f, 6.0f);
		
		fatigue = Random.Range(2.0f, 4.0f)*1000000; //Zmeczenie
		rest = Random.Range(1.0f, 2.0f)*1000000+fatigue; //Odpoczynek
		zmeczyl = false; //Jeszcze sie nie zmeczyl
		
    }
	
	// Update is called once per frame
	void Update () {
        
        NPCcode[] allAgents = GameObject.FindObjectsOfType<NPCcode>();
        Debug.Log(allAgents[0]);
        //var myVariable = myObject.GetComponent<NPCcode>().alreadyInPanic;
        

        UnityEngine.AI.NavMeshAgent _navMeshAgent;
        _navMeshAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
		if(Time.time*1000000 > fatigue){
			if(_navMeshAgent.speed > 1.0f && zmeczyl == false){
				_navMeshAgent.speed = _navMeshAgent.speed-0.01f;
				//Fetch the Renderer from the GameObject
				//Renderer rend = GetComponent<Renderer>();

				//Set the main Color of the Material to green
				//rend.material.shader = Shader.Find("_Color");
				//rend.material.SetColor("_Color", Color.blue);

				//Find the Specular shader and change its Color to red
				//rend.material.shader = Shader.Find("Specular");
				//rend.material.SetColor("_SpecColor", Color.red);
			}else{
				zmeczyl = true;
				if(_navMeshAgent.speed < 6.0f){
					_navMeshAgent.speed = _navMeshAgent.speed+0.02f;
					
					//Fetch the Renderer from the GameObject
					//Renderer rend = GetComponent<Renderer>();

					//Set the main Color of the Material to green
					//rend.material.shader = Shader.Find("_Color");
					//rend.material.SetColor("_Color", Color.white);

					//Find the Specular shader and change its Color to red
					//  rend.material.shader = Shader.Find("Specular");
					//rend.material.SetColor("_SpecColor", Color.red);
				}
			}
		}

        /*UnityEngine.AI.NavMeshHit hit;
        UnityEngine.AI.NavMeshAgent _navMeshAgent;
        _navMeshAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (UnityEngine.AI.NavMesh.FindClosestEdge(transform.position, out hit, 1))
        {
            double radius = (hit.distance);
            if (radius > 2)
            {
                radius = 2;
            }

            float radiusRound = Mathf.Round((float)radius * 10.0f) * 0.1f;
            _navMeshAgent.radius = radiusRound;

            FindClosestAgents();

            DrawCircle(transform.position, hit.distance, Color.red);
            Debug.DrawRay(hit.position, Vector3.up, Color.red);
        }*/
        //FindClosestWall();
    }
	


    void DrawCircle(Vector3 center, float radius, Color color)
    {
        Vector3 prevPos = center + new Vector3(radius, 0, 0);
        for (int i = 0; i < 30; i++)
        {
            float angle = (float)(i + 1) / 30.0f * Mathf.PI * 2.0f;
            Vector3 newPos = center + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
            Debug.DrawLine(prevPos, newPos, color);
            prevPos = newPos;
        }
    }

    void FindClosestAgents()
    {
        float distanceToClosestAgents = Mathf.Infinity;
        NPCcode closestAgent = null;
        NPCcode[] allAgents = GameObject.FindObjectsOfType<NPCcode>();

        UnityEngine.AI.NavMeshAgent _navMeshAgent;
        _navMeshAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();

        //Znalezienie najblizszego obiektu
        foreach (NPCcode currentAgent in allAgents)
        {
            float distanceToAgent = (currentAgent.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToAgent < distanceToClosestAgents)
            {
                distanceToClosestAgents = distanceToAgent;
                closestAgent = currentAgent;
            }
        };
        NPCcode agentOne = closestAgent;
        Debug.Log(distanceToClosestAgents);
        /*Debug.Log("###########");
       // Debug.Log(cubeOne.transform.position);

        float sizeX = Mathf.Round((float)cubeOne.transform.localScale.x * 10.0f) * 0.1f;
        float sizeZ = Mathf.Round((float)cubeOne.transform.localScale.z * 10.0f) * 0.1f;

        float x = 0;
        float z = 0;
        float distanceToClosestEdge = Mathf.Infinity;




        Vector3 clousestPossition = new Vector3();
        while (x < sizeX/2)
        {
            z = 0;
            while (z < sizeZ/2)
            {
                Vector3 newPostition = new Vector3(cubeOne.transform.position[0] + x, cubeOne.transform.position[1], cubeOne.transform.position[2] + z);
                float distanceToEdge = (newPostition - this.transform.position).sqrMagnitude;
                if (distanceToEdge < distanceToClosestCube)
                {
                    distanceToClosestEdge = distanceToEdge;
                    clousestPossition = newPostition;
                }

                z = z + 0.1f;
            }
            x = x + 0.1f;

        }
        Debug.Log(clousestPossition);
        Debug.DrawLine(clousestPossition, this.transform.position);
        */


        //Zmiana radiusa
        //double radius = (distanceToClosestCube * 0.01)/3;
        //float radiusRound = Mathf.Round((float)radius * 10.0f) * 0.1f;
        //_navMeshAgent.radius = radiusRound;




        //Debug.DrawLine(cubeOne.transform.position, this.transform.position);
    }
}
