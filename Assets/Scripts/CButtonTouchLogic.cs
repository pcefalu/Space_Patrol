using System.Threading;
using UnityEngine;
using System.Collections;

//****************************************************************************
[System.Serializable]
[RequireComponent(typeof(PhotonView))]
public class CButtonTouchLogic : Photon.MonoBehaviour 
{	// Declare Data Members
	//----------------------------------------------------------
	
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
