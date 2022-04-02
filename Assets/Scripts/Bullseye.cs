using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullseye : MonoBehaviour {
    public Color onColor;
    public Color offColor;
    public Color warningColor;

    public bool isOn = false;
    private bool locked = false;
    private Target target;
    // Start is called before the first frame update
    void Start() {
        target = transform.parent.gameObject.GetComponent<Target>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void turnOn() {
        if (!target.locked) {
            target.turnOn();
            GetComponent<Renderer>().material.SetColor("_EmissionColor", onColor);
        }
    }
    public void turnOff() {
        if (target.isOn) {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", offColor);
            FindObjectOfType<GameController>().addScore(4);
            Debug.Log("Bullseye");
        } else {
            StartCoroutine(FlashWarningColor());
        }
        target.turnOff();
    }

    private IEnumerator FlashWarningColor() {
        GetComponent<Renderer>().material.SetColor("_EmissionColor", warningColor);
        yield return new WaitForSeconds(1);
        GetComponent<Renderer>().material.SetColor("_EmissionColor", offColor);
    }

}
