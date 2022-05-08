using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 targetOffset;
    public float movementSpeed = 3;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Exacutes the move camera function once per frame to keep up with the player
        MoveCamera();
    }

    /// <summary>
    /// Moves(not snaps) the camera to the position of the player at a certain position
    /// Will extract the position (Vector3) of the GameObject this script is
    /// attached to (the camera) from its transform component. It will set this
    /// position to be at the player position at a specific offset.
    /// To do this, it uses a Vector3.Lerp method, which takes in the parameters
    /// of the current position, and targets position wanted and the speed it should
    /// move at. the target is a transform variable, so the position is derived
    /// straight off that, the taregt position is added by the targetOffset,
    /// which all it does is takes the vector3 of the target.position and adds
    /// new vector 3 with a larger y so as to off set if from the actual targe
    /// position, because then the camera would place it self in the player.
    /// The third argument is the move speed, this is the speed the the camara
    /// will move towards its player off set at, so that it doesnt stay snepped
    /// at the players position
    /// </summary>
    void MoveCamera()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + targetOffset, movementSpeed * Time.deltaTime);
    }
}
