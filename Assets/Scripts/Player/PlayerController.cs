using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region ComponentsOfPlayerVariables
    [SerializeField] private Rigidbody rig;
    [SerializeField] private GameObject ammo;

    private static PlayerController _Instance;

    public static PlayerController Instance // Design Pattern Singleton
    {
        get
        {
            _Instance = FindObjectOfType<PlayerController>();

            return _Instance;
        }
    }

    public float lifePlayer{get; set;}
    #endregion
    #region MovimentVelocityPlayerVariables
    public float SpeedPlayer{get; set;}
    #endregion
    #region DirectionPlayerVariables
    public float DirX{get; set;}
    public float DirZ{get; set;}

    private Vector3 dirPlayerToMouse;
    
    #endregion
    #region RotationPlayerVariables
    public float rotX{get; set;}
    public float rotZ{get; set;}
    #endregion
    #region RaycastPlayerVariables

    private RaycastHit hit;
    #endregion
    #region ModePlayerVariables
    private string modePlayerStyle;
    #endregion
    #region ActionPlayer

    #endregion  
    private void Start()
    {
        lifePlayer = 10;
        rotX = 3;
        rotZ = 3;
        SpeedPlayer = 10;
        modePlayerStyle = "WalkPlayer";
        
    }
    void Update()
    {
        #region SetDirectionsPlayer
        DirX = Input.GetAxisRaw("Horizontal");
        DirZ = Input.GetAxisRaw("Vertical");
        #endregion
        
        #region ActionPlayerMethods
        
        Shoot(); 
        Jump();
        SetModePlayer();

        #endregion

        Debug.Log($"Life of player: {lifePlayer}");
        
    }

    void FixedUpdate()
    {
        Walk();
        PlayerRotateFollowingMouse();
    }
    
    public void Walk()
    {
        
            rig.velocity = new Vector3(DirX*SpeedPlayer,rig.velocity.y,DirZ*SpeedPlayer);
        
            if(modePlayerStyle == "WalkPlayer")
            {
                if(!Input.anyKey || Input.GetKey("a") || Input.GetKey("d") || Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2) || Input.GetMouseButton(3) || Input.GetMouseButton(4) || Input.GetMouseButton(5) || Input.GetMouseButton(6))
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0,transform.rotation.y,0), Time.deltaTime+0.05f);
                } 
             
             
                transform.rotation *= Quaternion.Euler(new Vector3((DirZ*rotX) ,0,(DirX*rotZ) * -1));
            }
        
    }

    public void SetModePlayer()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            modePlayerStyle = "ShootPlayer";
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            modePlayerStyle = "WalkPlayer";
        }
    }


    public void PlayerRotateFollowingMouse() 
    {
        
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit ,100) && modePlayerStyle == "ShootPlayer")
        {

            dirPlayerToMouse = hit.point - transform.position;
            dirPlayerToMouse.y = 0;
            Quaternion newRotation = Quaternion.LookRotation(dirPlayerToMouse);
            rig.MoveRotation(newRotation);

            
        }

    }

    public void Shoot()
    {
         if(Input.GetMouseButtonDown(0) && modePlayerStyle == "ShootPlayer")
         {
            GameObject bulletP =  Instantiate(ammo,transform.position,Quaternion.identity);
            bulletP.GetComponent<BulletPlayer>().DirBullet = dirPlayerToMouse.normalized; // normalizacao do vetor para que a direcao seja sempre constante de comprimento 1
         }

         return;

    }

    public void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rig.AddForce(0,7,0,ForceMode.Impulse);
        }
    }

    

    
}
