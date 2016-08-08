using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlatformPool
{
	[SerializeField]
	[Tooltip("The total number of objects to spawn in the pool.")]
	private int m_Amount = 10;
	[SerializeField]
	[Tooltip("The default platform to spawn.")]
	private GameObject[] m_PlatformPrefabs = null;
	[SerializeField]
	private List<Platform> m_ActivePlatforms = new List<Platform> ();
	[SerializeField]
	private List<Platform> m_InactivePlatforms = new List<Platform> ();

	/// Generates the initial pool elements and ensures they are ready for use.
	public void GeneratePool (GameObject parent)
	{
		for(int i = 0; i < m_Amount; i++)
		{
			for (int j =0; j < m_PlatformPrefabs.Length; j++)
			{
				CreatePlatform (m_PlatformPrefabs[j], parent);
			}
		}
	}

	void CreatePlatform (GameObject platformToSpawm, Transform parent)
	{
		var platform = (GameObject)MonoBehaviour.Instantiate (platformToSpawm, Vector3.zero, Quaternion.identity);
		platform.transform.SetParent (parent.transform);

		var c = platform.GetComponent <Platform> ();

		if (c == null)
			Debug.LogError ("Platform prefab must have platform component attached!");
		else
		{
			c.Initialise (this);
			AddToPool (platform.GetComponent <Platform> ());
		}
	}

	/// Add's the given object to the pool and disables it for later use.
	void AddToPool (Platform platform)
	{
		platform.gameObject.SetActive (false);
		m_InactivePlatforms.Add (platform);
	}

	/// Retrieves a platform from the pool and enables it for use.
	public Platform RetrieveFromPool ()
	{
		if(m_InactivePlatforms.Count > 0)
		{
			var platform = m_InactivePlatforms [Random.Range (0, m_InactivePlatforms.Count)];
			platform.gameObject.SetActive (true);

			m_InactivePlatforms.Remove (platform);
			m_ActivePlatforms.Add (platform);

			return platform;
		}

		return null;
	}

	/// Returns the given object back to the bool and disables it for later use.
	public void ReturnToPool (Platform platform)
	{
		platform.gameObject.SetActive (false);
		m_ActivePlatforms.Remove (platform);
		m_InactivePlatforms.Add (platform);
	}
}

