using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class Sky : MonoBehaviour {
	public float DayLengthMinutes;
	public Transform Sun;
	public Material SkyBox;
	public float HourOfDay;
	public Transform MidPoint;
	public float SkyRadius = 1200;
	private float m_sunAngle;
	 float LerpTime;
	public float LerpSpeed;
	public float sunAngle
	{
		get{
			return m_sunAngle;
		}
		set {
			m_sunAngle = value;
			HourClamp();
		}
	}

	// Use this for initialization
	void Start () {
		LerpTime = 1;
	}
	void HourClamp()
	{
		if (HourOfDay <= 0)
			HourOfDay = 0;

		if (HourOfDay >= 24)
			HourOfDay = 0;
	}
	//This is setting the day in hours, and this will be able to be adjusted inside unity.

	// Update is called once per frame
	void Update () {
		float OneDay = DayLengthMinutes * 60; //In game minutes
		float OneHour = OneDay / 24;   // On day is 24 hours, in game
		HourOfDay += Time.deltaTime / OneHour;  // Combining hour of the day with the hour
		sunAngle = ((HourOfDay / 6) * 90) - 90;  // Depending on the hour in game, it will change the angle of the sun
		Sun.transform.position = Quaternion.Euler (0, 0, sunAngle) * (SkyRadius * Vector3.right);  // Setting the suns angle and postion
		Sun.transform.LookAt (MidPoint);  // This will keep the sun in position throughout the day

		if (HourOfDay > 5 && HourOfDay < 17)
		{
			LerpTime = (HourOfDay - 5) / 2;

			LerpTime = Mathf.Lerp(LerpTime,0,LerpSpeed);
		}
		// Setting the speed of how quickly the time passes, this is also adjustable in game
		else if (HourOfDay > 17)
		{
			LerpTime = Mathf.Lerp(LerpTime,1,LerpSpeed);
		}

		SkyBox.SetFloat ("_Blend", LerpTime);
	}
}
 // Blending both of the different skybox's together over the time
