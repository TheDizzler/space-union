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
    class AlphaShip : Ship
    {
        public AlphaShip(Game1 game1, Color teamColor)
            : base(assets.alpha_ship_texture, assets.laser, game1, teamColor)
        {
            maxSpeed = 100;
            accelSpeed = 850.0f;
            turnSpeed = 10.0f;
            base.scale = 0.75f;

            mass = 500;

            mainFireDelay = TimeSpan.FromSeconds(.5f);
            altFireDelay = TimeSpan.FromSeconds(1f);

            //mainWeapon = new Launcher<MoltenBullet>(10);
            mainWeapon = Launcher<Laser>.CreateLauncher(this, (x, y) => new Laser(x, y), 8);
            weaponOrigin = new Vector2(position.X, position.Y - height / 2); // start position of weapon
        }


        protected override void altFire(GameTime gameTime)
        {

        }

        protected override void additionalUpdate(GameTime gameTime, QuadTree quadTree)
        {

        }

        protected override void additionalDraw(SpriteBatch sBatch)
        {

        }

        protected override void additionalFire(GameTime gameTime)
        {

        }



    }
}
