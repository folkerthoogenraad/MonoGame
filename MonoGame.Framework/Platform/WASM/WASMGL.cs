using MonoGame.OpenAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xna.Framework.Platform.WASM
{
    internal partial class WASMGL
    {
        // =========================================== //
        // Basics
        // =========================================== //
        [JSImport("gl.clearColor", "main.js")]
        internal static partial void GLClearColor(JSObject gl, float r, float g, float b, float a);
        [JSImport("gl.clear", "main.js")]
        internal static partial void GLClear(JSObject gl, [JSMarshalAs<JSType.Number>] int bits);

        [JSImport("gl.getError", "main.js")]
        internal static partial int GLGetError(JSObject gl);

        [JSImport("gl.getSupportedExtensions", "main.js")]
        internal static partial string GLGetSupportedExtensions(JSObject gl);

        [JSImport("gl.getParameter", "main.js")]
        internal static partial int GLGetParameterInt(JSObject gl, int param);

        [JSImport("gl.getParameter", "main.js")]
        internal static partial string GLGetParameterString(JSObject gl, int param);

        [JSImport("gl.getParameter", "main.js")]
        internal static partial JSObject GLGetParameterObject(JSObject gl, int param);

        [JSImport("gl.enable", "main.js")]
        internal static partial int GLEnable(JSObject gl, int param);

        [JSImport("gl.disable", "main.js")]
        internal static partial int GLDisable(JSObject gl, int param);

        [JSImport("gl.colorMask", "main.js")]
        internal static partial int GLColorMask(JSObject gl, bool r, bool g, bool b, bool a);

        [JSImport("gl.finish", "main.js")]
        internal static partial int GLFinish(JSObject gl);

        // =========================================== //
        // Frame buffer
        // =========================================== //
        [JSImport("gl.createFramebuffer", "main.js")]
        internal static partial JSObject GLCreateFramebuffer(JSObject gl, bool r, bool g, bool b, bool a);
        [JSImport("gl.bindFramebuffer", "main.js")]
        internal static partial void GLBindFramebuffer(JSObject gl, int target, JSObject buffer);
        [JSImport("gl.deleteFramebuffer", "main.js")]
        internal static partial void GLDeleteFramebuffer(JSObject gl, JSObject buffer);

        // =========================================== //
        // Rasterizer
        // =========================================== //
        [JSImport("gl.cullFace", "main.js")]
        internal static partial int GLCullFace(JSObject gl, int mode);
        [JSImport("gl.frontFace", "main.js")]
        internal static partial int GLFrontFace(JSObject gl, int mode);
        [JSImport("gl.polygonOffset", "main.js")]
        internal static partial int GLPolygonOffset(JSObject gl, float factor, float units);

        // =========================================== //
        // Blend
        // =========================================== //
        [JSImport("gl.blendEquationSeparate", "main.js")]
        internal static partial int GLBlendEquationSeparate(JSObject gl, int a, int b);

        [JSImport("gl.blendFuncSeparate", "main.js")]
        internal static partial int GLBlendFuncSeperate(JSObject gl, int srcRgb, int dstRGB, int srcAlpha, int dstAlpha);

        [JSImport("gl.blendColor", "main.js")]
        internal static partial int GLBlendColor(JSObject gl, float r, float g, float b, float a);

        // =========================================== //
        // Depth
        // =========================================== //
        [JSImport("gl.depthMask", "main.js")]
        internal static partial int GLDepthMask(JSObject gl, bool mask);

        [JSImport("gl.depthFunc", "main.js")]
        internal static partial int GLDepthFunc(JSObject gl, int func);

        [JSImport("gl.depthRange", "main.js")]
        internal static partial int GLDepthRange(JSObject gl, float min, float max);

        // =========================================== //
        // Stencil
        // =========================================== //
        [JSImport("gl.stencilMask", "main.js")]
        internal static partial int GLStencilMask(JSObject gl, int mask);

        [JSImport("gl.stencilFunc", "main.js")]
        internal static partial int GLStencilFunc(JSObject gl, int func, int r, int mask);

        [JSImport("gl.stencilOp", "main.js")]
        internal static partial int GLStencilOp(JSObject gl, int fail, int zfail, int zpass);

        [JSImport("gl.stencilFuncSeparate", "main.js")]
        internal static partial int GLStencilFuncSeparate(JSObject gl, int face, int func, int r, int mask);

        [JSImport("gl.stencilOpSeparate", "main.js")]
        internal static partial int GLStencilOpSeparate(JSObject gl, int face, int fail, int zfail, int zpass);


        [JSImport("gl.viewport", "main.js")]
        internal static partial void GLViewport(JSObject gl, int x, int y, int width, int height);

        [JSImport("gl.scissor", "main.js")]
        internal static partial void GLScissor(JSObject gl, int x, int y, int width, int height);

        // =========================================== //
        // Textures
        // =========================================== //

        [JSImport("gl.createTexture", "main.js")]
        internal static partial JSObject GLCreateTexture(JSObject gl);

        [JSImport("gl.deleteTexture", "main.js")]
        internal static partial void GLDeleteTexture(JSObject gl, JSObject texture);

        [JSImport("gl.bindTexture", "main.js")]
        internal static partial void GLBindTexture(JSObject gl, int target, JSObject texture);

        [JSImport("gl.texParameterf", "main.js")]
        internal static partial void GLTexParameterf(JSObject gl, int target, int param, float value);

        [JSImport("gl.texParameteri", "main.js")]
        internal static partial void GLTexParameteri(JSObject gl, int target, int param, int value);

        [JSImport("gl.texImage2D", "main.js")]
        internal static partial void GLTexImage2D(JSObject gl, int target, int level, int internalFormat, int width, int height, int border, int format, int type, [JSMarshalAs<JSType.MemoryView>] Span<byte> pixels);

        [JSImport("gl.texImage2D", "main.js")]
        internal static partial void GLTexImage2D_null(JSObject gl, int target, int level, int internalFormat, int width, int height, int border, int format, int type, JSObject nullable);

        [JSImport("gl.texSubImage2D", "main.js")]
        internal static partial void GLTexSubImage2D(JSObject gl, int target, int level, int xoffset, int yoffset, int width, int height, int format, int type, [JSMarshalAs<JSType.MemoryView>] Span<byte> pixels);

        [JSImport("gl.pixelStorei", "main.js")]
        internal static partial void GLPixelStorei(JSObject gl, int pname, int param);

        [JSImport("gl.activeTexture", "main.js")]
        internal static partial void GLActiveTexture(JSObject gl, int texture);

        // =========================================== //
        // Shaders
        // =========================================== //

        [JSImport("gl.createShader", "main.js")]
        internal static partial JSObject GLCreateShader(JSObject gl, int bits);

        [JSImport("gl.shaderSource", "main.js")]
        internal static partial void GLShaderSource(JSObject gl, JSObject shader, string source);

        [JSImport("gl.compileShader", "main.js")]
        internal static partial void GLCompileShader(JSObject gl, JSObject shader);

        [JSImport("gl.deleteShader", "main.js")]
        internal static partial void GLDeleteShader(JSObject gl, JSObject shader);

        [JSImport("gl.getShaderParameter", "main.js")]
        internal static partial bool GLGetShaderParameter(JSObject gl, JSObject shader, int parameter);

        [JSImport("gl.getShaderInfoLog", "main.js")]
        internal static partial string GLGetShaderInfoLog(JSObject gl, JSObject shader);

        // =========================================== //
        // Shader Programs
        // =========================================== //
        [JSImport("gl.createProgram", "main.js")]
        internal static partial JSObject GLCreateProgram(JSObject gl);

        [JSImport("gl.attachShader", "main.js")]
        internal static partial void GLAttachShader(JSObject gl, JSObject program, JSObject shader);

        [JSImport("gl.linkProgram", "main.js")]
        internal static partial void GLLinkProgram(JSObject gl, JSObject program);

        [JSImport("gl.deleteProgram", "main.js")]
        internal static partial void GLDeleteProgram(JSObject gl, JSObject program);

        [JSImport("gl.getProgramParameter", "main.js")]
        internal static partial bool GLGetProgramParameter(JSObject gl, JSObject program, int parameter);

        [JSImport("gl.getProgramInfoLog", "main.js")]
        internal static partial string GLGetProgramInfoLog(JSObject gl, JSObject program);

        [JSImport("gl.useProgram", "main.js")]
        internal static partial void GLUseProgram(JSObject gl, JSObject program);

        [JSImport("gl.getAttribLocation", "main.js")]
        internal static partial int GLGetAttribLocation(JSObject gl, JSObject program, string attribute);

        [JSImport("gl.getUniformLocation", "main.js")]
        internal static partial JSObject GLGetUniformLocation(JSObject gl, JSObject program, string uniform);

        [JSImport("gl.uniform1i", "main.js")]
        internal static partial JSObject GLUniform1i(JSObject gl, JSObject location, int value);

        [JSImport("gl.uniform4fv", "main.js")]
        internal static partial JSObject GLUniform4fv(JSObject gl, JSObject location, [JSMarshalAs<JSType.MemoryView>] Span<byte> data);

        // =========================================== //
        // Buffers
        // =========================================== //
        [JSImport("gl.createBuffer", "main.js")]
        internal static partial JSObject GLCreateBuffer(JSObject gl);
        [JSImport("gl.deleteBuffer", "main.js")]
        internal static partial JSObject GLDeleteBuffer(JSObject gl, JSObject buffer);
        [JSImport("gl.bindBuffer", "main.js")]
        internal static partial void GLBindBuffer(JSObject gl, int type, JSObject buffer);
        [JSImport("gl.bufferData", "main.js")]
        internal static partial void GLBufferData(JSObject gl, int type, [JSMarshalAs<JSType.MemoryView>] Span<byte> data, int drawType);

        [JSImport("gl.bufferSubData", "main.js")]
        internal static partial void GLBufferSubData(JSObject gl, int type, int offset, [JSMarshalAs<JSType.MemoryView>] Span<byte> data);

        [JSImport("gl.vertexAttribPointer", "main.js")]
        internal static partial void GLVertexAttribPointer(JSObject gl, int index, int size, int type, bool normalized, int stride, int offset);

        [JSImport("gl.enableVertexAttribArray", "main.js")]
        internal static partial void GLEnableVertexAttribArray(JSObject gl, int index);

        [JSImport("gl.disableVertexAttribArray", "main.js")]
        internal static partial void GLDisableVertexAttribArray(JSObject gl, int index);


        // =========================================== //
        // Drawing
        // =========================================== //
        [JSImport("gl.drawArrays", "main.js")]
        internal static partial void GLDrawArrays(JSObject gl, int mode, int first, int number);

        [JSImport("gl.drawElements", "main.js")]
        internal static partial void GLDrawElements(JSObject gl, int mode, int count, int type, int offset);
    }
}
