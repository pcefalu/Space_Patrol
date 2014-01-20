using System.Threading;
using UnityEngine;
using System.Collections;

//****************************************************************************
[System.Serializable]
public class CExitGameMode : Photon.MonoBehaviour 
{	// Declare Data Members
	//----------------------------------------------------------
	
	public   GUIText          TitleText;
	private  CHomeController  m_oHomeController;
	
	
	//========================================================================
	void Start ()
	{	// Declare Variables
		//------------------------------------------------------
		
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("HomeController");
		
		if (gameControllerObject != null)
		{
			m_oHomeController = gameControllerObject.GetComponent <CHomeController>();
		}
		
		if (m_oHomeController == null)
		{
			Debug.Log ("Cannot find 'HomeController' script");
		}
		
		TitleText.text = "Space Patrol";
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected)
		{
			PhotonNetwork.Disconnect();
		}
		
		//------------------------------------------------------
	}	// End of Start Method
	
	
	//========================================================================
	public void ExitGame()
	{	// Declare Variables
		//------------------------------------------------------
		
		TitleText.text = "Exiting Game...";
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected)
		{
			PhotonNetwork.Disconnect();
		}
		
		m_oHomeController.ExitGame();
		Application.Quit();
		
		//------------------------------------------------------
	}	// End of ExitGame Method
	
	
	//----------------------------------------------------------
}	// End of CExitGameMode Class









