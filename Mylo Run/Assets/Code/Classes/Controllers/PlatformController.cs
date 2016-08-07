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
		platform.transform.localScale = new Vector3 (Random.Range (4f, 8f), 1.0f, Random.Range (2f, 6f));
	}

	void PositionPlatform (Platform platform)
	{
		if (m_PreviousPlatform != null)
		{
			platform.transform.position = new Vector3 (
				m_PreviousPlatform.transform.position.x + Random.Range (3f, 6f),
				m_PreviousPlatform.transform.position.y + Random.Range (-3f, 3f),
				0.0f
			);
		}
		else
			platform.transform.position = new Vector3 (15f, 0.0f, 0.0f);
	}

	float GetVerticalPosition ()
	{
		int outcome = Random.Range (0, 2);

		if(outcome == 0)
			return 2.0f;
		else if (outcome == 1)
			return -2.0f;

		return GetVerticalPosition ();
	}
}

