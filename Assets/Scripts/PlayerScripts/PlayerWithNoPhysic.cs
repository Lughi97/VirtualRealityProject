using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWithNoPhysic : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float mass;
    private float radious;

    private CharacterController characterController;
    private float originalStepOffset;
    private float ySpeed;
    // Start is called before the first frame update
    void Start()
    {
     
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
        radious = (transform.localScale.x) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        
    }

    void playerMovement()
    {
        /*character Movement withouth rigidBody and Physics*/

        float horizzontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        rotationSpeed = speed / radious;

        /*Movement direction*/
        Vector3 movementDirection = new Vector3(horizzontalInput, 0, verticalInput);

        float magnitudeDirection = Mathf.Clamp01(movementDirection.magnitude) * speed;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * mass * Time.deltaTime;

        if (characterController.isGrounded)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
        }
        else
        {
            characterController.stepOffset = 0;
        }

        Vector3 velocity = movementDirection * magnitudeDirection;
        velocity = AdjustVelocityToSlope(velocity);
        velocity.y += ySpeed;
        characterController.Move(velocity * Time.deltaTime);

        /*Movement Rotaion*/
        Vector3 movementRotation = new Vector3(verticalInput, 0, -horizzontalInput);
        float magnitudeRotation = Mathf.Clamp01(movementRotation.magnitude) * rotationSpeed;
        movementRotation.Normalize();
        movementRotation *=  rotationSpeed * (2 * Mathf.PI * magnitudeRotation)*Time.deltaTime;
        transform.Rotate(movementRotation, Space.World);
     
    }

    private Vector3 AdjustVelocityToSlope(Vector3 velocity)
    {
        var ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 5f))
        {
            Debug.Log(hitInfo);
            var slopeRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            var adjustVelocity = slopeRotation * velocity;

            if (adjustVelocity.y < 0)
            {
                return adjustVelocity;
            }
        }

        return velocity;
    }
}
