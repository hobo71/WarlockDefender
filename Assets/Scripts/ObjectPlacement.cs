using UnityEngine;
using System.Collections;

public class ObjectPlacement : MonoBehaviour
{

    Vector3 currentTileCoord;

    public Material validMat;
    public Material invalidMat;

    public GameObject selectionPrefab;
    public GameObject Object;

    public Camera buildingCamera;

    public float tileSize;

    private GameObject selectionCube;

    void Start()
    {
        selectionCube = (GameObject)Instantiate(selectionPrefab, buildingCamera.transform.position, gameObject.transform.rotation);
    }

    void Update()
    {
        Ray mapRay = buildingCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit mapHitInfo;
        RaycastHit objectHitInfo = new RaycastHit();
        Renderer selectCubeRend = selectionCube.GetComponent<MeshRenderer>();
        bool canBuild = true;

        if (GetComponent<Collider>().Raycast(mapRay, out mapHitInfo, Mathf.Infinity))
        {
            //int x = Mathf.FloorToInt(mapHitInfo.point.x / tileSize);
            //int z = Mathf.FloorToInt(mapHitInfo.point.z / tileSize);

            currentTileCoord.x = mapHitInfo.point.x;
            currentTileCoord.y = 2f;
            currentTileCoord.z = mapHitInfo.point.z;

            selectionCube.transform.position = currentTileCoord/* * tileSize*/;

            Vector3 pointA = new Vector3(mapHitInfo.point.x, 1000f, mapHitInfo.point.z);

            Physics.SphereCast(pointA, 2.5f, Vector3.down, out objectHitInfo, Mathf.Infinity);

            if (objectHitInfo.collider.tag == "Object")
            {
                selectCubeRend.material = invalidMat;
                canBuild = false;
            }
            else if (objectHitInfo.collider.tag == "Map")
            {
                selectCubeRend.material = validMat;
                canBuild = true;
            }

        }
        else
        {
            canBuild = false;
            selectCubeRend.material = invalidMat;

        }

        if (!canBuild)
        {
            if (objectHitInfo.collider == null)
                return;
            if (Input.GetMouseButtonDown(1) && objectHitInfo.collider.tag == "Object")
            {
                Destroy(objectHitInfo.transform.parent.gameObject);
                Debug.Log("Destroy");
            }
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(Object, new Vector3(selectionCube.transform.position.x, 0f, selectionCube.transform.position.z), selectionCube.transform.rotation);
        }

    }
}