namespace Othaura {

    using UnityEngine;

    /// <summary>
    /// This is your game entry point. You can rename this class to whatever you'd like.
    /// </summary>
    public class RoguelikeEntry : MonoBehaviour {

        private void Awake() {

            // Initialize your game!
            // You can call RB.Initialize multiple times to switch between RetroBlit games!
            RB.Initialize(new RoguelikeGame());
        }
    }
}