using UnityEngine;
using System.Collections;

//****************************************************************************
public class CHomeController : MonoBehaviour 
{	// Declare Data Members
	//----------------------------------------------------------
	
	public GameObject[]   Hazards;
	public Vector3        SpawnValues;
	public int            HazardCount;
	public float          SpawnWait;
	public float          StartWait;
	public float          WaveWait;
	
	private bool          m_bGameOver;
	private bool          m_bRestart;

	
	//========================================================================
	void Start ()
	{	// Declare Variables
		//------------------------------------------------------

		m_bGameOver  = false;

		StartCoroutine (SpawnWaves ());
		
		//------------------------------------------------------
	}	// End of Start Method


	//========================================================================
	IEnumerator SpawnWaves ()
	{	// Declare Variables
		//------------------------------------------------------
		
		yield return new WaitForSeconds (StartWait);
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
				Application.LoadLevel (CButtonPressed.MAIN_SCENE_SCREEN);
				break;
			}
		}
		
		//------------------------------------------------------
	}	// End of SpawnWaves Method
	
	
	//========================================================================
	public void GameOver ()
	{	// Declare Variables
		//------------------------------------------------------
		
		m_bGameOver = true;
		
		//------------------------------------------------------
	}	// End of GameOver Method
	
	
	//----------------------------------------------------------
}	// End of CHomeController Class
