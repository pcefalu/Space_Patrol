using UnityEngine;
using System.Collections;

//****************************************************************************
public enum ePlayerTypes
{
	SinglePlayer,
	MultiPlayer
};


//****************************************************************************
[System.Serializable]
public class CButtonPressed : CButtonTouchLogic 
{	// Declare Data Members
	//----------------------------------------------------------

	public   GUIText          TitleText;
	public   ePlayerTypes     eType;
	private  CHomeController  m_oHomeController;
		
	public const int HOME_SCENE_SCREEN = 0;
	public const int MAIN_SCENE_SCREEN = 1;

	
	//========================================================================
	void Start ()
	{	// Declare Variables
		//------------------------------------------------------
		
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("HomeController");
		
		if (gameControllerObject != null)
		{
			m_oHomeController = gameControllerObject.GetComponent <CHomeController>();
		}
		
		if (m_oHomeController == null)
		{
			Debug.Log ("Cannot find 'HomeController' script");
		}

		TitleText.text = "Space Patrol";

		//------------------------------------------------------
	}	// End of Start Method
	
	
	//========================================================================
	void OnMouseUp()
	{	// Declare Variables
		//------------------------------------------------------

		switch (eType) 
		{
			case ePlayerTypes.MultiPlayer:
				MultiPlayer ();
				break;

			case ePlayerTypes.SinglePlayer:
			default:
				SinglePlayer ();
				break;
		}

		//------------------------------------------------------
	}	// End of OnMouseUp Method


	//========================================================================
	void OnTouchEnded()
	{	// Declare Variables
		//------------------------------------------------------
		
		switch (eType) 
		{
			case ePlayerTypes.MultiPlayer:
				MultiPlayer ();
				break;
				
			case ePlayerTypes.SinglePlayer:
			default:
				SinglePlayer ();
				break;
		}
		
		//------------------------------------------------------
	}	// End of OnTouchEnded Method
	
	
	//========================================================================
	void SinglePlayer()
	{	// Declare Variables
		//------------------------------------------------------

		TitleText.text = "Ready...";
		m_oHomeController.GameOver ();

		//------------------------------------------------------
	}	// End of SinglePlayer Method
	
	
	//========================================================================
	void MultiPlayer()
	{	// Declare Variables
		//------------------------------------------------------
		
		SinglePlayer ();
		
		//------------------------------------------------------
	}	// End of MultiPlayer Method
	
	
	//----------------------------------------------------------
}	// End of CButtonPressed Class
