using UnityEngine;
using System.Collections;

//****************************************************************************
public class CHomeScreenCleanUp : MonoBehaviour 
{	// Declare Data Members
	//----------------------------------------------------------
	
	private CHomeController  m_oHomeController;
	
	
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
		
		//------------------------------------------------------
	}	// End of Start Method
	
	
	//========================================================================
	void OnTriggerExit (Collider other) 
	{	// Declare Variables
		//------------------------------------------------------
		
		Destroy(other.gameObject);
		
		//------------------------------------------------------
	}	// End of OnTriggerExit Method
	
	
	//----------------------------------------------------------
}	// End of CHomeScreenCleanUp Class
