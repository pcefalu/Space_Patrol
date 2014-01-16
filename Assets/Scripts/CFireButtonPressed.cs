using UnityEngine;
using System.Collections;

//****************************************************************************
[System.Serializable]
public class CFireButtonPressed : CButtonTouchLogic 
{	// Declare Data Members
	//----------------------------------------------------------
	
	private  CPlayerController  m_oPlayerController;


	//========================================================================
	void Start ()
	{	// Declare Variables
		//------------------------------------------------------
		
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("Player");
		
		if (gameControllerObject != null)
		{
			m_oPlayerController = gameControllerObject.GetComponent <CPlayerController>();
		}
		
		if (m_oPlayerController == null)
		{
			Debug.Log ("Cannot find 'Player' script");
		}
		
		//------------------------------------------------------
	}	// End of Start Method
	
	
	//========================================================================
	void OnMouseDown()
	{	// Declare Variables
		//------------------------------------------------------

		if(m_oPlayerController != null) 
			m_oPlayerController.FireLaser();

		//------------------------------------------------------
	}	// End of OnMouseUp Method
	
	
	//========================================================================
	void OnTouchEnded()
	{	// Declare Variables
		//------------------------------------------------------

		if(m_oPlayerController != null) 
			m_oPlayerController.FireLaser();
		
		//------------------------------------------------------
	}	// End of OnTouchEnded Method
	
	
	//----------------------------------------------------------
}	// End of CFireButtonPressed Class
