using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour {

    private Rigidbody rb;

	public Vector3 target;

    public float h = 25;
    public float gravity = -18;

	public bool debugPath;

	public bool hasInit;


	IEnumerator Start()
    {
		while (true) {
			if (transform.position.x >= 3) {
				target = new Vector3 (0, 1, 1);
			}
			if (transform.position.y > 0.5f) {
				h = 0.5f;
			}
			StartCoroutine(Launch (target));
			target += new Vector3 (2, 0, 0);
			print (transform.position);

			yield return new WaitForSeconds (3);
		}
    }

    void Update()
    {
        if (debugPath)
        {
            DrawPath();
        }
			
		
    }

    void Init()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false;
        hasInit = true;

    }
		

	public IEnumerator Launch(Vector3 target)
    {
        if(!hasInit)
        {
            Init();
        }

        this.target = target;
        Physics.gravity = Vector3.up * gravity;
        rb.useGravity = true;
        rb.velocity = CalculateLaunchData().initialVelocity;
		yield return 0;
    }
		
    LaunchData CalculateLaunchData()
    {
        float displacementY = target.y - rb.position.y;
        Vector3 displacementXZ = new Vector3(target.x - rb.position.x, 0, target.z - rb.position.z);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    void DrawPath()
    {
        LaunchData launchData = CalculateLaunchData();
        Vector3 previousDrawPoint = rb.position;

        int resolution = 30;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = rb.position + displacement;
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }

    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }

    }
		






}
