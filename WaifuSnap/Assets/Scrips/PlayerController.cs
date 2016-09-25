using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public UIManager ui;
    public PhotoManager photoManager;

    RaycastHit hit;
    public bool zoom;
    public bool waifuInSight;

    public GameObject[] waypoints;
    public int waypoint;

	// Use this for initialization
	void Start ()
    {
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
        photoManager = GameObject.Find("GameManager").GetComponent<PhotoManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypoint].transform.position, 4 * Time.deltaTime);
        if( Vector3.Distance(transform.position, waypoints[waypoint].transform.position) < 0.1)
        {
            waypoint++;
        }

        if(Input.GetButton("Fire2"))
        {
            zoom = true;
            ui.crossHair.SetActive(true);
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 200))
            {
                if (hit.collider.tag == "Waifu")
                {
                    ui.crossHair.GetComponent<Animator>().SetBool("Photo", true);
                    ui.redDot.SetActive(true);

                    if (Input.GetButtonDown("Fire1"))
                    {
                        OnClickScreenCaptureButton();
                        ui.ChangeWaifuText(hit.collider.gameObject.GetComponent<WaifuClass>());
                        photoManager.photosLeft--;
                        photoManager.AddPhoto(hit.collider.gameObject.GetComponent<WaifuClass>());
                    }
                }
                else
                {
                    ui.redDot.SetActive(false);
                    ui.crossHair.GetComponent<Animator>().SetBool("Photo", false);
                }

            }
            else
            {
                ui.redDot.SetActive(false);
                ui.crossHair.GetComponent<Animator>().SetBool("Photo", false);
            }

        }
        else
        {
            ui.crossHair.SetActive(false);
            ui.redDot.SetActive(false);
            ui.crossHair.GetComponent<Animator>().SetBool("Photo", false);
            zoom = false;
        }

        if(zoom)
        {
            if(Camera.main.fieldOfView > 50)
            {
                Camera.main.fieldOfView--;
            }
            
        }
        else
        {
            if(Camera.main.fieldOfView < 60)
            {
                Camera.main.fieldOfView++;
            }
            
        }
        
    }

    public void OnClickScreenCaptureButton()
    {
        StartCoroutine(CaptureScreen());
    }
    public IEnumerator CaptureScreen()
    {
        // Wait till the last possible moment before screen rendering to hide the UI
        yield return null;
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;

        // Wait for screen rendering to complete
        yield return new WaitForEndOfFrame();

        // Take screenshot
        Application.CaptureScreenshot("screenshot.png");

        // Show UI after we're done
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
    }
}
