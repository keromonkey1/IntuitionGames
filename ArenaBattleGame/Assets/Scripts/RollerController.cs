using UnityEngine;
using System.Collections;

public class RollerController : MonoBehaviour {

    float speed = 10f;
    bool isGround = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        RollForward();

	}

    void LateUpdate()
    {
        RotateTowardsPlayer();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Floor")
        {
            isGround = true;
        }
        else if (col.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Floor")
        {
            isGround = false;
        }
    }

    void RollForward()
    {
        if (isGround)
        {
            //this.GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + this.transform.forward * speed);
            if (Mathf.Abs(this.GetComponent<Rigidbody>().velocity.x) < 10 && 
                Mathf.Abs(this.GetComponent<Rigidbody>().velocity.z) < 10)
            {
                this.GetComponent<Rigidbody>().AddForce(this.transform.forward * speed);
            }

            this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);

            this.transform.FindChild("Body").gameObject.transform.Rotate(2.5f, 0, 0);
        }
    }


    void RotateTowardsPlayer()
    {
        GameObject player = GameObject.Find("PlayerCharacter");
        Vector3 distanceVector = this.gameObject.transform.position - player.transform.position;

        this.gameObject.transform.LookAt(player.transform.position);

    }
}
