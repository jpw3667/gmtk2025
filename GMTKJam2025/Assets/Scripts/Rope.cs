using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D hook;
    public GameObject prefabRopeSegment;
    public int numLinks;
    public HingeJoint2D top;
    public Rigidbody2D bottom;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateRope();
      
    }

    void GenerateRope()
    {
        Rigidbody2D prevBody = hook;

        for (int i = 0; i < numLinks; i++)
        {
            GameObject newSeg = Instantiate(prefabRopeSegment);
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            HingeJoint2D joint = newSeg.GetComponent<HingeJoint2D>();
            joint.connectedBody = prevBody;

            prevBody = newSeg.GetComponent<Rigidbody2D>();

            if (i == 0)
            {
                top = joint;
            }
        }
    }

    public void addLink()
    {
        {
            numLinks++;
            GameObject newSeg = Instantiate(prefabRopeSegment);
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            HingeJoint2D joint = newSeg.GetComponent<HingeJoint2D>();
            joint.connectedBody = hook;

            if(top == null)
            {
                top = joint;
                top.GetComponent<RopeSegment>().ResetAnchor();
                top.connectedBody = hook;
                bottom = newSeg.GetComponent<Rigidbody2D>();
            }


            else
            {
                newSeg.GetComponent<RopeSegment>().connectedBelow = top.gameObject;
                top.connectedBody = newSeg.GetComponent<Rigidbody2D>();
                top.GetComponent<RopeSegment>().ResetAnchor();
                top = joint;
            }


           
        }

    }


    public void attach(GameObject loop)
    {
        GameObject tempSeg = bottom.GetComponent<RopeSegment>().connectedBelow;

        Debug.Log("connected!!");
        bottom.transform.position = loop.transform.position;
        //loop.transform.parent = hook.transform;
        loop.GetComponent<HingeJoint2D>().connectedBody = bottom.GetComponent<Rigidbody2D>();
        loop.GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, bottom.GetComponent<SpriteRenderer>().bounds.size.y * -0.666f);
       // bottom.GetComponent<RopeSegment>().connectedAbove = loop.gameObject;
       // loop.GetComponent<RopeSegment>().connectedAbove = bottom.gameObject;
         //loop.GetComponent<RopeSegment>().ResetAnchor();


    }

    public void removeLink()
    {
        if (numLinks > -1)
        {
            numLinks--;
            if (top.gameObject.GetComponent<RopeSegment>().connectedBelow != null)
            {
                HingeJoint2D newTop = top.gameObject.GetComponent<RopeSegment>().connectedBelow.GetComponent<HingeJoint2D>();
                newTop.connectedBody = hook;
                newTop.gameObject.transform.position = hook.transform.position;
                newTop.GetComponent<RopeSegment>().ResetAnchor();
                Destroy(top.gameObject);
                top = newTop;
            }
            else
            {
                Destroy(top.gameObject);
            }
        }
    }

}
