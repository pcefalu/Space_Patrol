using System;
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
	
	public GameObject        PlayerExplosion;
	
	private int              m_intFireLaser;
	private int              m_intPrevFire;
	
	private float            m_sngHAxis     = 0;
	private float            m_sngVAxis     = 0;

//	private CharacterState   m_oCharacterState;
	private CGameController  m_oGameController;
	
	private Vector3          m_oCorrectPlayerPos;
	private Quaternion       m_oCorrectPlayerRot;
	private Vector3          m_oProjectilePosition;
	private Quaternion       m_oProjectileRotation;
	
	
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
		
		m_intFireLaser     = 0;
		m_intPrevFire      = 0;
		
		PlayerPrefs.SetInt ("Terminate", 0);
		StartCoroutine (FireLaserBolts());

		//------------------------------------------------------
	}	// End of Start Method
	
	
	//========================================================================
	void Update ()
	{	// Process the Fire Laser Bolt Requests
		//------------------------------------------------------

		if (PhotonNetwork.connectionState == ConnectionState.Disconnected) 
		{
//			if (photonView.isMine)
//			{
				if (Input.GetButton("Fire1")) 
				{
					m_intPrevFire = 1;
					
					FireLaser(1);
//					SendRemoteFireLaser(1);
				}
				else
				{
					if(m_intPrevFire == 1)
					{
						m_intPrevFire = 0;
						
						FireLaser(0);
//						SendRemoteFireLaser(0);
					}
				}
//			}
		}

		//------------------------------------------------------
	}	// End of Update Method
	

	//========================================================================
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{	// Declare Variables
		//------------------------------------------------------
		
		if (stream.isWriting)
		{	// We own this player: send the others our data
			//--------------------------------------------------
//			stream.SendNext((int)m_oCharacterState);
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		}
		else
		{	// Network player, receive data
			//--------------------------------------------------
//			this.m_oCharacterState    = (CharacterState)stream.ReceiveNext();
			
			this.m_oCorrectPlayerPos  = (Vector3) stream.ReceiveNext();
			this.m_oCorrectPlayerRot  = (Quaternion) stream.ReceiveNext();
		}
		
		//------------------------------------------------------
	}	// End of OnPhotonSerializeView Method
	
	
	//========================================================================
	void FixedUpdate ()
	{	// 
		//------------------------------------------------------
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected) 
		{	// Team Player Mode
			//--------------------------------------------------
			if (photonView.isMine)
			{
				MoveShip ();
			}
			else
			{
				transform.position = Vector3.Lerp(transform.position, this.m_oCorrectPlayerPos, Time.deltaTime * 10);
				transform.rotation = Quaternion.Lerp(transform.rotation, this.m_oCorrectPlayerRot, Time.deltaTime * 10);
			}
		}
		else
		{	// Single Player Mode
			//--------------------------------------------------
			MoveShip ();
		}

		//------------------------------------------------------
	}	// End of FixedUpdate Method
	
	
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
					CreateExplosion ("explosion_player", PlayerExplosion, transform.position, transform.rotation);
					CreateExplosion ("explosion_player", PlayerExplosion, other.transform.position, other.transform.rotation);
					DestroyComponent(gameObject);
					DestroyComponent(other.gameObject);
				}
				else
				{
					CreateExplosion ("explosion_player", PlayerExplosion, other.transform.position, other.transform.rotation);
					DestroyComponent(gameObject);
					DestroyComponent(other.gameObject);
				}
			}
			else
			{
				CreateExplosion ("explosion_player", PlayerExplosion, transform.position, transform.rotation);
				DestroyComponent(gameObject);
				DestroyComponent(other.gameObject);
			}
			
			if (m_oGameController != null) 
				m_oGameController.GameOver();
		}

		//------------------------------------------------------
	}	// End of OnTriggerEnter Method
	

	//========================================================================
	void CreateExplosion (string strComponent, UnityEngine.Object oExplosion, Vector3 oPosition, Quaternion oRotation)
	{	// Declare Variables
		//------------------------------------------------------
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected) 
		{
			PhotonNetwork.Instantiate(strComponent, oPosition, oRotation, 0);
		}
		else
		{
			Instantiate(oExplosion, oPosition, oRotation);
		}
		
		//------------------------------------------------------
	}	// End of CreateExplosion Method
	
	
	//========================================================================
	void DestroyComponent (GameObject oComponent)
	{	// Declare Variables
		//------------------------------------------------------

		if (oComponent != null) 
		{
			if (PhotonNetwork.connectionState == ConnectionState.Connected) 
			{
				PhotonNetwork.Destroy(oComponent);
			}
			else
			{
				Destroy(oComponent);
			}
		}

		//------------------------------------------------------
	}	// End of CreateExplosion Method
	
	
	//========================================================================
	public void DestroyShip()
	{	// Declare Variables
		//------------------------------------------------------

		if (gameObject != null) 
		{
			CreateExplosion ("explosion_player", PlayerExplosion, transform.position, transform.rotation);
			DestroyComponent(gameObject);
			
			if (m_oGameController != null) 
				m_oGameController.GameOver();
		}

		//------------------------------------------------------
	}	// End of DestroyShip Method
	
	
	//========================================================================
	public void FireLaser(int intState)
	{	// 
		//------------------------------------------------------
		
		m_intFireLaser = intState;
		
		//------------------------------------------------------
	}	// End of FireLaser Method
	
	
	//========================================================================
	public int getFireLaser()
	{	// 
		//------------------------------------------------------
		
		return m_intFireLaser;
		
		//------------------------------------------------------
	}	// End of getFireLaser Method
	
	
	//========================================================================
	public void SendRemoteFireLaser(int intFireLaser)
	{	// Declare Variables
		//------------------------------------------------------
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected) 
		{
			PhotonView oPhotonView = PhotonView.Get (this);
			oPhotonView.RPC ("RemoteFireLaser", PhotonTargets.All, intFireLaser);
		}
		
		//------------------------------------------------------
	}	// End of SendRemoteFireLaser Method
	
	
	//========================================================================
	public void SendRemoteLaserControl(int intState)
	{	// Declare Variables
		//------------------------------------------------------
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected) 
		{
			PhotonView oPhotonView = PhotonView.Get (this);
			oPhotonView.RPC ("RemoteLaserControl", PhotonTargets.All, intState);
		}
		
		//------------------------------------------------------
	}	// End of SendRemoteLaserControl Method
	
	
	//========================================================================
	[RPC]
	public void RemoteFireLaser(int intFireLaser)
	{	// Declare Variables
		//------------------------------------------------------
		
		if (!photonView.isMine) 
		{	
			PlayerPrefs.SetInt("Laser", intFireLaser);
			FireLaser(intFireLaser);
		}
		
		//------------------------------------------------------
	}	// End of RemoteFireLaser Method
	
	
	//========================================================================
	[RPC]
	public void RemoteLaserControl(int intState)
	{	// Declare Variables
		//------------------------------------------------------
		
		if (!photonView.isMine) 
		{	
			PlayerPrefs.SetInt("Laser", intState);
		}
		
		//------------------------------------------------------
	}	// End of RemoteLaserControl Method
	
	
	//========================================================================
	IEnumerator FireLaserBolts()
	{	// Declare Variables
		//------------------------------------------------------
		
		while (PlayerPrefs.GetInt("Terminate") == 0)
		{
			if(getFireLaser() == 1)
			{	// Rapid Fire Laser
				//----------------------------------------------
				if(PlayerPrefs.GetInt("Laser") == 1)
				{
					Instantiate (Shot, ShotSpawn.position, ShotSpawn.rotation);
					audio.Play ();
				}
			}
			
			yield return new WaitForSeconds (FireRate);
		}
		
		//------------------------------------------------------
	}	// End of FireLaserBolts Method
	
	
	//========================================================================
	public void SendRemoteMoveShip (Vector3 oPosition, Vector3 oVelocity, Quaternion oRotation)
	{	// 
		//------------------------------------------------------
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected) 
		{
			PhotonView oPhotonView = PhotonView.Get (this);
			oPhotonView.RPC ("RemoteMoveShip", PhotonTargets.All, oPosition, oVelocity, oRotation);
		}

		//------------------------------------------------------
	}	// End of SendRemoteMoveShip Method
	
	
	//========================================================================
	[RPC]
	public void RemoteMoveShip (Vector3 oPosition, Vector3 oVelocity, Quaternion oRotation)
	{	// 
		//------------------------------------------------------
		
		if (!photonView.isMine) 
		{	// Remote Player Processing
			//--------------------------------------------------
			rigidbody.velocity  = oVelocity;
			rigidbody.position  = oPosition;
			rigidbody.rotation  = oRotation;
		} 
		
		//------------------------------------------------------
	}	// End of RemoteMoveShip Method
	
	
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
	{	// Player Processing
		//------------------------------------------------------

		float sngMoveHorizontal  = InputGetAxis ("Horizontal");
		float sngMoveVertical    = InputGetAxis ("Vertical");

		Vector3 sngMovement      = new Vector3 (sngMoveHorizontal, 0.0f, sngMoveVertical);
		Vector3 oVelocity        = sngMovement * Speed;
		rigidbody.velocity       = oVelocity;

		Vector3 oPosition = new Vector3
			(
				Mathf.Clamp (rigidbody.position.x, Boundary.XMin, Boundary.XMax), 
				0.0f, 
				Mathf.Clamp (rigidbody.position.z, Boundary.ZMin, Boundary.ZMax)
			);
		
		rigidbody.position    = oPosition;
		
		Quaternion oRotation  = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -Tilt);
		rigidbody.rotation    = oRotation;

		if (PlayerPrefs.GetInt("Master") == 0 && PhotonNetwork.connectionState == ConnectionState.Connected) 
		{
			SendRemoteMoveShip(oPosition, oVelocity, oRotation);
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
















