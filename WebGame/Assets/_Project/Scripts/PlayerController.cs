using UnityEngine;

namespace Platformer397
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] private InputReader input;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            input.EnablePlayerActions();
        }

        private void OnEnable()
        {
            input.Move += GetMovement;
        }

        private void OnDisable()
        {
            input.Move -= GetMovement;
        }

        private void GetMovement(Vector2 move)
        {
            
        }
    }
}
