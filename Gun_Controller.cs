using UnityEngine;
using UnityEngine.UI;

public class Gun_Controller : MonoBehaviour
{
    Gun currentGun;
    public string defaultGun = "ShotGun_0";
    public Camera fpsCam;
    private float nextTimeToFire = 0f;
    private int magCapacity;
    private int currentAmmo;
    public int totalCapacity;
    public Text currentAmmo_count;
    public Text totalAmmo_count;

    private void Start()
    {

        magCapacity = (int) currentGun.magCapacity;
        currentAmmo = magCapacity;
        totalCapacity = 50;

    }

    private void Awake()
    {
        Gun_Generator.Generate();
        currentGun = Gun_Generator.GetGun("ShotGun_0");
    }

    public void OnGunChange(string newGun)
    {
        currentGun = Gun_Generator.GetGun(newGun);
    }

    public void Update()
    {
        bool canShoot = (currentAmmo > 0);
        currentAmmo_count.text = currentAmmo + "/" + magCapacity;
        totalAmmo_count.text = totalCapacity + "";

        if (Input.GetKeyDown(KeyCode.R) && totalCapacity >= 0)
        {

            Reload();
            Debug.Log("Reloading...");

        }

        nextTimeToFire -= Time.deltaTime;

        /*if (Input.GetButton("Fire1") && nextTimeToFire >= 0f)
        {
            nextTimeToFire = Time.time + 1 / currentGun.fireRate;
            Shoot();
            Debug.Log("Shooting...");
        }*/
        if (Input.GetButton("Fire1") && canShoot && nextTimeToFire <= 0f)
        {
            nextTimeToFire = 1 / currentGun.fireRate;
            Shoot();
            Debug.Log("Shooting...");
        }

    }

    public void Shoot()
    {
        currentAmmo -= 1;
        
		RaycastHit hit;
		if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, currentGun.range))
		{
            Target2 target2 = hit.transform.GetComponent<Target2>();
            
            if (target2 != null)
            {
                target2.TakeDamage(currentGun.damage);
                Debug.Log("Hit " + hit.transform.gameObject.tag);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * currentGun.impactForce);
            }

        }
	}

    public void Reload()
    {

        int bulletsToBeReloaded = magCapacity - currentAmmo;
        
        if(totalCapacity >= bulletsToBeReloaded)
        {
            totalCapacity -= bulletsToBeReloaded;
            currentAmmo += bulletsToBeReloaded;
        }
        if(totalCapacity < bulletsToBeReloaded)
        {
            currentAmmo += totalCapacity;
            totalCapacity = 0;
        }

    }

}
