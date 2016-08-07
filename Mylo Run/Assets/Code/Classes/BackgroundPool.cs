using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BackgroundPool
{
	[SerializeField]
	[Tooltip("The total number of objects to spawn in the pool.")]
	private int m_Amount = 10;
	[SerializeField]
	[Tooltip("The default platform to spawn.")]
	private GameObject[] m_BackgroundPrefabs = null;
	[SerializeField]
	private List<BackgoundObject> m_ActiveBackgroundObjects = new List<BackgoundObject> ();
	[SerializeField]
	private List<BackgoundObject> m_InactiveBackgroundObjects = new List<BackgoundObject> ();

	/// Generates the initial pool elements and ensures they are ready for use.
	public void GeneratePool ()
	{
		var bgObjectHolder = new GameObject ("Background Object Holder");
		bgObjectHolder.transform.SetParent (GameObject.Find ("Controller").transform);

		for(int i = 0; i < m_Amount; i++)
		{
			for (int j =0; j < m_BackgroundPrefabs.Length; j++)
			{
				CreateBackgroundObject (m_BackgroundPrefabs[j]);
			}
		}
	}

	void CreateBackgroundObject (GameObject platformToSpawm)
	{
		var bgObject = (GameObject)MonoBehaviour.Instantiate (platformToSpawm, Vector3.zero, Quaternion.identity);
		bgObject.transform.SetParent (GameObject.Find ("Background Object Holder").transform);

		var c = bgObject.GetComponent <BackgoundObject> ();

		if (c == null)
			Debug.LogError ("Background Object prefab must have platform component attached!");
		else
		{
			c.Initialise (this);
			AddToPool (bgObject.GetComponent <BackgoundObject> ());
		}
	}

	/// Add's the given object to the pool and disables it for later use.
	void AddToPool (BackgoundObject bgObject)
	{
		bgObject.gameObject.SetActive (false);
		m_InactiveBackgroundObjects.Add (bgObject);
	}

	/// Retrieves a bgObject from the pool and enables it for use.
	public BackgoundObject RetrieveFromPool ()
	{
		if(m_InactiveBackgroundObjects.Count > 0)
		{
			var bgObject = m_InactiveBackgroundObjects [Random.Range (0, m_InactiveBackgroundObjects.Count)];
			bgObject.gameObject.SetActive (true);

			m_InactiveBackgroundObjects.Remove (bgObject);
			m_ActiveBackgroundObjects.Add (bgObject);

			return bgObject;
		}

		return null;
	}

	/// Returns the given object back to the bool and disables it for later use.
	public void ReturnToPool (BackgoundObject bgObject)
	{
		bgObject.gameObject.SetActive (false);
		m_ActiveBackgroundObjects.Remove (bgObject);
		m_InactiveBackgroundObjects.Add (bgObject);
	}
}

