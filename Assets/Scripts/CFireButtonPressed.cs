using System.Threading;
using UnityEngine;
using System.Collections;

//****************************************************************************
[System.Serializable]
public class CFireButtonPressed : CButtonTouchLogic 
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
			PlayerPrefs.SetInt("Laser", 1);
			m_oGameController.SendRemoteFireLaser(1);
			m_oGameController.FireLaser(1);
		}

		//------------------------------------------------------
	}	// End of OnMouseDown Method
	
	
	//========================================================================
	void OnMouseUp()
	{	// Declare Variables
		//------------------------------------------------------
		
		if (m_oGameController != null) 
		{
			PlayerPrefs.SetInt("Laser", 0);
			m_oGameController.SendRemoteFireLaser(0);
			m_oGameController.FireLaser(0);
		}

		//------------------------------------------------------
	}	// End of OnMouseUp Method
	
	
	//========================================================================
	void OnTouchBegin()
	{	// Declare Variables
		//------------------------------------------------------
		
		if (m_oGameController != null) 
		{
			PlayerPrefs.SetInt("Laser", 1);
			m_oGameController.SendRemoteFireLaser(1);
			m_oGameController.FireLaser(1);
		}
		

		//------------------------------------------------------
	}	// End of OnTouchBegin Method
	
	
	//========================================================================
	void OnTouchEnded()
	{	// Declare Variables
		//------------------------------------------------------
		
		if (m_oGameController != null) 
		{
			PlayerPrefs.SetInt("Laser", 0);
			m_oGameController.SendRemoteFireLaser(0);
			m_oGameController.FireLaser(0);
		}
		
		//------------------------------------------------------
	}	// End of OnTouchEnded Method
	
	
	//----------------------------------------------------------
}	// End of CFireButtonPressed Class



















