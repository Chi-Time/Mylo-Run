using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour
{
	[SerializeField]
	public float Speed = 5.0f;
	[SerializeField]
	public float CullBoundary = -15.0f;

	private Transform m_Transform = null;
	private PlatformPool m_Pool = null;

	/// Platform constructor.
	public void Initialise (PlatformPool pool)
	{
		this.gameObject.tag = "Platform";
		this.gameObject.layer = LayerMask.NameToLayer ("Ground");

		m_Transform = GetComponent <Transform> ();
		m_Pool = pool;
	}

	void Update ()
	{
		Move ();
		CheckPosition ();
	}

	void Move ()
	{
		m_Transform.Translate (Vector3.left * Speed * Time.deltaTime);
	}

	void CheckPosition ()
	{
		if (m_Transform.position.x < CullBoundary)
			DeActivate ();
	}

	void DeActivate ()
	{
		m_Pool.ReturnToPool (this);
	}
}

