using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField]
    private float gravityStrength = 1f;
    public float GravityStrength => gravityStrength;
}
