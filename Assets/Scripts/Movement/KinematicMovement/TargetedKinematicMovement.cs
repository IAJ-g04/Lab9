namespace Assets.Scripts.Movement.KinematicMovement
{
    public abstract class TargetedKinematicMovement : KinematicMovement
    {
        public StaticData Target { get;  set; }
    }
}
