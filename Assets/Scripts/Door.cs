using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Door : MonoBehaviour, IInteractable
{
    public static event EventHandler isOpenCloseDoor;

    public static void ResetStaticEvent()
    {
        isOpenCloseDoor = null;
    }

    [SerializeField] private bool isOpen;

    private GridPosition gridPosition;
    private Animator animator;
    private float timer;
    private bool isActive;
    private Action onInteractionCompleted;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetInteractableAtGridPosition(gridPosition, this);

        if (isOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            isActive = false;
            onInteractionCompleted();
        }
    }

    public void Interact(Action onInteractionCompleted)
    {
        this.onInteractionCompleted = onInteractionCompleted;
        timer = 0.5f;
        isActive = true;

        if (isOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }
    private void OpenDoor()
    {
        isOpenCloseDoor?.Invoke(this, EventArgs.Empty);

        isOpen = true;
        animator.SetBool("IsOpen", isOpen);
        PathFinding.Instance.SetIsWalkableGridPosition(gridPosition, true);
    }
    private void CloseDoor()
    {
        isOpenCloseDoor?.Invoke(this, EventArgs.Empty);
        isOpen = false;
        animator.SetBool("IsOpen", isOpen);
        PathFinding.Instance.SetIsWalkableGridPosition(gridPosition, false);
    }
}
