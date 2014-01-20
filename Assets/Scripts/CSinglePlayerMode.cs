using System.Threading;
using UnityEngine;
using System.Collections;

//****************************************************************************
[System.Serializable]
[RequireComponent(typeof(PhotonView))]
public class CSinglePlayerMode : Photon.MonoBehaviour 
{	// Declare Data Members
	//----------------------------------------------------------
	
	public   GUIText          TitleText;
	private  CHomeController  m_oHomeController;

	
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

		TitleText.text = "Space Patrol";
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected)
		{
			PhotonNetwork.Disconnect();
		}
		
		m_oHomeController.SetDisableButtons(false);
		
		//------------------------------------------------------
	}	// End of Start Method
	
	
	//========================================================================
	public void SinglePlayer()
	{	// Declare Variables
		//------------------------------------------------------
		
		if (!m_oHomeController.IsDisableButtons ()) 
		{
			TitleText.text = "Ready...";
			m_oHomeController.GameOver ();
			m_oHomeController.SetDisableButtons(true);
		}
		
		//------------------------------------------------------
	}	// End of SinglePlayer Method
	
	
	//----------------------------------------------------------
}	// End of CSinglePlayerMode Class











