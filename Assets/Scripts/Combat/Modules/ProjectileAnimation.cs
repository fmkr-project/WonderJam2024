using System.Collections;
using UnityEngine;

namespace Upgrades.Combat.Modules
{
    public class ProjectileAnimation
    {
        public static IEnumerator Animate(Sprite p, Vector3 source, Vector3 target, float duration)
        {
            GameObject projectile = new GameObject("Projectile");
            projectile.SetActive(false);
            SpriteRenderer sr = projectile.AddComponent<SpriteRenderer>();
            sr.sprite = p;
            projectile.transform.localScale = 5 * Vector3.one;
            projectile.transform.rotation =
                Quaternion.LookRotation(target - source) *
                Quaternion.Euler(0, 0, 90);
            projectile.SetActive(true);
            
            var elapsed = 0f;
            while (elapsed < duration)
            {
                var dt = Time.deltaTime;
                elapsed += dt;
                yield return new WaitForSeconds(dt);
                projectile.transform.position = Vector3.Lerp(source, target, elapsed / duration);
            }

            Object.Destroy(projectile);
        }
    }
}