using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    public float speed;
    public float changeTimer;
    public bool directionSwitch;
    public GameObject particleEffect;
    float maxTimer;
    public float shootPower;
    public int hp;
    public bool canShoot;
    public int scoreReward;
    public Transform shootingPosition;
    public GameObject bullet;
    public GameObject powerUp;
    public GameObject powerDown;
    float shootTimer;
    float maxShootTimer;
    Rigidbody rigidbody;
    public MapLimits Limits;

    

    // Start is called before the first frame update
    void Start()
    {
        shootTimer = Random.Range(1f,8f);
        maxShootTimer = shootTimer;
        rigidbody = GetComponent<Rigidbody>();
        maxTimer = changeTimer;

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        switchTimer();

        if (transform.position.x == Limits.maximumX) { switchDir(directionSwitch); }

        if (transform.position.x == Limits.minimumX) {switchDir(directionSwitch); }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, Limits.minimumX, Limits.maximumX),
           Mathf.Clamp(transform.position.y, Limits.minimumY, Limits.maximumY), 0.0f);

        shootTimer -= Time.deltaTime;
        if (canShoot)
        {
            if (shootTimer <= 0)
            {
                GameObject newBullet = Instantiate(bullet, shootingPosition.transform.position, transform.rotation);
                newBullet.GetComponent<Rigidbody>().velocity = Vector3.up * -shootPower;
                shootTimer = maxShootTimer;
            }
        }
        

    }

    void Movement()
    {
        if(directionSwitch)
        rigidbody.velocity = new Vector3(speed* Time.deltaTime, -speed *Time.deltaTime, 0);

        else
            rigidbody.velocity = new Vector3(-speed * Time.deltaTime, -speed * Time.deltaTime, 0);
    }

    void switchTimer()
    {
        changeTimer -= Time.deltaTime;
        if (changeTimer<0)
        {
            switchDir(directionSwitch);
            changeTimer = maxTimer;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "EnemyBullet")
        {
            return;
        }
        if (col.gameObject.tag== "FriendlyBullet")
        {
            Destroy(col.gameObject);
            Instantiate(particleEffect, transform.position, transform.rotation);
            hp--;
            if (hp <= 0)
            {
                int randomNumber = Random.Range(0, 100);
                if (randomNumber < 30) Instantiate(powerUp, transform.position, powerUp.transform.rotation);
                if (randomNumber > 80) Instantiate(powerDown, transform.position, powerDown.transform.rotation);

                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().score += scoreReward;
                Destroy(gameObject);
                
                
            }
        }
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerCharacter>().hp--;
            Instantiate(particleEffect, transform.position, transform.rotation);
            hp--;
            if (hp<=0)
            {
                col.gameObject.GetComponent<PlayerCharacter>().score += scoreReward;
                Destroy(gameObject);
            }
        }
    }

    bool switchDir(bool dir)
    {
        if (dir) directionSwitch = false;
        else directionSwitch = true;
        return directionSwitch;

        
    }
}
