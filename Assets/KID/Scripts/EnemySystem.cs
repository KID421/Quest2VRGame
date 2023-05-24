using UnityEngine;

namespace KID
{
    /// <summary>
    /// 敵人系統
    /// </summary>
    public class EnemySystem : MonoBehaviour
    {
        [SerializeField, Header("目標物件名稱")]
        private string nameTarget;
        [SerializeField, Header("移動速度"), Range(0, 30)]
        private float speedWalk = 2;
        [SerializeField, Header("停止距離"), Range(0, 10)]
        private float stopDistance = 2;
        [SerializeField, Header("血量"), Range(0, 30)]
        private float hp = 50;
        [SerializeField, Header("子彈物件名稱")]
        private string nameBullet = "子彈";
        [SerializeField, Header("攻擊力"), Range(10, 100)]
        private float attack = 30;
        [SerializeField, Header("攻間隔"), Range(0, 10)]
        private float attackInterval = 3;

        private Transform target;
        private float timerAttack;
        private PlayerSystem player;

        private void Awake()
        {
            target = GameObject.Find(nameTarget).transform;
            player = target.GetComponent<PlayerSystem>();
            timerAttack = attackInterval;
        }

        private void Update()
        {
            Move();
            Attack();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name.Contains(nameBullet)) 
                Damage(collision.gameObject.GetComponent<Bullet>().damage);
        }

        /// <summary>
        /// 移動
        /// </summary>
        private void Move()
        {
            if (Vector3.Distance(transform.position, target.position) > stopDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, speedWalk * Time.deltaTime);
                Vector3 targetPoint = target.position;
                targetPoint.y = transform.position.y;
                transform.LookAt(targetPoint);
            }
        }

        /// <summary>
        /// 攻擊
        /// </summary>
        private void Attack()
        {
            if (Vector3.Distance(transform.position, target.position) <= stopDistance)
            {
                if (timerAttack >= attackInterval)
                {
                    timerAttack = 0;
                    player.Damage(attack);
                }
                else
                {
                    timerAttack += Time.deltaTime;
                }
            }
        }

        /// <summary>
        /// 受傷
        /// </summary>
        private void Damage(float damage)
        {
            AudioClip sound = SoundSystem.instance.soundHurtEnemy;
            SoundSystem.instance.PlaySound(sound, 0.7f, 1.5f);
            hp -= damage;
            if (hp <= 0) Dead();
        }

        /// <summary>
        /// 死亡
        /// </summary>
        private void Dead()
        {
            player.KillEnemy();
            Destroy(gameObject);
        }
    }
}
