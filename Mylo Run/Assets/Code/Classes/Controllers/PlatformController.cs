using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour
{
	[Tooltip("The speed at which the background scrolls along the screen.")]
	public float Speed = 0.0f;

	[SerializeField]
	[Tooltip("The range of height that a platform will be randomly spawned with.")]
	private Range m_HeightRange;
	[SerializeField]
	[Tooltip("The range of length that a platform will be randomly spawned with.")]
	private Range m_WidthRange;
	[SerializeField]
	[Tooltip("The range of depth that a platform will be randomly spawned with.")]
	private Range m_DepthRange;
	[SerializeField]
	[Tooltip("The random offset between each platform.")]
	private Range m_OffsetRange;
	[SerializeField]
	[Tooltip("The time between each object's spawn.")]
	private float m_SpawnDelay = 0.0f;
	[SerializeField]
	[Tooltip("The boundary at which to de-activate and return an object back to the pool.")]
	private float m_CullBoundary = 0.0f;
	[SerializeField]
	private PlatformPool m_Pool = new PlatformPool ();

	private Platform m_PreviousPlatform = null;

	void Awake ()
	{
		m_Pool.GeneratePool (this.gameObject);
		SpawnInitialObjects ();
	}

	void SpawnInitialObjects ()
	{
		for(int i = 0; i < 5; i++)
		{
			SpawnPlatform ();
		}
	}

	void Start ()
	{
		InvokeRepeating ("SpawnPlatform", 0.5f, m_SpawnDelay);
	}

	void SpawnPlatform ()
	{
		var platform = m_Pool.RetrieveFromPool ();

		if (platform != null) 
		{
			SetupObject (platform);
			ScalePlatform (platform);
			PositionPlatform (platform);
			m_PreviousPlatform = platform;
		}
	}

	void SetupObject (Platform platform)
	{
		platform.Speed = Speed;
		platform.CullBoundary = m_CullBoundary;
	}

	void ScalePlatform (Platform platform)
	{
		if (m_PreviousPlatform != null)
		{
			platform.transform.localScale = new Vector3 (
				Random.Range (m_WidthRange.Min, m_WidthRange.Max),
				Random.Range (m_HeightRange.Min, m_HeightRange.Max),
				Random.Range (m_DepthRange.Min, m_DepthRange.Max)
			);
		}
		else
			platform.transform.localScale = new Vector3 (8f, 1f, 2f);

	}

	void PositionPlatform (Platform platform)
	{
		if (m_PreviousPlatform != null)
		{
			float edge = m_PreviousPlatform.transform.position.x + m_PreviousPlatform.transform.localScale.x / 2 + platform.transform.localScale.x / 2;

			platform.transform.position = new Vector3 (
				edge + Random.Range (m_OffsetRange.Min, m_OffsetRange.Max),
				GetVerticalHeight (),
				0.0f
			);
		}
		else
			platform.transform.position = new Vector3 (2.0f, 0.0f, 0.0f);
	}

	float GetVerticalHeight ()
	{
		if(m_PreviousPlatform != null)
		{
			if (m_PreviousPlatform.transform.position.y > 200f)
				return Random.Range (-3f, 0);

			if (m_PreviousPlatform.transform.position.y < -15f)
				return Random.Range (0f, 3.0f);

			return m_PreviousPlatform.transform.position.y + Random.Range (-3f, 3f);
		}

		return 0.0f;
	}
}

