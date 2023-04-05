using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static float predictedDisplacement;
    public static float predictedTime;
    public GameObject displacementLine;
    public Motor objectA;
    public Motor objectB;
    public float fixedDeltaTime = 0.02f;

    // Start is called before the first frame update
    void Start()
    {

        // Time Calculation
        Time.fixedDeltaTime = fixedDeltaTime;

        float h = objectA.transform.position.x - objectB.transform.position.x;

        //     abc equation
        float a = objectB.acceleration - objectA.acceleration;
        float b = 2 * (objectB.initialVelocity - objectA.initialVelocity);
        float c = -2*h;

        predictedTime = (-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
        print("Predicted Time: " + predictedTime);


        // Displacement Object A Calcualtion
        predictedDisplacement = objectA.initialVelocity * predictedTime + objectA.acceleration * predictedTime * predictedTime / 2;
        print("Predicted Displacement: " + predictedDisplacement);

        displacementLine.transform.Translate(Vector3.right * (predictedDisplacement + objectA.transform.position.x));

    }


}
