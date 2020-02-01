using UnityEngine;

namespace Controller
{
    public class PlayerController : MonoBehaviour
    {
        public void MoveUp()
        {
            Debug.Log("Moving " + gameObject.name +" up!!!");
        }
    
        public void MoveDown()
        {
            Debug.Log("Moving " + gameObject.name +" down!!!");
        }

        public void Fire1()
        {
            Debug.Log("Firing1 " + gameObject.name +"!!!");
        }
    
        public void Jump()
        {
            Debug.Log("Jumping " + gameObject.name +"!!!");
        }
    }
}
