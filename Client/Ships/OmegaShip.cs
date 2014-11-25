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
    class OmegaShip : Ship
    {
        public OmegaShip(Game1 game1, Color teamColor)
            : base(assets.zoid, assets.laser, game1, teamColor)
        {
            maxSpeed = 50;
            accelSpeed = 150.0f;
            turnSpeed = 3.0f;
            mass = 500;

            base.scale = 2f;

            mainFireDelay = TimeSpan.FromSeconds(.5f);
            altFireDelay = TimeSpan.FromSeconds(1f);

            mainWeapon = Launcher<Laser>.CreateLauncher(this, (x, y) => new Laser(x, y), 8);
            weaponOrigin = new Vector2(position.X, position.Y - height / 2); // start position of weapon
        }
    }
}
