using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
public class Quest : MonoBehaviour {
	public string QuestName;
	public string Description;
	public List<QuestObjective> Objectives = new List<QuestObjective>();

	public QuestObjective CurrentObjective;
	
	public bool IsComplete;
	public string CharacterName;
	public bool IsInProgress;
	
	
	public Quest Predecessor;
	public Quest Successor;
	
	// Use this for initialization
	void Start () {
		gameObject.SetActive(CanPlay());
		
	}
	public bool CanPlay()
	{
		if(!IsComplete)
		{
			if(Predecessor != null)
			{
				if(Predecessor.IsComplete)
					return true;
				else
					return false;
			}
			return true;
		}
		else
		{
			return false;
		}
		
	}
	public void OnStartQuest()
	{
		IsInProgress = true;
		IsComplete = false;
		GameManager.Instance.CurrentQuest = this;
		CurrentObjective = Objectives[0];

		GUIManager.Instance.QuestName.text = QuestName;
		GUIManager.Instance.ObjectiveName.text = CurrentObjective.ObjectiveName;
		GUIManager.Instance.ObjectiveDescription.text = CurrentObjective.ObjectiveDescription;


		GUIManager.Instance.QuestName.gameObject.SetActive(true);
		GUIManager.Instance.ObjectiveName.gameObject.SetActive(true);
		GUIManager.Instance.ObjectiveDescription.gameObject.SetActive(true);

	}
	public void OnCompleteQuest()
	{
		IsInProgress = false;
		IsComplete = true;
		GameManager.Instance.CurrentQuest = null;
		GUIManager.Instance.QuestEndText.text = QuestName;




		GUIManager.Instance.QuestName.gameObject.SetActive(false);
		GUIManager.Instance.ObjectiveName.gameObject.SetActive(false);
		GUIManager.Instance.ObjectiveDescription.gameObject.SetActive(false);
		GUIManager.Instance.UIAnimator.SetTrigger("QuestEnd");

	}
	// Update is called once per frame
	void Update () 
	{
		if(GameManager.Instance.CurrentQuest == this)
		{
			if(CurrentObjective.IsComplete == true)
			{
				int CurObjIndex = Objectives.IndexOf(CurrentObjective);
				if ( CurObjIndex == Objectives.Count - 1)
					OnCompleteQuest();
				else
				{
					CurrentObjective = Objectives[CurObjIndex + 1];
					CurrentObjective.IsComplete = false;

					//GUIManager.Instance.QuestName.text = CurrentObjective.name;
					GUIManager.Instance.ObjectiveName.text = CurrentObjective.ObjectiveName;
					GUIManager.Instance.ObjectiveDescription.text = CurrentObjective.ObjectiveDescription;


				}
			}
		}
	}

	void OnTriggerEnter ( Collider col )
	{
		if(CanPlay())
		{
			if(CharacterName == "All" || GameManager.Instance.CurrentCharacter.Name == CharacterName);
			{
				if(!IsInProgress && !IsComplete)
					OnStartQuest();
			}
		}
	}
}
