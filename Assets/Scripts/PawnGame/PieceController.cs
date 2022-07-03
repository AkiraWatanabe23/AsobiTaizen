using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    //�v���C���[
    public int Player;
    //��̎��
    public Type _type;
    //����o�߂�����
    public int _turnCount;
    //��̔z�u�ʒu
    public Vector2Int Pos, _oldPos;
    //�ړ����
    public List<Status> _status;


    //Queen = 5, Rook = 4, Bishop = 3, Knight = 2, Pawn = 1 �Ɛ�����U��
    public enum Type
    {
        None = -1,
        Pawn = 1,
        Knight,
        Bishop,
        Rook,
        Queen,
    }

    //�ړ����
    public enum Status
    {
        None = -1,
        EnPassant = 1, //�A���p�b�T��
        Check,
    }

    // Start is called before the first frame update
    void Start()
    {
        _turnCount = -1;
        _status = new List<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�����ݒ�
    public void SetPiece(int player, Type type, GameObject tile)
    {
        Player = player;
        _type = type;
        MovePiece(tile);
        _turnCount = -1; //�����ɖ߂�
    }

    public void MovePiece(GameObject tile)
    {
        //board�̔ԍ�����z��ԍ��ɕϊ�
        Vector2Int idx = new Vector2Int
            ((int)tile.transform.position.x + GameScene._boardWidth / 2,
             (int)tile.transform.position.z + GameScene._boardHeight / 2);

        //�ꏊ�̈ړ�
        Vector3 _pos = tile.transform.localPosition;
        _pos.y = 21.25f;
        transform.localPosition = _pos;

        //�ړ���Ԃ��Z�b�g
        _status.Clear();

        //�A���p�b�T���̏���


        //�C���f�b�N�X�̍X�V
        _oldPos = Pos;
        Pos = idx;

        //�z�u���Ă���̌o�߃^�[�������Z�b�g
        _turnCount = 0;
    }
}
