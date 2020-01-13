using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mirror : MonoBehaviour
{
    public RenderTexture texture;
    public Material screenshotMat;
    public bool repeat = false;
    private Camera mirror;

    // Use this for initialization
    void Start()
    {
        texture = new RenderTexture((int)128, (int)128, 24, RenderTextureFormat.RGB565);
        mirror = this.transform.GetChild(1).GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(screenshotFunc());
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            repeat = true;
        }

        if (repeat)
        {
            StartCoroutine(screenshotFunc());

        }

    }

    IEnumerator screenshotFunc()
    {
        yield return new WaitForEndOfFrame();
        mirror.Render();
        texture = mirror.targetTexture;
        //texture.Apply();
       
        screenshotMat.mainTexture = texture;
        //		texture.Resize(0, 0);
        //		Destroy(texture);

    }

}
