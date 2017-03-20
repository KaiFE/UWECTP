using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

	public Transform[] SpawnPoints;
	public float spawnTime = 1.5f;
	//public GameObject shapes;

	 public GameObject[] shapes;






	// Use this for initialization
	void Start () {

		InvokeRepeating("SpawnShapes",spawnTime,spawnTime); // Repeats the function/call

	}

	// Update is called once per frame
	void Update () {


	}

	void SpawnShape()
	{
		int spawnIndex = Random.Range (0,SpawnPoints.Length); //Sets the index number of the array randomly

		int objectIndex = Random.Range (0,SpawnPoints.Length);  //Sets the object index of the array randomly

		Instantiate(shapes[1],SpawnPoints[objectIndex].position,SpawnPoints [spawnIndex].rotation); //clones these objects by the spawn points
	}
}
