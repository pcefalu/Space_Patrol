using System.Threading;
using UnityEngine;
using System.Collections;

//****************************************************************************
public class CHomeController : Photon.MonoBehaviour 
{	// Declare Data Members
	//----------------------------------------------------------
	
	public GameObject[]   Hazards;
	public Vector3        SpawnValues;
	public int            HazardCount;
	public float          SpawnWait;
	public float          StartWait;
	public float          WaveWait;
	
	private bool          m_bDisableButtons;
	private bool          m_bExitGame;
	private bool          m_bGameOver;
	private bool          m_bRestart;
	
	public const int HOME_SCENE_SCREEN = 0;
	public const int MAIN_SCENE_SCREEN = 1;
	

	//========================================================================
	void Start ()
	{	// Declare Variables
		//------------------------------------------------------

		m_bGameOver        = false;
		m_bExitGame        = false;
		m_bDisableButtons  = false;

		StartCoroutine (SpawnWaves ());
		
		//------------------------------------------------------
	}	// End of Start Method
	
	
	//========================================================================
	IEnumerator SpawnWaves ()
	{	// Declare Variables
		//------------------------------------------------------
		
//		yield return new WaitForSeconds (StartWait);
		while (true)
		{
			for (int i = 0; i < HazardCount; i++)
			{
				GameObject hazard = Hazards [Random.Range (0, Hazards.Length)];
				
				Vector3 spawnPosition = new Vector3 (Random.Range (-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				
				yield return new WaitForSeconds (SpawnWait);
			}
			yield return new WaitForSeconds (WaveWait);

			if (m_bGameOver)
			{
				PhotonNetwork.LoadLevel(MAIN_SCENE_SCREEN);
				break;
			}
			
			if (m_bExitGame)
			{
				break;
			}
		}
		
		//------------------------------------------------------
	}	// End of SpawnWaves Method
	
	
	//========================================================================
	void OnLevelWasLoaded (int level)
	{	// Declare Variables
		//------------------------------------------------------
		
		PhotonNetwork.networkingPeer.NewSceneLoaded();
		
		//------------------------------------------------------
	}	// End of OnLevelWasLoaded Method
	
	
	//========================================================================
	public void GameOver ()
	{	// Declare Variables
		//------------------------------------------------------
		
		m_bGameOver = true;
		
		//------------------------------------------------------
	}	// End of GameOver Method
	
	
	//========================================================================
	public void ExitGame ()
	{	// Declare Variables
		//------------------------------------------------------
		
		m_bExitGame = true;
		
		//------------------------------------------------------
	}	// End of GameOver Method
	
	
	//========================================================================
	public void SetDisableButtons (bool bState)
	{	// Declare Variables
		//------------------------------------------------------
		
		m_bDisableButtons  = bState;
		
		//------------------------------------------------------
	}	// End of SetDisableButtons Method
	
	
	//========================================================================
	public bool IsDisableButtons ()
	{	// Declare Variables
		//------------------------------------------------------
		
		return m_bDisableButtons;
		
		//------------------------------------------------------
	}	// End of IsDisableButtons Method
	
	
	//----------------------------------------------------------
}	// End of CHomeController Class












