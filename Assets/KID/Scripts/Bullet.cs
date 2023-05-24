using UnityEngine;

namespace KID
{
    /// <summary>
    /// 子彈
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        [Header("傷害值"), Range(0, 500)]
        public float damage = 10;

        private void Awake()
        {
            Destroy(gameObject, 5);
        }


        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }
    }
}
