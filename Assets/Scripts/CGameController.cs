using System;
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

	private GameObject    m_oPlayer;
	private GameObject    m_oNetworkPlayer;

//	private  CharacterState     m_eCharacterState;
	private  CPlayerController  m_oPlayerController;

	private const int     MAIN_NOT_ASSIGNED   = 0;
	private const int     MAIN_SCENE_SYNCED   = 2;

	
	//========================================================================
	void Start ()
	{	// Declare Variables
		//------------------------------------------------------
		
		m_bGameOver        = false;
		m_bUpdate          = false;

		GameOverText.text  = "";
		m_intScore         = 0;
		ScoreText.text     = "Score: " + m_intScore;

		StartCoroutine (SpawnWaves ());
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected) 
		{	// Team Player Mode
			//--------------------------------------------------
			m_intSyncMainScene = MAIN_NOT_ASSIGNED;
		}
		else
		{	// Single Player Mode
			//--------------------------------------------------
			m_intSyncMainScene = MAIN_SCENE_SYNCED;
		}

		//------------------------------------------------------
	}	// End of Start Method

	
	//========================================================================
	void Update ()
	{	// Declare Variables
		//------------------------------------------------------
		
		if (m_bUpdate) 
		{
			SendRemoteSceneHasLoaded(PlayerPrefs.GetInt("Main_Scene"));
			
			Debug.Log ("Update()\nSceneID= " + PlayerPrefs.GetInt("Main_Scene").ToString ());
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
			stream.SendNext((int)m_intScore);

			ScoreText.text = "Score: " + m_intScore;
		}
		else
		{	// Network player, receive data
			//--------------------------------------------------
			ScoreText.text  = "Score: " + (int)stream.ReceiveNext();
		}
		
		//------------------------------------------------------
	}	// End of OnPhotonSerializeView Method
	
	
	//========================================================================
	public void SendRemoteLaserControl(int intState)
	{	// Declare Variables
		//------------------------------------------------------
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected) 
		{	// Team Player Mode
			//--------------------------------------------------
			m_oPlayerController = m_oNetworkPlayer.GetComponent<CPlayerController>();
			
			if (m_oPlayerController != null) 
			{
				m_oPlayerController.SendRemoteLaserControl(intState);
				Debug.Log ("CGameController.SendRemoteLaserControl()\nState= " + intState.ToString());
			}
		}

		//------------------------------------------------------
	}	// End of SendRemoteFireLaser Method
	
	
	//========================================================================
	public void SendRemoteFireLaser(int intState)
	{	// Declare Variables
		//------------------------------------------------------
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected) 
		{	// Team Player Mode
			//--------------------------------------------------
			m_oPlayerController = m_oNetworkPlayer.GetComponent<CPlayerController>();
			
			if (m_oPlayerController != null) 
			{
				m_oPlayerController.SendRemoteFireLaser(intState);
			}
		}

		//------------------------------------------------------
	}	// End of SendRemoteFireLaser Method
	
	
	//========================================================================
	public void FireLaser(int intState)
	{	// Declare Variables
		//------------------------------------------------------
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected) 
		{	// Team Player Mode
			//--------------------------------------------------
			m_oPlayerController = m_oNetworkPlayer.GetComponent<CPlayerController>();
			
			if (m_oPlayerController != null) 
			{
				m_oPlayerController.FireLaser(intState);
			}
		}
		else
		{	// Single Player Mode
			//--------------------------------------------------
			m_oPlayerController = m_oPlayer.GetComponent<CPlayerController>();
			
			if (m_oPlayerController != null) 
			{
				m_oPlayerController.FireLaser(intState);
			}
		}

		//------------------------------------------------------
	}	// End of SendRemoteFireLaser Method
	
	
	//========================================================================
	public string ClientID ()
	{	// Declare Variables
		//------------------------------------------------------
		
		return PlayerPrefs.GetString("ClientID");
		
		//------------------------------------------------------
	}	// End of ClientID Method
	
	
	//========================================================================
	public void MoveShipHorizontal (float sngValue)
	{	// 
		//------------------------------------------------------
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected) 
		{	// Team Player Mode
			//--------------------------------------------------
			m_oPlayerController = m_oNetworkPlayer.GetComponent<CPlayerController>();
			
			if (m_oPlayerController != null) 
			{
				m_oPlayerController.MoveShipHorizontal(sngValue);
			}
		}
		else
		{	// Single Player Mode
			//--------------------------------------------------
			m_oPlayerController = m_oPlayer.GetComponent<CPlayerController>();
			
			if (m_oPlayerController != null) 
			{
				m_oPlayerController.MoveShipHorizontal(sngValue);
			}
		}
		
		//------------------------------------------------------
	}	// End of MoveShipHorizontal Method
	
	
	//========================================================================
	public void MoveShipVertical (float sngValue)
	{	// 
		//------------------------------------------------------
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected) 
		{	// Team Player Mode
			//--------------------------------------------------
			m_oPlayerController = m_oNetworkPlayer.GetComponent<CPlayerController>();
			
			if (m_oPlayerController != null) 
			{
				m_oPlayerController.MoveShipVertical(sngValue);
			}
		}
		else
		{	// Single Player Mode
			//--------------------------------------------------
			m_oPlayerController = m_oPlayer.GetComponent<CPlayerController>();
			
			if (m_oPlayerController != null) 
			{
				m_oPlayerController.MoveShipVertical(sngValue);
			}
		}
		
		//------------------------------------------------------
	}	// End of MoveShipVertical Method
	
	
	//========================================================================
	public bool IsClientID (string strClientID)
	{	// Declare Variables
		//------------------------------------------------------

		bool bRet = false;

		if (PlayerPrefs.GetString("ClientID") == strClientID) bRet = true;

		return bRet;

		//------------------------------------------------------
	}	// End of ClientID Method
	
	
	//========================================================================
	public bool IsMasterClient()
	{	// Declare Variables
		//------------------------------------------------------
		
		bool bRet = false;
		
		if (PlayerPrefs.GetInt("Master") == 1) bRet = true;
		
		return bRet;
		
		//------------------------------------------------------
	}	// End of IsMasterClient Method
	
	
	//========================================================================
	public void CreatePlayerObject (Vector3 oPosition, Quaternion oRotation)
	{	// Declare Variables
		//------------------------------------------------------
		
		m_oPlayer = (GameObject) Instantiate (Player, oPosition, oRotation);
		
		//------------------------------------------------------
	}	// End of CreatePlayerObject Method
	
	
	//========================================================================
	public void CreateNetworkPlayerObject ()
	{	// Declare Variables
		//------------------------------------------------------

		if (IsMasterClient()) 
		{
			m_oNetworkPlayer = (GameObject) PhotonNetwork.Instantiate("Player_1", TeamPosition_1, PlayerRotation, 0);
		} 
		else 
		{
			m_oNetworkPlayer = (GameObject) PhotonNetwork.Instantiate("Player_2", TeamPosition_2, PlayerRotation, 0);
		}

		//------------------------------------------------------
	}	// End of CreateNetworkPlayerObject Method
	
	
	//========================================================================
	IEnumerator SpawnWaves ()
	{	// Declare Variables
		//------------------------------------------------------

		bool bWaitForSync  = false;

		PhotonNetwork.networkingPeer.NewSceneLoaded();
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected) 
		{
			m_bUpdate = true;
			m_intSyncMainScene++;

			GameOverText.text = "Transporting...";
		}

		while (!bWaitForSync)
		{
			if(m_intSyncMainScene >= MAIN_SCENE_SYNCED)
			{	// Wait for Main Scene to Synchronize
				//----------------------------------------------
				m_bUpdate     = false;
				bWaitForSync  = true;

				if (PhotonNetwork.connectionState == ConnectionState.Connected) 
				{	// Team Player Mode
					//--------------------------------------------------
					if(IsMasterClient()) 
						yield return new WaitForSeconds (10);

					else 
						yield return new WaitForSeconds (10);

					CreateNetworkPlayerObject ();
					Debug.Log ("SpawnWaves()\nCreating Network Player...");
				}
				else
				{	// Single Player Mode
					//--------------------------------------------------
					CreatePlayerObject (PlayerPosition, PlayerRotation);
					Debug.Log ("SpawnWaves()\nCreating Single Player...");
				}
			}
			
			yield return new WaitForSeconds (StartWait);
		}

		GameOverText.text = "";

		while (true)
		{
			for (int i = 0; i < HazardCount; i++)
			{
				if (PhotonNetwork.connectionState == ConnectionState.Connected) 
				{	// Team Player Mode
					//--------------------------------------------------
					if(IsMasterClient())
					{
						GameObject oHazard = Hazards [UnityEngine.Random.Range (0, Hazards.Length)];						
						Vector3 spawnPosition = new Vector3 (UnityEngine.Random.Range (-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
						Quaternion spawnRotation = Quaternion.identity;

						if(oHazard.tag == "Large_Asteroid")
						{
							PhotonNetwork.Instantiate ("Large Asteroid", spawnPosition, spawnRotation, 0);
							Debug.Log ("Creating Large Asteroid...");
						}
						
						if(oHazard.tag == "Small_Asteroid")
						{
							PhotonNetwork.Instantiate ("Small Asteroid", spawnPosition, spawnRotation, 0);
							Debug.Log ("Creating Small Asteroid...");
						}
					}
				}
				else
				{	// Single Player Mode
					//--------------------------------------------------
					GameObject oHazard = Hazards [UnityEngine.Random.Range (0, Hazards.Length)];
	
					Vector3 spawnPosition = new Vector3 (UnityEngine.Random.Range (-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (oHazard, spawnPosition, spawnRotation);
				}

				yield return new WaitForSeconds (SpawnWait);
			}

			yield return new WaitForSeconds (WaveWait);

			if (m_bGameOver)
			{
				yield return new WaitForSeconds (StartWait);

				PhotonNetwork.LoadLevel (PlayerPrefs.GetInt("Home_Scene"));
				break;
			}
		}
		
		//------------------------------------------------------
	}	// End of SpawnWaves Method


	//========================================================================
	void OnLevelWasLoaded (int level)
	{	// Declare Variables
		//------------------------------------------------------
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected) 
		{	// Team Player Mode
			//--------------------------------------------------
			PhotonNetwork.networkingPeer.NewSceneLoaded();
			m_bUpdate = true;
			m_intSyncMainScene++;
		}

		//------------------------------------------------------
	}	// End of OnLevelWasLoaded Method
	

	//========================================================================
	public void SendRemoteSceneHasLoaded(int intSceneID)
	{	// Declare Variables
		//------------------------------------------------------
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected) 
		{
			PhotonView oPhotonView = PhotonView.Get (this);
			oPhotonView.RPC ("RemoteSceneHasLoaded", PhotonTargets.All, intSceneID);
		}

		//------------------------------------------------------
	}	// End of SendRemoteSceneHasLoaded Method
	
	
	//========================================================================
	public void SendAckRemoteSceneHasLoaded(int intSceneID)
	{	// Declare Variables
		//------------------------------------------------------
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected) 
		{
			PhotonView oPhotonView = PhotonView.Get (this);
			oPhotonView.RPC ("AckRemoteSceneHasLoaded", PhotonTargets.All, intSceneID);
		}
		
		//------------------------------------------------------
	}	// End of RemoteSceneHasLoaded Method
	
	
	//========================================================================
	[RPC]
	public void RemoteSceneHasLoaded(int intSceneID)
	{	// Declare Variables
		//------------------------------------------------------
		
		if (!photonView.isMine && intSceneID == PlayerPrefs.GetInt("Main_Scene")) 
		{
			m_intSyncMainScene++;

			SendAckRemoteSceneHasLoaded(intSceneID);
		}

		//------------------------------------------------------
	}	// End of RemoteSceneHasLoaded Method
	
	
	//========================================================================
	[RPC]
	public void AckRemoteSceneHasLoaded(int intSceneID)
	{	// Declare Variables
		//------------------------------------------------------
		
		if (photonView.isMine && intSceneID == PlayerPrefs.GetInt("Main_Scene")) 
		{
			m_bUpdate = false;
		}
		//------------------------------------------------------
	}	// End of AckRemoteSceneHasLoaded Method
	
	
	//========================================================================
	public void AddScore (int newScoreValue)
	{	// Declare Variables
		//------------------------------------------------------

		if (!m_bGameOver) 
		{
			if (PhotonNetwork.connectionState == ConnectionState.Connected) 
			{	// Team Player Mode
				//--------------------------------------------------
				if (IsMasterClient()) 
				{
					m_intScore += newScoreValue;
				}
				else
				{
					PhotonView oPhotonView = PhotonView.Get (this);
					oPhotonView.RPC ("RemoteUpdateScore", PhotonTargets.All, newScoreValue);
				}
			}
			else
			{	// Single Player Mode
				//--------------------------------------------------
				m_intScore += newScoreValue;
				ScoreText.text     = "Score: " + m_intScore;
			}
		}

		//------------------------------------------------------
	}	// End of AddScore Method
	
	
	//========================================================================
	[RPC]
	public void RemoteUpdateScore(int intScore)
	{	// Declare Variables
		//------------------------------------------------------
		
		if (!m_bGameOver) 
		{
			if (PhotonNetwork.connectionState == ConnectionState.Connected) 
			{	// Team Player Mode
				//--------------------------------------------------
				if(!photonView.isMine)
				{
					m_intScore += intScore;
				}
			}
		}

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
		PlayerPrefs.SetInt("Terminate", 1);
		m_bGameOver = true;

		SendRemoteGameOver ();

		//------------------------------------------------------
	}	// End of GameOver Method


	//========================================================================
	public void SendRemoteGameOver()
	{	// Declare Variables
		//------------------------------------------------------
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected) 
		{
			PhotonView oPhotonView = PhotonView.Get (this);
			oPhotonView.RPC ("RemoteGameOver", PhotonTargets.All, null);
		}
		
		//------------------------------------------------------
	}	// End of SendRemoteGameOver Method
	
	
	//========================================================================
	[RPC]
	public void RemoteGameOver ()
	{	// Declare Variables
		//------------------------------------------------------

		if (!photonView.isMine) 
		{
			GameOverText.text = "Game Over!";
			PlayerPrefs.SetInt("Terminate", 1);
			m_bGameOver = true;
		}
		
		//------------------------------------------------------
	}	// End of RemoteGameOver Method
	
	
	//----------------------------------------------------------
}	// End of CGameController Class





//{
//	
//	PlayerPrefs.SetInt ("HiScore", scorevar);
//	score1 = PlayerPrefs.GetInt("HiScore");
//	
//	PlayerPrefs.SetInt("Master", 0);
//	PlayerPrefs.SetInt("Laser", 0);
//	PlayerPrefs.SetString("ClientID", guidClientID.ToString());
//	PlayerPrefs.SetString("Player", "Player_2");
//	
//	PlayerPrefs.SetInt("Home_Scene", HOME_SCENE_SCREEN);
//	PlayerPrefs.SetInt("Main_Scene", MAIN_SCENE_SCREEN);
//	
//}
	
	
	
	
	
	
	





