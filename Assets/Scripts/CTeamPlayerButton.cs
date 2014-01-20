using System.Threading;
using UnityEngine;
using System.Collections;

//****************************************************************************
[System.Serializable]
[RequireComponent(typeof(PhotonView))]
public class CTeamPlayerButton : CButtonTouchLogic 
{	// Declare Data Members
	//----------------------------------------------------------
	
	private  CTeamPlayerMode  m_oHomeController;


	//========================================================================
	void Start ()
	{	// Declare Variables
		//------------------------------------------------------
		
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("HomeController");
		
		if (gameControllerObject != null)
		{
			m_oHomeController = gameControllerObject.GetComponent <CTeamPlayerMode>();
		}
		
		if (m_oHomeController == null)
		{
			Debug.Log ("Cannot find 'CTeamPlayerMode' script");
		}
		
		//------------------------------------------------------
	}	// End of Start Method
	
	
	//========================================================================
	void OnMouseUp()
	{	// Declare Variables
		//------------------------------------------------------
		
		m_oHomeController.MultiPlayer();
		
		//------------------------------------------------------
	}	// End of OnMouseUp Method
	
	
	//========================================================================
	void OnTouchEnded()
	{	// Declare Variables
		//------------------------------------------------------
		
		m_oHomeController.MultiPlayer();
		
		//------------------------------------------------------
	}	// End of OnTouchEnded Method
	
	
	//----------------------------------------------------------
}	// End of CTeamPlayerButton Class











