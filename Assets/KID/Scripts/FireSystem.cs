using UnityEngine;

namespace KID
{
    /// <summary>
    /// 開槍系統
    /// </summary>
    public class FireSystem : MonoBehaviour
    {
        [SerializeField, Header("子彈發射位置")]
        private Transform pointFire;
        [SerializeField, Header("子彈發射速度"), Range(0, 5000)]
        private float speedFire = 1500;
        [SerializeField, Header("子彈預製物")]
        private GameObject prefabBullet;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) Fire();
        }

        /// <summary>
        /// 開槍
        /// </summary>
        public void Fire()
        {
            AudioClip sound = SoundSystem.instance.soundFire;
            SoundSystem.instance.PlaySound(sound, 0.7f, 1.5f);
            GameObject tempBullet = Instantiate(prefabBullet, pointFire.position, Quaternion.identity);
            tempBullet.GetComponent<Rigidbody>().AddForce(pointFire.forward * speedFire);
        }
    }
}
