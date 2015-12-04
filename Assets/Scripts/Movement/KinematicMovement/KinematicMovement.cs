namespace Assets.Scripts.Movement.KinematicMovement
{
    public abstract class KinematicMovement : Movement
    {
        public StaticData Character { get; set; }
        public float MaxSpeed { get; set; }
    }
}
