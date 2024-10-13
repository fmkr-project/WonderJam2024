namespace Modules
{
    public enum WeaponType
    {
        Laser, // Basic single-target weapon.
        PlasmaThrower, // Basic all-target weapon.
        Disruptor, // Anti-shield single-target weapon. Only deals damage if the target has shields.
        ArcEmitter, // Anti-shield all-target weapon. Only deals damage if the target has shields.
        Autocannon, // Repetitive random-target weapon.
        Missiles, // Single-target weapon that multiplies its damage by two if the target has no shields.
        Torpedoes // All-target weapon that multiplies its damage by two if the target has no shields.
    }
}