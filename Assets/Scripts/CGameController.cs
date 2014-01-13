using UnityEngine;
using System.Collections;

//****************************************************************************
public enum eHazardTypes
{
	Small_Asteroid,
	Large_Asteroid,
	EnemyShip
};


//****************************************************************************
public class CGameController : MonoBehaviour 
{	// Declare Data Members
	//----------------------------------------------------------
	
	public GameObject[]   Hazards;
	public Vector3        SpawnValues;
	public int            HazardCount;
	public float          SpawnWait;
	public float          StartWait;
	public float          WaveWait;
	
	public GUIText        ScoreText;
	public GUIText        RestartText;
	public GUIText        GameOverText;
	
	private bool          m_bGameOver;
	private bool          m_bRestart;
	private int           m_intScore;
	
	
	//========================================================================
	void Start ()
	{	// Declare Variables
		//------------------------------------------------------
		
		m_bGameOver          = false;
		m_bRestart           = false;
		RestartText.text     = "";
		GameOverText.text    = "";
		m_intScore           = 0;

		UpdateScore ();
		StartCoroutine (SpawnWaves ());
		
		//------------------------------------------------------
	}	// End of Start Method

	
	//========================================================================
	void Update ()
	{	// Declare Variables
		//------------------------------------------------------
		
		if (m_bRestart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}
		
		//------------------------------------------------------
	}	// End of Update Method

	
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
				RestartText.text = "Press 'R' for Restart";
				m_bRestart = true;
				break;
			}
		}
		
		//------------------------------------------------------
	}	// End of SpawnWaves Method

	
	//========================================================================
	public void AddScore (int newScoreValue)
	{	// Declare Variables
		//------------------------------------------------------
		
		m_intScore += newScoreValue;
		UpdateScore ();
		
		//------------------------------------------------------
	}	// End of AddScore Method

	
	//========================================================================
	void UpdateScore ()
	{	// Declare Variables
		//------------------------------------------------------
		
		ScoreText.text = "Score: " + m_intScore;
		
		//------------------------------------------------------
	}	// End of UpdateScore Method

	
	//========================================================================
	public void GameOver ()
	{	// Declare Variables
		//------------------------------------------------------
		
		GameOverText.text = "Game Over!";
		m_bGameOver = true;
		
		//------------------------------------------------------
	}	// End of GameOver Method

	
	//----------------------------------------------------------
}	// End of CGameController Class
