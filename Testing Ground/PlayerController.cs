using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour {
	public Animator  Anim;
	public bool CanPlay;
	public Character LocalCharacter;
	public LootChest SelectedChest;
	public Transform Hand;

	public QuestObjective KillObjective;

	public Transform GunSpawnPoint;
	public Transform Transform;

	public float Health;

	public bool IsAlive;

	private List<Item> _inventory = new List<Item>();

	public float v;
	public float h;

	public List<Item> Inventory
	{
		get {
			return _inventory;
		}
	}

	private Item _InHand;

	public Item InHand
	{
		get
		{
			return _InHand;
		}
		set
		{
			_InHand = value;
			Destroy(InHandInstance);
			if(value != null)
			{
				Weapon w = value.InstancePrefab.GetComponent<Weapon>();
				if(w) {
					if(w.TypeOfWeapon == WeaponType.Gun) {

				InHandInstance = Instantiate(value.InstancePrefab.gameObject, GunSpawnPoint.position, GunSpawnPoint.rotation) as GameObject;
				InHandInstance.transform.parent = GunSpawnPoint;
			}
					else
					{
						InHandInstance = Instantiate(value.InstancePrefab.gameObject, Hand.position, Hand.rotation) as GameObject;
						InHandInstance.transform.parent = Hand;
					}
			}
				else
				{
					InHandInstance = Instantiate(value.InstancePrefab.gameObject, Hand.position, Hand.rotation) as GameObject;
					InHandInstance.transform.parent = Hand;
				}
			}
			}
		}
		// This will spawn and recgognise what Item is in the hand, and what abilities the object has, it also shpawns where the  empty gameobject is


	private GameObject InHandInstance;

	// Use this for initialization
	void Start () {
		Transform = transform;

		Health = 100;
		IsAlive = true;
		SetRigidBodys(true);

	} // health of the character

	// Update is called once per frame
	void Update () {
		if (CanPlay) {
			v = Input.GetAxis ("Vertical");
			h = Input.GetAxis ("Horizontal");
			Anim.SetFloat ("Speed", v * 2);
			Anim.SetFloat ("Direction", h);
			Anim.SetBool ("Running", Input.GetKey (KeyCode.LeftShift)); // Character Sprinting

			if (Input.GetKeyDown (KeyCode.U)) {
				if (IsAlive)
					TakeDamage (1000); // Instant dieing ability
				else
					Revive (); //Once U is clicked again, you will spawn back
			}
		} else {
			Anim.SetFloat ("Speed", v);
			Anim.SetFloat ("Direction", h);
			Anim.SetBool ("Running", false);


		}

		if (Input.GetKeyDown (KeyCode.F)) {
			if (!LocalCharacter.IsInVehicle) {

				RaycastHit hit;
				if (Physics.Raycast (transform.position + new Vector3 (0, 1, 0), transform.forward, out hit, 5)) {
					if (hit.collider.transform.root.CompareTag ("Vehicle")) {
						LocalCharacter.IsInVehicle = true;
						transform.parent = hit.collider.transform.root.GetComponent<BaseVehicle>().transform;
						hit.collider.transform.root.GetComponent<BaseVehicle> ().GetInVehicle ();
						hit.collider.transform.root.GetComponent<BaseVehicle> ().Driver = LocalCharacter;
						LocalCharacter.CurrentVehicle = hit.collider.transform.root.GetComponent<BaseVehicle> ();
						Camera.main.GetComponent<SmoothFollow2> ().target = hit.collider.transform;
					}

				}
			}
		}
	}
// This is trying to get into the vehicles, using raycast and colliders









	void OnAnimatorIK()
	{
		if(Anim != null)
		{
			if (InHandInstance != null)
			{
				Weapon w = InHandInstance.GetComponent<Weapon>();

				if(w != null && w.TypeOfWeapon == WeaponType.Gun)
				{
					Anim.SetIKPositionWeight(AvatarIKGoal.LeftHand,1);
					Anim.SetIKPositionWeight(AvatarIKGoal.RightHand,1);

					Anim.SetIKRotationWeight(AvatarIKGoal.LeftHand,1);
					Anim.SetIKRotationWeight(AvatarIKGoal.RightHand,1);

					Anim.SetIKPosition(AvatarIKGoal.LeftHand,w.LeftHandPoint.position);
					Anim.SetIKRotation(AvatarIKGoal.LeftHand,w.LeftHandPoint.rotation);

					Anim.SetIKPosition(AvatarIKGoal.RightHand,w.RightHandPoint.position);
					Anim.SetIKRotation(AvatarIKGoal.RightHand,w.RightHandPoint.rotation);

				}
			}
		}
	} // Creating spawns for the weapoons

	public void TakeDamage(float damage)
	{
		if (IsAlive) {
			Health -= damage;
			if (Health <= 0)
				Die ();
		}
	}
	//The amount of damage, if it = 0 you die


	void Die()
	{
		IsAlive = false;

		if(KillObjective)
			KillObjective.IsComplete = true;
		Anim.enabled = false;
		SetRigidBodys (false);

	} // This will judge if someone has died

	void Revive()
	{
		IsAlive = true;
		Anim.enabled = true;
		SetRigidBodys (true);
	} // Coming back to life

	void SetRigidBodys(bool active){
		Rigidbody[] temp = GetComponentsInChildren<Rigidbody> ();
		foreach (Rigidbody r in temp)
		{
			r.isKinematic = active;
			}

		}
	} // this is so the otehr characters can be shot
