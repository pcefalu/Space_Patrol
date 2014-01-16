using UnityEngine;
using System.Collections;

//****************************************************************************
[System.Serializable]
public class CUpButtonPressed : CButtonTouchLogic 
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
	void OnMouseUp()
	{	// Declare Variables
		//------------------------------------------------------
		
		if(m_oPlayerController != null) MoveShip();
		
		//------------------------------------------------------
	}	// End of OnMouseUp Method
	
	
	//========================================================================
	void OnTouchEnded()
	{	// Declare Variables
		//------------------------------------------------------
		
		if(m_oPlayerController != null) MoveShip();
		
		//------------------------------------------------------
	}	// End of OnTouchEnded Method
	
	
	//========================================================================
	void MoveShip()
	{	// Declare Variables
		//------------------------------------------------------
		
		m_oPlayerController.MoveShip();
		
		Debug.Log ("Move Up Button Pressed...");
		
		//------------------------------------------------------
	}	// End of OnTouchEnded Method
	
	
	//----------------------------------------------------------
}	// End of CUpButtonPressed Class
