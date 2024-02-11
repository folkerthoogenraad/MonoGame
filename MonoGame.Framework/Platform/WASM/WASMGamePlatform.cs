using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Microsoft.Xna.Framework.Platform.WASM
{
    internal class WASMGamePlatform : GamePlatform
    {
        public WASMGamePlatform(Game game) : base(game)
        {
            Window = new WASMGameWindow();
        }

        public override GameRunBehavior DefaultRunBehavior => GameRunBehavior.Asynchronous;

        public override bool BeforeDraw(GameTime gameTime)
        {
            // throw new NotImplementedException();
            return true;
        }

        public override bool BeforeUpdate(GameTime gameTime)
        {
            // throw new NotImplementedException();
            return true;
        }

        public override void BeginScreenDeviceChange(bool willBeFullScreen)
        {
            JSBootstrap.Log("Begin device chagne?! " + willBeFullScreen);
        }

        public override void EndScreenDeviceChange(string screenDeviceName, int clientWidth, int clientHeight)
        {
            JSBootstrap.Log("Enmd device change! " + screenDeviceName);
        }

        public override void EnterFullScreen()
        {
            JSBootstrap.Log("EnterFullScreen?! Not implemented!");
        }

        public override void Exit()
        {
            JSBootstrap.Log("Exit?! Not implemented!");
        }

        public override void ExitFullScreen()
        {
            JSBootstrap.Log("ExitFullScreen?! Not implemented!");
        }

        public override void RunLoop()
        {
            throw new NotImplementedException("Not supported");
        }

        public override void StartRunLoop()
        {
            // XNA runs a frame before actually running :)
            WASMUpdate();

        }

        private void WASMUpdate()
        {
            Game.Tick();
            Threading.Run();

            JSBootstrap.RequestAnimationFrame(() => {
                WASMUpdate();
            });
        }
    }
}
