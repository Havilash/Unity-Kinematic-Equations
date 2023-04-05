using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public Rigidbody ball;
    public Transform target;

    public float h;
    public float gravity = -18;

    public bool debugPath;
    public int drawPathResolution = 30;

    LaunchData launchData;
    Vector3 ballStartPosition;
    bool launched = false;

    // Start is called before the first frame update
    void Start()
    {
        ball.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !launched) {
            Launch();
            launched = true;
        }

        if (debugPath) {
            if (!launched) {
                launchData = CalculateLaunchData();
                ballStartPosition = ball.position;
            }

            DrawPath(launchData);
        }

    }

    void Launch()
    {
        Physics.gravity = Vector3.up * gravity;
        ball.useGravity = true;
        ball.velocity = CalculateLaunchData().initialVelocity;
    }

    LaunchData CalculateLaunchData()
    {
        // Ball Target Displacement
        float pY = target.position.y - ball.position.y;
        Vector3 pXZ = new Vector3(target.position.x - ball.position.x, 0, target.position.z - ball.position.z);

        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (pY - h) / gravity);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = pXZ / time;

        return new LaunchData(velocityY + velocityXZ, time);
    }

    void DrawPath(LaunchData launchData)
    {
        Vector3 previousDrawPoint = ballStartPosition;

        int resolution = drawPathResolution;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = ballStartPosition + displacement;
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }

    struct LaunchData {
		public readonly Vector3 initialVelocity;
		public readonly float timeToTarget;

		public LaunchData (Vector3 initialVelocity, float timeToTarget)
		{
			this.initialVelocity = initialVelocity;
			this.timeToTarget = timeToTarget;
		}
	}
}
