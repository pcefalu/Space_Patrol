using System.Threading;
using UnityEngine;
using System.Collections;

//****************************************************************************
[System.Serializable]
public class CExitGameButton : CButtonTouchLogic 
{	// Declare Data Members
	//----------------------------------------------------------
	
	private  CExitGameMode    m_oHomeController;
	
	
	//========================================================================
	void Start ()
	{	// Declare Variables
		//------------------------------------------------------
		
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("HomeController");
		
		if (gameControllerObject != null)
		{
			m_oHomeController = gameControllerObject.GetComponent <CExitGameMode>();
		}
		
		if (m_oHomeController == null)
		{
			Debug.Log ("Cannot find 'CExitGameMode' script");
		}
		
		//------------------------------------------------------
	}	// End of Start Method
	
	
	//========================================================================
	void OnMouseUp()
	{	// Declare Variables
		//------------------------------------------------------
		
		m_oHomeController.ExitGame();
		
		//------------------------------------------------------
	}	// End of OnMouseUp Method
	
	
	//========================================================================
	void OnTouchEnded()
	{	// Declare Variables
		//------------------------------------------------------
		
		m_oHomeController.ExitGame();
		
		//------------------------------------------------------
	}	// End of OnTouchEnded Method
	
	
	//----------------------------------------------------------
}	// End of CExitGameButton Class
