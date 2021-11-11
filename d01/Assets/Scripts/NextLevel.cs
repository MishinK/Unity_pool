using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
  	public GameObject   Clair;
    public GameObject   John;
    public GameObject   Thomas;

	private bool		win;
	private bool		next;
	void Start()
    {
		win = false;
		next = false;
	}
    
    void Update()
    {
		if (!win)
		{
        	if (Clair.GetComponent<Check_exit>().exit && John.GetComponent<Check_exit>().exit && Thomas.GetComponent<Check_exit>().exit)
			{
            	Debug.Log("Level finished");
				win = true;
				next = true;
				GameObject.Destroy(Clair);
				GameObject.Destroy(John);
				GameObject.Destroy(Thomas);

			}
		}
		else if (next)
		{
			int s = 0;
			while (s < 500)
				 s++;
			SceneManager.LoadScene("ex02_2");
		}
    }
}
