using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField]
    private float gravityStrength = 1f;
    [SerializeField]
    private float rotationSpeed = 20f;
    public float GravityStrength => gravityStrength;

    private void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
