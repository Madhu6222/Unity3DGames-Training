using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{

    public float movementSpeed;
    public MapLimits Limits;
    public GameObject bullet;
    public Transform position1;
    public Transform positionLeft;
    public Transform positionRight;
    public float shotPower;
    public Text highScoreText;
    AudioSource audioSource;
    public AudioClip shotSound;
    public int score;
    int highScore;
    public Text scoreText;
    int power;
    public int hp;


    // Start is called before the first frame update
    void Start()
    {
        power = 1;
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("highscore"))
        {
            highScoreText.text = PlayerPrefs.GetInt("highScore", 0).ToString();
           // PlayerPrefs.SetInt("highScore", highScore);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
        scoreText.text = score.ToString();
      //  highScoreText.text = PlayerPrefs.GetInt("highscore").ToString();
        if (score> PlayerPrefs.GetInt("highScore",0))
        {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
            highScoreText.text = highScore.ToString();

        }
        
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, Limits.minimumX, Limits.maximumX),
           Mathf.Clamp(transform.position.y, Limits.minimumY, Limits.maximumY), 0.0f);

        if (hp <= 0)
        {
            Destroy(gameObject);
            FindObjectOfType<GameManager>().EndGame();
        }
        
       
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.right * -movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.up * -movementSpeed * Time.deltaTime);
        }
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("shot!");
            audioSource.PlayOneShot(shotSound);
            switch (power)
            {
                case 1:
                    {
                        GameObject newBullet = Instantiate(bullet, position1.position, transform.rotation);
                        newBullet.GetComponent<Rigidbody>().velocity = Vector3.up * shotPower;
                    }break;
                case 2:
                    {
                        GameObject Bullet1 = Instantiate(bullet, positionLeft.position, transform.rotation);
                        Bullet1.GetComponent<Rigidbody>().velocity = Vector3.up * shotPower;
                        GameObject Bullet2 = Instantiate(bullet, positionRight.position, transform.rotation);
                        Bullet2.GetComponent<Rigidbody>().velocity = Vector3.up * shotPower;
                    }break;
                case 3:
                    {
                        GameObject Bullet1 = Instantiate(bullet, positionLeft.position, transform.rotation);
                        Bullet1.GetComponent<Rigidbody>().velocity = Vector3.up * shotPower;
                        GameObject Bullet2 = Instantiate(bullet, positionRight.position, transform.rotation);
                        Bullet2.GetComponent<Rigidbody>().velocity = Vector3.up * shotPower;
                        GameObject Bullet3 = Instantiate(bullet, position1.position, transform.rotation);
                        Bullet3.GetComponent<Rigidbody>().velocity = Vector3.up * shotPower;

                    }break;
                default:
                    {
                        GameObject newBullet = Instantiate(bullet, position1.position, transform.rotation);
                        newBullet.GetComponent<Rigidbody>().velocity = Vector3.up * shotPower;
                    }
                    break;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "PowerUp")
        {
            if (power<3)
            {
                power++;
                Destroy(col.gameObject);
            }
        }
        if (col.gameObject.tag == "PowerDown")
        {
            if (power > 1)
            {
                power--;
                Destroy(col.gameObject);
            }

        }
    }

   
}
