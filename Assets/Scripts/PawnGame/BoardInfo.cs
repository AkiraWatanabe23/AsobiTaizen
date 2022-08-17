using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �}�X�ɂ����̏����擾����
/// </summary>
public class BoardInfo : MonoBehaviour
{
    RaycastHit _hit;

    // Update is called once per frame
    void Update()
    {
        GetInfo();
    }

    /// <summary>
    /// ���ꂼ��̃}�X���������Ray���΂��A��ɓ��������炻�̋��tag("WhitePiece" or "BlackPiece")���}�X�ɑ��
    /// �Ȃɂ��Ȃ����"Tile"tag�ɂ���
    /// �����}�X��Collider��off�ɂ���
    /// </summary>
    void GetInfo()
    {
        //Ray���w��̕����ɔ�΂�����(�I�u�W�F�N�g�ɓ�����̂��O��̏�����)
        if (Physics.Raycast(gameObject.transform.position, Vector3.up, out _hit, 5))
        {
            //�}�X�ɔ������ꍇ
            if (_hit.collider.gameObject.tag == "WhitePiece")
            {
                this.gameObject.tag = "WhitePiece";
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
            //�}�X�ɍ������ꍇ
            else if (_hit.collider.gameObject.tag == "BlackPiece")
            {
                this.gameObject.tag = "BlackPiece";
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
        //�}�X�ɋ�Ȃ��ꍇ
        else if (_hit.collider == null)
        {
            this.gameObject.tag = "Tile";
            this.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
