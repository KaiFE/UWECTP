using UnityEngine;
using System.Collections;

public class QuestObjective : MonoBehaviour {
	public ObjectiveType Type;
	public string ObjectiveName;
	public string ObjectiveDescription;

	public float GoDistance;
	Transform _trans;



	public bool IsComplete = false;


	// Use this for initialization
	void Start () {
		//_trans = Transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(GameManager.Instance.CurrentQuest != null)
		{
	if(GameManager.Instance.CurrentQuest.CurrentObjective == this)
		{
			switch (Type)
			
			{
			case ObjectiveType.Go:
				float dist = Vector3.Distance(transform.position,GameManager.Instance.CurrentCharacter.Instance.Transform.position);
				if(dist <= GoDistance)
					IsComplete = true;
				break;
						}
		}
	}
	}
}
public enum ObjectiveType
{
	Go,
	GetIn,
	Kill
}