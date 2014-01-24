using System.Threading;
using UnityEngine;
using System.Collections;

//****************************************************************************
[System.Serializable]
public class CRightButtonPressed : CButtonTouchLogic 
{	// Declare Data Members
	//----------------------------------------------------------
	
	private CGameController  m_oGameController;
	
	
	//========================================================================
	void Start ()
	{	// Declare Variables
		//------------------------------------------------------
		
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		
		if (gameControllerObject != null)
		{
			m_oGameController = gameControllerObject.GetComponent <CGameController>();
		}
		
		if (m_oGameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
		
		//------------------------------------------------------
	}	// End of Start Method
	
	
	//========================================================================
	void OnMouseDown()
	{	// Declare Variables
		//------------------------------------------------------
		
		if (m_oGameController != null) 
		{
			PlayerPrefs.SetInt("Laser", 0);
			m_oGameController.SendRemoteLaserControl(0);
			MoveShip(1);
		}
		
		//------------------------------------------------------
	}	// End of OnMouseDown Method
	
	
	//========================================================================
	void OnMouseUp()
	{	// Declare Variables
		//------------------------------------------------------
		
		if (m_oGameController != null) 
		{
			PlayerPrefs.SetInt("Laser", 1);
			m_oGameController.SendRemoteLaserControl(1);
			MoveShip(0);
		}
		
		//------------------------------------------------------
	}	// End of OnMouseUp Method
	
	
	//========================================================================
	void OnTouchBegin()
	{	// Declare Variables
		//------------------------------------------------------
		
		if (m_oGameController != null) 
		{
			PlayerPrefs.SetInt("Laser", 0);
			m_oGameController.SendRemoteLaserControl(0);
			MoveShip(1);		
		}
		
		//------------------------------------------------------
	}	// End of OnTouchBegin Method
	
	
	//========================================================================
	void OnTouchEnded()
	{	// Declare Variables
		//------------------------------------------------------
		
		if (m_oGameController != null) 
		{
			PlayerPrefs.SetInt("Laser", 1);
			m_oGameController.SendRemoteLaserControl(1);
			MoveShip(0);		
		}
		
		//------------------------------------------------------
	}	// End of OnTouchEnded Method
	
	
	//========================================================================
	void MoveShip(float sngValue)
	{	// Declare Variables
		//------------------------------------------------------
		
		m_oGameController.MoveShipHorizontal(sngValue);
		
		//------------------------------------------------------
	}	// End of MoveShip Method
	
	
	//----------------------------------------------------------
}	// End of CRightButtonPressed Class
