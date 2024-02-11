// MonoGame - Copyright (C) MonoGame Foundation, Inc
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Platform.WASM;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Threading.Tasks;

namespace MonoGame.OpenGL
{
    [SupportedOSPlatform("browser")]
    partial class GL
    {
        private static Dictionary<int, JSObject> WasmTextures = new Dictionary<int, JSObject>();
        private static Dictionary<JSObject, int> WasmTexturesReverse = new Dictionary<JSObject, int>();

        private static Dictionary<int, JSObject> WasmShaders = new Dictionary<int, JSObject>();
        private static Dictionary<int, JSObject> WasmPrograms  = new Dictionary<int, JSObject>();
        private static Dictionary<int, JSObject> WasmBuffers  = new Dictionary<int, JSObject>();

        private static int LastTextureId = 0;
        private static int LastShaderId = 0;
        private static int LastProgramId = 0;
        private static int LastBufferId = 0;

        static partial void LoadPlatformEntryPoints()
        {
            BoundApi = RenderApi.ES;

            GetError = () => (ErrorCode)WASMGL.GLGetError(WASMGameWindow.Instance.GLContext);
            Finish = () => WASMGL.GLFinish(WASMGameWindow.Instance.GLContext);

            Enable = (enable) => WASMGL.GLEnable(WASMGameWindow.Instance.GLContext, (int)enable);
            Disable = (disable) => WASMGL.GLDisable(WASMGameWindow.Instance.GLContext, (int)disable);

            ColorMask = (r,g,b,a) => WASMGL.GLColorMask(WASMGameWindow.Instance.GLContext, r, g, b, a);

            ClearColor = (r, g, b, a) => WASMGL.GLClearColor(WASMGameWindow.Instance.GLContext, r, g, b, a);
            Clear = (bits) => WASMGL.GLClear(WASMGameWindow.Instance.GLContext, (int)bits);

            BlendEquationSeparate = (colorMode, alphaMode) => WASMGL.GLBlendEquationSeparate(WASMGameWindow.Instance.GLContext, (int)colorMode, (int)alphaMode);
            BlendFuncSeparate = (a, b, c, d) => WASMGL.GLBlendFuncSeperate(WASMGameWindow.Instance.GLContext, (int)a, (int)b, (int)c, (int)d);
            BlendColor = (r, g, b, a) => WASMGL.GLBlendColor(WASMGameWindow.Instance.GLContext, r, g, b, a);

            DepthFunc = (func) => WASMGL.GLDepthFunc(WASMGameWindow.Instance.GLContext, (int)func);
            DepthMask = (mask) => WASMGL.GLDepthMask(WASMGameWindow.Instance.GLContext, mask);

            StencilMask = (mask) => WASMGL.GLStencilMask(WASMGameWindow.Instance.GLContext, mask);
            StencilFunc = (func, r, mask) => WASMGL.GLStencilFunc(WASMGameWindow.Instance.GLContext, (int)func, r, mask);
            StencilOp = (fail, zfail, zpass) => WASMGL.GLStencilOp(WASMGameWindow.Instance.GLContext, (int)fail, (int)zfail, (int)zpass);
            StencilFuncSeparate = (face, func, r, mask) => WASMGL.GLStencilFuncSeparate(WASMGameWindow.Instance.GLContext, (int)face, (int)func, r, mask);
            StencilOpSeparate = (face, fail, zfail, zpass) => WASMGL.GLStencilOpSeparate(WASMGameWindow.Instance.GLContext, (int)face, (int)fail, (int)zfail, (int)zpass);

            CullFace = (mode) => WASMGL.GLCullFace(WASMGameWindow.Instance.GLContext, (int)mode);
            FrontFace = (mode) => WASMGL.GLFrontFace(WASMGameWindow.Instance.GLContext, (int)mode);
            PolygonOffset = (factor, units) => WASMGL.GLPolygonOffset(WASMGameWindow.Instance.GLContext, factor, units);

            BindFramebuffer = (type, buffer) => WASMGL.GLBindFramebuffer(WASMGameWindow.Instance.GLContext, (int)type, null); // TODO pass the framebuffer somehow?

            Viewport = (x, y, w, h) => WASMGL.GLViewport(WASMGameWindow.Instance.GLContext, x, y, w, h);
            Scissor = (x, y, w, h) => WASMGL.GLScissor(WASMGameWindow.Instance.GLContext, x, y, w, h);

            GenTextures = WASM_GenTextures;
            DeleteTextures = WASM_DeleteTextures;
            BindTexture = (target, texture) => WASMGL.GLBindTexture(WASMGameWindow.Instance.GLContext, (int)target, WASM_GetTexture(texture));
            TexParameterf = (target, name, value) => WASMGL.GLTexParameterf(WASMGameWindow.Instance.GLContext, (int)target, (int)name, value);
            TexParameteri = (target, name, value) => WASMGL.GLTexParameteri(WASMGameWindow.Instance.GLContext, (int)target, (int)name, value);
            PixelStore = (parameter, size) => WASMGL.GLPixelStorei(WASMGameWindow.Instance.GLContext, (int)parameter, size);
            ActiveTexture = (texture) => WASMGL.GLActiveTexture(WASMGameWindow.Instance.GLContext, (int)texture);

            CreateShader = WASM_CreateShader;
            DeleteShader = WASM_DeleteShader;
            CompileShader  = (shaderId) => WASMGL.GLCompileShader(WASMGameWindow.Instance.GLContext, WASM_GetShader(shaderId));

            CreateProgram = WASM_CreateProgram;
            AttachShader = (programId, shaderId) => WASMGL.GLAttachShader(WASMGameWindow.Instance.GLContext, WASM_GetProgram(programId), WASM_GetShader(shaderId));
            LinkProgram = (programId) => WASMGL.GLLinkProgram(WASMGameWindow.Instance.GLContext, WASM_GetProgram(programId));
            UseProgram = (programId) => WASMGL.GLUseProgram(WASMGameWindow.Instance.GLContext, WASM_GetProgram(programId));
            DeleteProgram = WASM_DeleteProgram;

            GetAttribLocation = (programId, name) => WASMGL.GLGetAttribLocation(WASMGameWindow.Instance.GLContext, WASM_GetProgram(programId), name);
            // GetUniformLocation = (programId, name) => WASMGL.GLGetUniformLocation(WASMGameWindow.Instance.GLContext, WASM_GetProgram(programId), name);

            EnableVertexAttribArray = (attrib) => WASMGL.GLEnableVertexAttribArray(WASMGameWindow.Instance.GLContext, attrib);
            DisableVertexAttribArray = (attrib) => WASMGL.GLDisableVertexAttribArray(WASMGameWindow.Instance.GLContext, attrib);

            VertexAttribPointer = (index, size, type, normalized, stride, offset) => WASMGL.GLVertexAttribPointer(WASMGameWindow.Instance.GLContext, index, size, (int)type, normalized, stride, (int)offset);

            GenBuffers = WASM_GenBuffers;
            DeleteBuffers = WASM_DeleteBuffers;
            BindBuffer = (target, buffer) => WASMGL.GLBindBuffer(WASMGameWindow.Instance.GLContext, (int)target, WASM_GetBuffer(buffer));

            DrawElements = (mode, count, type, offset) => WASMGL.GLDrawElements(WASMGameWindow.Instance.GLContext, (int)mode, count, (int)type, (int)offset);
        }

