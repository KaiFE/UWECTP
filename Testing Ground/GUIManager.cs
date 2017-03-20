using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
	public int InventoryColumns = 7;
	public int InventoryRows = 6;
	public int ButtonWidth = 32;
	public int ButtonHeight = 32;
	public int ButtonOffset = 5;
	int _inventoryID = 0;
	int _ChestID = 1;
	Rect InventoryRect;
	public bool ShowInventory;
	Rect ChestRect;

	public Text QuestName;
	public Text ObjectiveName;
	public Text ObjectiveDescription;


	public Text QuestEndText;
	public Animator UIAnimator;


	public static GUIManager Instance;

	void Awake ()
	{
		Instance = this;
	}
	// Use this for initialization
	void Start () {
		InventoryRect = new Rect (0, 0, (5 + ButtonWidth) * InventoryColumns, (5 + ButtonHeight) * InventoryRows);
		ChestRect = new Rect (0, 0, (5 + ButtonWidth) * InventoryColumns, (5 + ButtonHeight) * InventoryRows);
	} 	// This is the rectangles displayed in the ches (loot box) and the inventory system

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.I))
			ShowInventory = !ShowInventory;
	} 	// to show inventory

	void OnGUI()
	{
		if (ShowInventory) {
			InventoryRect = GUI.Window (_inventoryID, InventoryRect, InventoryWindow, "Inventory");
			Time.timeScale = 0.5f;
		}
		else
		{
			Time.timeScale = 1;
		}

		if (GameManager.Instance.SelectedChest != null)
			ChestRect = GUI.Window(_ChestID,ChestRect,ChestWindow,"Chest");

		if (GameManager.Instance.CurrentCharacter.Instance.InHand != null)
			GUI.Box (new Rect (Screen.width - Screen.width / 7.5f, 0, Screen.width / 7.5f, Screen.height / 8.4f), new GUIContent ("Selected Item: \n" + GameManager.Instance.CurrentCharacter.Instance.InHand.Name));
		else {
			GUI.Box (new Rect (Screen.width - Screen.width / 7.5f, 0, Screen.width / 7.5f, Screen.height / 8.4f), new GUIContent ("Selected Item: \n None"));
		}
	}

	//  When the inventory is clicked, this is display width of the boxs, as well as how large an item is, inside of the box

	void InventoryWindow(int id)
	{
		int c = 0;
		int cu = 0;
		for(int x = 0; x < InventoryColumns; x++)
		{
			for(int y = 0; y < InventoryRows; y++)
			{
				if(c < GameManager.Instance.CurrentCharacter.Instance.Inventory.Count)
				{
				if(GUI.Button(new Rect(ButtonOffset + (ButtonWidth * x), ButtonOffset + (ButtonHeight * y), ButtonWidth,ButtonHeight),GameManager.Instance.CurrentCharacter.Instance.Inventory[cu].Name))
					{


						if(Event.current.button == 0) {
						if(GameManager.Instance.CurrentCharacter.Instance.Inventory[cu].Selectable)
						GameManager.Instance.CurrentCharacter.Instance.InHand = GameManager.Instance.CurrentCharacter.Instance.Inventory[cu];
					}
						else if(Event.current.button == 1) {
						if(GameManager.Instance.SelectedChest !=null)
							{
								GameManager.Instance.SelectedChest.MyItems.Add(GameManager.Instance.CurrentCharacter.Instance.Inventory[cu]);
								if(GameManager.Instance.CurrentCharacter.Instance.InHand == GameManager.Instance.CurrentCharacter.Instance.Inventory[cu])
									GameManager.Instance.CurrentCharacter.Instance.InHand = null;
								GameManager.Instance.CurrentCharacter.Instance.Inventory.Remove(GameManager.Instance.CurrentCharacter.Instance.Inventory[cu]);
							}
						}
					}

				cu++;
			}



			else
			{
					GUI.Label(new Rect(ButtonOffset + (ButtonWidth * x), ButtonOffset + (ButtonHeight * y), ButtonWidth,ButtonHeight),((x + y * InventoryColumns)+1).ToString(),"box");
			}
			c++;
					}
			    }
		GUI.DragWindow ();
	}
		//  This is the general window for the inventroy, I have made is dragable so people can chose where to have it. This is also showing the button size




	void ChestWindow(int id)
	{
		int c = 0;
		for (int x = 0; x < InventoryColumns; x++)
		{
			for (int y = 0; y < InventoryRows; y++)
			{

				if (c <= GameManager.Instance.SelectedChest.MyItems.Count-1)
					if(GUI.Button(new Rect(ButtonOffset + (ButtonWidth * x), ButtonOffset + (ButtonHeight * y), ButtonWidth,ButtonHeight), GameManager.Instance.SelectedChest.MyItems[c].Name))
				{
					GameManager.Instance.CurrentCharacter.Instance.Inventory.Add(GameManager.Instance.SelectedChest.MyItems[c]);
					GameManager.Instance.SelectedChest.MyItems.Remove(GameManager.Instance.SelectedChest.MyItems[c]);
				}
					   c++;
			}
		}
		GUI.DragWindow();
	}
}
	// This shows the items inside of the chest, and the chests position
