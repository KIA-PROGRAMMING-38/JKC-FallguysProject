using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopPropellerUpDuration : MonoBehaviour
{
    private GoalCheck _goalCheck;
    private Vector3 _originPosition;
    private bool _isMoving;
    private bool _playerEntered;
    [SerializeField] private float _duration = 3f; // 기본은 5초, 골드는 7초
    [SerializeField] private float _heightOffset; // 기본은 20, 골드는 30
    [SerializeField] private float _moveSpeed; // 기본, 골드 1
    private float _currentHeight;

    private void Awake()
    {
        _goalCheck = GetComponentInChildren<GoalCheck>();
        _originPosition = transform.position;
        _currentHeight = _originPosition.y;
    }

    void Start()
    {
        _goalCheck.OnPlayerEnter += HandlePlayerEnter;
        _goalCheck.OnPlayerExit += HandlePlayerExit;
    }

    private void OnDestroy()
    {
        _goalCheck.OnPlayerEnter -= HandlePlayerEnter;
        _goalCheck.OnPlayerExit -= HandlePlayerExit;
    }

    private void HandlePlayerEnter()
    {
        if (!_playerEntered && !_isMoving)
        {
            _playerEntered = true;
            StartCoroutine(PropellerAnimation());
        }
    }

    private void HandlePlayerExit()
    {
        _playerEntered = false;
    }

    private IEnumerator PropellerAnimation()
    {
        _isMoving = true;
        float targetHeight = _originPosition.y + _heightOffset;
        float elapsedTime = 0f;
        float moveSpeedMultiplier = _moveSpeed; // 이동 속도에 대한 보정을 위한 계수
        while (_currentHeight < targetHeight)
        {
            elapsedTime += Time.deltaTime * moveSpeedMultiplier;
            _currentHeight = Mathf.Lerp(_originPosition.y, targetHeight, elapsedTime / _duration);
            transform.position = new Vector3(_originPosition.x, _currentHeight, _originPosition.z);
            yield return null;
        }

        yield return new WaitForSeconds(_duration);

        elapsedTime = 0f;
        while (_currentHeight > _originPosition.y)
        {
            elapsedTime += Time.deltaTime * moveSpeedMultiplier;
            _currentHeight = Mathf.Lerp(targetHeight, _originPosition.y, elapsedTime / _duration);
            transform.position = new Vector3(_originPosition.x, _currentHeight, _originPosition.z);
            yield return null;
        }

        _isMoving = false;
    }
}