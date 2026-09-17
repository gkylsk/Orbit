using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GravitySimulation : MonoBehaviour
{
    [SerializeField]
    private GameObject planet;
    [SerializeField]
    private float gravityStrength = 1f;
    [SerializeField]
    private Vector2 velocity;
    [SerializeField]

    private Vector2 initialVelocity;
    bool isSpacePressed = false;

    [SerializeField]
    private float maxDist = 30f;
    [SerializeField]
    private float planetRadius = 0.5f;
    [SerializeField]
    private float orbitTime = 0f;
    private Vector2 startPos;
    private TrailRenderer trail;

    [SerializeField]
    private float orbitMinTime = 5f;
    [SerializeField]
    private float orbitMinDist = 2f;
    [SerializeField]
    private float orbitMaxDist = 10f;

    private bool hasReachedOrbitZone = false;
    private float previousDistance;
    Vector2 offset;

    [SerializeField]
    private float launchSpeed = 5f;
    [SerializeField]
    private float speedChange = 1f;
    [SerializeField]
    private float directionChangeSpeed = 1f;

    private void Start()
    {
        startPos = transform.position;
        trail = GetComponentInChildren<TrailRenderer>();
        offset = planet.transform.position - transform.position;
        previousDistance = offset.magnitude;
    }


    // Update is called once per frame
    void Update()
    {
        
        if (!isSpacePressed)
        {
            //player changes launch speed
            if (Keyboard.current.upArrowKey.isPressed)
            {
                launchSpeed += speedChange * Time.deltaTime;
            }
            if (Keyboard.current.downArrowKey.isPressed)
            {
                launchSpeed -= speedChange * Time.deltaTime;
            }

            launchSpeed = MathF.Max(1f, launchSpeed);

            if (Keyboard.current.leftArrowKey.isPressed)
            {
                initialVelocity = Quaternion.Euler(0f, 0f, directionChangeSpeed * Time.deltaTime) * initialVelocity;
            }
            if (Keyboard.current.rightAltKey.isPressed)
            {
                initialVelocity = Quaternion.Euler(0f, 0f, -directionChangeSpeed * Time.deltaTime) * initialVelocity;
            }
            return;
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !isSpacePressed)
        {
            transform.position = startPos;
            velocity = initialVelocity.normalized * launchSpeed;
            orbitTime = 0f;
            isSpacePressed = true;
            hasReachedOrbitZone = false;
            trail.Clear();
        }
        
        orbitTime += Time.deltaTime;

        offset = planet.transform.position - transform.position;
        // gravity pull
        Vector2 direction = offset.normalized;

        Debug.DrawLine(transform.position, transform.position + (Vector3)direction * 3f);
        //distance to planet
        float distance = offset.magnitude;

        //checks if closer to planet orbit
        float distanceChange = Mathf.Abs(distance - previousDistance);
        previousDistance = distance;

        if(distance > orbitMinDist && distance <= orbitMaxDist)
        {
            hasReachedOrbitZone = true;
        }    

        //check collision with planet
        if (distance <= planetRadius)
        {
            Debug.Log("CRASH" + orbitTime.ToString("F1"));
            isSpacePressed = false;
            return;
        }
        //check if escaped
        if (distance > maxDist)
        {
            Debug.Log("ESCAPED!" + orbitTime.ToString("F1"));
            isSpacePressed = false;
            return;
        }

        if(hasReachedOrbitZone && orbitTime >= orbitMinTime)
        {
            Debug.Log("ORBIT" + orbitTime.ToString("F1"));
            isSpacePressed = false;
            return;
        }
        
        //closer spacecraft = stronger accelaration
        float gravity = gravityStrength / Mathf.Pow(distance, 2);
        Vector2 accelaration = direction * gravity;

        //gravity changes spacecrafts velocity
        velocity = velocity + accelaration * Time.deltaTime;

        transform.position = transform.position +  (Vector3)velocity * Time.deltaTime;

    }
}
