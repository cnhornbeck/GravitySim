using UnityEngine;

public class GravityController : MonoBehaviour
{

    Transform objectTransform;
    [SerializeField] Vector3 velocity;
    [SerializeField] Vector3 gravityForce;
    [SerializeField] public float mass;
    [SerializeField] Vector3 tangentialVelocityForOrbit;


    // Start is called before the first frame update
    void Start()
    {
        objectTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        SetGravityForce();

        // Apply the gravity to the object
        ApplyGravity(gravityForce);

        MoveObject(velocity);
    }

    void SetGravityForce()
    {
        gravityForce = Vector3.zero;

        // Get all objects with the tag of GravitySource
        GameObject[] gravitySources = GameObject.FindGameObjectsWithTag("GravitySource");

        foreach (GameObject gravitySource in gravitySources)
        {
            // Check if game object is this object
            if (gravitySource == gameObject)
            {
                continue;
            }
            Vector3 gravityDirection = (gravitySource.transform.position - objectTransform.position).normalized;
            float distance = Vector3.Distance(gravitySource.transform.position, objectTransform.position);
            float gravityMagnitude = gravitySource.GetComponent<GravityController>().mass * mass / Mathf.Max(distance * distance, 0.1f);

            gravityForce += gravityDirection * gravityMagnitude / 100f;

            if (gravitySource.GetComponent<GravityController>().mass > 100)
            {
                Vector3 tangentialDirection = Vector3.Cross(gravityDirection, Vector3.up);
                tangentialVelocityForOrbit = tangentialDirection.normalized * Mathf.Sqrt(gravityForce.magnitude * distance);
            }
        }
    }


    void ApplyGravity(Vector3 gravity)
    {
        // Apply the gravity to the object
        velocity += gravity / mass;
    }

    void MoveObject(Vector3 velocity)
    {
        // Move the object in the given direction
        objectTransform.position += velocity * Time.deltaTime;
    }
}
