using UnityEngine;
using System.Collections;

public class Plane : BaseVehicle {
	public GameObject BackLeftWheel;
	public GameObject BackRightWheel;
	public GameObject FrontWheel;

	public float MaxForce;
	public float MaxVelocity;
	 float CurForce;
	 float CurVelocity;

	public float MaxTailForce;
	public float MaxTailVelocity;
	float CurTailForce;
	float CurTailVelocity;

	Transform trans_BackLeftWheel;
	Transform trans_BackRightWheel;
	Transform trans_FrontWheel;
	Transform _trans;

	float MouseX;
	float MouseY;

	float H;
	float V;
	 
	Vector3 TorqueValue;
	// Use this for in

	void Start () {
		trans_BackLeftWheel = BackLeftWheel.transform;
		trans_BackRightWheel = BackRightWheel.transform;
		trans_FrontWheel = FrontWheel.transform;
		_trans = transform;
		base.Start();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		MouseX = Input.GetAxis ("Mouse X");
		MouseY = Input.GetAxis ("Mouse Y");

		Vector3 ControlTorque = new Vector3(MouseY,1.0f, MouseX);

		TorqueValue = ControlTorque * MaxForce * CurVelocity;

		GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * MaxForce * CurVelocity);


		TorqueValue -= (Vector3.up * MaxTailForce * CurTailVelocity);

		GetComponent<Rigidbody>().AddRelativeTorque(TorqueValue);

	}

	void Update() {
		base.Update ();
		V = Input.GetAxis ("Vertical");
		H = Input.GetAxis ("Horizontal");

		CurVelocity += V * 0.001f;
		CurVelocity = Mathf.Clamp (CurVelocity, 0, 1);
		float IdleTailVelocity = (MaxForce * CurVelocity) / MaxTailForce;
		CurTailVelocity = IdleTailVelocity - H;

	}

}
