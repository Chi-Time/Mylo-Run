using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour
{
	[SerializeField]
	public Range HeightRange;
	[SerializeField]
	public Range WidthRange;
	[SerializeField]
	public Range DepthRange;
	[SerializeField]
	public Range OffsetRange;
	[SerializeField]
	[Tooltip("The time between each object's spawn.")]
	private float m_SpawnDelay = 0.0f;
	[SerializeField]
	[Tooltip("The speed at which the background scrolls along the screen.")]
	private float m_ParralexSpeed = 0.0f;
	[SerializeField]
	[Tooltip("The boundary at which to de-activate and return an object back to the pool.")]
	private float m_CullBoundary = 0.0f;
	[SerializeField]
	private BackgroundPool m_Pool = new BackgroundPool ();

	private BackgoundObject m_PreviousObject = null;

	void Awake ()
	{
		m_Pool.GeneratePool (this.gameObject);
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
			bgObject.Speed = m_ParralexSpeed;
			m_PreviousObject = bgObject;
		}

		StartCoroutine ("SpawnObject");
	}

	void ScaleObject (BackgoundObject bgObject)
	{
		bgObject.transform.localScale = new Vector3 (
			Random.Range (WidthRange.Min, WidthRange.Max), 
			Random.Range (HeightRange.Min, HeightRange.Max), 
			Random.Range (DepthRange.Min, DepthRange.Max)
		);
	}

	void PositionObject (BackgoundObject bgObject)
	{
		if (m_PreviousObject != null)
		{
			float alignment = m_PreviousObject.transform.localScale.x / 2 + bgObject.transform.localScale.x / 2;

			bgObject.transform.position = new Vector3 (
				m_PreviousObject.transform.position.x + alignment + Random.Range (OffsetRange.Min, OffsetRange.Max),
				bgObject.transform.parent.position.y,
				bgObject.transform.parent.position.z
			);
		}
		else
			bgObject.transform.position = new Vector3 (25.0f, bgObject.transform.parent.position.y, bgObject.transform.parent.position.z);
	}
}

