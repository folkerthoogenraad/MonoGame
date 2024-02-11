using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xna.Framework.Platform.WASM
{
    public static partial class JSBootstrap
    {
        [JSImport("globalThis.console.log")]
        public static partial void Log([JSMarshalAs<JSType.String>] string message);

        [JSImport("bootstrap.getCanvas", "main.js")]
        internal static partial JSObject GetCanvas([JSMarshalAs<JSType.String>] string message);

        [JSImport("bootstrap.getCanvasWidth", "main.js")]
        internal static partial int GetCanvasWidth(JSObject canvas);

        [JSImport("bootstrap.getCanvasHeight", "main.js")]
        internal static partial int GetCanvasHeight(JSObject canvas);

        [JSImport("bootstrap.getGLContext", "main.js")]
        internal static partial JSObject GetGLContext(JSObject canvas);


        [JSImport("globalThis.requestAnimationFrame")]
        internal static partial int RequestAnimationFrame([JSMarshalAs<JSType.Function>] Action action);


        [JSImport("bootstrap.getBaseUrl", "main.js")]
        internal static partial string GetBaseURL();

        [JSImport("globalThis.screen.width")]
        public static partial int GetScreenWidth();

        [JSImport("globalThis.screen.height")]
        public static partial int GetScreenHeight();
    }
}
