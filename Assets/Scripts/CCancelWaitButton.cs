using System.Threading;
using UnityEngine;
using System.Collections;

//****************************************************************************
[System.Serializable]
[RequireComponent(typeof(PhotonView))]
public class CCancelWaitButton : CButtonTouchLogic 
{	// Declare Data Members
	//----------------------------------------------------------

	private  bool             m_bCurrentState;
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

		m_bCurrentState = false;
		ShowObject(m_bCurrentState);

		//------------------------------------------------------
	}	// End of Start Method
	
	
	//========================================================================
	void Update()
	{	// Declare Variables
		//------------------------------------------------------

		
		bool bState = m_oHomeController.IsCancelWait();

		if (bState != m_bCurrentState) 
		{
			m_bCurrentState = bState;
			ShowObject(bState);
		}

		//------------------------------------------------------
	}	// End of Update Method
	
	
	//========================================================================
	void OnMouseUp()
	{	// Declare Variables
		//------------------------------------------------------
		
		m_oHomeController.CancelWait();
		ShowObject(true);
		
		//------------------------------------------------------
	}	// End of OnMouseUp Method
	
	
	//========================================================================
	void OnTouchEnded()
	{	// Declare Variables
		//------------------------------------------------------
		
		m_oHomeController.CancelWait();
		ShowObject(false);

		//------------------------------------------------------
	}	// End of OnTouchEnded Method

	
	//========================================================================
	public void ShowObject(bool bState)
	{	// Declare Variables
		//------------------------------------------------------

		if(guiTexture != null) 
			guiTexture.enabled = bState;
		
		if(guiText != null) 
			guiText.enabled = bState;
		
		//------------------------------------------------------
	}	// End of ShowObject Method
	
	
	//----------------------------------------------------------
}	// End of CCancelWaitButton Class











