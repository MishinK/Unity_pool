using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_win : MonoBehaviour
{
    
    public GameObject   Clair;
    public GameObject   John;
    public GameObject   Thomas;

	private bool		win;

	void Start()
    {
		win = false;
	}
    
    void Update()
    {
		if (!win)
		{
        	if (Clair.GetComponent<Check_exit>().exit && John.GetComponent<Check_exit>().exit && Thomas.GetComponent<Check_exit>().exit)
			{
            	Debug.Log("Level finished");
				win = true;
				GameObject.Destroy(this.gameObject);
			}
		}
    }
}
