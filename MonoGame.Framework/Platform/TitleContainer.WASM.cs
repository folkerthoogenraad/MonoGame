// MonoGame - Copyright (C) MonoGame Foundation, Inc
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.IO;
using System.Net.Http;
using Microsoft.Xna.Framework.Platform.WASM;
using MonoGame.Framework.Utilities;

namespace Microsoft.Xna.Framework
{
    partial class TitleContainer
    {
        static partial void PlatformInit()
        {
            // TODO :)
        }

        private static Stream PlatformOpenStream(string safeName)
        {
            HttpClient sharedClient = new()
            {
                BaseAddress = new Uri(JSBootstrap.GetBaseURL()),
            };

            JSBootstrap.Log("Created the shared client");

            var response = sharedClient.GetAsync(safeName).GetAwaiter().GetResult();
            var stream = response.Content.ReadAsStream();

            return stream;
        }
    }
}

