using UnityEngine;
using System.Collections;

public class NejikoController : MonoBehaviour
{
    const int MinLane = -2;
    const int MaxLane = 2;
    const float LaneWidth = 1.0f;
    const int DefaultLife = 3;
    const float StunDuration = 0.5f;
    const int DefaultMoney = 0;   //最初のお金の所持金 

    CharacterController controller;
    Animator animator;
    AnimatorStateInfo currentState;     // 現在のステート状態を保存する参照
    AnimatorStateInfo previousState;	// ひとつ前のステート状態を保存する参照

    Vector3 moveDirection = Vector3.zero;
    int targetLane;
    int life = DefaultLife;
    int money = DefaultMoney; //最初のお金の所持金
    float recoverTime = 0.0f;

    public float gravity;
    public float speedZ;
    public float speedX;
    public float speedJump;
    public float accelerationZ;

    private AnimatorStateInfo currentBaseState;			// base layerで使われる、アニメーターの現在の状態の参照

    // アニメーター各ステートへの参照
    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int DamageState = Animator.StringToHash("Base Layer.DamageDown");
    static int SAMKState = Animator.StringToHash("Base Layer.SAMK");
    static int RunState = Animator.StringToHash("Base Layer.Run");
    static int KickState = Animator.StringToHash("Base Layer.ScrewKick");

    public int Life()
    {
        return life;
    }

    public int Money()
    {
        return money;
    }

    public bool IsStan()
    {
        return recoverTime > 0.0f || life <= 0;
    }

    public float Position() {
        return transform.position.z;
    }


    void Start()
    {
        // 必要なコンポーネントを自動取得
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        currentState = animator.GetCurrentAnimatorStateInfo(0);
        previousState = currentState;
    }

    void Update()
    {
        // デバッグ用
        if (Input.GetKeyDown("left")) MoveToLeft();
        if (Input.GetKeyDown("right")) MoveToRight();
        //if (Input.GetKeyDown("space")) Jump();
        if (Input.GetKeyDown("space")) Jump();
        if (Input.GetKeyDown(KeyCode.A)) Kick();
        if (Input.GetKeyDown(KeyCode.S)) animator.SetTrigger("Slide");

        if (IsStan())
        {
            // 動きを止め気絶状態からの復帰カウントを進める
            moveDirection.x = 0.0f;
            moveDirection.z = 0.0f;
            recoverTime -= Time.deltaTime;
        }
        else
        {
            // 徐々に加速しZ方向に常に前進させる
            float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
            moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

            // X方向は目標のポジションまでの差分の割合で速度を計算
            float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
            moveDirection.x = ratioX * speedX;
        }

        // 重力分の力を毎フレーム追加
        moveDirection.y -= gravity * Time.deltaTime;

        // 移動実行
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        controller.Move(globalDirection * Time.deltaTime);

        // 移動後接地してたらY方向の速度はリセットする
        if (controller.isGrounded) moveDirection.y = 0;

        // 速度が0以上なら走っているフラグをtrueにする
        animator.SetBool("Run", moveDirection.z > 0.0f);
    }

    // 左のレーンに移動を開始
    public void MoveToLeft()
    {
        if (IsStan()) return;
        if (controller.isGrounded && targetLane > MinLane) targetLane--;
    }

    // 右のレーンに移動を開始
    public void MoveToRight()
    {
        if (IsStan()) return;
        if (controller.isGrounded && targetLane < MaxLane) targetLane++;
    }

    public void Jump()
    {
        if (IsStan()) return;
        if (controller.isGrounded)
        {
            moveDirection.y = speedJump;

            // ジャンプトリガーを設定
            //animator.SetTrigger("Jump");
            animator.SetBool("SAMK", true);
        }
    }

    public void Kick()
    {
        if (IsStan()) return;
        animator.SetBool("ScrewKick", true);
    }

    // CharacterControllerにコンジョンが生じたときの処理
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (IsStan()) return;

        if (hit.gameObject.tag == "Heart")
        {
            if (life < 3)
                life++;
            Destroy(hit.gameObject);
        }

        if (hit.gameObject.tag == "Money")
        {
            Destroy(hit.gameObject);
            money += 300;
        }

        if (currentBaseState.nameHash != KickState)
        {
            if (hit.gameObject.tag == "Mofumoroid")
            {
                recoverTime = StunDuration;
                animator.SetTrigger("DamageDown");
                Destroy(hit.gameObject);
            }

            if (hit.gameObject.tag == "Robo")
            {
                // ライフを減らして気絶状態に移行
                life--;
                recoverTime = StunDuration;
                // ダメージトリガーを設定
                animator.SetTrigger("DamageDown");
                // ヒットしたオブジェクトは削除
                Destroy(hit.gameObject);
            }
        }
        else if (currentBaseState.nameHash == KickState)
        {
            if (hit.gameObject.tag == "Robo" || hit.gameObject.tag == "Mofumoroid")
                Destroy(hit.gameObject);
            return;
        }
    }
}