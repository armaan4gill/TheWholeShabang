using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Johnson, Owen, Villeneuve Sophia
 * 05/10/2024
 *Controls damage
 */
public class DamageController : MonoBehaviour
{
    public GameObject invulnShield;
    public bool invincible;
    private int health = 100;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Activates sheild    
    /// </summary>
    public void ShieldOn()
    {
        invulnShield.gameObject.SetActive(true);
        Invoke(nameof(ShieldOff), 0.5f);
    }
    /// <summary>
    /// deactivates sheild
    /// </summary>
    public void ShieldOff()
    {
        invulnShield.gameObject.SetActive(false);
        if (invincible) Invoke(nameof(ShieldOn), 0.5f);
    }
    /// <summary>
    /// Makes  player take damage
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        //if the player has invincibilty frames then they can't take damage
        if (!invincible)
        {
            health -= damage;

            //When the player takes damage they get invincibility frames
            StartCoroutine(InvincibilityFrames());
        }
    }
    IEnumerator InvincibilityFrames()
    {
        invincible = true;
        ShieldOn();
        yield return new WaitForSeconds(5f);
        invincible = false;
    }
}
