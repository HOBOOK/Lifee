using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GenderManager : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        GenderChange();
	}
	

    public void GenderChange()
    {
        User.LoadData();
        if (SceneManager.GetActiveScene().buildIndex == 5)
            return;
        if (User.isDead)
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
            this.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
