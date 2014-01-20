using System.Threading;
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
public class CPlayerController : Photon.MonoBehaviour
{	// Declare Data Members
	//----------------------------------------------------------
	
	public float             Speed;
	public float             Tilt;

	public CBoundary         Boundary;
	public CShields          Shields;
	
	public GameObject        Shot;
	public Transform         ShotSpawn;
	public float             FireRate;
	
	private float            m_sngNextFire  = 0;
	private float            m_sngHAxis     = 0;
	private float            m_sngVAxis     = 0;

	private CGameController  m_oGameController;

	
	//========================================================================
	void Start ()
	{	// Declare Variables
		//------------------------------------------------------
		
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		
		if (gameControllerObject != null)
		{
			m_oGameController = gameControllerObject.GetComponent <CGameController>();
		}
		
		if (m_oGameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
		
		//------------------------------------------------------
	}	// End of Start Method
	
	
	//========================================================================
	void Update ()
	{	// Process the Fire Laser Bolt Requests
		//------------------------------------------------------
		
		if (Input.GetButton("Fire1") && Time.time > m_sngNextFire) 
		{
			FireLaser();
		}
		
		//------------------------------------------------------
	}	// End of Update Method
	

	//========================================================================
	void OnTriggerEnter (Collider other)
	{	// Declare Variables
		//------------------------------------------------------
		
		if (other.tag != "Boundary" && other.tag != "Enemy")
		{	// Process Request
			//--------------------------------------------------
			if(gameObject.tag == "Player_1" || gameObject.tag == "Player_2")
			{
				if (other.tag == "Player_1" || other.tag == "Player_2")
				{
					m_oGameController.DestroyBothShips();
				}
				else
				{
					m_oGameController.DestroyShip(other.tag, gameObject);
				}
			}
			else
			{
				m_oGameController.DestroyShip(gameObject.tag, other.gameObject);
			}
		}

		//------------------------------------------------------
	}	// End of OnTriggerEnter Method
	
	
	//========================================================================
	public void FireLaser ()
	{	// Process the Fire Laser Bolt Requests
		//------------------------------------------------------

		if (gameObject.tag == "Player" || gameObject.tag == "Player_1") 
		{	// Local Player Processing
			//--------------------------------------------------
			m_sngNextFire = Time.time + FireRate;
			Instantiate (Shot, ShotSpawn.position, ShotSpawn.rotation);
			audio.Play ();
		}
		else 
		{	// Network Player Processing
			//--------------------------------------------------
			
		}

		//------------------------------------------------------
	}	// End of FireLaser Method
	
	
	//========================================================================
	public float NextFire ()
	{	// Process the Fire Laser Bolt Requests
		//------------------------------------------------------
		
		return m_sngNextFire;
		
		//------------------------------------------------------
	}	// End of NextFire Method
	
	
	//========================================================================
	void FixedUpdate ()
	{	// 
		//------------------------------------------------------
		
		MoveShip ();

		//------------------------------------------------------
	}	// End of FixedUpdate Method


	//========================================================================
	public void MoveShipHorizontal (float sngValue)
	{	// 
		//------------------------------------------------------
		
		m_sngHAxis = sngValue;

		//------------------------------------------------------
	}	// End of MoveShipHorizontal Method
	
	
	//========================================================================
	public void MoveShipVertical (float sngValue)
	{	// 
		//------------------------------------------------------
		
		m_sngVAxis = sngValue;

		//------------------------------------------------------
	}	// End of MoveShipVertical Method
	
	
	//========================================================================
	public void MoveShip ()
	{	// 
		//------------------------------------------------------
		
		
		if (gameObject.tag == "Player" || gameObject.tag == "Player_1") 
		{	// Local Player Processing
			//--------------------------------------------------
			float sngMoveHorizontal = InputGetAxis ("Horizontal");
			float sngMoveVertical = InputGetAxis ("Vertical");

			Vector3 sngMovement = new Vector3 (sngMoveHorizontal, 0.0f, sngMoveVertical);
			rigidbody.velocity = sngMovement * Speed;

			rigidbody.position = new Vector3
				(
					Mathf.Clamp (rigidbody.position.x, Boundary.XMin, Boundary.XMax), 
					0.0f, 
					Mathf.Clamp (rigidbody.position.z, Boundary.ZMin, Boundary.ZMax)
				);
			
			rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -Tilt);
		} 
		else 
		{	// Network Player Processing
			//--------------------------------------------------

		}
		
		//------------------------------------------------------
	}	// End of MoveShip Method
	
	
	//========================================================================
	public float InputGetAxis(string strAxis)
	{	// 
		//------------------------------------------------------
		
		float sngMovement = Input.GetAxis(strAxis);
		
		if (Mathf.Abs(sngMovement) > 0.005) 
			return sngMovement;

		if (strAxis == "Horizontal")
			return m_sngHAxis;

		if (strAxis == "Vertical")
			return m_sngVAxis;
		
		return sngMovement;

		//------------------------------------------------------
	}	// End of InputGetAxis Method
	
	
	//----------------------------------------------------------
}	// End of CPlayerController Class
