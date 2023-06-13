using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopPropellerRoot : MonoBehaviour
{
        private GoalCheck goalCheck;
        public float speed = 10f;
        public float duration = 3f;
        private Vector3 originalPosition;
        private Rigidbody rb;

        private void Awake()
        {
            goalCheck = GetComponentInChildren<GoalCheck>();
        }
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            originalPosition = transform.position;

            // �ڷ�ƾ ����
            StartCoroutine(ConstantMovementCoroutine());
        }

        private IEnumerator ConstantMovementCoroutine()
        {
            while (true)
            {
                if (goalCheck.GoalInPlayer)
                {
                    // ���� �̵�
                    Vector3 targetPosition = originalPosition + Vector3.up;
                    Vector3 direction = targetPosition - transform.position;
                    rb.velocity = direction.normalized * speed;

                    // ���� �ð�(���⼭�� duration)��ŭ ���
                    yield return new WaitForSeconds(duration);
                   
                    // ���� ��ġ�� �̵�
                    direction = originalPosition - transform.position;
                    rb.velocity = direction.normalized * speed;
                    goalCheck.GoalInPlayer = false;
                }
            }
        }
    }
  
    //
    //[SerializeField] private float _theHoopMovesUp = 10;
    //private Vector3 _hoopUpPosition;
    //private Vector3 _originHoopPosition;
    //void Awake()
    //{
    //    
    //    _originHoopPosition = transform.position;
    //}

    //void Update()
    //{
    //    if ()
    //    {
    //        transform.Translate(Vector3.up * _theHoopMovesUp * Time.deltaTime);
    //    }
    //}

    //IEnumerator HoopPosition()
    //{

    //}
