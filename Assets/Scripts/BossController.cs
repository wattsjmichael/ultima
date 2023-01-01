using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController instance;
    public string bossName;
    public int bossHealth;
    public int bossCurrHealth;
    public int stage2Threshold;
    public int stage3Threshold;

    public GameObject theBoss,
        door;
    public Transform[] spawnPoints;
    private Vector3 moveTarget;
    public float bossMoveSpeed;

    public float timeActive,
        timeBetweenSpawns,
        firstSpawnDelay;
    private float activeCounter,
        spawnCounter;

    public GameObject deathEffect;

    public BossShotController bossShot;
    public Transform[] shotPoints;
    public Transform shotCenter;
    public float timeBetweenShots,
        shotRotateSpeed,
        shootTime;
    private float shotCounter,
        shootingCounter;

        public AudioSource levelBGM, bossBGM;

        public GameObject victoryObject;



    // Start is called before the first frame update
    void Start()
    {
        door.SetActive(true);

        spawnCounter = firstSpawnDelay;
        UIManager.instance.bossSlider.maxValue = bossHealth;
        UIManager.instance.bossSlider.value = bossHealth;
        UIManager.instance.bossSlider.gameObject.SetActive(true);

        UIManager.instance.bossText.text = bossName;
        Debug.Log(bossName);
        UIManager.instance.bossText.gameObject.SetActive(true);

        levelBGM.Stop();
        bossBGM.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossHealth > 0)
        {
            if (spawnCounter > 0)
            {
                spawnCounter -= Time.deltaTime;

                if (spawnCounter < 0)
                {
                    activeCounter = timeActive;
                    shootingCounter = shootTime;
                    theBoss.transform.position = spawnPoints[
                        Random.Range(0, spawnPoints.Length)
                    ].position;

                    moveTarget = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

                    while (moveTarget == theBoss.transform.position)
                    {
                        moveTarget = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                    }

                    theBoss.SetActive(true);
                }
            }
            else
            {
                activeCounter -= Time.deltaTime;
                if (activeCounter <= 0)
                {
                    spawnCounter = timeBetweenSpawns;
                    theBoss.SetActive(false);
                }

                theBoss.transform.position = Vector3.MoveTowards(
                    theBoss.transform.position,
                    moveTarget,
                    bossMoveSpeed * Time.deltaTime
                );

                if (shootingCounter > 0)
                {
                    shootingCounter -= Time.deltaTime;

                    shotCounter -= Time.deltaTime;
                    if (shotCounter <= 0)
                    {
                        shotCounter = timeBetweenShots;

                        if (bossHealth > stage2Threshold)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                Instantiate(
                                        bossShot,
                                        shotPoints[i].position,
                                        shotPoints[i].rotation
                                    )
                                    .SetDirection(shotCenter.position);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < shotPoints.Length; i++)
                            {
                                Instantiate(
                                        bossShot,
                                        shotPoints[i].position,
                                        shotPoints[i].rotation
                                    )
                                    .SetDirection(shotCenter.position);
                            }
                        }
                    }

                    if (bossHealth <= stage3Threshold)
                    {
                        shotCenter.transform.rotation = Quaternion.Euler(
                            shotCenter.transform.rotation.eulerAngles.x,
                            shotCenter.transform.rotation.eulerAngles.y,
                            shotCenter.transform.rotation.eulerAngles.z
                                + (shotRotateSpeed * Time.deltaTime)
                        );
                    }
                }
            }
        }
    }

    public void TakeDamage(int damageToTake)
    {
        bossHealth -= damageToTake;
        if (bossHealth <= 0)
        {
            bossHealth = 0;
            theBoss.SetActive(false);
            //door.SetActive(false);
            Instantiate(deathEffect, theBoss.transform.position, transform.rotation);

            UIManager.instance.bossSlider.gameObject.SetActive(false);
            UIManager.instance.bossText.gameObject.SetActive(false);

            levelBGM.Play();
            bossBGM.Stop();

            victoryObject.SetActive(true);
        }
        UIManager.instance.bossSlider.value = bossHealth;
    }
}
