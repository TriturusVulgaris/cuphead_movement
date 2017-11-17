using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))] // Ce script laché sur un objet créera automatiquement un spriterenderer et un animator dans l'objet.
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControls : DualBehaviour
{
    #region Public Members

    [Range(1, 10)]
    public float m_speed;
    public Vector2 m_stickTolerance;

    #endregion

    #region Public void

    #endregion

    #region System

    void Awake()
    {
        m_body = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
	}
	
	void Update()
    {
        GetInput();
        UpdateBuffer();
        UpdateAnimator();
        FlipSprite();
    }

    void FixedUpdate()
    {
        MoveSprite();
    }

    #endregion

    #region Class Methods

    void GetInput()
    {
        m_movement.y = Input.GetAxisRaw("Vertical");
        m_movement.x = Input.GetAxisRaw("Horizontal");
        m_absmovement.y = m_movement.y;
        m_absmovement.x = m_movement.x;
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
        m_body.velocity = m_speed * m_absmovement;
        //var move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //transform.position += move * speed * Time.deltaTime;
    }

    #endregion

    #region Tools Debug And Utility

    #endregion

    #region Private an Protected Members

    private SpriteRenderer m_spriteRenderer;
    private Animator m_animator;
    private Rigidbody2D m_body;
    private Vector2 m_movement;
    private Vector2 m_absmovement;
    private Vector2 m_bufmovement;

    #endregion
}