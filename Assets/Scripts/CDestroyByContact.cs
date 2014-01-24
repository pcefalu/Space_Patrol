using System.Threading;
using UnityEngine;
using System.Collections;

//****************************************************************************
public class CDestroyByContact : Photon.MonoBehaviour 
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
	void CreateExplosion (string strComponent, Object oExplosion, Vector3 oPosition, Quaternion oRotation)
	{	// Declare Variables
		//------------------------------------------------------
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected) 
		{
			PhotonNetwork.Instantiate(strComponent, oPosition, oRotation, 0);
		}
		else
		{
			Instantiate(oExplosion, oPosition, oRotation);
		}
		
		//------------------------------------------------------
	}	// End of CreateExplosion Method
	
	
	//========================================================================
	void DestroyComponent (GameObject oComponent)
	{	// Declare Variables
		//------------------------------------------------------
		
		if (PhotonNetwork.connectionState == ConnectionState.Connected) 
		{
			PhotonNetwork.Destroy(oComponent);
		}
		else
		{
			Destroy(oComponent);
		}
		
		//------------------------------------------------------
	}	// End of CreateExplosion Method
	
	
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
					CreateExplosion ("explosion_asteroid", Explosion, transform.position, transform.rotation);
					DestroyComponent(gameObject);
				}
				else
				{
					if (other.tag == "Player")
					{
						CreateExplosion ("explosion_asteroid", Explosion, transform.position, transform.rotation);
						CreateExplosion ("explosion_player", PlayerExplosion, other.transform.position, other.transform.rotation);
						m_oGameController.GameOver();
					}
					else
					{
						CreateExplosion ("explosion_asteroid", Explosion, transform.position, transform.rotation);
						m_oGameController.AddScore(10);
					}
					
					DestroyComponent (other.gameObject);
					DestroyComponent (gameObject);
				}
			}

			if(gameObject.tag == "Large_Asteroid")
			{
				if (other.tag == "Small_Asteroid")
				{
					CreateExplosion ("explosion_asteroid", Explosion, transform.position, transform.rotation);
					DestroyComponent (other.gameObject);
				}
				else
				{
					if (other.tag == "Player" || other.tag == "Player_1" || other.tag == "Player_2")
					{
						CreateExplosion ("explosion_asteroid", Explosion, transform.position, transform.rotation);
						CreateExplosion ("explosion_player", PlayerExplosion, other.transform.position, other.transform.rotation);
						m_oGameController.GameOver();
					}
					else
					{
						CreateExplosion ("explosion_asteroid", Explosion, transform.position, transform.rotation);
						m_oGameController.AddScore(10);
					}
					
					DestroyComponent (other.gameObject);
					DestroyComponent (gameObject);
				}
			}
			
			if(gameObject.tag == "Player_1" || gameObject.tag == "Player_2")
			{
				if (other.tag == "Player_1" || other.tag == "Player_2")
				{
					CreateExplosion ("explosion_player", PlayerExplosion, transform.position, transform.rotation);
					CreateExplosion ("explosion_player", PlayerExplosion, other.transform.position, other.transform.rotation);
					m_oGameController.GameOver();
					
					DestroyComponent (other.gameObject);
					DestroyComponent (gameObject);
				}
			}
		}
		
		//------------------------------------------------------
	}	// End of OnTriggerEnter Method
	
	
	//----------------------------------------------------------
}	// End of CDestroyByContact Class













