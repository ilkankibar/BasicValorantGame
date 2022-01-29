using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M4A1 : MonoBehaviour
{
    public GameObject bullet;
    public GameObject mag;
    public GameObject player;
    //�arj�rdeki mermi say�s� / maksimum �arj�r kapasitesi / toplam cephane / at�� h�z�
    private int currentBulletCount = 30;
    private int maxBulletCount = 30;
    private int ammunition = 90;
    private float attackRateCount;

    [SerializeField] private float attackRate;
    
    //Sesler
    public AudioSource singleShot;
    public AudioSource finishMagazine;

    [SerializeField] private Text currentBulletText;
    [SerializeField] private Text ammunitionText;

    [SerializeField] private Transform ejectionPos;

    [SerializeField] private GameObject muzzleEffect;
    [SerializeField] private GameObject muzzleEffectPos;

    //Mermi izleri
    [SerializeField] private GameObject metalEffect;
    [SerializeField] private GameObject stoneEffect;
    [SerializeField] private GameObject woodEffect;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        //Sol t�k a bas�l� tuttuk�a ve �arj�r bitmediyse ate� eder.
        if (Input.GetMouseButton(0))
        {
            if (attackRateCount <= 0 && currentBulletCount > 0)
            {
                player.GetComponent<PlayerMovement>().anim.Play("Fire");
                currentBulletCount -= 1;
                singleShot.Play();
                attackRateCount = attackRate;
                Instantiate(bullet, ejectionPos.position, ejectionPos.localRotation);
                GameObject muzzle = Instantiate(muzzleEffect, muzzleEffectPos.transform.position, muzzleEffectPos.transform.rotation);
                muzzle.transform.SetParent(muzzleEffectPos.transform);
                BulletTrack();
            }
        }
    }
    private void Update()
    {
        currentBulletText.text = currentBulletCount.ToString();
        ammunitionText.text = ammunition.ToString();
        //de�i�ken 0 dan b�y�k oldu�unda ger�ek zamanl� onu d���r�r.
        if (attackRateCount > 0)
        {
            attackRateCount -= Time.deltaTime;
        }
        //�arj�rde mermi yoksa ve ate� etmeye �al���yorsa ses oynat�r.
        if (Input.GetMouseButtonDown(0) && currentBulletCount <= 0)
        {
                finishMagazine.Play();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }
    void BulletTrack()
    {
        //Vector3 ray = Camera.main.ViewportToScreenPoint(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position,transform.forward, out hit))
        {
            if (hit.transform.CompareTag("Stone"))
            {
                Instantiate(stoneEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else if (hit.transform.CompareTag("Metal"))
            {
                Instantiate(metalEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else if (hit.transform.CompareTag("Wood"))
            {
                Instantiate(woodEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
    void Reload()
    {
        if (currentBulletCount < maxBulletCount && ammunition > 0)
        {
            if (ammunition < 30)
            {
                currentBulletCount += ammunition;
                ammunition = 0;
            }
            else
            {
                int reloadCount = currentBulletCount - maxBulletCount;
                currentBulletCount -= reloadCount;
                ammunition += reloadCount;
            }
            
            
        }
    }
}
