using UnityEngine;
using System.Collections;

public class DoublePoints : MonoBehaviour
{
	[SerializeField]
	[Tooltip("How many points are awarded to the player upon collection.")]
	private int m_Value = 100;

	void OnDestroy ()
	{
		PickupController.CurrentPickupState = PickupStates.Doubles;
		GameController.Instance.IncreaseScore (m_Value);
	}
}

