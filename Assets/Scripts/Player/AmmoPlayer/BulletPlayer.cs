  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody rig;
    public Vector3 DirBullet {get; set;}

    // Update is called once per frame
    void Update()
    {
        rig.velocity = new Vector3(DirBullet.x * 25, 0, DirBullet.z * 25);
    }
}
