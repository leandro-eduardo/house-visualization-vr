using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambient : MonoBehaviour {

    public string Name { get; set; }
    public string CubeMapsSource { get; set; }

    public Ambient(string name, string cubeMapsSource) {
        Name = name;
        CubeMapsSource = cubeMapsSource;

    }

}
