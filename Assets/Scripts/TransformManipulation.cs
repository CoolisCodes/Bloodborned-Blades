using UnityEngine;

public class TransformManipulation : MonoBehaviour
{
    //What the class HAS:
    public int AkereosArithmos;



    //What the class DOES:


    void Start()
    {

    }

    void Update()
    {
        MoveObject();
    }

    public void MoveObject()
    {
        // Move the object to a new position
        transform.position = new Vector3(0, AkereosArithmos, 0);
    }

}
