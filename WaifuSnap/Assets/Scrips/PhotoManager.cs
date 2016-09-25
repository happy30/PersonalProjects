using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhotoManager : MonoBehaviour {

    public List<PhotoClass> photos = new List<PhotoClass>();
    public PlayerController player;


    public int photosLeft;

    // Use this for initialization
    void Start ()
    {
        photosLeft = 60;
        player = GameObject.Find("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddPhoto(WaifuClass waifu)
    {
        PhotoClass newPhoto = new PhotoClass();
        newPhoto.waifu = waifu;
        float distance = Vector3.Distance(player.transform.position, waifu.transform.position);
        newPhoto.size = Mathf.Round(1000f - (distance * 25f));
        if(distance < waifu.perfectDistance)
        {
            newPhoto.size = 1000 * (distance / 100);
        }
        if (newPhoto.size < 100)
        {
            newPhoto.size = 100;
        }


        newPhoto.pose = 1000;
        newPhoto.technique = true;
        newPhoto.totalScore = newPhoto.size + newPhoto.pose;
        if(newPhoto.technique)
        {
            newPhoto.totalScore *= 2;
        }
        photos.Add(newPhoto);
    }
}
