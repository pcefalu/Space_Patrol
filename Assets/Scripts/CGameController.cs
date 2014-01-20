using System.Threading;
using UnityEngine;
using System.Collections;

//****************************************************************************
public enum eHazardTypes
{
	Small_Asteroid,
	Large_Asteroid,
	EnemyShip
};


//****************************************************************************
public class CGameController : Photon.MonoBehaviour 
{	// Declare Data Members
	//----------------------------------------------------------
	
	public GameObject     Player;
	public Vector3        PlayerPosition;
	public Quaternion     PlayerRotation;

	public GameObject     Player_1;
	public Vector3        TeamPosition_1;

	public GameObject     Player_2;
	public Vector3        TeamPosition_2;
	
	public GameObject     PlayerExplosion;
	
	public GameObject[]   Hazards;
	public Vector3        SpawnValues;
	public int            HazardCount;
	public float          SpawnWait;
	public float          StartWait;
	public float          WaveWait;

	public GUITexture     FireButton;
	public GUIText        ScoreText;
	public GUIText        GameOverText;
	
	private bool          m_bGameOver;
	private bool          m_bUpdate;
	private int           m_intScore;
	private int           m_intSyncMainScene;

	private CharacterState  m_oCharacterState;

	private GameObject      m_oPlayer;
	private GameObject      m_oPlayer_1;
	private GameObject      m_oPlayer_2;

	private const int     MAIN_NOT_ASSIGNED = 0;
	private const int     MAIN_SCENE_SYNCED = 2;

	
	//========================================================================
	void Start ()
	{	// Declare Variables
		//------------------------------------------------------
		
		m_bGameOver          = false;
		m_bUpdate            = false;
		GameOverText.text    = "";
		m_intScore           = 0;

		UpdateScore ();
		StartCoroutine (SpawnWaves ());
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected) 
		{	// Team Player Mode
			//--------------------------------------------------
			m_intSyncMainScene = MAIN_NOT_ASSIGNED;
			CreateNetworkPlayerObject (TeamPosition_1, TeamPosition_2, PlayerRotation);
		}
		else
		{	// Single Player Mode
			//--------------------------------------------------
			m_intSyncMainScene = MAIN_SCENE_SYNCED;
			CreatePlayerObject (PlayerPosition, PlayerRotation);
		}

