using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour {

    GameObject _backgroundImage;
    // Use this for initialization
    void Start() {
        if (!_backgroundImage) {
            _backgroundImage = gameObject.transform.Find("selectionBackground").gameObject;
            _backgroundImage.GetComponent<Image>().fillAmount = 0;
        }
    }

    public void OnGazeEnter() {
        StartCoroutine("BackgroundTransition");
    }

    public void OnGazeExit() {
        StopCoroutine("BackgroundTransition");
        _backgroundImage.GetComponent<Image>().fillAmount = 0;
    }

    private IEnumerator BackgroundTransition() {
        float start = Time.time;
        float elapsed = 0;
        float duration = 1.5f;
        while (elapsed < duration) {
            elapsed = Time.time - start;
            float normalisedTime = Mathf.Clamp(elapsed / duration, 0, 1);
            _backgroundImage.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1, normalisedTime);
            yield return null;
        }
        GetComponent<Button>().onClick.Invoke();
    }
}