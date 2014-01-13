using UnityEngine;
using System.Collections;

//****************************************************************************
public class CDestroyByBoundry : MonoBehaviour 
{	// Declare Data Members
	//----------------------------------------------------------
		
	//========================================================================
	void OnTriggerExit (Collider other) 
	{	// Declare Variables
		//------------------------------------------------------
		
		Destroy(other.gameObject);
		
		//------------------------------------------------------
	}	// End of OnTriggerExit Method
	
	
	//----------------------------------------------------------
}	// End of CDestroyByBoundry Class
