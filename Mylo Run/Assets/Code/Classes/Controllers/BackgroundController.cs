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
		SpawnInitialObjects ();
	}

	void SpawnInitialObjects ()
	{
		for(int i = 0; i < 5; i++)
		{
			SpawnObject ();
		}
	}

	void Start ()
	{
		StartCoroutine ("NextObject");
	}

	IEnumerator NextObject ()
	{
		yield return new WaitForSeconds (m_SpawnDelay);

		SpawnObject ();

		StartCoroutine ("NextObject");
	}

	void SpawnObject ()
	{
		var bgObject = m_Pool.RetrieveFromPool ();

		if(bgObject != null)
		{
			SetupObject (bgObject);
			ScaleObject (bgObject);
			PositionObject (bgObject);
			AssignPreviousObject (bgObject);
		}
	}

	void SetupObject (BackgoundObject bgObject)
	{
		bgObject.Speed = m_ParralexSpeed;
		bgObject.CullBoundary = m_CullBoundary;
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
			// Making an alignment by getting the previous object's position then offsetting it 
			// by both the previous objects origin and the new objects origin as they are at the center of both objects.
			// This means that the object will be placed directly next to the other.
			float alignment = m_PreviousObject.transform.position.x + m_PreviousObject.transform.localScale.x / 2 + bgObject.transform.localScale.x / 2;

			bgObject.transform.position = new Vector3 (
				alignment + Random.Range (OffsetRange.Min, OffsetRange.Max),
				bgObject.transform.parent.position.y,
				bgObject.transform.parent.position.z
			);
		}
		else
			// Default position of object is no previous object exists.
			bgObject.transform.position = new Vector3 (-15.0f, bgObject.transform.parent.position.y, bgObject.transform.parent.position.z);
	}

	void AssignPreviousObject (BackgoundObject bgObject)
	{
		m_PreviousObject = bgObject;
	}
}

