using System.Threading;
using UnityEngine;
using System.Collections;

//****************************************************************************
[System.Serializable]
public class CDataRecordInfo : Photon.MonoBehaviour 
{	// Declare Data Members
	//----------------------------------------------------------
	
	private  CharacterState  	m_oCharacterState;
	private  Vector3        	m_oPlayerPosition;
	private  Quaternion        	m_oPlayerRotation;
	private  bool  	            m_bFireLaser;


	//========================================================================
	public CDataRecordInfo(CharacterState oCharacterState, Vector3 oPlayerPosition, Quaternion  oPlayerRotation, bool bFireLaser)
	{	// Declare Variables
		//------------------------------------------------------

		m_oCharacterState  = oCharacterState;
		m_oPlayerPosition  = oPlayerPosition;
		m_oPlayerRotation  = oPlayerRotation;
		m_bFireLaser       = bFireLaser;

		//------------------------------------------------------
	}	// End of Constructor Method

	
	//========================================================================
	public CharacterState CharacterState()
	{	// Declare Variables
		//------------------------------------------------------
		
		return m_oCharacterState;
		
		//------------------------------------------------------
	}	// End of CharacterState Method
	
	
	//========================================================================
	public Vector3 PlayerPosition()
	{	// Declare Variables
		//------------------------------------------------------
		
		return m_oPlayerPosition;
		
		//------------------------------------------------------
	}	// End of PlayerPosition Method
	
	
	//========================================================================
	public Quaternion PlayerRotation()
	{	// Declare Variables
		//------------------------------------------------------
		
		return m_oPlayerRotation;
		
		//------------------------------------------------------
	}	// End of PlayerRotation Method
	
	
	//========================================================================
	public bool FireLaser()
	{	// Declare Variables
		//------------------------------------------------------
		
		return m_bFireLaser;
		
		//------------------------------------------------------
	}	// End of FireLaser Method
	
	
	//----------------------------------------------------------
}	// End of CDataRecordInfo Class
