using UnityEngine;
using System.Collections;

//****************************************************************************
public class CDestroyByContact : MonoBehaviour 
{	// Declare Data Members
	//----------------------------------------------------------
	
	public GameObject        Explosion;
	public GameObject        PlayerExplosion;
	public int               ScoreValue;

	private CGameController  m_oGameController;


	//========================================================================
	void Start ()
	{	// Declare Variables
		//------------------------------------------------------
		
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");

		if (gameControllerObject != null)
		{
			m_oGameController = gameControllerObject.GetComponent <CGameController>();
		}

		if (m_oGameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
		
		//------------------------------------------------------
	}	// End of Start Method

	
	//========================================================================
	void OnTriggerEnter (Collider other)
	{	// Declare Variables
		//------------------------------------------------------
		
		if (other.tag != "Boundary" && other.tag != "Enemy")
		{	// Process Request
			//--------------------------------------------------
			if(gameObject.tag == "Small_Asteroid")
			{
				if (other.tag == "Large_Asteroid")
				{
					Instantiate(Explosion, transform.position, transform.rotation);
					Destroy (gameObject);
				}
				else
				{
					if (other.tag == "Player")
					{
						Instantiate(Explosion, transform.position, transform.rotation);
						Instantiate(PlayerExplosion, other.transform.position, other.transform.rotation);
						m_oGameController.GameOver();
					}
					else
					{
						Instantiate(Explosion, transform.position, transform.rotation);
						m_oGameController.AddScore(10);
					}
					
					Destroy (other.gameObject);
					Destroy (gameObject);
				}
			}

			if(gameObject.tag == "Large_Asteroid")
			{
				if (other.tag == "Small_Asteroid")
				{
					Instantiate(Explosion, transform.position, transform.rotation);
					Destroy (other.gameObject);
				}
				else
				{
					if (other.tag == "Player")
					{
						Instantiate(Explosion, transform.position, transform.rotation);
						Instantiate(PlayerExplosion, other.transform.position, other.transform.rotation);
						m_oGameController.GameOver();
					}
					else
					{
						Instantiate(Explosion, transform.position, transform.rotation);
						m_oGameController.AddScore(10);
					}
					
					Destroy (other.gameObject);
					Destroy (gameObject);
				}
			}
		}
		
		//------------------------------------------------------
	}	// End of OnTriggerEnter Method
	
	
	//----------------------------------------------------------
}	// End of CDestroyByContact Class
