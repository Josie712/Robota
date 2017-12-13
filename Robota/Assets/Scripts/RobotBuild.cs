using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBuild : MonoBehaviour {

    public GameObject head;
    public GameObject torso;
    public GameObject arms;
    public GameObject legs;

    public Sprite blankSprite;

    private bool RobotFullyBuilt()
    {
        GameObject[] parts = { head, torso, arms, legs };
        return true;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
