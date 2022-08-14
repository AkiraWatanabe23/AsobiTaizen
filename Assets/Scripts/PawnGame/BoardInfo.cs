using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �}�X�ɂ����̏����擾����
/// </summary>
public class BoardInfo : MonoBehaviour
{
    RaycastHit _hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInfo();
    }

    void GetInfo()
    {
        //���ꂼ��̃}�X���������Ray���΂��A��ɓ��������炻�̋��tag���}�X�ɑ��
        //����������̑����OK���������������[Tile]tag�ɖ߂�Ȃ�
        if (Physics.Raycast(gameObject.transform.position, Vector3.up, out _hit, 5))
        {
            if (_hit.collider != null)
            {
                if (_hit.collider.gameObject.tag == "WhitePiece")
                {
                    this.gameObject.tag = "WhitePiece";
                    Debug.Log("aaa");
                }
                else if (_hit.collider.gameObject.tag == "BlackPiece")
                {
                    this.gameObject.tag = "BlackPiece";
                    Debug.Log("bbb");
                }
            }
            else if (_hit.collider == null)
            {
                this.gameObject.tag = "Tile";
                Debug.Log("ccc");
            }
        }
    }
}
