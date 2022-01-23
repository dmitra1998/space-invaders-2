using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    private Transform aimTransform;
    private Animator aimAnimation;
    [SerializeField] GameObject bulletSpawn;
    Coroutine firingCorountine;
    float angle;
    //public GameObject fire;

    // Start is called before the first frame update
    void Awake()
    {
        aimTransform = transform.Find("Aim");
        aimAnimation = aimTransform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        HandleShootinmg();
        Fire();
    }

    private void HandleShootinmg()
    {
        if(Input.GetMouseButtonDown(0))
        {
            aimAnimation.SetTrigger("Shoot");
        }
    }

    private void Aim()
    {
        Vector3 mousePosition = MousePosition();
        //Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }


    //Get Mouse Position with z = 0f
    public static Vector3 MousePosition()
    {
        Vector3 vec = MousePositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 MousePositionWithZ()
    {
        return MousePositionWithZ(Input.mousePosition, Camera.main);
    }

    public static Vector3 MousePositionWithZ(Camera worldCamera)
    {
        return MousePositionWithZ(Input.mousePosition, worldCamera);
    }

    public static Vector3 MousePositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCorountine = StartCoroutine(ShootOnPress());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCorountine);
        }
    }

    IEnumerator ShootOnPress()
    {
       // Vector2 shootingDirection = Aim.transform.localPosition;
        while (true) 
        {
            Debug.Log("W");
            GameObject tbullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity) as GameObject;
            tbullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5f);
            //tbullet.transform.Rotate(0, 0, Mathf.Atan2(shootinDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
            tbullet.transform.Rotate(0, 0, angle);
            yield return new WaitForSeconds(5f);
        } 
    }

}
