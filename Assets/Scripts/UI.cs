using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class UI : MonoBehaviour
{
    public string title;
    public string nextLevel;
    [SerializeField]
    private Text text;
    [SerializeField]
    private AudioSource player;
    [SerializeField]
    private AudioClip echoe;
    [SerializeField]
    private AudioClip hurt;
    [SerializeField]
    private GameObject lifeImg;
    [SerializeField]
    private float life = 10f;
    [SerializeField]
    private float Maxlife = 10f;
    private bool invicible = false;

    private float last = 10f;

    private bool playing = false;

    [SerializeField]
    private GameObject deadText;

    [SerializeField]
    private float COOLDOWN = 2;

    private Gun gun;

    private void Awake()
    {
        gun = GetComponentInChildren<Gun>();
        
    }

    void Start()
    {
        life = Maxlife;
        Invoke("write", 1.0f);
    }

    private void Update()
    {
        last += Time.deltaTime;

        if (playing && UnityEngine.Input.GetAxis("Fire1") == 1 && last >= COOLDOWN)
        {
            last = 0;

            gun.shoot();
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        else if (Input.GetKey(KeyCode.R))
        {
            reload();
        }
    }

    void write()
    {
        text.text = title;
        player.PlayOneShot(echoe);
        Invoke("hideText", 2.0f);
    }

    void hideText()
    {
        text.text = "";
        player.PlayOneShot(echoe);
        text.gameObject.SetActive(false);
        gameObject.GetComponent<FirstPersonController>().enabled = true;
        playing = true;

    }

    public void black()
    {
        playing = false;
        text.gameObject.SetActive(true);
        gameObject.GetComponent<FirstPersonController>().enabled = false;
        player.PlayOneShot(echoe);
        Invoke("loadNext", 1f);

    }

    void loadNext()
    {
       SceneManager.LoadScene(nextLevel);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="End")black();
        if (other.tag == "BossTrigger") other.gameObject.GetComponent<BossTrigger>().spawn();
        if (other.tag == "FallTrigger")
        {
            loseLife(1);
            gameObject.GetComponent<FirstPersonController>().enabled = false;
            transform.position = other.GetComponent<FallTrigger>().getRespawnPoint();
            Invoke("enableFps", 0.5f);
        }
        if (other.tag == "meat")
        {
            gainLife(10);
            Destroy(other.gameObject);
            object[] parms = new object[2] { Color.red, 2f };
            StartCoroutine("colorSelf", parms);
        }
        if (other.tag == "star")
        {
            invicible = true;
            GetComponentInChildren<ParticleSystem>().Play();
            Invoke("loseInvicibility", 10f);
            Destroy(other.gameObject);
            object[] parms = new object[2] { Color.yellow, 10f };
            StartCoroutine("colorSelf", parms);
        }
    }

    private void loseInvicibility()
    {
        invicible = false;
    }

    private void enableFps()
    {
        gameObject.GetComponent<FirstPersonController>().enabled = true;
    }

    public void loseLife(float minus)
    {
        if (life- minus <= 0) die();

        life -= minus;
        lifeImg.transform.localScale = new Vector3(life / Maxlife, 1, 1);
        player.PlayOneShot(hurt);
    }

    private void gainLife(float add)
    {
        print(life);
        life += add;
        lifeImg.transform.localScale = new Vector3(life / Maxlife, 1, 1);
    }

    private void die()
    {
        text.gameObject.SetActive(true);
        gameObject.GetComponent<FirstPersonController>().enabled = false;
        text.text = "DEAD";
        player.PlayOneShot(echoe);
        deadText.SetActive(true);
    }

    private void reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator colorSelf(object[] parms)
    {
        Renderer r = transform.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Renderer>();

        Color original = r.material.color;
        r.material.color = (Color)parms[0];

        yield return new WaitForSeconds((float)parms[1]);

        r.material.color = original;
    }

}
