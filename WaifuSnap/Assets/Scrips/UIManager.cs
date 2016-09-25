using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject crossHair;
    public GameObject redDot;
    public Text photosLeft;
    public Text waifuText;

    public float timer;
    public bool activateTimer;

    public PhotoManager photoManager;

	// Use this for initialization
	void Start ()
    {
        photoManager = GameObject.Find("GameManager").GetComponent<PhotoManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        photosLeft.text = "Photos Left: " + photoManager.photosLeft;
        if(activateTimer)
        {
            timer += Time.deltaTime;
            if(timer > 1.5f)
            {
                activateTimer = false;
                waifuText.text = "";
                timer = 0;
            }
        }
	}

    public void ChangeWaifuText(WaifuClass waifu)
    {
        waifuText.text = waifu.waifuName;
        activateTimer = true;
    }

}
