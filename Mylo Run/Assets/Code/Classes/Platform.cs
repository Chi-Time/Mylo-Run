using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour
{
	[SerializeField]
	private float m_Speed = 5.0f;
	[SerializeField]
	private float m_CullBoundary = -15.0f;

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
		m_Transform.Translate (Vector3.left * m_Speed * Time.deltaTime);
	}

	void CheckPosition ()
	{
		if (m_Transform.position.x < m_CullBoundary)
			DeActivate ();
	}

	void DeActivate ()
	{
		m_Pool.ReturnToPool (this);
	}
}

