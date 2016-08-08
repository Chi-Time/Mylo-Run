using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Grabber : MonoBehaviour
{
	[SerializeField]
	private float m_Speed = 2.0f;
	[SerializeField]
	private float m_Lifetime = 2.0f;

	private Rigidbody m_Rigidbody = null;

	void Awake ()
	{
		AssignReferences ();
	}

	void AssignReferences ()
	{
		m_Rigidbody = GetComponent <Rigidbody> ();
	}

	void Start ()
	{
		Destroy (this.gameObject, m_Lifetime);
	}

	void Update ()
	{
		Move ();
	}

	void Move ()
	{
		m_Rigidbody.velocity = Vector3.up * m_Speed;
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("Double"))
		{
			Destroy (other.gameObject);
			Destroy (this.gameObject);
		}
		else if (other.CompareTag("Boost"))
		{
			Destroy (other.gameObject);
			Destroy (this.gameObject);
		}
		else if (other.CompareTag ("Slow"))
		{
			Destroy (other.gameObject);
			Destroy (this.gameObject);
		}
		else if (other.CompareTag ("Fragment"))
		{
			Destroy (other.gameObject);
			Destroy (this.gameObject);
		}
	}
}

