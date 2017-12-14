using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RobotBuild : MonoBehaviour {

    public GameObject head;
    public GameObject torso;
    public GameObject arms;
    public GameObject legs;
    public GameObject nextButton;

    public Sprite blankSprite;

    private bool RobotPartSelected(GameObject part)
    {
        return part.GetComponent<Image>().sprite != blankSprite;
    }

    private bool RobotFullyBuilt()
    {
        GameObject[] parts = { head, torso, arms, legs };
        return parts.All(RobotPartSelected);
    }

    public void EnableNextButtonIfFullyBuilt()
    {
        if (RobotFullyBuilt())
        {
            nextButton.GetComponent<Button>().interactable = true;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
