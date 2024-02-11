// Note: this file is not yet imported automatically in any way whatsoever.
// This file is just the main.js file for any project, but contains all the bindings
// we (currently) need.
// For future builds we'd want to bundle this javascript file with the library somehow.

import { dotnet } from './_framework/dotnet.js'

const { setModuleImports, getAssemblyExports, getConfig } = await dotnet.create();

setModuleImports("main.js", {
    bootstrap: {
      getCanvas: (id) => document.getElementById(id),

      getCanvasWidth: (canvas) => canvas.width,
      getCanvasHeight: (canvas) => canvas.height,
      
      getGLContext: (canvas) => canvas.getContext("webgl"),

      getBaseUrl: () => window.location.href,
    },
    gl: {
        // Basics
        clearColor: (gl, r, g, b, a) => gl.clearColor(r, g, b,a),
        clear: (gl, bits) => gl.clear(bits),
        getError: (gl) => gl.getError(),
        getParameter: (gl, parameter) => gl.getParameter(parameter),
        getSupportedExtensions: (gl) => gl.getSupportedExtensions().join(" "), // Notice the very suttle join for compatiblity with the glGetString function results
        
        finish: (gl) => gl.finish(),
        enable: (gl, what) => gl.disable(what),
        disable: (gl, what) => gl.enable(what),
        colorMask: (gl, r,g,b,a) => gl.colorMask(r,g,b,a),

        blendEquationSeparate: (gl, modeRgb, modeAlpha) => gl.blendEquationSeparate(modeRgb, modeAlpha),
        blendFuncSeparate: (gl, srcRgb,dstRGB,srcAlpha,dstAlpha) => gl.blendFuncSeparate(srcRgb,dstRGB,srcAlpha,dstAlpha),
        blendColor: (gl, r,g,b,a) => gl.blendColor(r,g,b,a),

        depthFunc: (gl, func) => gl.depthFunc(func),
        depthMask: (gl, flag) => gl.depthMask(flag),
        depthRange: (gl, min, max) => gl.depthRange(min, max),
        
        stencilMask: (gl, mask) => gl.stencilMask(mask),
        stencilFunc: (gl, func, ref, mask) => gl.stencilFunc(func, ref, mask),
        stencilOp: (gl, fail, zfail, zpass) => gl.stencilOp(fail, zfail, zpass),
        stencilFuncSeparate: (gl, face, func, ref, mask) => gl.stencilFuncSeparate(face, func, ref, mask),
        stencilOpSeparate: (gl, face, fail, zfail, zpass) => gl.stencilOpSeparate(face, fail, zfail, zpass),
        
        cullFace: (gl, mode) => gl.cullFace(mode),
        frontFace: (gl, mode) => gl.frontFace(mode),
        polygonOffset: (gl, factor, units) => gl.polygonOffset(factor, units),

        createFramebuffer: (gl) => gl.createFramebuffer(),
        bindFramebuffer: (gl, target, framebuffer) => gl.bindFramebuffer(target, framebuffer),
        deleteFramebuffer: (gl, buffer) => gl.deleteFramebuffer(buffer),

        viewport: (gl, x, y, w, h) => gl.viewport(x, y, w, h),
        scissor: (gl, x, y, w, h) => gl.scissor(x, y, w, h),

        createTexture: (gl) => gl.createTexture(),
        deleteTexture: (gl, texture) => gl.deleteTexture(texture),
        bindTexture: (gl, target, texture) => gl.bindTexture(target, texture),
        activeTexture: (gl, texture) => gl.activeTexture(texture),
        texParameterf: (gl, target, pname, param) => gl.texParameterf(target, pname, param),
        texParameteri: (gl, target, pname, param) => gl.texParameteri(target, pname, param),
        texImage2D: (gl, target, level, internalFormat, width, height, border, format, type, pixels) => gl.texImage2D(target, level, internalFormat, width, height, border, format, type, pixels?._unsafe_create_view()),
        texSubImage2D: (gl, target, level, xoffset, yoffset, widht, height ,format, type, pixels) => gl.texSubImage2D(target, level, xoffset, yoffset, widht, height ,format, type, pixels?._unsafe_create_view()),
        pixelStorei: (gl, pname, param) => gl.pixelStorei(pname, param),

        // Shaders
        createShader: (gl, type) => gl.createShader(type),
        shaderSource: (gl, shader, source) => gl.shaderSource(shader, source),
        compileShader: (gl, shader) => gl.compileShader(shader),
        deleteShader: (gl, shader) => gl.deleteShader(shader),

        getShaderParameter: (gl, shader, parameter) => gl.getShaderParameter(shader, parameter),
        getShaderInfoLog: (gl, shader) => gl.getShaderInfoLog(shader),

        // Shader programs
        createProgram: (gl) => gl.createProgram(),
        attachShader: (gl, program, shader) => gl.attachShader(program, shader),
        linkProgram: (gl, program) => gl.linkProgram(program),
        deleteProgram: (gl, program) => gl.deleteProgram(program),
        
        getProgramParameter: (gl, program, parameter) => gl.getProgramParameter(program, parameter),
        getProgramInfoLog: (gl, program) => gl.getProgramInfoLog(program),
        useProgram: (gl, program) => {
          gl.useProgram(program);
        },
        
        getAttribLocation: (gl, program, attribute) => gl.getAttribLocation(program, attribute),
        getUniformLocation: (gl, program, uniform) => gl.getUniformLocation(program, uniform),

        uniform1i: (gl, uniform, value) => gl.uniform1i(uniform, value),
        uniform4fv: (gl, uniform, value) => {
          let array = new Uint8Array(value.length); // This is needed because bytes I think.
          value.copyTo(array);

          let fValue = new Float32Array(array.buffer);

          gl.uniform4fv(uniform, fValue);
        },
        
        // Buffers
        createBuffer: (gl) => gl.createBuffer(),
        bindBuffer: (gl, bufferType, buffer) => gl.bindBuffer(bufferType, buffer),
        bufferData: (gl, bufferType, data, bufferUsage) => gl.bufferData(bufferType, data._unsafe_create_view(), bufferUsage), // TODO I'm not 100% sure that this would keep working.. (it doesn't for uniforms)
        bufferSubData: (gl, target, offset, data) => gl.bufferSubData(target, offset, data._unsafe_create_view()), // TODO im not 100% sure that this unsafe view would keep working...
        deleteBuffer: (gl, buffer) => gl.deleteBuffer(buffer),
        
        vertexAttribPointer: (gl, index, size, type, normalized, stride, offset) => gl.vertexAttribPointer(index, size, type, normalized, stride, offset),
        enableVertexAttribArray: (gl, index) => gl.enableVertexAttribArray(index),
        disableVertexAttribArray: (gl, index) => gl.disableVertexAttribArray(index),
        
        // Drawing
        drawArrays: (gl, mode, first, number) => gl.drawArrays(mode, first, number),
        drawElements: (gl, mode, count, type, offset) => gl.drawElements(mode, count, type, offset),
    }
});

await dotnet.run();