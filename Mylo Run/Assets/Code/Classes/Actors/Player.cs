using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
	[SerializeField]
	private float m_JumpHeight = 6.0f;
	[SerializeField]
	private LayerMask m_GroundLayers;

	private bool m_HasSecondJump = true;
	private Rigidbody m_Rigidbody = null;

	void Awake ()
	{
		SetupObject ();
	}

	void SetupObject ()
	{
		this.gameObject.tag = "Player";

		m_Rigidbody = GetComponent <Rigidbody> ();
	}

	void Update ()
	{
		CheckInput ();
	}

	void CheckInput ()
	{
		//HACK: Using keycodes. Works for now, but more customisable system should be implemented.
		if (Input.GetKeyUp (KeyCode.W) || Input.GetKeyUp (KeyCode.UpArrow))
			Jump ();
	}

	void Jump ()
	{
		if(CanJump ())
			m_Rigidbody.AddForce (Vector3.up * m_JumpHeight, ForceMode.Impulse);
	}

	bool CanJump ()
	{
		if (Physics.Linecast (transform.position, new Vector3 (transform.position.x, transform.position.y - 0.65f, 0.0f), m_GroundLayers))
		{
			m_HasSecondJump = true;
			return true;
		}

		if (m_HasSecondJump)
		{
			m_HasSecondJump = false;
			return true;
		}			

		return false;
	}
}

