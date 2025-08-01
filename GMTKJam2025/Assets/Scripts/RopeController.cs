using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D.Animation;

public class RopeController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    [SerializeField] Rigidbody2D mouseTest;


    private Rigidbody2D endRB;
    [SerializeField] private GameObject playerHook;

    private Rigidbody2D grabbed;
    private float maxDistance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Rope>().numLinks > 0)
        {
            Debug.Log(this.GetComponent<Rope>().numLinks);
            endRB = this.GetComponent<Rope>().bottom.GetComponent<Rigidbody2D>().GetComponent<Rigidbody2D>();


            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            float mouseDistance = Vector2.Distance(playerHook.transform.position, mouseWorldPosition);
            mouseWorldPosition.z = 0f;

            maxDistance = endRB.GetComponent<SpriteRenderer>().bounds.size.y * this.GetComponent<Rope>().numLinks;
            //Vector2.Distance(playerHook.transform.position, endRB.transform.position);

            if (Input.GetMouseButtonDown(0))
            {
                if (Vector2.Distance(mouseWorldPosition, endRB.position) < 0.5f)
                {
                    grabbed = endRB;
                }

            }
            else if (Input.GetMouseButtonUp(0))
            {
                grabbed = null;
            }

            if (grabbed != null)
            {
                if (mouseDistance > maxDistance)
                {
                    Vector3 rotation = mouseWorldPosition - new Vector3(playerHook.transform.position.x, playerHook.transform.position.y, 0);

                    float angle = Mathf.Atan2(rotation.y, rotation.x);
                    if (angle < 0)
                    {
                        angle = -2 * Mathf.PI + angle;
                    }

                    float grabx = (maxDistance * Mathf.Cos(angle));
                    float graby = (maxDistance * Mathf.Sin(angle));
                    Debug.Log(mouseWorldPosition);
                    //grabbed.position = rotation;
                    //grabbed.transform.Translate(rotation);
                    // float a2 = (float)Math.Sqrt((mouseDistance * mouseDistance)/2f);
                    // grabbed.position = new Vector2(a2,a2);
                    //Debug.Log(a2);
                    grabbed.position = new Vector2(playerHook.transform.position.x + grabx, playerHook.transform.position.y + graby);
                    mouseTest.position = new Vector2(playerHook.transform.position.x + grabx, playerHook.transform.position.y + graby);
                }
                else
                {
                    grabbed.position = mouseWorldPosition;
                }

            }
        }

        //Debug.Log(Vector2.Distance(mouseWorldPosition, endRB.position));
        

    }
}