		//------------------------------------------------------
	}	// End of Start Method

	
	//========================================================================
	void Update ()
	{	// Declare Variables
		//------------------------------------------------------
		
		if (m_bUpdate) 
		{
			if (PhotonNetwork.connectionState == ConnectionState.Connected) 
			{
				PhotonView oPhotonView = PhotonView.Get (this);
				oPhotonView.RPC ("RemoteSceneHasLoaded", PhotonTargets.All, CHomeController.MAIN_SCENE_SCREEN);
			}
		} 
//		else 
//		{
//			if (PhotonNetwork.connectionState == ConnectionState.Connected) 
//				PhotonNetwork.SendOutgoingCommands();
//		}

		//------------------------------------------------------
	}	// End of Update Method
	
	
	//========================================================================
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{	// Declare Variables
		//------------------------------------------------------

		if (stream.isWriting)
		{	// We own this player: send the others our data
			//--------------------------------------------------
			stream.SendNext((int)this.m_oCharacterState);
			stream.SendNext(m_oPlayer_1.transform.position);
			stream.SendNext(m_oPlayer_1.transform.rotation);
		}
		else
		{	// Network player, receive data
			//--------------------------------------------------
			this.m_oCharacterState           = (CharacterState)(int)stream.ReceiveNext();

			Vector3 oRemotePosition          = (Vector3)stream.ReceiveNext();
			Vector3 oLocalPosition           = new Vector3(0, 0, 0);

			oLocalPosition.x                 = -oRemotePosition.x;
			oLocalPosition.y                 = oRemotePosition.y;
			oLocalPosition.z                 = oRemotePosition.z;


			m_oPlayer_2.transform.position   = oLocalPosition;
			m_oPlayer_2.transform.rotation   = (Quaternion)stream.ReceiveNext();

//			Quaternion oRemoteRotation       = (Quaternion)stream.ReceiveNext();
//			Quaternion oLocalRotation        = new Quaternion();
//			
//			oLocalRotation.x                 = -oRemoteRotation.x;
//			oLocalRotation.y                 = oRemoteRotation.y;
//			oLocalRotation.z                 = oRemoteRotation.z;
//			oLocalRotation.w                 = oRemoteRotation.w;
//
//			m_oPlayer_2.transform.rotation   = oLocalRotation;
		}

		//------------------------------------------------------
	}	// End of OnPhotonSerializeView Method
	
	
	//========================================================================
	public void CreatePlayerObject (Vector3 oPosition, Quaternion oRotation)
	{	// Declare Variables
		//------------------------------------------------------
		
		m_oPlayer = (GameObject) Instantiate (Player, oPosition, oRotation);
		
		//------------------------------------------------------
	}	// End of CreatePlayerObject Method
	
	
	//========================================================================
	public void CreateNetworkPlayerObject (Vector3 oPosition_1, Vector3 oPosition_2, Quaternion oRotation)
	{	// Declare Variables
		//------------------------------------------------------

		m_oPlayer_1 = (GameObject) Instantiate (Player_1, oPosition_1, oRotation);
		m_oPlayer_2 = (GameObject) Instantiate (Player_2, oPosition_2, oRotation);

		//------------------------------------------------------
		}	// End of CreateNetworkPlayerObject Method
	
	
	//========================================================================
	IEnumerator SpawnWaves ()
	{	// Declare Variables
		//------------------------------------------------------
		
		PhotonNetwork.networkingPeer.NewSceneLoaded();
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected) 
		{
			m_bUpdate = true;
			m_intSyncMainScene++;
		}

//		yield return new WaitForSeconds (StartWait);


		while (true)
		{
			if(m_intSyncMainScene >= MAIN_SCENE_SYNCED)
			{	// Wait for Main Scene to Synchronize
				//----------------------------------------------
				m_bUpdate = false;

				for (int i = 0; i < HazardCount; i++)
				{
//					GameObject oHazard = Hazards [Random.Range (0, Hazards.Length)];
//
//					Vector3 spawnPosition = new Vector3 (Random.Range (-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
//					Quaternion spawnRotation = Quaternion.identity;
//					Instantiate (oHazard, spawnPosition, spawnRotation);

					yield return new WaitForSeconds (SpawnWait);
				}
			}
			
			yield return new WaitForSeconds (WaveWait);

			if (m_bGameOver)
			{
				PhotonNetwork.LoadLevel (CHomeController.HOME_SCENE_SCREEN);
				break;
			}
		}
		
		//------------------------------------------------------
	}	// End of SpawnWaves Method


	//========================================================================
	void OnLevelWasLoaded (int level)
	{	// Declare Variables
		//------------------------------------------------------
		
		PhotonNetwork.networkingPeer.NewSceneLoaded();

		if (PhotonNetwork.connectionState == ConnectionState.Connected) 
		{
			m_bUpdate = true;
			m_intSyncMainScene++;
		}

		//------------------------------------------------------
	}	// End of OnLevelWasLoaded Method
	

	//========================================================================
	[RPC]
	public void RemoteSceneHasLoaded(int intSceneID)
	{	// Declare Variables
		//------------------------------------------------------
		
		if (intSceneID == CHomeController.MAIN_SCENE_SCREEN) 
		{
			m_bUpdate = false;
			m_intSyncMainScene++;
			
			Debug.Log ("RemoteSceneHasLoaded()\n" + PhotonNetwork.connectionStateDetailed.ToString ());
		}

		//------------------------------------------------------
	}	// End of RemoteSceneHasLoaded Method
	
	
