using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Explosion;
    public ParticleSystem particle;
    GameObject bullets;
    void Start()
    {
        //particle = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
       /* if (particle.isStopped) //パーティクルが終了したか判別
        {
            Destroy(bullets.gameObject, 4);//パーティクル用ゲームオブジェクトを削除
        }*/

    }
    private void OnTriggerEnter(Collider other)// オブジェクトに触れた時の処理
    {
        if (other.gameObject.tag == "bullet")// もし、触れたオブジェクトのタグがBulletだったら、
        {
            //Instantiate(Explosion.gameObject, this.transform.position, Quaternion.identity);
            bullets = Instantiate(Explosion.gameObject);// bulletを作成し、作成したものはbulletsとする
            bullets.transform.position = this.transform.position;// bulletsをプレイヤーの場所に移動させる
        }
    }
}
