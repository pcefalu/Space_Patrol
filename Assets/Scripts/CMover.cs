﻿using UnityEngine;
using System.Collections;

//****************************************************************************
public class CMover : MonoBehaviour 
{	// Declare Data Members
	//----------------------------------------------------------
	
	public float          Speed;
	public eEnergyLevels  EnergyLevel;
	
	
	//========================================================================
	void Start ()
	{	// Declare Variables
		//------------------------------------------------------
		
		rigidbody.velocity = transform.forward * Speed;
		
		//------------------------------------------------------
	}	// End of Start Method


	//----------------------------------------------------------
}	// End of CMover Class
