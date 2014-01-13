using UnityEngine;
using System.Collections;

//****************************************************************************
public class CDestroyByTime : MonoBehaviour 
{	// Declare Data Members
	//----------------------------------------------------------
	
	public float Lifetime;
	
	
	//========================================================================
	void Start ()
	{	// Declare Variables
		//------------------------------------------------------
		
		Destroy (gameObject, Lifetime);

		//------------------------------------------------------
	}	// End of Start Method
	
	
	//----------------------------------------------------------
}	// End of CDestroyByTime Class
