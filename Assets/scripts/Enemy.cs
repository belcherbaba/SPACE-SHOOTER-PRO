using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
     [SerializeField]
    private float _speed = 8.0f;
    // Start is called before the first frame update

    private Player1 _player;
    private Animator _anim;


    
    void Start()
    {
        transform.position = new Vector3(0,7f,0);
        _player = GameObject.Find("Player1").GetComponent<Player1>();

        if (_player == null)
        {
            Debug.LogError("The Player is Null");
        }

        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Animator is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y<-5f)
        {
            float randomx = Random.Range(-8f,8f);
            transform.position = new Vector3(randomx,7f,0);

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " +other.transform.name);
        if(other.tag == "Player1")
        {
            Player1 player = other.transform.GetComponent<Player1>();

            if(player != null)
            {
                player.Damage();

            }

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.5f);

        }
       

        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(+10);
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.5f);

        }


    }
}
