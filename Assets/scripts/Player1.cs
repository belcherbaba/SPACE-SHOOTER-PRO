using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _speedMultiplier = 2f;
    [SerializeField]
    private GameObject _LaserPrefabs;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _sheildVisualiser;
     [SerializeField]
    private float _firerate = 0.5f;
    
     
    private float _canFire = -1f;
     [SerializeField]
    private int _Lives = 3;
    private SpawnManager _spawnManager;
    
     [SerializeField]
     private Boolean _isTripleShotActive = false;
     [SerializeField]
     private Boolean _isSpeedBoostActive = false;
     [SerializeField]
     private Boolean _isShieldActive = false;
     [SerializeField]
     private int _score;

     private UIManager _uiManager;
     

    void Start()
    {
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn manager is NULL.");
            
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is no");
        }


    }

    // Update is called once per frame
    void Update()
    {
       
       CalculateMovement();
       FireLaser();
       

      

    }
    void CalculateMovement()
    {
         float horizontalInput = Input.GetAxis("Horizontal");

        float verticalInput = Input.GetAxis("Vertical");

       

        if (_isSpeedBoostActive == false)
        {
             transform.Translate(Vector3.right* horizontalInput * _speed * Time.deltaTime);

             transform.Translate(Vector3.up* verticalInput *_speed*Time.deltaTime);

        }
        else
        {
             transform.Translate(Vector3.right* horizontalInput * _speed * Time.deltaTime*_speedMultiplier);

             transform.Translate(Vector3.up* verticalInput *_speed*Time.deltaTime*_speedMultiplier);



        }

        if (transform.position.y>=0)
        {
            transform.position = new Vector3(transform.position.x,0,0);
            
        }
        else if(transform.position.y<= -3.8f)
        {
            transform.position = new Vector3(transform.position.x,-3.8f,0);
            
        }
        if(transform.position.x<= -11.3f)
        {
            transform.position = new Vector3(11.3f,transform.position.y,0);

        }
        else if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f,transform.position.y,0);
            
        }

    }
    void FireLaser()
    {


        if (_isTripleShotActive == true)
        {
              if(Input.GetKeyDown(KeyCode.Space) && Time.time>_canFire)
              {
             _canFire = Time.time + _firerate;
        Instantiate(_tripleShotPrefab,transform.position + new Vector3(-2f,0.8f,0),Quaternion.identity);
              }


        }
        else
        {
             if(Input.GetKeyDown(KeyCode.Space) && Time.time>_canFire)
       {
        Debug.Log("Space Key Pressed");
        _canFire = Time.time + _firerate;
        Instantiate(_LaserPrefabs,transform.position + new Vector3(0,0.8f,0),Quaternion.identity);


       }

        }
        

    }
    public void Damage()
    {

        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _sheildVisualiser.SetActive(false);
            return;
            
        }
        _Lives -= 1;

        _uiManager.UpdateLives(_Lives);

        if(_Lives <1)
        {
            
            Destroy(this.gameObject);
            

        }

    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;

        StartCoroutine(TripleShotPowerDownRoutine());



    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;

    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostCoolDown());

    }

    IEnumerator SpeedBoostCoolDown()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;

    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _sheildVisualiser.SetActive(true);

    }
    //method to add 10 to the score
    //ccommunicate with the ui
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);


    }
   
}
