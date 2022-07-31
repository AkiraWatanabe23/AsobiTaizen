using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// �I�u�W�F�N�g��I�����A�ړ������鏈��(�e�X�g)
/// </summary>
public class ClickTest : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Material _normal;
    [SerializeField] Material _move;
    [SerializeField] float _rayDistance = 100;
    [SerializeField] LayerMask _layerMask;
    [SerializeField] Vector3 _offset = Vector3.up;
    Status _status = Status.Normal;
    Renderer _ren;

    public void OnPointerClick(PointerEventData eventData)
    {
        print($"{ name } ���N���b�N����");
        ChangeState();
    }

    bool Move()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _layerMask))
        {
            GameObject target = hit.collider.gameObject;
            print($"ray �� {target.name} �ɓ�������");
            this.transform.position = target.transform.position + _offset;
            return true;
        }

        return false;
    }

    void ChangeState()
    {
        if (_status == Status.Normal)
        {
            _status = Status.Move;
            _ren.material = _move;
        }
        else if (_status == Status.Move)
        {
            _status = Status.Normal;
            _ren.material = _normal;
        }
    }

    void Start()
    {
        _ren = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_status == Status.Move)
            {
                if (Move())
                {
                    ChangeState();
                }
            }
        }
    }

    
}

enum Status
{
    Normal,
    Move,
}
