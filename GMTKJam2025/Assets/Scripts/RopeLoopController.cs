using UnityEngine;

public class RopeLoopController : MonoBehaviour
{
    [SerializeField] PlayerController player;
    private Vector2 startingPos;

    private void Start()
    {
        startingPos = transform.position;
    }

    private void Update()
    {
        transform.position = startingPos;
    }
    // Update is called once per frame
    void OnMouseEnter()
    {
        player.attachedLoop = this.gameObject;
        Debug.Log("Connected");
        
    }
    void OnMouseExit()
    {
        player.attachedLoop = null;
        Debug.Log("Disconnected");
    }
}
