using UnityEngine;
using UnityEngine.SceneManagement;

namespace KID
{
    /// <summary>
    /// 玩家系統
    /// </summary>
    public class PlayerSystem : MonoBehaviour
    {
        [SerializeField, Header("血量"), Range(0, 5000)]
        private float hp = 1000;
        [SerializeField, Header("擊殺敵人獲勝數量"), Range(0, 100)]
        private int countKillEnemyToWin = 10;

        private int countKillEnemy;

        /// <summary>
        /// 受傷
        /// </summary>
        /// <param name="damage">傷害值</param>
        public void Damage(float damage)
        {
            AudioClip sound = SoundSystem.instance.soundHurtPlay;
            SoundSystem.instance.PlaySound(sound, 0.7f, 1.5f);
            hp -= damage;
            if (hp <= 0) Dead();
        }

        /// <summary>
        /// 擊殺敵人
        /// </summary>
        public void KillEnemy()
        {
            countKillEnemy++;
            if (countKillEnemy == countKillEnemyToWin)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// 死亡
        /// </summary>
        private void Dead()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
