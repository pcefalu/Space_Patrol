using UnityEngine;
using System.Collections;

//****************************************************************************
[System.Serializable]
public class CBoundary 
{	// Declare Data Members
	//----------------------------------------------------------
	
	public float XMin, XMax, ZMin, ZMax;
	
	//----------------------------------------------------------
}	// End of CBoundary Class


//****************************************************************************
public class CPlayerController : MonoBehaviour
{	// Declare Data Members
	//----------------------------------------------------------
	
	public float          Speed;
	public float          Tilt;

	public CBoundary      Boundary;
	public CShields       Shields;
	
	public GameObject     Shot;
	public Transform      ShotSpawn;
	public float          FireRate;
	
	private float         m_sngNextFire;
	
	
	//========================================================================
	void Update ()
	{	// Process the Fire Laser Bolt Requests
		//------------------------------------------------------
		
		if (Input.GetButton("Fire1") && Time.time > m_sngNextFire) 
		{
			m_sngNextFire = Time.time + FireRate;
			Instantiate(Shot, ShotSpawn.position, ShotSpawn.rotation);
			audio.Play ();
		}
		
		//------------------------------------------------------
	}	// End of Update Method

	
	//========================================================================
	void FixedUpdate ()
	{	// Set Shields to Full Strength
		//------------------------------------------------------
		
		float sngMoveHorizontal  = Input.GetAxis ("Horizontal");
		float sngMoveVertical    = Input.GetAxis ("Vertical");
		
//		float sngMoveHorizontal  = Input.GetAxis ("Mouse X");
//		float sngMoveVertical    = Input.GetAxis ("Mouse Y");
		
		Vector3 sngMovement      = new Vector3 (sngMoveHorizontal, 0.0f, sngMoveVertical);
		rigidbody.velocity       = sngMovement * Speed;
		
		rigidbody.position       = new Vector3
			(
				Mathf.Clamp (rigidbody.position.x, Boundary.XMin, Boundary.XMax), 
				0.0f, 
				Mathf.Clamp (rigidbody.position.z, Boundary.ZMin, Boundary.ZMax)
			);
		
		rigidbody.rotation  = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -Tilt);
		
		//------------------------------------------------------
	}	// End of FixedUpdate Method


//	//========================================================================
//	void FixedUpdate () 
//	{	// Track Ship to follow Fingure on Touch Screen
//		//----------------------------------------------------------
//		
//		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) 
//		{	// Get movement of the finger since last frame
//			//----------------------------------------------------------
//			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
//
//			float moveHorizontal  = -touchDeltaPosition.x;
//			float moveVertical    = -touchDeltaPosition.y;
//			
//			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
//			rigidbody.velocity = movement * Speed;
//			
//			rigidbody.position = new Vector3
//				(
//					Mathf.Clamp (rigidbody.position.x, Boundary.XMin, Boundary.XMax), 
//					0.0f, 
//					Mathf.Clamp (rigidbody.position.z, Boundary.ZMin, Boundary.ZMax)
//				);
//			
//			rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -Tilt);
//			
//		}
//		
//		//----------------------------------------------------------
//	}	// End of FixedUpdate Method

	
	
	
	//----------------------------------------------------------
}	// End of CPlayerController Class
