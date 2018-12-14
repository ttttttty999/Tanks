using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxHealth : MonoBehaviour
{

	public float m_StartingHealth = 1000f;
	public Slider m_Slider;
	public Image m_FillImage;                      
	public Color m_FullHealthColor = Color.green;  
	public Color m_ZeroHealthColor = Color.red;
	public GameObject m_ExplosionPrefab;
	
	private bool m_Dead;
	private float m_CurrentHealth;
	private ParticleSystem m_ExplosionParticles;
	private AudioSource m_ExplosionAudio;

	void Awake()
	{
		m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
		m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();
		
		m_ExplosionParticles.gameObject.SetActive(false);
	}

	// Use this for initialization
	void Start ()
	{
		m_CurrentHealth = m_StartingHealth;
		m_Slider.maxValue = m_StartingHealth;
		m_Dead = false;
		SetHealthUI();
	}
	
	// Update is called once per frame
	public void TakeDamage(float DamageAmount)
	{
		m_CurrentHealth -= DamageAmount;
		
		SetHealthUI();

		if (m_CurrentHealth <= 0 && !m_Dead)
		{
			OnDeath();
		}
	}
	
	public void SetHealthUI()
	{
		// Adjust the value and colour of the slider.
		m_Slider.value = m_CurrentHealth;
        
		m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
	}
	
	private void OnDeath()
	{
		// Play the effects for the death of the tank and deactivate it.
		print("dead");
		m_Dead = true;

		m_ExplosionParticles.transform.position = transform.position;
		m_ExplosionParticles.gameObject.SetActive(true);
        
		m_ExplosionParticles.Play();
		m_ExplosionAudio.Play();
        
		gameObject.SetActive(false);
	}
}
