using System;
using LiteralRepository;
using Photon.Pun;
using UnityEngine;

public class GoalCheck : MonoBehaviourPun
{
    public event Action OnPlayerEnter;
    public event Action OnPlayerExit;

    private bool _goalInPlayer;
    public bool GoalInPlayer => _goalInPlayer;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(TagLiteral.Player))
        {
            PhotonView playerPhotonView = col.GetComponent<PhotonView>();
            if (playerPhotonView != null && playerPhotonView.IsMine)
            {
                _goalInPlayer = true;
                OnPlayerEnter?.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag(TagLiteral.Player))
        {
            PhotonView playerPhotonView = col.GetComponent<PhotonView>();
            if (playerPhotonView != null && playerPhotonView.IsMine)
            {
                _goalInPlayer = false;
                OnPlayerExit?.Invoke();    
            }
        }
    }
} 