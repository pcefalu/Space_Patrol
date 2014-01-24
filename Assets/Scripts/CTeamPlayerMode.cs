using System;
using System.Threading;
using UnityEngine;
using System.Collections;

//****************************************************************************
[System.Serializable]
[RequireComponent(typeof(PhotonView))]
public class CTeamPlayerMode : Photon.MonoBehaviour 
{	// Declare Data Members
	//----------------------------------------------------------
	
	public   GUIText          TitleText;

	private  bool             m_bWaitForPlayer;
	private  bool             m_bInstantiatedAvatars;
	private  CHomeController  m_oHomeController;

	private const int MAX_TEAM_OF_PLAYERS    = 2;
	private const int MAX_NUMBER_OF_PLAYERS  = 4;

	
	//========================================================================
	void Start ()
	{	// Declare Variables
		//------------------------------------------------------
		
		GameObject homeControllerObject = GameObject.FindGameObjectWithTag ("HomeController");
		
		if (homeControllerObject != null)
		{
			m_oHomeController = homeControllerObject.GetComponent <CHomeController>();
		}
		
		if (m_oHomeController == null)
		{
			Debug.Log ("Cannot find 'HomeController' script");
		}
		
		TitleText.text          = "Space Patrol";
		m_bWaitForPlayer        = false;
		m_bInstantiatedAvatars  = false;
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected)
		{
			PhotonNetwork.Disconnect();
		}

		m_oHomeController.SetDisableButtons(false);
		
		//------------------------------------------------------
	}	// End of Start Method
	
	
	//========================================================================
	public void MultiPlayer()
	{	// Declare Variables
		//------------------------------------------------------
		
		if (!m_oHomeController.IsDisableButtons()) 
		{
			if (PhotonNetwork.connectionState == ConnectionState.Disconnected) 
			{
				m_oHomeController.SetDisableButtons(true);
				PhotonNetwork.ConnectUsingSettings ("1");
			}
		}
		
		//------------------------------------------------------
	}	// End of MultiPlayer Method
	
	
	//========================================================================
	public virtual void OnConnectedToMaster()
	{	// Now this client is connected and could join a room
		//------------------------------------------------------
		
		Debug.Log ("OnConnectedToMaster()\n" + PhotonNetwork.connectionStateDetailed.ToString ());
		PhotonNetwork.JoinRandomRoom();
		
		//------------------------------------------------------
	}	// End of MultiPlayer Method
	
	
	//========================================================================
	public virtual void OnPhotonRandomJoinFailed()
	{	// No random room available, so we create one
		//------------------------------------------------------
		
		Debug.Log ("OnPhotonRandomJoinFailed()\n" + PhotonNetwork.connectionStateDetailed.ToString ());
		PhotonNetwork.CreateRoom(null, true, true, MAX_NUMBER_OF_PLAYERS);

		//------------------------------------------------------
	}	// End of MultiPlayer Method
	
	
	//========================================================================
	
	public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
	{	// Declare Variables
		//------------------------------------------------------
		
		m_oHomeController.SetDisableButtons(false);
		Debug.LogError("Cause: " + cause);
		
		//------------------------------------------------------
	}	// End of MultiPlayer Method
	
	
	//========================================================================
	void OnJoinedRoom()
	{	// rom here on, your game would be running
		//------------------------------------------------------
		
		Debug.Log ("OnJoinedRoom()\n" + PhotonNetwork.connectionStateDetailed.ToString ());
		
		if (PhotonNetwork.countOfPlayers == MAX_TEAM_OF_PLAYERS) 
		{
			Guid guidClientID = System.Guid.NewGuid();
			
			PlayerPrefs.SetInt("Master", 0);
			PlayerPrefs.SetString("ClientID", guidClientID.ToString());
			PlayerPrefs.SetString("Player", "Player_2");

			BeginGame();
		}
		else
		{
			TitleText.text          = "Waiting For Players...";
			m_bWaitForPlayer        = true;
			m_bInstantiatedAvatars  = true;
			
			Guid guidClientID = System.Guid.NewGuid();
			
			PlayerPrefs.SetInt("Master", 1);
			PlayerPrefs.SetString("ClientID", guidClientID.ToString());
			PlayerPrefs.SetString("Player", "Player_1");
		}
		
		//------------------------------------------------------
	}	// End of OnJoinedRoom Method
	
	
	//========================================================================
	void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{	// rom here on, your game would be running
		//------------------------------------------------------
		
		Debug.Log ("OnPhotonPlayerConnected()\n" + PhotonNetwork.connectionStateDetailed.ToString ());
		
		if (m_bInstantiatedAvatars) 
		{
			BeginGame();
		}
		else
		{
			TitleText.text = "Join Team Player...";
			Debug.Log ("OnPhotonPlayerConnected\n" + "Someone is in the Room...");
		}
		
		//------------------------------------------------------
	}	// End of OnPhotonPlayerConnected Method
	
	
	//========================================================================
	public virtual void OnJoinedLobby()
	{	// Use a GUI to show existing rooms 
		// available in PhotonNetwork.GetRoomList()
		//------------------------------------------------------
		
		Debug.Log("OnJoinedLobby()\n" + "Use a GUI to show existing rooms available in PhotonNetwork.GetRoomList().");
		PhotonNetwork.JoinRandomRoom();
		
		//------------------------------------------------------
	}	// End of MultiPlayer Method
	
	
	//========================================================================
	void BeginGame()
	{	// Initiate Game Play
		//------------------------------------------------------
		
		TitleText.text          = "Ready...";
		m_bWaitForPlayer        = false;
		m_bInstantiatedAvatars  = false;
		m_oHomeController.GameOver ();
		
		Debug.Log ("BeginGame()\n");

		//------------------------------------------------------
	}	// End of BeginGame Method
	
	
	//========================================================================
	public void CancelWait ()
	{	// Declare Variables
		//------------------------------------------------------
		
		TitleText.text          = "Space Patrol";
		m_bWaitForPlayer        = false;
		m_bInstantiatedAvatars  = false;
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected)
		{
			PhotonNetwork.Disconnect();
		}
		
		m_oHomeController.SetDisableButtons(false);
		
		Debug.Log ("CancelWait()\n");

		//------------------------------------------------------
	}	// End of CancelWait Method
	
	
	//========================================================================
	public bool IsCancelWait()
	{	// Declare Variables
		//------------------------------------------------------
		
		return m_bWaitForPlayer;
		
		//------------------------------------------------------
	}	// End of IsCancelWait Method
	
	
	//----------------------------------------------------------
}	// End of CTeamPlayerMode Class











