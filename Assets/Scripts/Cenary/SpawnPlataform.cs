using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlataform : MonoBehaviour
{
    [SerializeField] private GameObject plataformObject;
    [SerializeField] private GameObject plataformInicial;
    private GameObject plataformReference;
    private float contadorSpawn;

    void Start()
    {
        plataformReference = plataformInicial;
        contadorSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        contadorSpawn+=Time.deltaTime;
        SpawnPlat();
    }

    public void SpawnPlat()
    {
        if(contadorSpawn >=4)
        {
            GameObject plataformCurrent = Instantiate(plataformObject,plataformReference.transform.position + new Vector3(Random.Range(-15,15),0,20),Quaternion.identity);
            plataformReference = plataformCurrent;
            contadorSpawn = 0;
        }
    }
}
