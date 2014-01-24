using System.Threading;
using UnityEngine;
using System.Collections;

//****************************************************************************
[System.Serializable]
[RequireComponent(typeof(PhotonView))]
public class CButtonTouchLogic : Photon.MonoBehaviour 
{	// Declare Data Members
	//----------------------------------------------------------

	private  bool               m_bDone;

	public   CPlayerController  m_oPlayerController;
	public   GameObject         m_oGameControllerObject;
	

	//========================================================================
	void Start ()
	{	// Declare Variables
		//------------------------------------------------------

		m_bDone = false;

		StartCoroutine (InitializeObjects());

		//------------------------------------------------------
	}	// End of Start Method
	
	
	//========================================================================
	IEnumerator InitializeObjects ()
	{	// Declare Variables
		//------------------------------------------------------
		
		while (!m_bDone)
		{
			if (PhotonNetwork.connectionState == ConnectionState.Connected) 
			{	// Team Player Mode
				//--------------------------------------------------
				m_oGameControllerObject = GameObject.FindGameObjectWithTag (PlayerPrefs.GetString ("Player"));
			}
			else
			{	// Single Player Mode
				//--------------------------------------------------
				m_oGameControllerObject = GameObject.FindGameObjectWithTag ("Player");
			}
			
			if (m_oGameControllerObject != null)
			{
				m_oPlayerController = m_oGameControllerObject.GetComponent <CPlayerController>();
			}
			
			if (m_oGameControllerObject != null && m_oPlayerController != null)
			{
				m_bDone = true;
			}

			yield return new WaitForSeconds (1);
		}

		//------------------------------------------------------
	}	// End of InitializeObjects Method
	
	
	//========================================================================
	void Update()
	{	// Declare Variables
		//------------------------------------------------------
		
		if (Input.touchCount > 0) 
		{	// Touches have been Detected
			//--------------------------------------------------
			for(int intNextTouch = 0; intNextTouch < Input.touchCount; intNextTouch++)
			{
				if(this.guiTexture.HitTest(Input.GetTouch(intNextTouch).position))
				{	// Button has been Touched
					//------------------------------------------
					if(Input.GetTouch(intNextTouch).phase == TouchPhase.Began)
					{	// Touch Began Motion
						//--------------------------------------
						SendMessage("OnTouchBegan");
					}
					
					if(Input.GetTouch(intNextTouch).phase == TouchPhase.Canceled)
					{	// Touch Canceled Motion
						//--------------------------------------
						SendMessage("OnTouchCanceled");
					}
					
					if(Input.GetTouch(intNextTouch).phase == TouchPhase.Ended)
					{	// Touch Ended Motion
						//--------------------------------------
						SendMessage("OnTouchEnded");
					}
					
					if(Input.GetTouch(intNextTouch).phase == TouchPhase.Moved)
					{	// Touch Moved Motion
						//--------------------------------------
						SendMessage("OnTouchMoved");
					}
					
					if(Input.GetTouch(intNextTouch).phase == TouchPhase.Stationary)
					{	// Touch Stationary Motion
						//--------------------------------------
						SendMessage("OnTouchStationary");
					}
				}
			}
		}

		//------------------------------------------------------
	}	// End of Update Method

	
	//----------------------------------------------------------
}	// End of CButtonTouchLogic Class
