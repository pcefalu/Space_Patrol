using UnityEngine;
using System.Collections;

//****************************************************************************
public class CRandomRotator : MonoBehaviour 
{	// Declare Data Members
	//----------------------------------------------------------
	
	public float Tumble;
	
	
	//========================================================================
	void Start ()
	{	// Declare Variables
		//------------------------------------------------------
		
		rigidbody.angularVelocity = Random.insideUnitSphere * Tumble;
		
		//------------------------------------------------------
	}	// End of Start Method
	
	
	//----------------------------------------------------------
}	// End of CRandomRotator Class
