using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnionXNA.Tools;
using SpaceUnionXNA.Weapons;
using SpaceUnionXNA.Weapons.Projectiles;
using SpaceUnionXNA.Weapons.Systems;


namespace SpaceUnionXNA.Ships
{
    /// <summary>
    /// A tiny ship.
    /// @Written by Tristan.
    /// </summary>
    class ThetaShip : Ship
    {
        public ThetaShip(Game1 game1, bool team)
            : base(assets.scout, assets.laser, game1, team)
        {
            maxSpeed = 75;
            accelSpeed = 400.0f;
            turnSpeed = 7.0f;

            mass = 500;

            mainFireDelay = TimeSpan.FromSeconds(.5f);
            altFireDelay = TimeSpan.FromSeconds(1f);

            mainWeapon = Launcher<Laser>.CreateLauncher(this, (x, y) => new Laser(x, y), 8);
            weaponOrigin = new Vector2(position.X, position.Y - height / 2); // start position of weapon
        }
    }
}
