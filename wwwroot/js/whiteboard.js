window.whiteboardInterop = {
    canvasContext: null,
    tempCanvas: null,
    tempContext: null,
    permanentCanvas: null,
    permanentContext: null,
    showUserLabels: true,
    cursors: new Map(), // Store cursor positions by connectionId
    laserPoints: new Map(), // Store laser points by connectionId
    laserTrails: new Map(), // Store laser trail history by connectionId
    remotePreviews: new Map(), // Store remote user previews by connectionId
    currentUserName: null,
    currentUserColor: null,

    initialize: function(canvas) {
        if (canvas) {
            const ctx = canvas.getContext('2d');
            this.canvasContext = ctx;
            
            // Create temporary canvas for shape preview
            this.tempCanvas = document.createElement('canvas');
            this.tempCanvas.width = canvas.width;
            this.tempCanvas.height = canvas.height;
            this.tempContext = this.tempCanvas.getContext('2d');
            
            // Create permanent canvas to store all drawings (without laser)
            this.permanentCanvas = document.createElement('canvas');
            this.permanentCanvas.width = canvas.width;
            this.permanentCanvas.height = canvas.height;
            this.permanentContext = this.permanentCanvas.getContext('2d');
            
            // Set initial canvas properties
            ctx.lineCap = 'round';
            ctx.lineJoin = 'round';
            
            // Fill with white background
            ctx.fillStyle = '#FFFFFF';
            ctx.fillRect(0, 0, canvas.width, canvas.height);
            
            // Initialize permanent canvas with white background
            this.permanentContext.fillStyle = '#FFFFFF';
            this.permanentContext.fillRect(0, 0, this.permanentCanvas.width, this.permanentCanvas.height);

            // Start rendering loop
            this.startRenderingLoop(canvas);
        }
    },

    setCurrentUser: function(userName, userColor) {
        this.currentUserName = userName;
        this.currentUserColor = userColor;
    },

    // Start rendering loop for cursors and laser pointers
    startRenderingLoop: function(canvas) {
        const render = () => {
            // Restore permanent canvas (all finalized drawings)
            const ctx = canvas.getContext('2d');
            ctx.clearRect(0, 0, canvas.width, canvas.height);
            ctx.drawImage(this.permanentCanvas, 0, 0);
            
            // Draw local temporary canvas (for local shape preview)
            if (this.tempCanvas) {
                ctx.drawImage(this.tempCanvas, 0, 0);
            }
            
            // Draw all remote previews
            this.drawAllRemotePreviews(ctx);
            
            // Draw all laser points
            this.drawAllLaserPoints(ctx);
            
            // Draw all cursors on top
            this.drawAllCursors(ctx);
            
            requestAnimationFrame(render);
        };
        requestAnimationFrame(render);
    },

    // Update cursor position for a user
    updateCursor: function(canvas, connectionId, x, y, userName, userColor) {
        this.cursors.set(connectionId, {
            x: x,
            y: y,
            userName: userName,
            userColor: userColor,
            lastUpdate: Date.now()
        });
    },

    // Remove cursor when user disconnects or stops drawing
    removeCursor: function(connectionId) {
        this.cursors.delete(connectionId);
    },

    // Draw all active cursors
    drawAllCursors: function(ctx) {
        if (!ctx) return;
        
        const now = Date.now();
        
        // Remove stale cursors (not updated in last 2 seconds)
        for (const [connectionId, cursor] of this.cursors.entries()) {
            if (now - cursor.lastUpdate > 2000) {
                this.cursors.delete(connectionId);
            }
        }

        // Draw each cursor
        for (const [connectionId, cursor] of this.cursors.entries()) {
            this.drawCursor(ctx, cursor.x, cursor.y, cursor.userName, cursor.userColor);
        }
    },

    // Draw all remote user previews
    drawAllRemotePreviews: function(ctx) {
        if (!ctx) return;
        
        const now = Date.now();
        
        // Remove stale previews (not updated in last 2 seconds)
        for (const [connectionId, preview] of this.remotePreviews.entries()) {
            if (now - preview.lastUpdate > 2000) {
                this.remotePreviews.delete(connectionId);
            }
        }

        // Draw each remote preview
        for (const [connectionId, preview] of this.remotePreviews.entries()) {
            this._drawShapeInternal(
                ctx,
                preview.tool,
                preview.startX,
                preview.startY,
                preview.endX,
                preview.endY,
                preview.color,
                preview.lineWidth,
                preview.filled,
                preview.userName,
                preview.userColor
            );
        }
    },

    // Store a remote preview for a specific connection
    storeRemotePreview: function(connectionId, tool, startX, startY, endX, endY, color, lineWidth, filled, userName, userColor) {
        this.remotePreviews.set(connectionId, {
            tool: tool,
            startX: startX,
            startY: startY,
            endX: endX,
            endY: endY,
            color: color,
            lineWidth: lineWidth,
            filled: filled,
            userName: userName,
            userColor: userColor,
            lastUpdate: Date.now()
        });
    },

    // Clear remote preview for a specific connection
    clearRemotePreview: function(connectionId) {
        this.remotePreviews.delete(connectionId);
    },

    // Draw a single cursor pointer
    drawCursor: function(ctx, x, y, userName, userColor) {
        ctx.save();
        
        // Draw realistic mouse cursor icon
        // Main cursor body (white with black outline like a real cursor)
        ctx.shadowColor = 'rgba(0, 0, 0, 0.4)';
        ctx.shadowBlur = 4;
        ctx.shadowOffsetX = 1;
        ctx.shadowOffsetY = 1;
        
        // Draw cursor shape (classic arrow pointer)
        ctx.beginPath();
        ctx.moveTo(x, y);
        ctx.lineTo(x, y + 18);
        ctx.lineTo(x + 4, y + 14);
        ctx.lineTo(x + 7, y + 20);
        ctx.lineTo(x + 9, y + 19);
        ctx.lineTo(x + 6, y + 13);
        ctx.lineTo(x + 11, y + 13);
        ctx.closePath();
        
        // Fill with white
        ctx.fillStyle = '#FFFFFF';
        ctx.fill();
        
        // Black outline
        ctx.strokeStyle = '#000000';
        ctx.lineWidth = 1.5;
        ctx.stroke();
        
        // Add user color accent at the tip
        ctx.beginPath();
        ctx.arc(x + 2, y + 2, 3, 0, 2 * Math.PI);
        ctx.fillStyle = userColor || '#6c63ff';
        ctx.fill();
        ctx.strokeStyle = '#FFFFFF';
        ctx.lineWidth = 1;
        ctx.stroke();
        
        // Draw user label next to cursor
        if (userName) {
            ctx.shadowColor = 'rgba(0, 0, 0, 0.3)';
            ctx.shadowBlur = 4;
            ctx.shadowOffsetX = 1;
            ctx.shadowOffsetY = 1;
            
            ctx.font = 'bold 12px Arial';
            const text = userName;
            const textMetrics = ctx.measureText(text);
            const padding = 5;
            
            const bgX = x + 14;
            const bgY = y - 8;
            const bgWidth = textMetrics.width + padding * 2;
            const bgHeight = 18;
            
            // Rounded background with user color
            ctx.fillStyle = userColor || '#6c63ff';
            ctx.globalAlpha = 0.95;
            this.roundRect(ctx, bgX, bgY, bgWidth, bgHeight, 3);
            ctx.fill();
            
            // Text
            ctx.shadowColor = 'rgba(0, 0, 0, 0.5)';
            ctx.shadowBlur = 2;
            ctx.fillStyle = '#ffffff';
            ctx.globalAlpha = 1;
            ctx.fillText(text, bgX + padding, bgY + 13);
        }
        
        ctx.restore();
    },

    // Helper function for rounded rectangles
    roundRect: function(ctx, x, y, width, height, radius) {
        ctx.beginPath();
        ctx.moveTo(x + radius, y);
        ctx.lineTo(x + width - radius, y);
        ctx.quadraticCurveTo(x + width, y, x + width, y + radius);
        ctx.lineTo(x + width, y + height - radius);
        ctx.quadraticCurveTo(x + width, y + height, x + width - radius, y + height);
        ctx.lineTo(x + radius, y + height);
        ctx.quadraticCurveTo(x, y + height, x, y + height - radius);
        ctx.lineTo(x, y + radius);
        ctx.quadraticCurveTo(x, y, x + radius, y);
        ctx.closePath();
    },

    // Update laser point for a user
    updateLaserPoint: function(connectionId, x, y, color, userName, userColor) {
        this.laserPoints.set(connectionId, {
            x: x,
            y: y,
            color: color,
            userName: userName,
            userColor: userColor,
            lastUpdate: Date.now()
        });
        
        // Add to trail history
        if (!this.laserTrails.has(connectionId)) {
            this.laserTrails.set(connectionId, []);
        }
        
        const trail = this.laserTrails.get(connectionId);
        trail.push({
            x: x,
            y: y,
            timestamp: Date.now()
        });
        
        // Keep only last 15 points (about 150ms of trail at 100ms updates)
        if (trail.length > 15) {
            trail.shift();
        }
    },

    // Remove laser point for a user
    removeLaserPoint: function(connectionId) {
        this.laserPoints.delete(connectionId);
        this.laserTrails.delete(connectionId);
    },

    // Draw all active laser points
    drawAllLaserPoints: function(ctx) {
        if (!ctx) return;
        
        const now = Date.now();
        
        // Remove stale laser points (not updated in last 1 second)
        for (const [connectionId, laser] of this.laserPoints.entries()) {
            if (now - laser.lastUpdate > 1000) {
                this.laserPoints.delete(connectionId);
                this.laserTrails.delete(connectionId);
            }
        }

        // Draw each laser trail and point
        for (const [connectionId, laser] of this.laserPoints.entries()) {
            // Draw trail first
            const trail = this.laserTrails.get(connectionId);
            if (trail && trail.length > 1) {
                this.drawLaserTrail(ctx, trail, laser.color);
            }
            
            // Draw main laser point on top
            this.drawSingleLaserPoint(ctx, laser.x, laser.y, laser.color, laser.userName, laser.userColor);
        }
    },

    // Draw the laser trail (subtle line behind the laser cursor)
    drawLaserTrail: function(ctx, trail, color) {
        if (!trail || trail.length < 2) return;
        
        ctx.save();
        
        const now = Date.now();
        const maxAge = 500; // Trail fades over 500ms
        
        // Draw trail as connected line segments with fading opacity
        for (let i = 0; i < trail.length - 1; i++) {
            const point1 = trail[i];
            const point2 = trail[i + 1];
            
            // Calculate age-based opacity
            const age = now - point2.timestamp;
            const opacity = Math.max(0, 1 - (age / maxAge));
            
            if (opacity > 0) {
                ctx.beginPath();
                ctx.moveTo(point1.x, point1.y);
                ctx.lineTo(point2.x, point2.y);
                
                // Simple gradient stroke
                const gradient = ctx.createLinearGradient(point1.x, point1.y, point2.x, point2.y);
                const startOpacity = i === 0 ? opacity * 0.4 : opacity * 0.6;
                const endOpacity = opacity * 0.6;
                
                gradient.addColorStop(0, this.hexToRgba(color, startOpacity));
                gradient.addColorStop(1, this.hexToRgba(color, endOpacity));
                
                ctx.strokeStyle = gradient;
                ctx.lineWidth = 2 + (opacity * 2); // Thinner trail
                ctx.lineCap = 'round';
                ctx.lineJoin = 'round';
                ctx.stroke();
                
                // Subtle glow effect
                ctx.shadowColor = color;
                ctx.shadowBlur = 5 * opacity;
                ctx.stroke();
            }
        }
        
        ctx.restore();
    },

    // Helper function to convert hex color to rgba
    hexToRgba: function(hex, alpha) {
        // Handle hex colors with or without #
        hex = hex.replace('#', '');
        
        const r = parseInt(hex.substring(0, 2), 16);
        const g = parseInt(hex.substring(2, 4), 16);
        const b = parseInt(hex.substring(4, 6), 16);
        
        return `rgba(${r}, ${g}, ${b}, ${alpha})`;
    },

    // Draw a single laser point with cursor icon (instead of glow)
    drawSingleLaserPoint: function(ctx, x, y, color, userName, userColor) {
        ctx.save();
        
        // Draw realistic mouse cursor icon (same as regular cursor)
        ctx.shadowColor = 'rgba(0, 0, 0, 0.4)';
        ctx.shadowBlur = 4;
        ctx.shadowOffsetX = 1;
        ctx.shadowOffsetY = 1;
        
        // Draw cursor shape (classic arrow pointer)
        ctx.beginPath();
        ctx.moveTo(x, y);
        ctx.lineTo(x, y + 18);
        ctx.lineTo(x + 4, y + 14);
        ctx.lineTo(x + 7, y + 20);
        ctx.lineTo(x + 9, y + 19);
        ctx.lineTo(x + 6, y + 13);
        ctx.lineTo(x + 11, y + 13);
        ctx.closePath();
        
        // Fill with white
        ctx.fillStyle = '#FFFFFF';
        ctx.fill();
        
        // Black outline
        ctx.strokeStyle = '#000000';
        ctx.lineWidth = 1.5;
        ctx.stroke();
        
        // Add laser color accent at the tip (larger and brighter for laser)
        ctx.beginPath();
        ctx.arc(x + 2, y + 2, 4, 0, 2 * Math.PI);
        ctx.fillStyle = color || '#FF0000';
        ctx.fill();
        ctx.strokeStyle = '#FFFFFF';
        ctx.lineWidth = 1;
        ctx.stroke();
        
        // Add glow effect around the colored tip
        ctx.beginPath();
        ctx.arc(x + 2, y + 2, 8, 0, 2 * Math.PI);
        ctx.fillStyle = this.hexToRgba(color, 0.3);
        ctx.fill();
        
        // Draw user name label next to cursor
        if (userName) {
            ctx.shadowColor = 'rgba(0, 0, 0, 0.3)';
            ctx.shadowBlur = 4;
            ctx.shadowOffsetX = 1;
            ctx.shadowOffsetY = 1;
            
            ctx.font = 'bold 12px Arial';
            const textMetrics = ctx.measureText(userName);
            const padding = 5;
            
            const bgX = x + 14;
            const bgY = y - 8;
            const bgWidth = textMetrics.width + padding * 2;
            const bgHeight = 18;
            
            // Rounded background with user color
            ctx.fillStyle = userColor || '#6c63ff';
            ctx.globalAlpha = 0.95;
            this.roundRect(ctx, bgX, bgY, bgWidth, bgHeight, 3);
            ctx.fill();
            
            // Text
            ctx.shadowColor = 'rgba(0, 0, 0, 0.5)';
            ctx.shadowBlur = 2;
            ctx.fillStyle = '#ffffff';
            ctx.globalAlpha = 1;
            ctx.fillText(userName, bgX + padding, bgY + 13);
        }
        
        ctx.restore();
    },

    // Get correct mouse/touch position relative to canvas
    getCanvasCoordinates: function(canvas, clientX, clientY) {
        const rect = canvas.getBoundingClientRect();
        const scaleX = canvas.width / rect.width;
        const scaleY = canvas.height / rect.height;
        
        return {
            x: (clientX - rect.left) * scaleX,
            y: (clientY - rect.top) * scaleY
        };
    },

    // Draw user label near the drawing
    drawUserLabel: function(ctx, x, y, userName, userColor) {
        if (!userName || !this.showUserLabels) return;

        ctx.save();
        
        // Add shadow for depth
        ctx.shadowColor = 'rgba(0, 0, 0, 0.25)';
        ctx.shadowBlur = 4;
        ctx.shadowOffsetX = 1;
        ctx.shadowOffsetY = 1;
        
        // Set font
        ctx.font = 'bold 13px Arial';
        const text = userName;
        const textMetrics = ctx.measureText(text);
        const padding = 6;
        
        // Draw rounded background
        const bgX = x + 12;
        const bgY = y - 24;
        const bgWidth = textMetrics.width + padding * 2;
        const bgHeight = 20;
        
        ctx.fillStyle = userColor || '#6c63ff';
        ctx.globalAlpha = 0.92;
        this.roundRect(ctx, bgX, bgY, bgWidth, bgHeight, 4);
        ctx.fill();
        
        // Draw text
        ctx.shadowColor = 'rgba(0, 0, 0, 0.4)';
        ctx.shadowBlur = 2;
        ctx.fillStyle = '#ffffff';
        ctx.globalAlpha = 1;
        ctx.fillText(text, bgX + padding, bgY + 14);
        
        ctx.restore();
    },

    drawLine: function(canvas, prevX, prevY, x, y, color, lineWidth, userName, userColor) {
        if (canvas) {
            const ctx = canvas.getContext('2d');
            ctx.beginPath();
            ctx.moveTo(prevX, prevY);
            ctx.lineTo(x, y);
            ctx.strokeStyle = color;
            ctx.lineWidth = lineWidth;
            ctx.lineCap = 'round';
            ctx.lineJoin = 'round';
            ctx.stroke();
            
            // Draw user label occasionally for remote users (5% of the time)
            if (Math.random() < 0.05 && userName && this.showUserLabels) {
                this.drawUserLabel(ctx, x, y, userName, userColor);
            }
            
            // Also draw on permanent canvas (without labels - labels are temporary)
            if (this.permanentContext) {
                this.permanentContext.beginPath();
                this.permanentContext.moveTo(prevX, prevY);
                this.permanentContext.lineTo(x, y);
                this.permanentContext.strokeStyle = color;
                this.permanentContext.lineWidth = lineWidth;
                this.permanentContext.lineCap = 'round';
                this.permanentContext.lineJoin = 'round';
                this.permanentContext.stroke();
            }
        }
    },

    // Draw shape on specific context (internal helper)
    _drawShapeInternal: function(ctx, tool, startX, startY, endX, endY, color, lineWidth, filled, userName, userColor) {
        if (!ctx) return;
        
        ctx.strokeStyle = color;
        ctx.fillStyle = color;
        ctx.lineWidth = lineWidth;
        ctx.lineCap = 'round';
        ctx.lineJoin = 'round';

        switch(tool) {
            case 'rectangle':
                const width = endX - startX;
                const height = endY - startY;
                if (filled) {
                    ctx.fillRect(startX, startY, width, height);
                } else {
                    ctx.strokeRect(startX, startY, width, height);
                }
                break;

            case 'circle':
                const radius = Math.sqrt(Math.pow(endX - startX, 2) + Math.pow(endY - startY, 2));
                ctx.beginPath();
                ctx.arc(startX, startY, radius, 0, 2 * Math.PI);
                if (filled) ctx.fill(); else ctx.stroke();
                break;

            case 'ellipse':
                const radiusX = Math.abs(endX - startX);
                const radiusY = Math.abs(endY - startY);
                ctx.beginPath();
                ctx.ellipse(startX, startY, radiusX, radiusY, 0, 0, 2 * Math.PI);
                if (filled) ctx.fill(); else ctx.stroke();
                break;

            case 'line':
                ctx.beginPath();
                ctx.moveTo(startX, startY);
                ctx.lineTo(endX, endY);
                ctx.stroke();
                break;

            case 'arrow':
                this.drawArrow(ctx, startX, startY, endX, endY, lineWidth);
                break;

            case 'triangle':
                const centerX = (startX + endX) / 2;
                ctx.beginPath();
                ctx.moveTo(centerX, startY);
                ctx.lineTo(startX, endY);
                ctx.lineTo(endX, endY);
                ctx.closePath();
                if (filled) {
                    ctx.fill();
                } else {
                    ctx.stroke();
                }
                break;
        }
        
        // Draw user label for shapes
        if (userName && this.showUserLabels) {
            this.drawUserLabel(ctx, endX, endY, userName, userColor);
        }
    },

    // Draw shape for preview (on temp canvas, will be shown by rendering loop)
    drawShapePreview: function(canvas, tool, startX, startY, endX, endY, color, lineWidth, filled, userName, userColor) {
        if (!canvas || !this.tempCanvas) return;
        
        // Clear temp canvas before drawing new preview to prevent accumulation
        this.tempContext.clearRect(0, 0, this.tempCanvas.width, this.tempCanvas.height);
        
        // Draw preview on temp canvas (will be composited by render loop)
        this._drawShapeInternal(this.tempContext, tool, startX, startY, endX, endY, color, lineWidth, filled, userName, userColor);
    },

    // Draw finalized shape (on both canvases)
    drawShape: function(canvas, tool, startX, startY, endX, endY, color, lineWidth, filled, userName, userColor) {
        if (!canvas) return;
        
        // Draw on main canvas
        const ctx = canvas.getContext('2d');
        this._drawShapeInternal(ctx, tool, startX, startY, endX, endY, color, lineWidth, filled, userName, userColor);
        
        // Also draw on permanent canvas (without user label)
        if (this.permanentContext) {
            this._drawShapeInternal(this.permanentContext, tool, startX, startY, endX, endY, color, lineWidth, filled, null, null);
        }
    },

    drawArrow: function(ctx, fromX, fromY, toX, toY, lineWidth) {
        const headLength = Math.max(10, lineWidth * 3);
        const angle = Math.atan2(toY - fromY, toX - fromX);

        // Draw line
        ctx.beginPath();
        ctx.moveTo(fromX, fromY);
        ctx.lineTo(toX, toY);
        ctx.stroke();

        // Draw arrowhead
        ctx.beginPath();
        ctx.moveTo(toX, toY);
        ctx.lineTo(
            toX - headLength * Math.cos(angle - Math.PI / 6),
            toY - headLength * Math.sin(angle - Math.PI / 6)
        );
        ctx.moveTo(toX, toY);
        ctx.lineTo(
            toX - headLength * Math.cos(angle + Math.PI / 6),
            toY - headLength * Math.sin(angle + Math.PI / 6)
        );
        ctx.stroke();
    },

    drawText: function(canvas, text, x, y, color, fontSize) {
        if (canvas && text) {
            const ctx = canvas.getContext('2d');
            ctx.font = `${fontSize}px Arial`;
            ctx.fillStyle = color;
            ctx.fillText(text, x, y);
            
            // Also draw on permanent canvas
            if (this.permanentContext) {
                this.permanentContext.font = `${fontSize}px Arial`;
                this.permanentContext.fillStyle = color;
                this.permanentContext.fillText(text, x, y);
            }
        }
    },

    // Draw laser point - update the laser point in the map
    drawLaserPoint: function(canvas, x, y, color, userName, userColor, connectionId) {
        // Use special ID for local user if not provided
        const id = connectionId || 'local';
        this.updateLaserPoint(id, x, y, color, userName, userColor);
    },

    clearLaserPoints: function(canvas) {
        // Clear all laser points and trails
        this.laserPoints.clear();
        this.laserTrails.clear();
    },

    clearCanvas: function(canvas) {
        if (canvas) {
            const ctx = canvas.getContext('2d');
            ctx.fillStyle = '#FFFFFF';
            ctx.fillRect(0, 0, canvas.width, canvas.height);
            
            // Also clear permanent canvas
            if (this.permanentContext) {
                this.permanentContext.fillStyle = '#FFFFFF';
                this.permanentContext.fillRect(0, 0, this.permanentCanvas.width, this.permanentCanvas.height);
            }
        }
    },

    getBoundingRect: function(canvas) {
        if (canvas) {
            const rect = canvas.getBoundingClientRect();
            const scaleX = canvas.width / rect.width;
            const scaleY = canvas.height / rect.height;
            
            return {
                left: rect.left,
                top: rect.top,
                scaleX: scaleX,
                scaleY: scaleY
            };
        }
        return { left: 0, top: 0, scaleX: 1, scaleY: 1 };
    },

    // Save current permanent canvas state (for shape preview)
    saveCanvasState: function(canvas) {
        // No-op - the rendering loop handles displaying permanent + temp canvas
        // We just need to make sure temp canvas is clear when starting a new shape
        if (this.tempCanvas) {
            this.tempContext.clearRect(0, 0, this.tempCanvas.width, this.tempCanvas.height);
        }
    },

    // Restore canvas state for preview (clear temp canvas between previews)
    restoreCanvasState: function(canvas) {
        // Clear temp canvas - the rendering loop will composite permanent + temp + remote previews
        if (this.tempCanvas) {
            this.tempContext.clearRect(0, 0, this.tempCanvas.width, this.tempCanvas.height);
        }
    },

    // Toggle user labels
    toggleUserLabels: function(show) {
        this.showUserLabels = show;
    }
};
