using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poignee : MonoBehaviour
{
    public Enums.Sens sensPoignee;

    public float dureeMontee = 2;

    public bool stayDown = true;

    int subdivision = 50;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && sensPoignee == Enums.Sens.Left && stayDown)
        {
            stayDown = false;
            for (int i = 0; i < subdivision; i++)
            {
                StartCoroutine(flipperRot());
            }
                
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && sensPoignee == Enums.Sens.Right && stayDown)
        {
            stayDown = false;
            for (int i = 0; i < subdivision; i++)
            {
                StartCoroutine(flipperRot());
            }
        }

        if (stayDown)
        {
            switch (sensPoignee)
            {
                case Enums.Sens.Right:
                    if (transform.rotation.z < 15)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 15));
                    }
                    break;
                case Enums.Sens.Left:
                    if (transform.rotation.z > -15)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, 0, - 15 ));
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public IEnumerator flipperRot()
    {
        Debug.Log("COROUTINE");

        switch (sensPoignee)
        {
            case Enums.Sens.Right:
                //transform.Rotate(new Vector3(0, 0, -15 / subdivision));
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -15 / subdivision));
                break;
            case Enums.Sens.Left:
                //transform.Rotate(new Vector3(0, 0, 15 / subdivision));
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 15 / subdivision));
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(dureeMontee / subdivision);
        

        stayDown = true;
    }
}
