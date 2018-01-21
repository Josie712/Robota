using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ChangeSceneWhenVideoEnds : MonoBehaviour {
    private const string LEVEL_1_SCENE = "Level1";

	void Start () {
        double videoLength = GetComponent<VideoPlayer>().clip.length;
        StartCoroutine(ChangeSceneAfterSeconds(videoLength, LEVEL_1_SCENE));
	}

    private IEnumerator ChangeSceneAfterSeconds(double seconds, string scene)
    {
        yield return new WaitForSeconds((float) seconds);
        SceneManager.LoadScene(scene);
    }
}
