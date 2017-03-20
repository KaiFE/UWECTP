var charobj : GameObject;

function Start()  {
	yield WaitForSeconds (1.5); //duration of wait
	var position: Vector3 = Vector3(Random.Range(0,0), Random.Range(0,4), Random.Range(0,5)); // the position of which the character can spawn
	Instantiate(charobj, position, Quaternion.identity); // clone the objects
	yield WaitForSeconds (0); 
	//Destroy(gameObject);
}
