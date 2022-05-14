using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRunAndDodgeStamina : MonoBehaviour
{
    [SerializeField] Slider staminaBar;
    public float stamina;
    public float staminaMax;
    public Transform playerPosition;

    public bool running => (Input.GetKey(KeyCode.Space) && GetComponent<PlayerMovement>().isMoving);
    public float boostSpeed;

    bool isExecuting = false;
    public float recoverStaminaTime;
    public float recoverStaminaTimeCount;

    public Vector3 staminaBarOffset;

    void Start()
    {
        stamina = staminaMax;
        staminaBar.maxValue = staminaMax;
        staminaBar.value = staminaMax;

        recoverStaminaTimeCount = recoverStaminaTime;

        staminaBar.gameObject.SetActive(false);
    }

    void Update()
    {
        //staminaBar.GetComponent<Transform>().position = Camera.main.WorldToScreenPoint(transform.position + staminaBarOffset);

        if(stamina < staminaMax)
            staminaBar.gameObject.SetActive(true);
        else
            staminaBar.gameObject.SetActive(false);
    }

    public void UseRunStamina(int staminaAmount)
    {
        recoverStaminaTimeCount = recoverStaminaTime;
        if (stamina > 0)
        {
            stamina -= staminaAmount * Time.deltaTime;
            staminaBar.value = stamina;
        }
        else
            Debug.Log("No tienes crack");
    }

    public void UseDodgeStamina(int staminaAmount)
    {
        recoverStaminaTimeCount = recoverStaminaTime;
        if (stamina > 0)
        {
            stamina -= staminaAmount;
            staminaBar.value = stamina;
        }
        else Debug.Log("No te queda para el dodgeo, crackity crack");
    }

    public void RefillStamina(int staminaAmount)
    {
        if(recoverStaminaTimeCount >= 0)
        {
            recoverStaminaTimeCount -= Time.deltaTime;
        }
  
        if(recoverStaminaTimeCount <= 0)
        {
            if (stamina < staminaMax)
            {
                stamina += staminaAmount * Time.deltaTime;
                staminaBar.value = stamina;
            }
        }
    }

    public IEnumerator RefillStaminaCoroutine(int staminaAmount)
    {
        if(!isExecuting)
        {
            isExecuting = true;
            yield return new WaitForSeconds(1f);
            if (stamina < staminaMax)
            {
                stamina += staminaAmount * Time.deltaTime * 2;
                staminaBar.value = stamina;
            }
            isExecuting = false;
        }
    }
}
