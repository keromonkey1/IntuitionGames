using UnityEngine;
using System.Collections;

public class BulletHandlerScript : MonoBehaviour {

    public bool isDead = false;
    float speed = 25f;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.transform.position += (this.transform.forward * Time.deltaTime * speed);
        }

    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(10f);
        Kill();
    }

    public void Kill()
    {
        isDead = true;
    }
}
