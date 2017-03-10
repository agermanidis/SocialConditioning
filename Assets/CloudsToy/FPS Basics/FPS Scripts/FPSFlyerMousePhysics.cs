using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FPSFlyerMousePhysics : MonoBehaviour
{
	public float fwdForce = 6;
	public float sideForce = 6;

	private Rigidbody myRigidbody;

	void Start ()
	{
		myRigidbody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate ()
	{
		myRigidbody.AddForce (transform.forward * fwdForce * Input.GetAxis ("Vertical"));
		myRigidbody.AddForce (transform.right * sideForce * Input.GetAxis ("Horizontal"));
	}

}