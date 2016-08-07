using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour
{
	[SerializeField]
	private float m_SpawnDelay = 1.0f;
	[SerializeField]
	private PlatformPool m_Pool = new PlatformPool ();
	[SerializeField]
	private Platform m_PreviousPlatform = null;

	void Awake ()
	{
		m_Pool.GeneratePool ();
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
			ScalePlatform (platform);
			PositionPlatform (platform);
			m_PreviousPlatform = platform;
		}
	}

	void ScalePlatform (Platform platform)
	{
		platform.transform.localScale = new Vector3 (Random.Range (4f, 8f), Random.Range (1f, 1.25f), Random.Range (2f, 4f));
	}

	void PositionPlatform (Platform platform)
	{
		if (m_PreviousPlatform != null)
		{
			float edge = m_PreviousPlatform.transform.position.x + platform.transform.localScale.x;
			float vertical = m_PreviousPlatform.transform.position.y + platform.transform.localScale.y;

			platform.transform.position = new Vector3 (
				edge + Random.Range (2f, 2.5f),
				vertical + Random.Range (-3f, 2.25f),
				0.0f
			);
		}
		else
			platform.transform.position = new Vector3 (15f, 0.0f, 0.0f);
	}
}

