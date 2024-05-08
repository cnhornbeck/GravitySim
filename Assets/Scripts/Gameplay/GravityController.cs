using UnityEngine;

public class GravityController : MonoBehaviour
{

    Transform objectTransform;
    [SerializeField] Vector3 velocity;
    [SerializeField] Vector3 gravityDirection;


    // Start is called before the first frame update
    void Start()
    {
        objectTransform = GetComponent<Transform>();
        velocity = Vector3.zero;
        gravityDirection = new(0, -0.01f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        SetGravity();

        // Apply the gravity to the object
        ApplyGravity(gravityDirection);

        MoveObject(velocity);
    }

    void SetGravity()
    {
        // Set the gravity direction based on this world position
        gravityDirection = (GetWorldSpaceMouseWorldPos() - objectTransform.position) / 150f;
    }

    Vector3 GetWorldSpaceMouseWorldPos()
    {
        // Create a ray from the camera through the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a plane at y = 0, assuming the normal of the plane is facing towards the camera
        Plane plane = new(Vector3.up, new Vector3(0, 0, 0));

        // Variable to store the distance along the ray where it intersects the plane
        float enter;

        // Check if the ray hits the plane
        if (plane.Raycast(ray, out enter))
        {
            //Get the point that is clicked
            Vector3 hitPoint = ray.GetPoint(enter);
            return hitPoint;
        }
        else
        {
            // Return zero vector if there is no intersection
            return Vector3.zero;
        }
    }


    void ApplyGravity(Vector3 gravity)
    {
        // Apply the gravity to the object
        velocity += gravity;
    }

    void MoveObject(Vector3 velocity)
    {
        // Move the object in the given direction
        objectTransform.position += velocity * Time.deltaTime;
    }
}
