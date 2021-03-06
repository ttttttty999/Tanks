﻿using System;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class TankMovement : MonoBehaviour
{
    //public variable
    public int m_PlayerNumber = 1;
    public float m_Speed = 12f;
    public float m_OriginalSpeed = 12f;
    public float m_TurnSpeed = 180f;
    public AudioSource m_MovementAudio;
    public AudioClip m_EngineIdling;
    public AudioClip m_EngineDriving;

    public float m_PitchRange = 0.2f;

    //
    //private variable
    private string m_MovementAxisName;
    private string m_TurnAxisName;
    private string m_JumpButtonName;

    private Rigidbody m_Rigidbody;

    private float m_MovementInputValue;
    private float m_TurnInputValue;
    private bool m_JumpInputValue;

    private float m_OriginalPitch;
    //


    private void Awake() //最先执行
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable() //after Awake()
    {
        m_Rigidbody.isKinematic = false; //isKinematic is true，no forces can affect the rigidbody
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;
        m_JumpButtonName = "Jump" + m_PlayerNumber;

        m_OriginalPitch = m_MovementAudio.pitch;
    }


    private void Update() //Running every rendered frame
    {
        // Store the player's input and make sure the audio for the engine is playing.
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);
        m_JumpInputValue = Input.GetButton(m_JumpButtonName);

        EngineAudio();
    }


    private void EngineAudio()
    {
        // Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.
        if (Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f)
        {
            if (m_MovementAudio.clip == m_EngineDriving)
            {
                m_MovementAudio.clip = m_EngineIdling;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
        else
        {
            if (m_MovementAudio.clip == m_EngineIdling)
            {
                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
    }


    private void FixedUpdate() //Running every physics step
    {
        // Move and turn the tank.
        Move();
        Turn();
    }


    private void Move()
    {
        // Adjust the position of the tank based on the player's input.

        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
//        print(m_Speed);

        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);

        if (m_JumpInputValue && m_Rigidbody.velocity.y == 0f) //跳跃
        {
            m_Rigidbody.AddForce(0f, 400f, 0f);
        }
    }


    private void Turn()
    {
        // Adjust the rotation of the tank based on the player's input.
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
}