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

        if (mouseOrigin.x <= 0 + delta && transform.position.z <= 380f)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        if (mouseOrigin.x >= (Screen.width - delta) && transform.position.z >= 107f)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        if (mouseOrigin.y <= 0 + delta && transform.position.x >= 5f)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        if (mouseOrigin.y >= (Screen.height - delta) && transform.position.x <= 370f)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
    }
}
