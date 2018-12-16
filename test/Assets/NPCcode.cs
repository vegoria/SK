using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class NPCcode : MonoBehaviour
{
    //public NavMeshSurface surface;

    [SerializeField]
    Transform _destination;
    
    [SerializeField]
    bool _enablePanic;
    
    [SerializeField]
    bool _enableFalling;
    
    [SerializeField]
    float wanderRadius = 0.5f;

    [SerializeField]
    float _panicProbability = 0.05f;
    
    [SerializeField]
    float _panicTimeout = 0.5f;

    [SerializeField]
    float _fallProbability = 0.05f;
    
	float _updateInterval=0.1f;

	bool destroyed=false;
	bool alreadyInPanic = false;
	bool alreadyFallen = false;
	float panicTimer;

    float fatigue;
    float rest;
    bool zmeczyl;


    NavMeshBuildSettings _navMeshBuildSettings;
	NavMeshAgent _navMeshAgent;
	Material mat;
	GameObject gobject;
	
	// Use this for initialization
	void Start ()
    {
		
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
	mat = GetComponent<Renderer>().material;
	gobject = _navMeshAgent.gameObject;
        if (_navMeshAgent == null)
        {
            //Debug.LogError("Nav mesh not attached to " + gameObject.name);
        }
        else
        {
            _navMeshAgent.speed = Random.Range(30.0f, 60.0f); //Predkosc poczatkowa
            fatigue = Random.Range(2.0f, 6.0f) * 1000000; //Zmeczenie
            rest = Random.Range(1.0f, 2.0f) * 1000000 + fatigue; //Odpoczynek
            zmeczyl = false; //Jeszcze sie nie zmeczyl

            SetDestination();
        }
        StartCoroutine(updateCoroutine());
    }

    private void SetDestination()
    {
        if (_destination != null)
        {
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);


        }
    }
    
    private void SetRandomDestination()
    {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            _navMeshAgent.SetDestination(newPos);
    }
    
        public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
		float randX = Random.Range(-dist, dist);
		float randZ = Random.Range(-dist, dist);
		if(randX>0.0f)
			randX+=dist;
		else
			randX-=dist;
		
		if(randZ>0.0f)
			randZ+=dist;
		else
			randZ-=dist;

		Vector3 position = new Vector3(randX, 0, randZ);
		//Debug.Log("x: "+position.x+"y: "+position.y+"z: "+position.z);	
        position += origin; 
        NavMeshHit navHit; 
        NavMesh.SamplePosition (position, out navHit, dist, layermask);        
        return navHit.position;
    }
    
    void checkEscape()
    {
         if (Vector3.Distance (_destination.transform.position, transform.position) <= 50.0f)
         {
			destroyed=true;
			Destroy(gameObject);
		 }
	}
    
    IEnumerator updateCoroutine()
    {
		while(true)
		{
			if(!destroyed)
			{
				checkEscape();
				UpdateCustomPeriod();
				yield return new WaitForSeconds(_updateInterval);
			}
			else
			{
				yield break;
			}
		}		
	}
	
	// Update is called once per frame
	void Update () {
		
		 //// If the next update is reached
         //if(Time.time>=nextUpdate){
             ////Debug.Log(Time.time+">="+nextUpdate);
             //// Change the next update (current second+1)
             //nextUpdate=Mathf.FloorToInt(Time.time)+1;
             //// Call your fonction
             //UpdateCustomPeriod();
         //}
         //panicTimer+=Time.deltaTime;
	}
	
	void UpdateCustomPeriod()
	{
        if (Time.time * 1000000 > fatigue)
        {
            if (_navMeshAgent.speed > 30.0f && zmeczyl == false)
            {
                _navMeshAgent.speed = _navMeshAgent.speed - 0.2f;
                if (!alreadyInPanic && !alreadyFallen)
                {
                    mat.color = Color.blue;
                }
                else
                {
                    if (!alreadyFallen)
                    {
                        mat.color = Color.magenta;
                    }
                }
                
            }
            else
            {
                zmeczyl = true;
                if (_navMeshAgent.speed < 40.0f)
                {
                    _navMeshAgent.speed = _navMeshAgent.speed + 0.3f;
                    if (!alreadyFallen && !alreadyInPanic)
                    {
                        mat.color = Color.white;
                    }
                    
                }
            }
        }
        if (_enablePanic)
		{
			panicTimer+=_updateInterval;
			if(!alreadyInPanic && UnityEngine.Random.Range(0f,1f)<=_panicProbability && !alreadyFallen)
			{
				alreadyInPanic = true;
				panicTimer = 0.0f;
				//To something!		
				//Debug.Log("PANIIIIIIIC");		
				SetRandomDestination();
				mat.color = Color.yellow;
			
			}
			else
			{
				//Debug.Log("paniTimer: "+panicTimer+" _paniTimeout: "+_panicTimeout);
				if(panicTimer>=_panicTimeout && !alreadyFallen)
				{
					//Debug.Log("I'm calm now...");	
					alreadyInPanic=false;
                    if (zmeczyl)
                    {
                        mat.color = Color.white;

                    }
					SetDestination();
					panicTimer=0.0f;
				}
			}
		}
		if(_enableFalling)
		{
			if(!alreadyFallen && UnityEngine.Random.Range(0f,1f)<=_fallProbability)
			{
				alreadyFallen = true;
				//To something!
				_navMeshAgent.isStopped = true;
				//_navMeshAgent.velocity = Vetor3.zero;
				GameObjectUtility.SetNavMeshArea(gobject,3);		
				//Debug.Log("I'm falling... I'm falling... I'm falling...");		
				mat.color = Color.red;
			}
		}
	//	surface.BuildNavMesh();	
	}
	
}

        


