using System.Threading;
using UnityEngine;
using System.Collections;

//****************************************************************************
[System.Serializable]
[RequireComponent(typeof(PhotonView))]
public class CSinglePlayerButton : CButtonTouchLogic 
{	// Declare Data Members
	//----------------------------------------------------------
	
	private  CSinglePlayerMode  m_oHomeController;
	
	
	//========================================================================
	void Start ()
	{	// Declare Variables
		//------------------------------------------------------
		
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("HomeController");
		
		if (gameControllerObject != null)
		{
			m_oHomeController = gameControllerObject.GetComponent <CSinglePlayerMode>();
		}
		
		if (m_oHomeController == null)
		{
			Debug.Log ("Cannot find 'CSinglePlayerMode' script");
		}
		
		//------------------------------------------------------
	}	// End of Start Method
	
	
	//========================================================================
	void OnMouseUp()
	{	// Declare Variables
		//------------------------------------------------------
		
		m_oHomeController.SinglePlayer();
		
		//------------------------------------------------------
	}	// End of OnMouseUp Method
	
	
	//========================================================================
	void OnTouchEnded()
	{	// Declare Variables
		//------------------------------------------------------
		
		m_oHomeController.SinglePlayer();

		//------------------------------------------------------
	}	// End of OnTouchEnded Method
	
	
	//----------------------------------------------------------
}	// End of CSinglePlayerButton Class











