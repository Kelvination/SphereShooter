using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject targetPrefab;
    public GameObject targetHolder;
    public float sphereRadius;
    public float numTargets;
    public float targetScale;
    public int targetCooldown;
    public GameObject scoreText;

    private List<Vector3> points;
    private List<GameObject> Targets;
    private int currentCooldown;

    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        Targets = new List<GameObject>();
        points = new List<Vector3>();
        currentCooldown = targetCooldown;


        CreateDiscoBall();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentCooldown > 0) {
            currentCooldown--;
        } else {
            currentCooldown = targetCooldown;
            highlightTarget();
        }
    }

    public void addScore(int amount) {
        score += amount;

        scoreText.GetComponent<Text>().text = score.ToString();
    }
    public void subtractScore(int amount) {
        score -= amount;

        scoreText.GetComponent<Text>().text = score.ToString();
    }

    void highlightTarget() {
        int index = Random.Range(0, Targets.Count);

        var target = Targets[index];
        target.GetComponent<Target>().turnOn();
    }

    void CreateDiscoBall() {
        
        
        // Find expected number of points
        float N = 0;
        float expectedPoints = 0;
        for(N = 1; N < numTargets; N++) {
            expectedPoints = 2f - ((2f * N * Mathf.Sin(Mathf.PI / N)) / (Mathf.Cos(Mathf.PI / N) - 1f));
            Debug.Log("N: " + N + "  ->  " + expectedPoints);
            if (expectedPoints >= numTargets) {
                break;
            }
        }
        float vAngle = Mathf.PI / N;
        points.Add(new Vector3(0, 0, sphereRadius));
        for(int i = 0; i < N; i++) {
            float hRadius = Mathf.Sin(i * vAngle);
            float circleLength = 2f * Mathf.PI * hRadius;
            float Z = Mathf.Cos(i * vAngle) * sphereRadius;
            float numPointsPerCircle = Mathf.Floor(circleLength / vAngle);
            float hAngle = 2f * Mathf.PI / numPointsPerCircle;

            for(float j = 0; j < numPointsPerCircle; ++j) {
                float X = Mathf.Cos(j * hAngle) * hRadius * sphereRadius;
                float Y = Mathf.Sin(j * hAngle) * hRadius * sphereRadius;
                points.Add(new Vector3(X, Y, Z));
            }
        }
            
        points.Add(new Vector3(0, 0, -sphereRadius));

        for(int i = 0; i < points.Count; i++) {
            var target = Instantiate(targetPrefab) as GameObject;
            target.transform.position = points[i];
            target.transform.LookAt(new Vector3(0, 0, 0));
            target.transform.Rotate(new Vector3(90, 0, 0));
            target.transform.SetParent(targetHolder.transform);
            target.transform.localScale = new Vector3(targetScale, targetScale, targetScale);

            Targets.Add(target);
        }

        targetHolder.transform.Rotate(new Vector3(90, 0, 0));
    }
}
