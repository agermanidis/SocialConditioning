#pragma warning disable 0414

using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSFlyerMouse : MonoBehaviour
{

	public float speed = 6.0f;
	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;
	private CollisionFlags flags;

	void Start ()
	{
		controller = GetComponent<CharacterController> ();
	}

	void FixedUpdate ()
	{
		moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
		moveDirection = transform.TransformDirection (moveDirection);
		moveDirection *= speed;

		flags = controller.Move (moveDirection * Time.deltaTime);
	}

}

