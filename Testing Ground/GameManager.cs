using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {
	public List<Character> Characters = new List<Character>();
	public Character CurrentCharacter;
	public List<Item> AllItem = new List<Item> ();
	bool ShowCharWheel;
	public int SelectedCharacter;
	int LastCharacter;
	public static GameManager Instance;
	public bool CanShowSwitch = true;

	public LootChest SelectedChest;
	public Quest CurrentQuest;

	void Awake()
	{
		Instance = this;
		foreach (Character c in Characters)
		{
			GameObject go = Instantiate (c.PlayerPrefab, c.HomeSpawn.position, c.HomeSpawn.rotation)as GameObject;
			c.Instance = go.GetComponent<PlayerController>();
			c.Instance.LocalCharacter = c;

		}
		ChangeCharacterStart (Characters[PlayerPrefs.GetInt("SelectedChar")]);
	}
	// When the scene si started the camera will gfocus on the last played character

	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.C))
		{
			ShowCharWheel = true;
			Time.timeScale = 0.5f;
		}
		else
		{
			ShowCharWheel = false;
			Time.timeScale = 1f;
		}


	}
	// This is the short key for the characters, and the time scale it takes to change to each character


	void ChangeCharacterStart (Character c)
	{

		LastCharacter = SelectedCharacter;
		SelectedCharacter = Characters.IndexOf(c);
		CurrentCharacter = c;
		Characters[SelectedCharacter].Instance.GetComponent<PlayerController>().CanPlay = true;
		Characters[LastCharacter].Instance.GetComponent<PlayerController>().CanPlay = false;
		Camera.main.GetComponent<SmoothFollow2> ().target = Characters [SelectedCharacter].Instance.transform;

		PlayerPrefs.SetInt ("SelectedChar", SelectedCharacter);
	}
	// This is the ability to change character, and will focus the camera on whoever is picked







	void ChangeCharacter (Character c)
	{
		c.Instance.GetComponent<ai> ().DoneHome = false;
		if (Vector3.Distance (Characters [SelectedCharacter].Instance.transform.position, c.Instance.transform.position) > 10)
		{
			SequenceManager.Instance.StartCoroutine ("DoCharSwitch", c);
			CanShowSwitch = false;
			LastCharacter = SelectedCharacter;
			SelectedCharacter = Characters.IndexOf(c);
			CurrentCharacter = c;
			Characters[SelectedCharacter].Instance.GetComponent<PlayerController>().CanPlay = true;
			Characters[LastCharacter].Instance.GetComponent<PlayerController>().CanPlay = false;
			PlayerPrefs.SetInt ("SelectedChar", SelectedCharacter);
		}

		else {
			LastCharacter = SelectedCharacter;
			SelectedCharacter = Characters.IndexOf(c);
			CurrentCharacter = c;
			Characters[SelectedCharacter].Instance.GetComponent<PlayerController>().CanPlay = true;
			Characters[LastCharacter].Instance.GetComponent<PlayerController>().CanPlay = false;
			PlayerPrefs.SetInt ("SelectedChar", SelectedCharacter);
			Camera.main.GetComponent<SmoothFollow2> ().target = Characters [SelectedCharacter].Instance.transform;
		}



	}
	// This is changing the character


	void OnGUI()
	{
		if (ShowCharWheel & CanShowSwitch)
		{
			GUILayout.BeginArea(new Rect(Screen.width - 64, Screen.height - 217,64,230));
			foreach(Character c in Characters)
			{
				if( GUILayout.Button(c.Icon,GUILayout.Width(64),GUILayout.Height(68)))
				{
					ChangeCharacter(c);
				}
			}
			GUILayout.EndArea();
		}
	}
}
//This is the layout of the character icons



[System.Serializable]
public class Character
{
	public string Name;
	public Texture2D Icon;
	public GameObject PlayerPrefab;
	public PlayerController Instance;
	public Transform HomeSpawn;
	public bool IsInVehicle;
	public BaseVehicle CurrentVehicle;

}
[System.Serializable]
public class Item
{
	public string Name;
	public Texture2D Icon;
	public ItemInstance InstancePrefab;
	public bool Selectable;

	public static Item None
	{
		get
		{
			return new Item();
		}
	}
}
