using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
    public float panSpeed = 30.0f;
    public int delta = 10;

    private Vector3 mouseOrigin;

    // Use this for initialization
    void Start () {

	}

    // Update is called once per frame
    void Update() {
        Cursor.visible = true;

        mouseOrigin = Input.mousePosition;
        //Vector3 pos = gameObject.GetComponent<Camera>().ScreenToViewportPoint(Input.mousePosition);
        //Vector3 move = pos;

        if (mouseOrigin.x <= 0 + delta)
        {
            transform.position -= transform.right * Time.deltaTime * panSpeed;
        }

        if (mouseOrigin.x >= (Screen.width - delta))
        {
            transform.position += transform.right * Time.deltaTime * panSpeed;
        }

        if (mouseOrigin.y <= 0 + delta)
        {
            transform.position -= new Vector3(0, 0, transform.forward.z * Time.deltaTime * panSpeed);
        }

        if (mouseOrigin.y >= (Screen.height - delta))
        {
            transform.position += new Vector3(0, 0, transform.forward.z * Time.deltaTime * panSpeed);
        }
    }
}
