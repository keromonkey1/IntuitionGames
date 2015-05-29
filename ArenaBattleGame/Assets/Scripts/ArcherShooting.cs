using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ArcherShooting : MonoBehaviour {


    public GameObject bulletSpawnPoint;
    public GameObject bulletLinePrefab;
    public GameObject bulletPrefab;

    private float shootTimer = 0f;
    private float shootCooldown = 1f;
    private GameObject lineRendererObject;
    private Vector3 bulletLookAt = Vector3.zero;

	// Use this for initialization
	void Start () {
        lineRendererObject = Instantiate(bulletLinePrefab, transform.position, Quaternion.identity) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateLineRenderer();
        AimManager();
        Shoot(); 
	}


    void FixedUpdate()
    {
        ShootingCooldown();
    }

    void ShootingCooldown()
    {
        shootTimer += Time.deltaTime;
    }

    public void Shoot()
    {
        if (CanShoot())
        {
            if (Input.GetMouseButton(0))
            {
                shootTimer = 0;
                SpawnBullet();
                //Debug.Log("SHOOTING");
            }
        }
    }

    public void AimManager()
    {
        GameObject cameraChild = this.gameObject.transform.FindChild("Main Camera").gameObject;
        if (Input.GetKey(KeyCode.Q))
        {
            cameraChild.transform.FindChild("ActiveGun").gameObject.transform.position = cameraChild.transform.FindChild("GunHardScope").gameObject.transform.position;
        }
        else
        {
            cameraChild.transform.FindChild("ActiveGun").gameObject.transform.position = cameraChild.transform.FindChild("GunHipFire").gameObject.transform.position;
        }
    }

    void SpawnBullet()
    {
        GameObject tempBullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position,
            lineRendererObject.GetComponent<LineRenderer>().transform.rotation) as GameObject;
        tempBullet.transform.LookAt(bulletLookAt);
    }

    public void UpdateLineRenderer()
    {
        float x = Screen.width / 2;
        float y = Screen.height / 2;
        Ray aimingRay = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));
      

        lineRendererObject.transform.parent = bulletSpawnPoint.transform;
        LineRenderer bulletLine = lineRendererObject.GetComponent<LineRenderer>();
        RaycastHit aimingRayHit;

        Vector3 startPos = bulletSpawnPoint.transform.TransformPoint(Vector3.zero);
        Vector3 endpos = Vector3.zero;

        if (Physics.Raycast(aimingRay, out aimingRayHit, Mathf.Infinity))
        {
            bulletLookAt = endpos = aimingRayHit.point;
        }
        else
        {
            bulletLookAt = endpos = aimingRay.GetPoint(2000f);
        }

        bulletLine.SetPosition(0, startPos);
        bulletLine.SetPosition(1, endpos);
    }

    public bool CanShoot()
    {
        return (shootTimer >= shootCooldown);
    }
}
