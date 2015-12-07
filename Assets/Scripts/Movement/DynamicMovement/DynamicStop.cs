using UnityEngine;

namespace Assets.Scripts.Movement.DynamicMovement
{
    public class DynamicStop : DynamicMovement
    { 
        public override string Name
        {
            get { return "Stop"; }
        }
        public float TimeToStop { get; set; }
        public float Delta { get; set; }
        public override KinematicData Target { get; set; }
        public DynamicStop(KinematicData character)
        {
            this.Target = character;
            this.Character = character;
            this.Delta = 0.05f;
        }
        
        public override MovementOutput GetMovement()
        {
            MovementOutput output = new MovementOutput();
            if (this.Character.velocity.magnitude < Delta)
                return output;
            output.linear = -this.Character.GetOrientationAsVector();
            output.linear *= TimeToStop;
            return output;
        }
    }
}
