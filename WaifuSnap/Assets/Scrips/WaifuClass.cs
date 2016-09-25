using UnityEngine;
using System.Collections;

public class WaifuClass : MonoBehaviour
{
    public string waifuName;
    public int waifuID;
    public bool waifuCaught;
    public float distance;
    public float perfectDistance;

    void Update()
    {
        distance = Vector3.Distance(GameObject.Find("Player").transform.position, transform.position);
    }
}
