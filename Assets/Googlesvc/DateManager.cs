using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateManager : MonoBehaviour {
    private void Awake()
    {
        GoogleManager.Instance.Login();
    }
    // Use this for initialization
    void Start ()
    {
        Invoke("CompleteFirstWrapper", 10f);

        

    }
	
    private void CompleteFirstWrapper()
    {
        if (GoogleManager.Instance.isAuthenticated)
        {
            GoogleManager.Instance.CompleteFirst();
        }
    }
}
