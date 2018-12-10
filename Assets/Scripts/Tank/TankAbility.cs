using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAbility : MonoBehaviour
{
    public int m_PlayerNumber;
    public TankShooting tankShooting;
    public TankHealth tankHealth;
    public TankMovement tankMovement;


    private float m_CdTime;
    private float m_AbilityTime;
    private string m_AbilityInputName;
    private bool m_inAbility;
    private bool m_AbilityInputValue;

    private void OnEnable()
    {
        m_AbilityInputName = "Ability" + m_PlayerNumber;
        m_AbilityInputValue = false;
        m_inAbility = false;
        m_CdTime = 0f;
        m_AbilityTime = 5f;
    }

    private void Update()
    {
        m_AbilityInputValue = Input.GetButton(m_AbilityInputName);
    }

    private void FixedUpdate()
    {
        Ability1();
    }

    private void Ability1()
    {
        //技能1
        print(m_CdTime);
        m_CdTime += Time.deltaTime;
        if (m_PlayerNumber == 1)
        {
            if (m_AbilityInputValue && m_CdTime >= 10f && !m_inAbility)
            {
                tankMovement.m_Speed = 1.5f * tankMovement.m_Speed;
                m_inAbility = true;
            }

            if (m_AbilityTime >= 0 && m_inAbility)
            {
                m_AbilityTime -= Time.deltaTime;
            }

            else if (m_AbilityTime < 0 && m_inAbility)
            {
                m_inAbility = false;
                tankMovement.m_Speed = tankMovement.m_OriginalSpeed;
                m_AbilityTime = 5f;
                m_CdTime = 0f;
            }
        }

        //
    }
}