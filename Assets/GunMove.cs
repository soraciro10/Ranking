using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMove : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController characterController;// CharacterControllerを使うための変数
    private Vector3 Velocity;// CharacterControllerに重力をかけるための変数

    [SerializeField]
    private float JumpPower = 8;// ジャンプ力

    [SerializeField]
    private float MoveSpeed = 10;// 移動スピード

    public float sensitivityX = 15F;// マウスの横の動きの強さ
    public float sensitivityY = 15F;// マウスの縦の動きの強さ

    public float minimumX = -360F;// 横の回転の最低値
    public float maximumX = 360F;// 横の回転の最大値

    public float minimumY = -60F;// 縦の回転の最低値
    public float maximumY = 60F;// 縦の回転の最大値

    float rotationX = 0f;// 横軸の回転量
    float rotationY = 0f;// 縦軸の回転量

    public GameObject CameraRot;
    public GameObject VerRot;// 縦回転させるオブジェクト（カメラ）
    public GameObject PlayerRot;
    public GameObject HorRot;// 横回転させるオブジェクト（プレイヤー）

    public GameObject bullet;// 発射する弾

    [SerializeField]
    private float bulletSpeed = 5000;// 弾のスピード

    private Vector3 force;// 弾を飛ばす力の変数

    public ParticleSystem gun;
    public GameObject Explosion;
    [SerializeField] GameObject GunTrans;


    void Start()
    {
        characterController = GetComponent<CharacterController>();// CharacterControllerの値を取得してcharacterControllerに代入
        //Cursor.visible = false;
    }

    void Update()
    {

        float X_Rotation = Input.GetAxis("Mouse X");//①X_RotationにマウスのX軸の動きを代入する
        float Y_Rotation = Input.GetAxis("Mouse Y");//①Y_RotationにマウスのY軸の動きを代入する
        /*HorRot.transform.Rotate(new Vector3(-Y_Rotation*5, X_Rotation * 5, 0));//①プレイヤーのY軸の回転をX_Rotationに合わせる
        CameraRot.transform.Rotate(new Vector3(0, X_Rotation * 2, 0));
        VerRot.transform.Rotate(-Y_Rotation * 2, 0, 0);//①カメラのX軸の回転をY_Rotationに合わせる
        //PlayerRot.transform.Rotate(-Y_Rotation * 2, 0, 0);*/

        rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;//rotationXを現在のyの向きにXの移動量*sensitivityXの分だけ回転させる

        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;//rotationYにYの移動量*sensitivityYの分だけ増やす
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);//rotationYを-60〜60の値にする

        //CameraRot.transform.localEulerAngles = new Vector3(0, 0, 0);//オブジェクトの向きをnew Vector3(-rotationY, rotationX, 0)にする
        PlayerRot.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);//オブジェクトの向きをnew Vector3(-rotationY, rotationX, 0)にする

        if (Input.GetKey(KeyCode.W))// もし、Wキーがおされたら、
        {
            characterController.Move(this.gameObject.transform.forward * MoveSpeed * Time.deltaTime);// 前方に動かす
        }

        if (Input.GetKey(KeyCode.S))// もし、Sキーがおされたら、
        {
            characterController.Move(this.gameObject.transform.forward * -1f * MoveSpeed * Time.deltaTime);// 後方に動かす
        }

        if (Input.GetKey(KeyCode.A))// もし、Aキーがおされたら、
        {
            characterController.Move(this.gameObject.transform.right * -1 * MoveSpeed * Time.deltaTime);// 左に動かす
        }

        if (Input.GetKey(KeyCode.D))// もし、Dキーがおされたら

        {
            characterController.Move(this.gameObject.transform.right * MoveSpeed * Time.deltaTime);// 右に動かす
        }

        if (Input.GetMouseButtonDown(0))// もし、左クリックされたら、
        {
            GameObject bullets = Instantiate(bullet) as GameObject;// bulletを作成し、作成したものはbulletsとする
            bullets.transform.position = this.transform.position+new Vector3(0,0.5f,0);// bulletsをプレイヤーの場所に移動させる
            force = this.gameObject.transform.forward * bulletSpeed;// forceに前方への力を代入する
            bullets.GetComponent<Rigidbody>().AddForce(force);// bulletsにforceの分だけ力をかける
            Destroy(bullets.gameObject, 4);// 作成されてから4秒後に消す
        }

        if (gun.isStopped) //パーティクルが終了したか判別
        {
            Destroy(gun.gameObject);//パーティクル用ゲームオブジェクトを削除
        }

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(Explosion.gameObject, GunTrans.transform.position, Quaternion.identity);
        }

        characterController.Move(Velocity * Time.deltaTime); //characterControllerを常にVelocity分動かす

        Velocity.y += Physics.gravity.y * Time.deltaTime;// Velocity.yには重力分足し続ける
        if (characterController.isGrounded)// もし地面に着いていたら、
        {
            if (Input.GetKey(KeyCode.Space))// もし、スペースキーがおされたら
            {
                Velocity.y = JumpPower;// ジャンプする
            }
        }
    }
}

