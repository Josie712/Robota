﻿using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ChangeSceneWhenVideoEnds : MonoBehaviour {
    private const string LEVEL_1_SCENE = "Level1";

	void Start () {
        double videoLength = GetComponent<VideoPlayer>().clip.length;
        StartCoroutine(ChangeSceneAfterSeconds(videoLength));
	}

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            StopAllCoroutines();
            SceneManager.LoadScene(LEVEL_1_SCENE);
        }
    }

    private IEnumerator ChangeSceneAfterSeconds(double seconds)
    {
        yield return new WaitForSeconds((float) seconds);
        SceneManager.LoadScene(LEVEL_1_SCENE);
    }
}
