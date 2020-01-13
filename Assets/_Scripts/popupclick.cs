using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popupclick : MonoBehaviour
{
    private bool isActive;
    private float activeTime;

    private void Start()
    {
        this.transform.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update ()
    {
        activeTime += Time.deltaTime;
        if (activeTime > 5.0f)
            this.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

		if(transform.localScale.x <0.4)
        {
            if(this.name.Contains("dog"))
            {
                this.gameObject.SetActive(false);
                isActive = false;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
	}

    public void close()
    {
        if (isActive)
            return;
        GetComponent<Animator>().SetInteger("isClose", 1);
        User.sleep-= 15;
        isActive = true;
    }
}
