using UnityEngine;
using System.Collections;

public class CastleLifePanelScript : MonoBehaviour {

    public GameObject lifeBar;

    private float life = 100f;
    private RectTransform transf;

    void Start () {
        transf = lifeBar.GetComponent<RectTransform> ();
    }

    void Update () {

        life = CastleStats.life;
        transf.sizeDelta = new Vector2(200F * life / 100F, 30F);
    }
}
