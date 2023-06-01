using System.Collections;
using UnityEngine;
using EnemyAI;

// This class is created for the example scene. There is no support for this script.
public class PlayerShooting : MonoBehaviour
{
    public Transform shotOrigin;
    public Transform drawShotOrigin;
    public LayerMask shotMask;
    public WeaponMode weaponMode = WeaponMode.SEMI;
    public int RPM = 600;
    public GameObject Gun;
    public GameObject Knife;
    public Animator anim;


    public enum WeaponMode
    {
        SEMI,
        AUTO
    }

    private LineRenderer laserLine;
    private float weaponRange = 100f;
    private float bulletDamage = 10f;
    private bool canShoot;
    private float knifeDamage = 100f;
    private float knifeRange = 5f;
    private bool canStab; 

    public AudioSource gunAudio;
    public AudioSource stabAudio;
    private WaitForSeconds halfShotDuration;

    

    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        canShoot = true;
        canStab = false;
        

        float waitTime = 60f / RPM;
        halfShotDuration = new WaitForSeconds(waitTime / 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponMode == WeaponMode.SEMI && Input.GetButtonDown("Fire1") && canShoot)
        {
            Shoot();
        }
        else if (weaponMode == WeaponMode.AUTO && Input.GetButton("Fire1") && canShoot)
        {
            Shoot();
        }

        if (Gun.activeSelf)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }
        if (Knife.activeSelf)
        {
            canStab = true;
        }
        else
        {
            canStab = false;
        }
       
        if (Knife.activeSelf && Input.GetButtonDown("Fire1") && canStab)
        {
            Stab();
            anim.SetTrigger("Attack");
        }
        

    }

    void Shoot()
    {
        StartCoroutine(ShotEffect());

        laserLine.SetPosition(0, drawShotOrigin.position);
        Physics.SyncTransforms();

        if (Physics.Raycast(shotOrigin.position, shotOrigin.forward, out RaycastHit hit, weaponRange, shotMask))
        {
            laserLine.SetPosition(1, hit.point);

            // Call the damage behaviour of target if exists.
            if (hit.collider != null)
            {
                hit.collider.SendMessageUpwards("HitCallback", new HealthManager.DamageInfo(hit.point, shotOrigin.forward, bulletDamage, hit.collider), SendMessageOptions.DontRequireReceiver);
            }
        }
        else
        {
            laserLine.SetPosition(1, drawShotOrigin.position + (shotOrigin.forward * weaponRange));
        }

        // Call the alert manager to notify the shot noise.
        GameObject.FindGameObjectWithTag("GameController").SendMessage("RootAlertNearby", shotOrigin.position, SendMessageOptions.DontRequireReceiver);
    }

    void Stab()
    {
        StartCoroutine(StabEffect());

        laserLine.SetPosition(0, drawShotOrigin.position);
        Physics.SyncTransforms();

        if (Physics.Raycast(shotOrigin.position, shotOrigin.forward, out RaycastHit hit, knifeRange, shotMask))
        {
            laserLine.SetPosition(1, hit.point);

            // Call the damage behaviour of target if exists.
            if (hit.collider != null)
            {
                hit.collider.SendMessageUpwards("HitCallback", new HealthManager.DamageInfo(hit.point, shotOrigin.forward, knifeDamage, hit.collider), SendMessageOptions.DontRequireReceiver);
            }
        }
        else
        {
            laserLine.SetPosition(1, drawShotOrigin.position + (shotOrigin.forward * knifeRange));
        }

        // Call the alert manager to notify the shot noise.
        GameObject.FindGameObjectWithTag("GameController").SendMessage("RootAlertNearby", shotOrigin.position, SendMessageOptions.DontRequireReceiver);

    }



        private IEnumerator ShotEffect()
    {
        gunAudio.Play();
        laserLine.enabled = true; // Turn on the line renderer
        canShoot = false;

        yield return halfShotDuration;

        laserLine.enabled = false; // Deactivate the line renderer

        yield return halfShotDuration;

        if (weaponMode == WeaponMode.SEMI)
        {
            yield return halfShotDuration;
            yield return halfShotDuration;
        }

        canShoot = true;
    }
    

    private IEnumerator StabEffect()
    {
        stabAudio.Play();
        laserLine.enabled = false; // Turn on the line renderer
        
        canStab = false;

        yield return halfShotDuration;

        laserLine.enabled = false; // Deactivate the line renderer

        yield return halfShotDuration;

        if (weaponMode == WeaponMode.SEMI)
        {
            yield return halfShotDuration;
            yield return halfShotDuration;
        }

        canStab = true;
    }

    // Player dead callback.
    public void PlayerDead()
    {
        canStab = false;
        canShoot = false;
    }
}
