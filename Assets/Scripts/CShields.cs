using UnityEngine;
using System.Collections;

//****************************************************************************
public enum eEnergyLevels
{
	Level_One    = 1,
	Level_Two    = 5,
	Level_Three  = 10
};


//****************************************************************************
[System.Serializable]
public class CShields : MonoBehaviour
{	// Declare Data Members
	//----------------------------------------------------------
	
	public   float  MaxShieldStrength;
	private  float  m_sngCurrentShieldStrength;
	
	
	//========================================================================
	public CShields()
	{	// Class Constructor
		//------------------------------------------------------
		
		m_sngCurrentShieldStrength = MaxShieldStrength;
		
		//------------------------------------------------------
	}	// End of Constructor Method
	
	
	//========================================================================
	public void Reset()
	{	// Set Shields to Full Strength
		//------------------------------------------------------
		
		m_sngCurrentShieldStrength = MaxShieldStrength;
		
		//------------------------------------------------------
	}	// End of Reset Method
	
	
	//========================================================================
	public bool Status()
	{	// Return Status of Shield Level
		//------------------------------------------------------
		
		if (m_sngCurrentShieldStrength <= 0)  return false;
		else                                  return true;
		
		//------------------------------------------------------
	}	// End of Status Method
	
	
	//========================================================================
	public float Level()
	{	// Return Current Shield Strength
		//------------------------------------------------------
		
		return m_sngCurrentShieldStrength;
		
		//------------------------------------------------------
	}	// End of Level Method
	
	
	//========================================================================
	public bool DecrementShield(eEnergyLevels eEnergyLevel)
	{	// Similate Weakening of Shields
		//------------------------------------------------------
		
		bool bRet = true;
		
		m_sngCurrentShieldStrength -= (float) eEnergyLevel;
		
		if (m_sngCurrentShieldStrength <= 0.0f) 
		{
			m_sngCurrentShieldStrength = 0.0f;
			bRet = false;
		}
		
		return bRet;
		
		//------------------------------------------------------
	}	// End of DecrementShield Method
	
	
	//========================================================================
	public void IncrementShield(eEnergyLevels eEnergyLevel)
	{	// Similate Strengthing of Shields
		//------------------------------------------------------
		
		m_sngCurrentShieldStrength += (float) eEnergyLevel;
		
		if (m_sngCurrentShieldStrength > MaxShieldStrength) 
			m_sngCurrentShieldStrength = MaxShieldStrength;
		
		//------------------------------------------------------
	}	// End of IncrementShield Method
	
	
	//----------------------------------------------------------
}	// End of CShields Class


