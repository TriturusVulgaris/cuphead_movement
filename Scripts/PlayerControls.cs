using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))] // Ce script laché sur un objet créera automatiquement un spriterenderer et un animator dans l'objet.
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Transform))]
public class PlayerControls : MonoBehaviour
{
    #region Public Members

    public Transform m_transform;
    [Range(1, 10)]
    public float m_speed;
    public Vector2 m_stickTolerance;
    public bool m_playerHaveControls;

    #endregion

    #region Public void

    public void Coins()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        if(coins.Length == 0)
        {
            m_playerHaveControls = false;
            m_animator.SetBool("Jump", true);
        }
    }

    #endregion

    #region System

    void Awake()
    {

        m_transform = GetComponent<Transform>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
	}
	
	void Update()
    {
        if (m_playerHaveControls)
        {
            GetInput();
            MoveSprite();
            UpdateBuffer();
            UpdateAnimator();
            FlipSprite();
            Coins();
        }
    }

    #endregion

    #region Class Methods

    void GetInput()
    {
        m_movement.y = Input.GetAxisRaw("Vertical");
        m_movement.x = Input.GetAxisRaw("Horizontal");
    }

    void UpdateBuffer()
    {
        if (m_movement.y < m_stickTolerance.y && m_movement.y > -m_stickTolerance.y && m_movement.x < m_stickTolerance.x && m_movement.x > -m_stickTolerance.x)
        {
            m_movement.x = m_bufmovement.x * 0.1f;
            m_movement.y = m_bufmovement.y * 0.1f;
        }
        else
        {
            m_bufmovement.x = m_movement.x;
            m_bufmovement.y = m_movement.y;
        }
    }

    void UpdateAnimator()
    {
        m_animator.SetFloat("SpeedVertical", m_movement.y);
        m_animator.SetFloat("SpeedHorizontal", m_movement.x);
    }
    void FlipSprite()
    {
        if (m_movement.x < 0f)
        {
            m_spriteRenderer.flipX = true;
        }
        else
        {
            m_spriteRenderer.flipX = false;
        }
    }

    void MoveSprite()
    {
        m_temporaryPosition = m_transform.position;
        m_temporaryPosition.x += m_movement.x * m_speed * Time.deltaTime;
        m_temporaryPosition.y += m_movement.y * m_speed * Time.deltaTime;
        m_transform.position = m_temporaryPosition;
    }

    #endregion

    #region Tools Debug And Utility

    #endregion

    #region Private an Protected Members

    private SpriteRenderer m_spriteRenderer;
    private Animator m_animator;
    private Vector2 m_movement;
    private Vector2 m_bufmovement;
    private Vector3 m_temporaryPosition;

    #endregion
}