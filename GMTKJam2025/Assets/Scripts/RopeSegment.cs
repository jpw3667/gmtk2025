using UnityEngine;

public class RopeSegment : MonoBehaviour
{

    public GameObject connectedAbove, connectedBelow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        ResetAnchor();
    }
    public void ResetAnchor()
    {
        if (connectedAbove != null)
        {
            connectedAbove = GetComponent<HingeJoint2D>().connectedBody.gameObject;
            RopeSegment aboveSegment = connectedAbove.GetComponent<RopeSegment>();

            if (aboveSegment != null)
            {
                aboveSegment.connectedBelow = gameObject;
                float spriteBottom = connectedAbove.GetComponent<SpriteRenderer>().bounds.size.y;
                GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, spriteBottom * -1);
            }
            else
            {
                GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0);
            }
        }
    }
}