        // ====================================================== //
        // Textures
        // ====================================================== //
        internal static void WASM_GenTextures(int count, [Out] out int id)
        {
            if (count != 1)
            {
                throw new NotSupportedException("At this point mutliple generation is not supported");
            }

            var texture = WASMGL.GLCreateTexture(WASMGameWindow.Instance.GLContext);
            LastTextureId++;

            id = LastTextureId;

            WasmTextures[id] = texture;
            WasmTexturesReverse[texture] = id;
        }
        internal static void WASM_DeleteTextures(int count, ref int id)
        {
            if (count != 1)
            {
                throw new NotSupportedException("At this point mutliple generation is not supported");
            }
            var texture = WASM_GetTexture(id);

            WASMGL.GLDeleteTexture(WASMGameWindow.Instance.GLContext, texture);

            WasmTextures.Remove(id);
            WasmTexturesReverse.Remove(texture);
        }

        internal static JSObject WASM_GetTexture(int id)
        {
            if (id == 0) return null;

            WasmTextures.TryGetValue(id, out var texture);
            return texture;
        }
        internal static int WASM_GetTextureId(JSObject texture)
        {
            return WasmTexturesReverse[texture];
        }
        public static void WASM_TexImage2D(TextureTarget target, int level, PixelInternalFormat internalFormat, int width, int height, int border, PixelFormat format, PixelType pixelType, Span<byte> data)
        {
            if (data.Length == 0)
            {
                WASMGL.GLTexImage2D_null(WASMGameWindow.Instance.GLContext, (int)target, level, (int)internalFormat, width, height, border, (int)format, (int)pixelType, null);
            }
            else
            {
                WASMGL.GLTexImage2D(WASMGameWindow.Instance.GLContext, (int)target, level, (int)internalFormat, width, height, border, (int)format, (int)pixelType, data);
            }
        }
        public static void WASM_TexSubImage2D(TextureTarget target, int level, int x, int y, int width, int height, PixelFormat format, PixelType pixelType, Span<byte> data)
        {
            WASMGL.GLTexSubImage2D(WASMGameWindow.Instance.GLContext, (int)target, level, x, y, width, height, (int)format, (int)pixelType, data);
        }

