using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public GameObject canvasThumbnail;
    public bool thumbnailVisible = false;
    public bool transitionBetweenStatus = false;
    private CanvasGroup thumbnailCanvasGroup;

	// Use this for initialization
	void Start () {
        thumbnailCanvasGroup = canvasThumbnail.GetComponent<CanvasGroup>();
	}
	
    public void ToggleThumbnail() {
        if (!transitionBetweenStatus) {
            if (thumbnailVisible) {
                StartCoroutine(FadeCanvas(1, 0));
            } else {
                StartCoroutine(FadeCanvas(0, 1));
            }
        }
       
    }

    IEnumerator FadeCanvas(int alphaStart, int alphaEnd) {
        transitionBetweenStatus = true;
        float start = Time.time;
        float elapsed = 0;
        const float duration = 1f;
        while (elapsed < duration) {
            elapsed = Time.time - start;
            float normalisedTime = Mathf.Clamp(elapsed / duration, 0, 1);
            thumbnailCanvasGroup.alpha = Mathf.Lerp(alphaStart, alphaEnd, normalisedTime);
            yield return null;
        }
        transitionBetweenStatus = false;
        thumbnailVisible = !thumbnailVisible;
        thumbnailCanvasGroup.blocksRaycasts = !thumbnailCanvasGroup.blocksRaycasts;
    }

}
