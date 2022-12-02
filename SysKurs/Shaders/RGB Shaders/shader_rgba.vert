﻿ #version 330 core
                
            uniform mat4 model;
            uniform mat4 view;
            uniform mat4 projection;

            layout (location = 0) in vec3 aPosition;
            layout (location = 1) in vec4 aColor;
            
            
            out vec4 vColor;

            void main(void)
            {
                 vColor = aColor;         
                 gl_Position = vec4(aPosition,1.0f) * model * view * projection;                   
            }
