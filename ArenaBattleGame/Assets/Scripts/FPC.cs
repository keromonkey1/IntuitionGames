using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

//first person camera
public class FPC : MonoBehaviour {

    public float speed = 10f;
    public float jumpHeight = 25f;
    public bool isGrounded = false;

    private Camera fpcCamera;
    private Canvas attachedCanvas;
    private float rotationY = 0f;
    private float health = 250f;
    private float maxHealth = 250f;
    private Color lightColor = new Color(0f, 255f, 0f);

	// Use this for initialization
	void Start () {
        fpcCamera = this.GetComponentInChildren<Camera>();
        attachedCanvas = this.GetComponentInChildren<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
        InputMovement();
        HealthHandler();
	}

    void CameraController()
    {
        this.transform.LookAt(this.transform);
        this.transform.RotateAround(this.transform.position, Vector3.up, Input.GetAxis("Mouse X") * speed);

        rotationY += Input.GetAxis("Mouse Y") * 15;
        rotationY = Mathf.Clamp(rotationY, -60, 60);
        fpcCamera.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
    }


    public void HealthHandler()
    {
        float percent = health / maxHealth;
        attachedCanvas.GetComponentInChildren<Text>().text = "Health: " + (percent * 100) + "%";
        //this.healthText.GetComponent<Text>().text = "Health: " + (percent * 100) + "%";
        //lightColor = new Color(1 - percent, percent, 0f);
        //this.GetComponentInChildren<Light>().color = lightColor;

        //Respawn();
    }

    void InputMovement()
    {

        if (Input.GetMouseButton(1))
        {
            CameraController();
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKey(KeyCode.W))
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + this.transform.forward * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position - this.transform.right * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position - this.transform.forward * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + this.transform.right * speed * Time.deltaTime);

    }
}
