using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour
{
	[SerializeField]
	private float m_MinHeight, m_MaxHeight;
	[SerializeField]
	private float m_MinWidth, m_MaxWidth;
	[SerializeField]
	private float m_SpawnDelay = 0.0f;
	[SerializeField]
	private BackgroundPool m_Pool = new BackgroundPool ();

	private BackgoundObject m_PreviousObject = null;

	void Awake ()
	{
		m_Pool.GeneratePool ();
	}

	void Start ()
	{
		StartCoroutine ("SpawnObject");
	}

	IEnumerator SpawnObject ()
	{
		yield return new WaitForSeconds (m_SpawnDelay);

		var bgObject = m_Pool.RetrieveFromPool ();

		if(bgObject != null)
		{
			ScaleObject (bgObject);
			PositionObject (bgObject);
			m_PreviousObject = bgObject;
		}

		StartCoroutine ("SpawnObject");
	}

	void ScaleObject (BackgoundObject bgObject)
	{
		bgObject.transform.localScale = new Vector3 (
			Random.Range (m_MinWidth, m_MaxWidth), 
			Random.Range (m_MinHeight, m_MaxHeight), 
			bgObject.transform.localScale.z
		);
	}

	void PositionObject (BackgoundObject bgObject)
	{
		if (m_PreviousObject != null)
		{
			bgObject.transform.position = new Vector3 (
				m_PreviousObject.transform.localScale.x / 2 + bgObject.transform.position.x,
				0.0f,
				0.0f
			);
		}
		else
			bgObject.transform.position = new Vector3 (15.0f, 0.0f, 0.0f);
	}
}

