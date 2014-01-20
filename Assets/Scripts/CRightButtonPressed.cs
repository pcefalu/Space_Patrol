using System.Threading;
using UnityEngine;
using System.Collections;

//****************************************************************************
[System.Serializable]
public class CRightButtonPressed : CButtonTouchLogic 
{	// Declare Data Members
	//----------------------------------------------------------
	
	private  CPlayerController  m_oPlayerController;
	private  GameObject         m_oGameControllerObject;
	
	
	//========================================================================
	void Start ()
	{	// Declare Variables
		//------------------------------------------------------
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected) 
		{	// Team Player Mode
			//--------------------------------------------------
			m_oGameControllerObject = GameObject.FindGameObjectWithTag ("Player_1");
		}
		else
		{	// Single Player Mode
			//--------------------------------------------------
			m_oGameControllerObject = GameObject.FindGameObjectWithTag ("Player");
		}
		
		if (m_oGameControllerObject != null)
		{
			m_oPlayerController = m_oGameControllerObject.GetComponent <CPlayerController>();
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
		
		if(m_oPlayerController != null) MoveShip(1);
		
		//------------------------------------------------------
	}	// End of OnMouseUp Method
	
	
	//========================================================================
	void OnMouseUp()
	{	// Declare Variables
		//------------------------------------------------------
		
		if(m_oPlayerController != null) MoveShip(0);
		
		//------------------------------------------------------
	}	// End of OnMouseUp Method
	
	
	//========================================================================
	void OnTouchBegin()
	{	// Declare Variables
		//------------------------------------------------------
		
		if(m_oPlayerController != null) MoveShip(1);
		
		//------------------------------------------------------
	}	// End of OnTouchBegin Method
	
	
	//========================================================================
	void OnTouchEnded()
	{	// Declare Variables
		//------------------------------------------------------
		
		if(m_oPlayerController != null) MoveShip(0);
		
		//------------------------------------------------------
	}	// End of OnTouchEnded Method
	
	
	//========================================================================
	void MoveShip(float sngValue)
	{	// Declare Variables
		//------------------------------------------------------
		
		m_oPlayerController.MoveShipHorizontal(sngValue);
		
		//------------------------------------------------------
	}	// End of MoveShip Method

	
	//----------------------------------------------------------
}	// End of CRightButtonPressed Class
