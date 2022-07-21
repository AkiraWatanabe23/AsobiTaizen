using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices;

public class PieceManager : MonoBehaviour, IPointerClickHandler
{
    /// <summary> �v���C���[(��) </summary>
    public const int _playerOne = 1;
    /// <summary> �v���C���[(��) </summary>
    public const int _playerTwo = 2;
    /// <summary> current(���݂�)�v���C���[ </summary>
    public int _currentPlayer;
    /// <summary> ���C���[�}�X�N(Inspector��Layer����I������) </summary>
    [SerializeField] LayerMask _tileLayer;
    [SerializeField] LayerMask _whiteLayer;
    [SerializeField] LayerMask _blackLayer;
    /// <summary> ���Ԗڐ��̃J���� </summary>
    Camera _camera;
    /// <summary> ����ړ���������collider�̏�ɒu�� </summary>
    [SerializeField] Vector3 _offset = Vector3.up;

    PieceController _phase;

    //extern...Unity��VisualStudio�ɂ͂Ȃ��@�\(�֐�)���Ƃ��Ă���(C++�ł����Ɓu::�v�Ɠ����炵��)
    //[DllImport("user32.dll")]...�O�̂ǂ̃t�@�C��(����́uuser32.dll�v)����Ƃ��Ă���̂�
    //SetCursorPos(�֐�)...�w�肵���t�@�C�����̂ǂ̋@�\(�֐�)���g���̂�
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int x, int y);

    /// <summary>
    /// �}�E�X�N���b�N���s��ꂽ(�ǂ̃}�E�X�N���b�N�ł����s�����)���̏���
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        var go = eventData.pointerCurrentRaycast.gameObject;
        var piece = go.GetComponent<PieceController>();

        print($"{ name } ��I��");
        piece.ChangeState();
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentPlayer = _playerOne;                                       //���Ԃ���n�߂�
        _camera = GameObject.Find("Camera(black)").GetComponent<Camera>(); //���Ԗڐ��̃J�����������Ă���

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Move()
    {
        //�}�E�X�̈ʒu���擾���ARay�ɑ��
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition); //���C���J����(���Ԗڐ�)����Ray���Ƃ΂�
        Ray _ray2 = _camera.ScreenPointToRay(Input.mousePosition);    //second�J����(���Ԗڐ�)����Ray���Ƃ΂�
        //Ray�̒���
        float _rayDistance = 100;

        //���Ԗڐ��̋�̈ړ�
        //���Ԗڐ���Ray�̏���(���D���ꍇ)
        if (Physics.Raycast(_ray, out RaycastHit hit, _rayDistance, _blackLayer))
        {
            GameObject _target = hit.collider.gameObject;

            //Ray�����������I�u�W�F�N�g���G�̋�����ꍇ�A���D���Ă��̃}�X�Ɉړ�����
            if (_target.tag == "BlackPiece")
            {
                _target.SetActive(false);
            }

            this.transform.position = _target.transform.position + _offset;
            _currentPlayer = _playerTwo;

            _phase.PhaseChange(_target);
            SetCursorPos(950, 400); //����ړ���������A�}�E�X�J�[�\�����Q�[����ʂ̒����ӂ�ɋ����ړ�������

            print($"Ray �� {_target.name} �Ɉړ�����");
            Debug.Log(_currentPlayer);
            return true;
        }
        //���Ԗڐ���Ray�̏���(�ړ��̂�)
        else if (Physics.Raycast(_ray, out RaycastHit hit2, _rayDistance, _tileLayer))
        {
            GameObject _target = hit2.collider.gameObject;
            this.transform.position = _target.transform.position + _offset;
            _currentPlayer = _playerTwo;

            _phase.PhaseChange(_target);
            SetCursorPos(950, 400); //����ړ���������A�}�E�X�J�[�\�����Q�[����ʂ̒����ӂ�ɋ����ړ�������

            print($"Ray �� {_target.name} �Ɉړ�����"); // print($"..."); ���� Debug.Log("..."); �Ɠ���
            Debug.Log(_currentPlayer);
            return true;
        }

        //���Ԗڐ��̋�̈ړ�
        //���Ԗڐ���Ray�̏���(���D���ꍇ)
        if (Physics.Raycast(_ray2, out RaycastHit hit3, _rayDistance, _whiteLayer))
        {
            GameObject _target = hit3.collider.gameObject;

            //Ray�����������I�u�W�F�N�g���G�̋�����ꍇ�A���D���Ă��̃}�X�Ɉړ�����
            if (_target.tag == "WhitePiece")
            {
                _target.SetActive(false);
            }

            this.transform.position = _target.transform.position + _offset;
            _currentPlayer = _playerOne;

            _phase.PhaseChange(_target);
            SetCursorPos(950, 400); //����ړ���������A�}�E�X�J�[�\�����Q�[����ʂ̒����ӂ�ɋ����ړ�������

            print($"Ray �� {_target.name} �Ɉړ�����");
            Debug.Log(_currentPlayer);
            return true;
        }
        //���Ԗڐ���Ray�̏���(�ړ��̂�)
        else if (Physics.Raycast(_ray2, out RaycastHit hit4, _rayDistance, _tileLayer))
        {
            GameObject _target = hit4.collider.gameObject;
            this.transform.position = _target.transform.position + _offset;
            _currentPlayer = _playerOne;

            _phase.PhaseChange(_target);
            SetCursorPos(950, 400); //����ړ���������A�}�E�X�J�[�\�����Q�[����ʂ̒����ӂ�ɋ����ړ�������

            print($"Ray �� {_target.name} �Ɉړ�����");
            Debug.Log(_currentPlayer);
            return true;
        }

        return false;
    }
}
