using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LootChest : MonoBehaviour {
	public int MaxItems;
	int ItemCount;
	List<Item> Items = new List<Item>();

	public int ID;

	public float Distance;

	public Color HoverColour;
	Color DefualtColour;
	bool Selected;



	public List<Item> MyItems
	{
		get
		{
			return Items;
		}

	}
	// Use this for initialization
	void Start () {
		ItemCount = Random.Range(1, MaxItems);
		for (int i = 0; i < ItemCount; i++)
		{
			int r = Random.Range(0, GameManager.Instance.AllItem.Count);
			Items.Add(GameManager.Instance.AllItem[r]);
		}
			DefualtColour = GetComponent<Renderer>().material.color;
	}
	//Randomly generating the items inside of the chest

	// Update is called once per frame
	void Update ()
	{
		if (Vector3.Distance (transform.position, GameManager.Instance.CurrentCharacter.Instance.transform.position) > Distance)
			OnMouseExit ();


		if(Selected)
		{
			if(Input.GetKeyDown(KeyCode.Escape))
				DeSelect();
		}
// Escape will close the window down


	}

	void OnMouseOver()
	{
		if(Vector3.Distance(transform.position,GameManager.Instance.CurrentCharacter.Instance.transform.position) < Distance)
		GetComponent<Renderer> ().material.color = HoverColour;
	}
// When the character is near the box it will hover a color, as well as I have set a set distance from the box

	void OnMouseExit()
	{

		GetComponent<Renderer> ().material.color = DefualtColour;
	}
	// the material colour goes back to defualt when the character is out of range

	void OnMouseDown()
	{
		if (Vector3.Distance (transform.position, GameManager.Instance.CurrentCharacter.Instance.transform.position) < Distance)
		{
			Selected = true;
			GameManager.Instance.SelectedChest = this;
		}

	}
	// When the character clicks the box, the inventory loads up
	void DeSelect()
	{
		Selected = false;
		GameManager.Instance.SelectedChest = null;
	}
//If it is not selected, it will do nothing

}
