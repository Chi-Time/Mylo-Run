using UnityEngine;
using System.Collections;

public class Boost : MonoBehaviour
{
	[SerializeField]
	[Tooltip("How many points are awarded to the player upon collection.")]
	private int m_Value = 50;

	void OnDestroy ()
	{
		PickupController.CurrentPickupState = PickupStates.Boosting;
	}
}

