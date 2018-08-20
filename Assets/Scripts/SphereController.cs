using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour {

    public List<Ambient> ambients = new List<Ambient>();
    public List<Cubemap> cubemaps = new List<Cubemap>();
    public GameObject sphere;
    private int _index;
    public bool transitionBetweenStatus = false;
    private Renderer _sphereRenderer;
    private UIController uiController;

	// Use this for initialization
	void Start () {
        //print(cubemaps.Length);
        uiController = GameObject.FindObjectOfType<UIController>();
        _sphereRenderer = sphere.gameObject.GetComponent<MeshRenderer>();

        ambients.Add(new Ambient("Sala", "Cubemaps/cubemap01"));
        ambients.Add(new Ambient("Cozinha", "Cubemaps/cubemap02"));
        ambients.Add(new Ambient("Sala", "Cubemaps/cubemap03"));
        ambients.Add(new Ambient("Sala", "Cubemaps/cubemap04"));
        ambients.Add(new Ambient("Sala", "Cubemaps/cubemap05"));
        ambients.Add(new Ambient("Sala", "Cubemaps/cubemap06"));
        ambients.Add(new Ambient("Sala", "Cubemaps/cubemap07"));
        ambients.Add(new Ambient("Sala", "Cubemaps/cubemap08"));

        foreach (var ambient in ambients) {
            cubemaps.Add(Resources.Load<Cubemap>(ambient.CubeMapsSource));
        }
    }

    public void Next() {
        if (_index < ambients.Count - 1) {
            _index ++;
        } else {
            _index = 0;
        }
        Transition();
        print("Index: " + _index);
    }

    public void Back() {
        if (_index == 0) {
            _index = ambients.Count - 1;
        }
        else {
            _index --;
        }
        Transition();
        print("Index: " + _index);
    }

    private void Transition() {
        if (uiController.thumbnailVisible) {
            uiController.ToggleThumbnail();
        }
        if (!transitionBetweenStatus) {
            // var texture = Resources.Load<Cubemap>(ambients[_index].CubeMapsSource);
            var texture = cubemaps[_index];
            _sphereRenderer.material.SetTexture("_Cubemap2", texture);
            StartCoroutine(FadeCanvas(0, 1));
        }
    }

    public void ChangeIndex(int index) {
        _index = index;
        Transition();
    }

    IEnumerator FadeCanvas(int alphaStart, int alphaEnd) {
        transitionBetweenStatus = true;
        float start = Time.time;
        float elapsed = 0;
        const float duration = 1f;
        while (elapsed < duration) {
            elapsed = Time.time - start;
            float normalisedTime = Mathf.Clamp(elapsed / duration, 0, 1);
            _sphereRenderer.material.SetFloat("_Slider", Mathf.Lerp(alphaStart, alphaEnd, normalisedTime));
            yield return null;
        }
        var texture = _sphereRenderer.material.GetTexture("_Cubemap2");
        _sphereRenderer.material.SetTexture("_Cubemap1", texture);
        transitionBetweenStatus = false;
    }
}