//	//========================================================================
//	[RPC]
//	public void SendPlayerInfo(Vector3 oPosition, Quaternion oRotation)
//	{	// Declare Variables
//		//------------------------------------------------------
//		
//		m_oPlayer_2.transform.position = (Vector3)stream.ReceiveNext();
//		m_oPlayer_2.transform.rotation = (Quaternion)stream.ReceiveNext();
//		if (intSceneID == CHomeController.MAIN_SCENE_SCREEN) 
//		{
//			m_bUpdate = false;
//			m_intSyncMainScene++;
//			
//			Debug.Log ("RemoteSceneHasLoaded()\n" + PhotonNetwork.connectionStateDetailed.ToString ());
//		}
//		
//		//------------------------------------------------------
//	}	// End of RemoteSceneHasLoaded Method
	
	
	//========================================================================
	public void AddScore (int newScoreValue)
	{	// Declare Variables
		//------------------------------------------------------
		
		m_intScore += newScoreValue;
		UpdateScore ();
		
		//------------------------------------------------------
	}	// End of AddScore Method
	
	
	//========================================================================
	public void DestroyBothShips()
	{	// Declare Variables
		//------------------------------------------------------
		
		Instantiate (PlayerExplosion, m_oPlayer_1.transform.position, m_oPlayer_1.transform.rotation);
		Destroy(m_oPlayer_1);

		Instantiate (PlayerExplosion, m_oPlayer_2.transform.position, m_oPlayer_2.transform.rotation);
		Destroy(m_oPlayer_2);

//		GameObject playerControllerObject_1 = GameObject.FindGameObjectWithTag ("Player_1");		
//		if (playerControllerObject_1 != null)
//		{
//			Instantiate (PlayerExplosion, playerControllerObject_1.transform.position, playerControllerObject_1.transform.rotation);
//			Destroy(m_oPlayer_1);
//		}
//		
//		GameObject playerControllerObject_2 = GameObject.FindGameObjectWithTag ("Player_2");
//		if (playerControllerObject_2 != null)
//		{
//			Instantiate (PlayerExplosion, playerControllerObject_2.transform.position, playerControllerObject_2.transform.rotation);
//			Destroy(m_oPlayer_2);
//		}

		GameOver();
		
		//------------------------------------------------------
	}	// End of DestroyBothShips Method
	
	
	//========================================================================
	public void DestroyShip(string strTagName, GameObject oLazerBolt)
	{	// Declare Variables
		//------------------------------------------------------
		
		GameObject playerControllerObject = GameObject.FindGameObjectWithTag (strTagName);		
		if (playerControllerObject != null)
		{
			Instantiate (PlayerExplosion, playerControllerObject.transform.position, playerControllerObject.transform.rotation);
			Destroy(playerControllerObject);
			Destroy(oLazerBolt);
		}

		GameOver();
		
		//------------------------------------------------------
	}	// End of DestroyShip Method
	
	
	//========================================================================
	void UpdateScore()
	{	// Declare Variables
		//------------------------------------------------------
		
		ScoreText.text = "Score: " + m_intScore;
		
		//------------------------------------------------------
	}	// End of UpdateScore Method
	
	
	//========================================================================
	public void SinglePlayerMode()
	{	// Declare Variables
		//------------------------------------------------------
		
		m_intSyncMainScene = MAIN_SCENE_SYNCED;
		
		//------------------------------------------------------
	}	// End of SinglePlayerMode Method
	
	
	//========================================================================
	public void TeamPlayerMode()
	{	// Declare Variables
		//------------------------------------------------------
		
		m_intSyncMainScene = MAIN_NOT_ASSIGNED;
		
		//------------------------------------------------------
	}	// End of TeamPlayerMode Method


	//========================================================================
	public void GameOver ()
	{	// Declare Variables
		//------------------------------------------------------
		
		GameOverText.text = "Game Over!";
		m_bGameOver = true;
		
		//------------------------------------------------------
	}	// End of GameOver Method


	//----------------------------------------------------------
}	// End of CGameController Class