        // ====================================================== //
        // Shaders
        // ====================================================== //
        internal static int WASM_CreateShader(ShaderType type)
        {
            var shader = WASMGL.GLCreateShader(WASMGameWindow.Instance.GLContext, (int)type);
            LastShaderId++;
            var id = LastShaderId;

            WasmShaders[id] = shader;

            return id;
        }
        internal static void WASM_DeleteShader(int id)
        {
            var shader = WASM_GetShader(id);

            WASMGL.GLDeleteShader(WASMGameWindow.Instance.GLContext, shader);

            WasmShaders.Remove(id);
        }

        internal static JSObject WASM_GetShader(int id)
        {
            if (id == 0) return null;
            WasmShaders.TryGetValue(id, out var shader);
            return shader;
        }


        // ====================================================== //
        // Shaders
        // ====================================================== //
        internal static int WASM_CreateProgram()
        {
            var program = WASMGL.GLCreateProgram(WASMGameWindow.Instance.GLContext);
            LastProgramId++;

            var id = LastProgramId;

            WasmPrograms[id] = program;

            return id;
        }
        internal static void WASM_DeleteProgram(int id)
        {
            var program = WASM_GetProgram(id);

            WASMGL.GLDeleteProgram(WASMGameWindow.Instance.GLContext, program);

            WasmPrograms.Remove(id);
        }

        internal static JSObject WASM_GetProgram(int id)
        {
            if (id == 0) return null;

            WasmPrograms.TryGetValue(id, out var program);

            return program;
        }

        internal static JSObject WASM_GetUniformLocation(JSObject program, string uniform)
        {
            return WASMGL.GLGetUniformLocation(WASMGameWindow.Instance.GLContext, program, uniform);
        }


        // ====================================================== //
        // Buffers
        // ====================================================== //
        internal static void WASM_GenBuffers(int count, [Out] out int id)
        {
            if (count != 1)
            {
                throw new NotSupportedException("At this point mutliple generation is not supported");
            }

            var buffer = WASMGL.GLCreateBuffer(WASMGameWindow.Instance.GLContext);

            LastBufferId++;
            id = LastBufferId;

            WasmBuffers[id] = buffer;
        }
        internal static void WASM_DeleteBuffers(int count, ref int id)
        {
            if (count != 1)
            {
                throw new NotSupportedException("At this point mutliple generation is not supported");
            }
            var texture = WASM_GetBuffer(id);

            WASMGL.GLDeleteBuffer(WASMGameWindow.Instance.GLContext, texture);

            WasmBuffers.Remove(id);
        }
        internal static JSObject WASM_GetBuffer(int id)
        {
            if (id == 0) return null;

            WasmBuffers.TryGetValue(id, out var buffer);
            return buffer;
        }

        // ====================================================== //
        // Misc
        // ====================================================== //
        private static T LoadFunction<T>(string function, bool throwIfNotFound = false)
            where T : System.Delegate
        {
            return default(T);
        }

        private static IGraphicsContext PlatformCreateContext (IWindowInfo info)
        {
            return new GraphicsContext(info);
        }
    }
}

