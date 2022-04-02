using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Color onColor;
    public Color offColor;
    public Color warningColor;

    public bool isOn = false;
    public bool locked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void turnOn() {
        if (!locked) {
            isOn = true;
            transform.Find("TargetOuter").GetComponent<Renderer>().material.SetColor("_EmissionColor", onColor);
            transform.Find("TargetBullseye").GetComponent<Renderer>().material.SetColor("_EmissionColor", onColor);
        }
    }
    public void turnOff() {
        if (isOn) {
            isOn = false;
            transform.Find("TargetOuter").GetComponent<Renderer>().material.SetColor("_EmissionColor", offColor);
            transform.Find("TargetBullseye").GetComponent<Renderer>().material.SetColor("_EmissionColor", offColor);
            FindObjectOfType<GameController>().addScore(1);
            Debug.Log("Turning off");
        } else {
            FindObjectOfType<GameController>().subtractScore(1);
            StartCoroutine(FlashWarningColor());
        }
    }

    private IEnumerator FlashWarningColor() {
        locked = true;
        transform.Find("TargetOuter").GetComponent<Renderer>().material.SetColor("_EmissionColor", warningColor);
        yield return new WaitForSeconds(1);
        transform.Find("TargetOuter").GetComponent<Renderer>().material.SetColor("_EmissionColor", offColor);
        locked = false;
    }
    
}
