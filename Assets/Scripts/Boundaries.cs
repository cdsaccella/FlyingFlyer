using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectHeight;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;

        if(viewPos.y < screenBounds.y * -1)
        {
            gameObject.SendMessage("UpdateState", "PlayerImpulseUp");            
        }
        else if (viewPos.y > screenBounds.y - objectHeight)
        {
            gameObject.SendMessage("UpdateState", "PlayerImpulseDown");
        }
    }
}
