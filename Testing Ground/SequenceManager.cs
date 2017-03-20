using UnityEngine;
using System.Collections;

public class SequenceManager : MonoBehaviour {
	public CharacterTransistion CharSwitch;
	public static SequenceManager Instance;

void Awake ()
	{
		Instance = this;
	}

	public IEnumerator DoCharSwitch(Character c)
	{
		int i = 0;
		while (i < 3)
		{
			if(i == 0)
			{
				CharSwitch.Follower.height = CharSwitch.Height;
				while(Mathf.Round(CharSwitch.Follower.transform.position.y) != CharSwitch.Height)
					yield return new WaitForEndOfFrame();
			} // this is selecting the character, between the first and last, then the camera will lock onto the selected character

			if( 1 == 1)
			{
				Vector3 pos = new Vector3(c.Instance.transform.position.x,CharSwitch.Height,c.Instance.transform.position.z);
				CharSwitch.Follower.transform.position = Vector3.Lerp(CharSwitch.Follower.transform.position, pos, CharSwitch.Speed*Time.deltaTime);

				if(CharSwitch.Follower.transform.position != pos)
					yield return new WaitForEndOfFrame();
			} // this creates a new postion for the camera when the character is selected

			if(i == 2)
			{
				Camera.main.GetComponent<SmoothFollow2> ().target = c.Instance.transform;
				CharSwitch.Follower.height = 2;
				GameManager.Instance.CanShowSwitch = true;
				if((int)CharSwitch.Follower.transform.position.y != 2)
					yield return new WaitForEndOfFrame();
			} // this creates a new postion for the camera when the character is selected

			i++;
		}
		StopCoroutine ("DoCharSwitch");
		yield return null;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}

[System.Serializable]
public class CharacterTransistion
{
	public float Height;
	public float Speed;
	public SmoothFollow2 Follower;
}
