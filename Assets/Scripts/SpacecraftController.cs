using UnityEngine;

public class SpacecraftController : MonoBehaviour
{
    [SerializeField]
    private LevelManager levelManager;
    [SerializeField]
    private float maxDistance = 10f;
    [SerializeField]
    private Planet currentPlanet;
    private Vector2 velocity;
    bool isMoving;

    public void Launch(Vector2 launchVelocity, Planet planet)
    {
        velocity = launchVelocity;
        currentPlanet = planet;
        isMoving = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Planet planet = collision.GetComponentInParent<Planet>();

        if(planet == null)
        {
            return;
        }

        currentPlanet = planet;

        Debug.Log("Gravity switched to: " + planet.name);

        levelManager.ReachPlanet(planet);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMoving || currentPlanet == null)
        {
            return;
        }

        ApplyGravity();
        Move();
    }

    private void ApplyGravity()
    {
        Vector2 offset = currentPlanet.transform.position - transform.position;
        //distance to planet
        float distance = offset.magnitude;

        if (distance < 0.01f) return;

        //check if escaped
        if (distance > maxDistance)
        {
            Stop();
            AudioManager.instance.PlaySfx("Failed");
            UIManager.instance.Retry();
            return;
        }

        // gravity pull
        Vector2 direction = offset.normalized;

        //closer spacecraft = stronger accelaration
        float gravity = currentPlanet.GravityStrength / Mathf.Pow(distance, 2);
        Vector2 accelaration = direction * gravity;

        //gravity changes spacecrafts velocity
        velocity += accelaration * Time.deltaTime;
    }

    private void Move()
    {
        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    public void Stop()
    {
        isMoving = false;
        velocity = Vector2.zero;
    }

    public Vector2 Velocity => velocity;
}
