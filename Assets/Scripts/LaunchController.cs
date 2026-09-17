using UnityEngine;

public class LaunchController : MonoBehaviour
{
    private UIManager uIManager;
    [SerializeField]
    private SpacecraftController spacecraft;
    [SerializeField]
    private Planet startingPlanet;
    [SerializeField]
    private float launchSpeed = 1f;
    [SerializeField]
    private float directionAngle = 90f;
    public Vector2 launchVelocity { get; private set; }


    private void Start()
    {
        uIManager = UIManager.instance;
        UpdateSpacecraftRotation();
    }

    public void RotateLeft()
    {
        directionAngle += 5f;
        uIManager.ChangeDirectionText(directionAngle.ToString());
        UpdateSpacecraftRotation();
    }

    public void RotateRight()
    {
        directionAngle -= 5f;
        uIManager.ChangeDirectionText(directionAngle.ToString());
        UpdateSpacecraftRotation();
    }

    public void IncreaseSpeed()
    {
        launchSpeed += 0.1f;
        uIManager.ChangeSpeedText(launchSpeed.ToString());
    }

    public void DecreaseSpeed()
    {
        launchSpeed = Mathf.Max(.1f, launchSpeed - 0.1f);
        uIManager.ChangeSpeedText(launchSpeed.ToString());
    }

    public void Launch()
    {
        float radians = directionAngle * Mathf.Deg2Rad;

        Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        launchVelocity = direction * launchSpeed;
        spacecraft.Launch(launchVelocity, startingPlanet);
    }

    private void UpdateSpacecraftRotation()
    {
        spacecraft.transform.rotation = Quaternion.Euler(0f, 0f, directionAngle);
    }
}
