using UnityEngine;
using System.Collections;

public class LineRenderSelfDestruct : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(SelfDestruct());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(.00001f);
        Destroy(this.gameObject);
    }
}
