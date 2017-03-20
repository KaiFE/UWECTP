using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public WeaponType TypeOfWeapon;

	public GameObject Sparks;
	Transform SpawnPoint;

	public Transform RightHandPoint;
	public Transform LeftHandPoint;
	public Texture2D CrossHairTexture;

	public float MeleeDamage;
	public float GunDamage;
	public float BaseSpread;
	public float BaseIncreaseRate;
	public float MaxSpread;
	public float CurSpread;

	bool IsFiring;

	// Use this for initialization
	void Start () {
		CurSpread = BaseSpread;
		if (TypeOfWeapon == WeaponType.Gun) {
			SpawnPoint = GameManager.Instance.CurrentCharacter.Instance.GunSpawnPoint;
		}

	}

	// Update is called once per frame
	void Update () {
	if (TypeOfWeapon == WeaponType.Gun)
		{
			if (Input.GetMouseButtonDown (0))
			{
				Fire();
			}
		}  //This will recognoise what weapon they have picked up, aswell as it will fire when pressed right mouse in
		else if(TypeOfWeapon == WeaponType.Melee)  //This will recognoise what weapon they have picked up, aswell as it will fire when pressed right mouse in
		{

			MeleeHandler();
		}
}

	void MeleeHandler()
	{
		if (Input.GetMouseButtonDown (0))  //This will recognoise what weapon they have picked up, aswell as swing once pressed right mouse in
		{

		}
	}

	void OnCollisionEnter(Collision col)
	{
		PlayerController pc = col.collider.GetComponent<PlayerController> ();
		if(pc)
		{
			pc.TakeDamage(MeleeDamage);
		}
	}
// This registers any Collision by the melee weapon



	void Fire()
	{
		RaycastHit hit;
		if(Physics.Raycast(SpawnPoint.transform.position, Spray(), out hit, 800))
		{
			Instantiate(Sparks, hit.point,Quaternion.FromToRotation(Vector3.forward,hit.normal));
			PlayerController pc = hit.collider.transform.root.GetComponent<PlayerController>();
			if(pc) {
				pc.TakeDamage(GunDamage);
			}
		}

	} // This is the crosshair as well as the direction on where they would shoot
	Vector3 Spray()
	{
		float X = (1-2 * Random.value) * CurSpread;
		float Y = (1 - 2 * Random.value) * CurSpread;
		float Z = 1;
		return transform.root.TransformDirection (new Vector3 (X, Y, Z));

	}
	// This is the crosshair
	void OnGUI()
	{
		Vector3 screenPos = Camera.main.WorldToScreenPoint (transform.root.TransformPoint (new Vector3 (0, 0, 50)));
		float temp = CurSpread * 1000;
		GUI.DrawTexture(new Rect(screenPos.x + 5f + temp,(float)Screen.height - screenPos.y - 1f,5f,2f), CrossHairTexture);
		GUI.DrawTexture(new Rect(screenPos.x - 10f - temp,(float)Screen.height - screenPos.y - 1f,5f,2f), CrossHairTexture);
		GUI.DrawTexture (new Rect (screenPos.x - 1f, (float)Screen.height - screenPos.y - 10f - temp, 2f, 5f), CrossHairTexture);
		GUI.DrawTexture (new Rect (screenPos.x - 1f, (float)Screen.height - screenPos.y + 5f + temp, 2f, 5f), CrossHairTexture);
	}
}
// the texture of the crosshair

public enum WeaponType
{
	Gun,
	Melee
}
// Types of weapons in the game
