using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestManager : MonoBehaviour
{

    public GameObject particleSlider;
    public GameObject particleUI;
    private float particleGauage;
	void Start ()
    {
        particleGauage = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(particleSlider!=null)
        {
            particleSlider.GetComponent<Slider>().value = particleGauage*0.01f;

            particleGauage += Time.deltaTime*10;
            if(particleGauage>=100)
            {
                particleGauage = 0;
                ParticleManager(1);
            }
        }
    }

    public void ParticleManager(float particle)
    {
        if (!GameObject.Find("SoundOfPop").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
            GameObject.Find("SoundOfPop").GetComponent<AudioSource>().Play();

        GameObject particleui = Instantiate(particleUI) as GameObject;
        particleui.transform.GetChild(1).GetComponent<Text>().text = User.ChangeUnit(particle);
        particleui.transform.SetParent(GameObject.Find("CanvasOverlay").transform);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(GameObject.Find("Users").transform.position);
        screenPos.y += Screen.height * 0.25f;
        particleui.transform.position = screenPos;
        particleui.transform.localScale = new Vector3(1, 1, 1);
        User.particle += particle;
    }
}
