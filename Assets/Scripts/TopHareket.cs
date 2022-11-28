using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopHareket : MonoBehaviour
{
    private Rigidbody rg;
    public UnityEngine.UI.Text sure, durum, soru, cevap,dogruluk;
    public UnityEngine.UI.Button buttonKolay, buttonZor;
    public UnityEngine.UI.Image panel;
    float sureSayaci = 60;
    public bool oyunKontrol = true;
    bool oyunKazan = false;
    bool soruKontrol = false;
    int sonuc;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
         
    }

    // Update is called once per frame
    void Update()
    {
        
        if (oyunKazan)
        {
            oyunKontrol = false;
            buttonKolay.gameObject.SetActive(true);
            buttonZor.gameObject.SetActive(true);
            soruKontrol = false;
            panel.gameObject.SetActive(false);
        }
        else if (sureSayaci < 0)
        {
            oyunKontrol = false;
            durum.text = "Kaybettin!";
            buttonKolay.gameObject.SetActive(true);
            buttonZor.gameObject.SetActive(true);
            soruKontrol = false;
            panel.gameObject.SetActive(false);

        }
        else if (oyunKontrol)
        {
            sureSayaci -= Time.deltaTime;
            sure.text = "Sure: " + (int)sureSayaci + " saniye";

        }



    }
    void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.Space) || !oyunKontrol || soruKontrol)
        {
            rg.velocity = Vector3.zero;
            rg.angularVelocity = Vector3.zero;

        }
        else
        {
            float yatay = Input.GetAxis("Horizontal");
            float dikey = Input.GetAxis("Vertical");
            Vector3 kuvvet = new Vector3(-yatay, 0, -dikey);
            rg.AddForce(15 * kuvvet);



        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        string objeIsmi = collision.gameObject.name;
        if (objeIsmi.Equals("bitisZemin"))
        {
            oyunKazan = true;
            durum.text = "Kazandın! \nPuanın: " + (int)sureSayaci;




        }
        else if (!objeIsmi.Equals("disZemin") && !objeIsmi.Equals("icZemin"))
        {
            GetComponent<AudioSource>().Play();
            RandomProblem();


        }
    }

    public void RandomProblem()
    {
        soruKontrol = true;
        panel.gameObject.SetActive(true);
        int temp;
        int r1 = Random.Range(1, 10);
        int r2 = Random.Range(1, 10);

        if (r1 < r2)
        {
            temp = r1;
            r1 = r2;
            r2 = temp;
        }
        temp = Random.Range(1, 3);
        if (temp == 1)
        {
            soru.text = r1 + "+" + r2 + "=?";
            sonuc = r1 + r2;
        }
        else
        {
            soru.text = r1 + "-" + r2 + "=?";
            sonuc = r1 - r2;
        }


    }
    public void CevapKontrol()
    {
        if (int.Parse(cevap.text) == sonuc)
        {
            panel.gameObject.SetActive(false);
            soruKontrol = false;
            dogruluk.text = "";
        }
        else
        {
            dogruluk.text = "Yanlış!";
        }
    }


}
