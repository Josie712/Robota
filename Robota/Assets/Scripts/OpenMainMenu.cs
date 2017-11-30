using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMainMenu : MonoBehaviour {

 
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var gameObject = GameObject.Find("GameObject"); // The parent game object to toggle
            gameObject.SetActive(true); //true or false
        }
    }
}