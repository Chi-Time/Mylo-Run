using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public static GameController Instance;

	private int m_Score = 0;

	void Awake ()
	{
		if (Instance == null)
			Instance = this;
	}

	public void IncreaseScore (int score)
	{
		if (PickupController.CurrentPickupState == PickupStates.Doubles)
			m_Score += score * 2;
		else
			m_Score = score;
	}
}

