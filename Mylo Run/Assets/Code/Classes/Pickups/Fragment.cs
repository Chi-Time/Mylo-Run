using UnityEngine;
using System.Collections;

public class Fragment : MonoBehaviour
{
	[SerializeField]
	[Tooltip("How many points are awarded to the player upon collection.")]
	private int m_Value = 15;

	void OnDestroy ()
	{
		GameController.Instance.IncreaseScore (m_Value);
	}
}

