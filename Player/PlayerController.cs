using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
	public bool FacingLeft { get { return facingLeft; } }

	[SerializeField] private float moveSpeed = 1f;
	[SerializeField] private float dashSpeed = 4f;
	[SerializeField] private TrailRenderer myTrailRenderer;
	[SerializeField] private Transform weaponCollider;

	private PlayerControls playerControls;
	private Vector2 movement;
	private Rigidbody2D rb;
	private Animator myAnimator;
	private SpriteRenderer mySpriteRender;
	private Knockback knockback;
	private float startingMoveSpeed;

	private bool facingLeft = false;
	private bool isDashing = false;
	private bool isInteractingWithUI = false;

	protected override void Awake()
	{
		base.Awake();

		playerControls = new PlayerControls();
		rb = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		mySpriteRender = GetComponent<SpriteRenderer>();
		knockback = GetComponent<Knockback>();

		playerControls.UI.Click.started += OnClickStarted;
		playerControls.UI.Click.canceled += OnClickCanceled;
	}

	private void Start()
	{
		playerControls.Combat.Dash.performed += _ => Dash();

		startingMoveSpeed = moveSpeed;

		ActiveInventory.Instance.EquipStartingWeapon();
	}

	private void OnEnable()
	{
		playerControls.Enable();
	}

	private void OnDisable()
	{
		playerControls.Disable();
	}

	private void Update()
	{
		PlayerInput();
	}

	private void FixedUpdate()
	{
		AdjustPlayerFacingDirection();
		Move();
	}

	public Transform GetWeaponCollider()
	{
		return weaponCollider;
	}

	private void PlayerInput()
	{
		if (isInteractingWithUI) return;

		movement = playerControls.Movement.Move.ReadValue<Vector2>();

		myAnimator.SetFloat("moveX", movement.x);
		myAnimator.SetFloat("moveY", movement.y);
	}

	private void Move()
	{
		if (knockback.GettingKnockedBack || PlayerHealth.Instance.isDead) { return; }

		rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
	}

	private void AdjustPlayerFacingDirection()
	{
		Vector3 mousePos = Mouse.current.position.ReadValue();
		Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

		if (mousePos.x < playerScreenPoint.x)
		{
			mySpriteRender.flipX = true;
			facingLeft = true;
		}
		else
		{
			mySpriteRender.flipX = false;
			facingLeft = false;
		}
	}

	private void Dash()
	{
		if (!isDashing && Stamina.Instance.CurrentStamina > 0)
		{
			Stamina.Instance.UseStamina();
			isDashing = true;
			moveSpeed *= dashSpeed;
			myTrailRenderer.emitting = true;
			StartCoroutine(EndDashRoutine());
		}
	}

	private IEnumerator EndDashRoutine()
	{
		float dashTime = .2f;
		float dashCD = .25f;
		yield return new WaitForSeconds(dashTime);
		moveSpeed = startingMoveSpeed;
		myTrailRenderer.emitting = false;
		yield return new WaitForSeconds(dashCD);
		isDashing = false;
	}

	private void OnClickStarted(InputAction.CallbackContext context)
	{
		isInteractingWithUI = true;
	}

	private void OnClickCanceled(InputAction.CallbackContext context)
	{
		isInteractingWithUI = false;
	}
}
