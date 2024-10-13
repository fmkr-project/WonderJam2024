namespace Modules
{
    public enum ShieldType
    {
        Shield, // Absorbs damage.
        RegenerativeShield, // Regenerates health. Does not absorb damage.
        PlasmaShield, // Absorbs damage and deals damage to the attacker until it is broken.
        PsionicShield // Absorbs damage and reduces the attacker's damage.
    }
}