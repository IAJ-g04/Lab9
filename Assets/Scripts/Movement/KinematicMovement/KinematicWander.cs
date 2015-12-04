using Assets.Scripts.Utils;

namespace Assets.Scripts.Movement.KinematicMovement
{
    public class KinematicWander : KinematicMovement
    {
        public override string Name
        {
            get { return "Wander"; }
        }

        public float MaxRotation { get; set; }

        public KinematicWander()  
        {
            this.MaxRotation = 8*MathConstants.MATH_PI;
        }

        public override MovementOutput GetMovement()
        {
            var output = new MovementOutput();

            // Move forward in the current direction
            output.linear = this.Character.GetOrientationAsVector();
            output.linear *= this.MaxSpeed;

            // Turn a little
            output.angular = RandomHelper.RandomBinomial() * this.MaxRotation;

            return output;
        }
    }
}
