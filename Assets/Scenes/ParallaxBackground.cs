using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform player;
    public float parallaxFactor = 0.1f;
    private Vector3 previousPlayerPosition;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        previousPlayerPosition = player.position;
    }

    void Update()
    {
        Vector3 delta = player.position - previousPlayerPosition;
        transform.position += new Vector3(delta.x * parallaxFactor, 0, 0);
        previousPlayerPosition = player.position;
    }
}

