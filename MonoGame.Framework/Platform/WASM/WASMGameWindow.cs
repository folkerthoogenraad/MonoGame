using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xna.Framework.Platform.WASM
{
    internal class WASMGameWindow : GameWindow
    {
        public static WASMGameWindow Instance;
        public override bool AllowUserResizing { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override Rectangle ClientBounds => new Rectangle(0, 0, JSBootstrap.GetCanvasWidth(_canvas), JSBootstrap.GetCanvasHeight(_canvas));

        public override Point Position { get => new Point(0, 0); set => throw new NotImplementedException(); }

        public override DisplayOrientation CurrentOrientation => DisplayOrientation.LandscapeRight;

        public override IntPtr Handle => IntPtr.Zero;

        public override string ScreenDeviceName => "WASM";

        private JSObject _canvas;
        public JSObject Canvas => _canvas;
        public JSObject GLContext { get; set; }

        public WASMGameWindow()
        {
            // Very ugly, I know, but we need the instance to get all the other functions
            Instance = this;

            _canvas = JSBootstrap.GetCanvas("canvas");
        }

        public override void BeginScreenDeviceChange(bool willBeFullScreen)
        {
            throw new NotImplementedException();
        }

        public override void EndScreenDeviceChange(string screenDeviceName, int clientWidth, int clientHeight)
        {
            throw new NotImplementedException();
        }

        protected override void SetTitle(string title)
        {
            throw new NotImplementedException();
        }

        protected internal override void SetSupportedOrientations(DisplayOrientation orientations)
        {
        }
    }
}
