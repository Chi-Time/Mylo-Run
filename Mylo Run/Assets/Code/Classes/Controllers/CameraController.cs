using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	[Tooltip("The target object to follow.")]
	public Transform Target;
	[Tooltip("The distance to keep from the target.")]
	public float Distance = 3.0f;
	[Tooltip("How high we are from the target.")]
	public float Height = 3.0f;
	[Tooltip("How fast we rotate to keep focus.")]
	public float Damping = 5.0f;
	[Tooltip("How fast we rotate.")]
	public float RotationDamping = 10.0f;
	[Tooltip("Are we using smooth rotation?")]
	public bool SmoothRotation = true;
	[Tooltip("Should we follow behind the target.")]
	public bool FollowBehind = true;
	[Tooltip("Should the component look for a player object?")]
	public bool FindPlayer = true;

	void Start ()
	{
		if(FindPlayer)
		{
			Target = GameObject.FindGameObjectWithTag ("Player").transform;
		}
	}

	void LateUpdate () 
	{
		if(Target != null)
		{
			Vector3 wantedPosition;

			if(FollowBehind)
			{
				wantedPosition = Target.TransformPoint (0, Height, -Distance);
			}
			else
			{
				wantedPosition = Target.TransformPoint (0, Height, Distance);
			}

			transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * Damping);

			if (SmoothRotation) 
			{
				Quaternion wantedRotation = Quaternion.LookRotation (Target.position - transform.position, Target.up);

				transform.rotation = Quaternion.Slerp (transform.rotation, wantedRotation, Time.deltaTime * RotationDamping);
			}
			else 
			{
				transform.LookAt (Target, Target.up);
			}
		}
		else 
		{
			if(FindPlayer)
			{
				Target = GameObject.FindGameObjectWithTag("Player").transform;
			}
		}
	}
}

