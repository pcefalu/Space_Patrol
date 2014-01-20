using System.Threading;
using UnityEngine;
using System.Collections;

//****************************************************************************
public class CDestroyByBoundry : Photon.MonoBehaviour 
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
	void OnTriggerExit (Collider other) 
	{	// Declare Variables
		//------------------------------------------------------
		
		Destroy(other.gameObject);

		if(other.tag != "Laser_Bolt") 
			m_oGameController.AddScore(-20);

		//------------------------------------------------------
	}	// End of OnTriggerExit Method
	
	
	//----------------------------------------------------------
}	// End of CDestroyByBoundry Class
