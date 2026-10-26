'Imports OpenTK.Graphics.OpenGL
Imports OpenTK.Graphics.OpenGL4
Imports OpenTK.GLControl
Imports OpenTK.Windowing.Common
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Runtime.InteropServices.JavaScript.JSType
Imports System.Runtime.Versioning
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.Drawing.Imaging
Imports System.Text
Imports MetadataExtractor
Imports MetadataExtractor.Formats.Exif
Imports OpenTK
'Imports System.Windows.Media.Imaging
'Imports System.Windows.Documents
Imports System.ComponentModel
Imports OpenTK.Mathematics
Imports System.Buffers.Text
Imports OpenTK.Compute.OpenCL
Imports OpenTK.Audio.OpenAL
Imports System.Windows.Documents
Imports MetadataExtractor.Formats
'Imports System.Windows.Input
'Imports System.Windows.Shapes


Public Class F_Stop

    ' 1. Ensure these are at the top of your Class
    <DllImport("dwmapi.dll")>
    Private Shared Function DwmSetWindowAttribute(hwnd As IntPtr, attr As Integer, ByRef attrValue As Integer, attrSize As Integer) As Integer
    End Function
    Private Sub ApplyDarkModeTitle()
        Try
            Dim darkMode As Integer = 1 ' 1 = True, 0 = False

            ' Attribute 20 is for Windows 11 and Windows 10 (Build 18985+)
            ' Attribute 19 was used in older Windows 10 versions
            Dim result As Integer = DwmSetWindowAttribute(Me.Handle, 20, darkMode, Marshal.SizeOf(darkMode))

            If result <> 0 Then
                ' If 20 failed, try 19
                DwmSetWindowAttribute(Me.Handle, 19, darkMode, Marshal.SizeOf(darkMode))
            End If

            ' FORCE the window to redraw its non-client area (the title bar)
            Me.Padding = New Padding(Me.Padding.Left, Me.Padding.Top, Me.Padding.Right, Me.Padding.Bottom)

        Catch ex As Exception
            ' Fail silently if OS doesn't support it
        End Try
    End Sub


    Private loadedFiles As New HashSet(Of String)
    Private loadedImage As Bitmap
    Private currentZoom As Double = 1.0F
    Private panX As Double = 0
    Private panY As Double = 0
    'Private GlControl2 As GLControl
    Private currentImage As Bitmap = Nothing
    Private zoomFactor As Double = 1.0
    Private vao As Integer
    Private vbo As Integer
    Private selectedThumb As Panel = Nothing
    Private isUpdatingUI As Boolean = False
    Private isRawFile As Boolean = False
    Private currentFilePath As String = ""
    Private baseGpuText As String = ""
    Private mainShader As ShaderProgram
    Private screenShader As ShaderProgram
    Private textShader As ShaderProgram
    Private CurrentParams As New RenderParams()
    Private textureID As Integer = 0
    Private imageWidth As Integer = 1
    Private imageHeight As Integer = 1
    Private useLut As Boolean = False
    Private lutTextureID As Integer = 0
    Private isShowingOriginal As Boolean = True
    Private sliderLookup As New Dictionary(Of String, ModernSlider)
    Private isPanning As Boolean = False
    Private lastMousePos As Point
    Private _isInitializing As Boolean = True
    Private hist(255) As Integer
    'Private histMax As Integer = 0
    Private showShadowClipping As Boolean = False
    Private showHighlightClipping As Boolean = False
    Private myShader As ShaderProgram
    Private vl_check_for_change As Boolean = False

    Private offsetX As Single = 0.0F
    Private offsetY As Single = 0.0F
    ' Locations for the shader uniforms
    Private uOffsetLocation As Integer
    Private uZoomLocation As Integer
    Private originalTextureID As Integer = -1 ' Stores the unedited, uncropped pixels
    Private origW As Integer = 0
    Private origH As Integer = 0


    Private isSelectingCrop As Boolean = False
    Private cropRect As RectangleF = New RectangleF(0.1, 0.1, 0.8, 0.8) ' Normalized 0.0 to 1.0
    Private cropHandles(7) As RectangleF ' 8 handles: 4 corners, 4 sides
    Private activeHandle As Integer = -1 ' -1: none, 0-3: corners, 4-7: sides
    Private Const HANDLE_SIZE As Integer = 8
    Private cropGrabOffset As PointF
    Private isBatchExport As Boolean = False


    Private lastImageBeforeCrop As Bitmap = Nothing
    Private stripFilePaths As New List(Of String)() ' for Catalog save
    Private currentImagePath As String = "" ' Update this whenever an image is loaded or selected
    Private _isInternalUpdating As Boolean = False
    Private totalLibraryItems As Integer = 0
    Private Pic_No_Changes As Boolean = True
    Private IsCropApplied As Boolean = False
    Private lastRightClickedThumb As Panel = Nothing
    Private clickedThumbnail As Panel = Nothing

    ' 0 = RGB (All), 1 = Luma (Sun), 2 = Red, 3 = Green, 4 = Blue
    Private histMode As Integer = 0

    'For drawing text into viewport
    ' --- Keep these ones ---
    Private _textTextureID As Integer = -1      ' The GPU "slot" number
    Private textPos_txt As New PointF(0.5F, 0.5F) ' Where the text sits (0.0 to 1.0)
    Private currentFont As New Font("Arial", 24, FontStyle.Regular)
    Private currentColor As Color = Color.White   ' The color from your dialog
    Private textColor As Color = Color.White ' Use 'textColor' since your sub uses it
    Private isWatermarkMoving As Boolean = False
    Private watermarkRefPt As Point  ' Dedicated specifically to text movement
    Private useText As Boolean = False

    Private Save_All_PicText As Boolean = False
    Private First_Load As Integer = 0

    'for replace color correction
    ' Values to be sent to the shader
    Private targetCol As New Vector3(0, 0, 0)
    Private replaceCol As New Vector3(0, 0, 0)
    Private colorRadius As Single = 0.0F ' maybe not in use
    Private colorSmoothness As Single = 0.01F
    Private pinnedHEXA As String
    Private selectionU As Single
    Private selectionV As Single
    Private selectionRange As Single = 0.1F
    Private pos_for_colors As PointF ' Stores the exact U,V where the right-click happened

    'addidtional buffer
    Private fboHUD As UInteger = 0
    Private fboTexture As UInteger = 0
    Private rboDepth As UInteger = 0
    Private screenShaderProgram As Integer = 0
    Private vboHUD As Integer = 0
    Private vaoHUD As Integer = 0

    Public fboPhoto, fboTextBuffer As Integer
    Public fboTexture1, fboTexture2 As Integer


    ' Private Declare Function ShowScrollBar Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal wBar As Integer, ByVal bShow As Boolean) As Integer
    ' Private Const SB_VERT As Integer = 1
    ' Private Const SB_HORZ As Integer = 0
    ' Private Const SB_BOTH As Integer = 3
    Private galleryMonitor As ScrollHider

    Dim uIsCropping As Integer = 0
    Dim currentIsCroppingState As Integer = 1 ' Default to "Marking"

    ' Histogram arrays
    Private histR(255) As Integer
    Private histG(255) As Integer
    Private histB(255) As Integer
    'Private histLuma(255) As Integer
    Private histL(255) As Integer ' This is the Luma (L) array
    Private histMax As Integer = 1

    ' Max values for scaling
    Private histMaxR As Integer = 1
    Private histMaxG As Integer = 1
    Private histMaxB As Integer = 1
    Private histMaxL As Integer = 1
    Private valBlack As Integer = 0
    Private valWhite As Integer = 255

    Private selR As Single = 1.0F ' Default White
    Private selG As Single = 1.0F
    Private selB As Single = 1.0F


    ' Shader uniform locations
    Private uExposureLoc As Integer
    Private uContrastLoc As Integer
    Private uHighlightsLoc As Integer
    Private uShadowsLiftLoc As Integer
    Private uWhitesLoc As Integer
    Private uBlacksLoc As Integer
    Private uTempLoc As Integer
    Private uTintLoc As Integer
    Private uDynConLoc As Integer
    Private uClarityLoc As Integer
    Private uStructXLoc As Integer
    Private uSharpenLoc As Integer
    Private uDefringeLoc As Integer
    Private uDenoiseLoc As Integer
    Private uNoiseLimitLoc As Integer
    Private uNoiseChromaLoc As Integer
    Private uNoiseLumaLoc As Integer
    Private uSaturationLoc As Integer
    Private uVibranceLoc As Integer
    Private uRedMixLoc As Integer
    Private uGreenMixLoc As Integer
    Private uBlueMixLoc As Integer
    Private uLutIntensLoc As Integer
    Private uDeHazeLoc As Integer
    Private uVignetteLoc As Integer
    Private uVignettestdLoc As Integer
    Private uFilmGrainLoc As Integer
    Private uGrainSizeLoc As Integer
    Private uVividLoc As Integer
    Private uSepiaMixLoc As Integer
    Private uNoirFactorLoc As Integer
    Private uSplitToningLoc As Integer
    Private uFadeLoc As Integer
    Private uFadeTintLoc As Integer
    Private uCinematicLoc As Integer
    Private uCineBalanceLoc As Integer
    Private uBarrelDistortionLoc As Integer
    Private uLevelHorizonLoc As Integer = -1
    Private uRotationMatrixLoc As Integer = -1


    Private uTextureXLoc As Integer
    Private ReadOnly lutFolderPath As String = System.IO.Path.Combine(Application.StartupPath, "LUTS")
    Private uIsCropApplied As Boolean
    ' --- For painting the text on the viewport ---
    Private uTextTexLocation As Integer
    Private uUseTextLocation As Integer
    Private uTextOpacityLocation As Integer ' Useful if you want a transparency slider later
    Private uTextColorLocation As Integer ' Add this with the others

    ' Locations for the shader uniforms
    Private uTargetRGBLoc As Integer
    Private uReplaceRGBLoc As Integer
    Private uRadiusLoc As Integer
    Private uSmoothnessLoc As Integer
    'Private uUseLoc As Integer
    Private uSelectionCenter As Integer
    Private uSelectionRangeLoc As Integer
    Private uOpacityLoc As Integer

    Private uRectangleSelColorLoc As Integer


    Public RedMix As Single = 1.0F
    Public GreenMix As Single = 1.0F
    Public BlueMix As Single = 1.0F

    Private currentRotation As Single = 0.0F
    ' Place these with your other global variables (e.g., near currentRotation)
    Private isStraighteningMode As Boolean = False
    Private lineStartPoint As Point = Point.Empty
    Private lineCurrentPoint As Point = Point.Empty
    Private previousRotation As Single = 0.0F
    Private preRotationCropRect As RectangleF = New RectangleF(0.0F, 0.0F, 1.0F, 1.0F)
    Private currentAutoFillScale As Single = 1.0F



    ' ===== RAW BRIDGE IMPORTS =====
    <DllImport("rawBridge.dll", CallingConvention:=CallingConvention.Cdecl)>
    Private Shared Function LoadRawImage(
        path As String,
        ByRef buffer As IntPtr,
        ByRef width As Integer,
        ByRef height As Integer,
        ByRef bitsPerChannel As Integer
    ) As Integer
    End Function

    <DllImport("RawBridge.dll", CallingConvention:=CallingConvention.Cdecl)>
    Private Shared Sub FreeRawBuffer(ptr As IntPtr)
    End Sub

    <DllImport("RawBridge.dll", CallingConvention:=CallingConvention.Cdecl)>
    Private Shared Function LoadRawPreview(
        path As String,
        ByRef buffer As IntPtr,
        ByRef dataSize As Integer,
        ByRef width As Integer,
        ByRef height As Integer,
        ByRef imageType As Integer
    ) As Integer
    End Function

    Public Sub New()
        ' 1. This creates viewportMenu and GlControl2
        InitializeComponent() ' Must be first!


        ' 5. Attach the menu to the control
        GlControl2.ContextMenuStrip = viewportMenu

        Dim settings As New GLControlSettings() With {
            .APIVersion = New Version(4, 5, 0, 0),
            .Profile = ContextProfile.Compatability ' <--- CHANGE THIS FROM .Core
}

        AddHandler Me.KeyDown, AddressOf GL_KeyDown
        AddHandler GlControl2.Load, AddressOf GL_Load
        AddHandler GlControl2.Paint, AddressOf GL_Paint
        AddHandler GlControl2.Resize, AddressOf GL_Resize

        AddHandler Me.Shown, Sub()
                                 GlControl2.MakeCurrent()
                                 GlControl2.Invalidate()
                             End Sub


        ' Initialize and add your GLControl to panelCenter


        panelCenter.Controls.Add(GlControl2)

        ' AddHandler glHist.Load, AddressOf glHist_Load
        'AddHandler glHist.Paint, AddressOf glHist_Paint   ======================
        'AddHandler glHist.Resize, AddressOf glHist_Resize

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.MinimumSize = New Size(1920, 1024)
        Me.Size = New Size(1920, 1024)

        ' 1. Standard Handlers
        AddHandler GlControl2.MouseWheel, AddressOf GL_MouseWheel
        AddHandler GlControl2.MouseDown, AddressOf GL_MouseDown
        AddHandler GlControl2.MouseMove, AddressOf GL_MouseMove
        AddHandler GlControl2.MouseUp, AddressOf GL_MouseUp
        AddHandler GlControl2.MouseDoubleClick, AddressOf GL_MouseDoubleClick
        ' Add this to Form1_Load to ensure the event is wired up
        ' 1. Wire the event
        AddHandler GlControl2.Resize, AddressOf GL_Resize

        ' 2. Force a message immediately to check the window
        System.Diagnostics.Debug.WriteLine("DEBUG SYSTEM ACTIVE")

        ' 3. Manually call the resize logic once to start
        GL_Resize(GlControl2, EventArgs.Empty)


        ' 2. Find the TabControl regardless of its name
        ' This prevents the "TabControl1 is not declared" error
        Dim tc As TabControl = Me.Controls.OfType(Of TabControl).FirstOrDefault()
        If tc Is Nothing Then
            ' Search inside panels if it's nested
            Dim found = Me.Controls.Find("TabControl1", True).FirstOrDefault()
            If found IsNot Nothing Then tc = DirectCast(found, TabControl)
        End If

        ' In form Load or after creating the panel


        flow_Correction.VerticalScroll.Visible = False

        ' Push the system scrollbar off-screen to the right 
        flow_Correction.Padding = New Padding(0, 0, -25, 0)
        flow_Correction.Margin = New Padding(0, 0, 0, 0)

        ' Ensure the horizontal one never shows up 
        flow_Correction.HorizontalScroll.Maximum = 0
        flow_Correction.HorizontalScroll.Visible = False

        PopulateLutCombo()
        ApplyDarkModeTitle()

        tsRGB.Renderer = New MyToolStripRenderer()


        ' 4. Initialize Text System so it doesn't Return Early
        currentFont = New Font("Arial", 24, FontStyle.Bold) ' Default startup font
        currentColor = Color.White                        ' Default startup color
        useText = False                                   ' Keep it off until user types
        _textTextureID = -1                               ' Start at -1 (No texture)

        ' If there is already text in the box, render it now
        If Not String.IsNullOrEmpty(txtWatermark.Text) Then
            RenderTextToBitmap()
            useText = True
        End If

        galleryMonitor = New ScrollHider(thumbnailPanel)
    End Sub


    Private Sub btn_Open_Click(sender As Object, e As EventArgs) Handles btn_Open.Click
        OpenFilePic()
    End Sub

    Private Sub OpenFilePic()
        Using ofd As New OpenFileDialog()
            ofd.Title = "Open Image"
            ofd.Multiselect = False

            ofd.Filter =
            "Standard Images (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|" &
            "RAW Images (*.cr2;*.cr3;*.nef;*.arw;*.dng;*.rw2;*.raf;*.cr3)|*.cr2;*.cr3;*.nef;*.arw;*.dng;*.rw2;*.raf|" &
            "All Supported Images|*.jpg;*.jpeg;*.png;*.bmp;*.cr2;*.cr3;*.nef;*.arw;*.dng;*.rw2;*.raf"

            ofd.FilterIndex = 3

            If ofd.ShowDialog() = DialogResult.OK Then
                Dim selectedPath As String = ofd.FileName

                ' 1. Extract the last folder name as we did before
                Dim folderPath As String = System.IO.Path.GetDirectoryName(ofd.FileName)
                Dim folderName As String = System.IO.Path.GetFileName(folderPath.TrimEnd(System.IO.Path.DirectorySeparatorChar))

                ' 2. Check for the "_Catalog" suffix (Case-Insensitive for safety)
                ' [cite: 2026-02-14] Consistency: Using StringComparison to ensure it finds "_Catalog" or "_catalog"
                If folderName.EndsWith("_Catalog", StringComparison.OrdinalIgnoreCase) Then
                    ' Remove the "_Catalog" part from the display if you want it cleaner
                    Dim cleanName As String = folderName.Replace("_Catalog", "")
                    lblCatalog.Text = "CATALOG : " & cleanName
                Else
                    lblCatalog.Text = "FOLDER : " & folderName
                End If



                ' --- NEW: Memorize the path for the Catalog feature ---
                ' We check if the list already contains the path to avoid duplicates
                If Not stripFilePaths.Contains(selectedPath) Then
                    stripFilePaths.Add(selectedPath)
                End If
                ' ------------------------------------------------------
                ' for save presets
                currentImagePath = selectedPath


                Renew_Edit_Mode()
                Try
                    AddImageToSession(selectedPath)
                Catch ex As Exception
                    MessageBox.Show("Error loading image: " & ex.Message)
                End Try
            End If
        End Using
        Panel1.Focus()
    End Sub

    Private Sub AddImageToSession(filePath As String)
        ' 🔒 Prevent duplicates
        If loadedFiles.Contains(filePath) Then
            Return
        End If

        Dim ext As String = System.IO.Path.GetExtension(filePath).ToLower()
        Dim rawExtensions As String() = {".cr2", ".cr3", ".nef", ".arw", ".dng", ".rw2", ".raf"}

        Dim bmp As Bitmap = Nothing

        If rawExtensions.Contains(ext) Then
            bmp = LoadRawThumbnail(filePath)
            If bmp Is Nothing Then
                Throw New Exception("RAW loading failed.")
            End If
        Else
            bmp = New Bitmap(filePath)
        End If

        ' --- NEW CONNECTION ---
        ' Store the bitmap globally so UpdateVertices can fix the aspect ratio
        loadedImage = bmp

        ' Reset zoom/pan for the new image if you want it centered
        currentZoom = 1.0F
        panX = 0
        panY = 0

        ' Immediately calculate the correct shape (no stretching!)
        UpdateVertices()
        ' ----------------------

        Dim thumbPanel As Panel = CreateThumbnail(bmp, filePath)
        thumbnailPanel.Controls.Add(thumbPanel)
        thumbnailPanel.Controls.SetChildIndex(thumbPanel, thumbnailPanel.Controls.Count - 1)

        loadedFiles.Add(filePath)
        SelectThumbnail(thumbPanel)
        Threading.Interlocked.Increment(totalLibraryItems)
        lblLibrary.Text = $"Library : {loadedFiles.Count} / {totalLibraryItems}"

        If galleryMonitor IsNot Nothing Then
            galleryMonitor.ForceUpdate()
        End If

        GlControl2.Invalidate() ' Refresh the screen
    End Sub


    Private Function LoadRawImageVB(path As String) As Bitmap
        ' --- 1. Memory Setup ---
        Dim bufferPtr As IntPtr = IntPtr.Zero
        Dim width, height, bpc As Integer
        If LoadRawImage(path, bufferPtr, width, height, bpc) <> 0 Then Return Nothing

        Dim totalPixels As Integer = width * height
        Dim srcBytes((totalPixels * 6) - 1) As Byte
        Marshal.Copy(bufferPtr, srcBytes, 0, srcBytes.Length)

        ' --- 2. Smart Analysis (Exposure & AWB) ---
        Dim sumR As Double, sumG As Double, sumB As Double, maxVal As Double
        Dim srcIndex As Integer = 0
        For i As Integer = 0 To totalPixels - 1
            Dim rV = BitConverter.ToUInt16(srcBytes, srcIndex)
            Dim gV = BitConverter.ToUInt16(srcBytes, srcIndex + 2)
            Dim bV = BitConverter.ToUInt16(srcBytes, srcIndex + 4)
            sumR += rV : sumG += gV : sumB += bV
            maxVal = Math.Max(maxVal, Math.Max(rV, Math.Max(gV, bV)))
            srcIndex += 6
        Next

        Dim autoExp As Double = If(maxVal > 0, (65535.0 / maxVal) * 0.95, 1.0)
        Dim avgAll As Double = (sumR + sumG + sumB) / (3.0 * totalPixels)
        Dim gR As Double = avgAll / (sumR / totalPixels)
        Dim gG As Double = avgAll / (sumG / totalPixels)
        Dim gB As Double = avgAll / (sumB / totalPixels)

        ' --- 3. Linear Pre-Process ---
        Dim linR(totalPixels - 1) As Double
        Dim linG(totalPixels - 1) As Double
        Dim linB(totalPixels - 1) As Double
        srcIndex = 0
        For i As Integer = 0 To totalPixels - 1
            linR(i) = Math.Max(0, Math.Min(1, (BitConverter.ToUInt16(srcBytes, srcIndex) / 65535.0) * gR * autoExp))
            linG(i) = Math.Max(0, Math.Min(1, (BitConverter.ToUInt16(srcBytes, srcIndex + 2) / 65535.0) * gG * autoExp))
            linB(i) = Math.Max(0, Math.Min(1, (BitConverter.ToUInt16(srcBytes, srcIndex + 4) / 65535.0) * gB * autoExp))
            srcIndex += 6
        Next

        ' --- 4. Final Pass: Adaptive Denoise & Gamma ---
        Dim bmp As New Bitmap(width, height, Imaging.PixelFormat.Format24bppRgb)
        Dim bmpData = bmp.LockBits(New Rectangle(0, 0, width, height), Imaging.ImageLockMode.WriteOnly, Imaging.PixelFormat.Format24bppRgb)
        Dim dst(bmpData.Stride * height - 1) As Byte

        ' Tuning Parameters
        Dim denoiseStrength As Double = 0.5 ' Higher = smoother shadows
        Dim contrast As Double = 1.3
        Dim saturation As Double = 1.1

        For y As Integer = 0 To height - 1
            Dim dstRow As Integer = y * bmpData.Stride
            For x As Integer = 0 To width - 1
                Dim r, g, b As Double
                Dim idx As Integer = x + y * width

                ' Adaptive 3x3 Denoise (Edge Preserving)
                If x > 0 And x < width - 1 And y > 0 And y < height - 1 Then
                    Dim avgR As Double, avgG As Double, avgB As Double
                    For ky As Integer = -1 To 1
                        Dim kLine = (y + ky) * width
                        For kx As Integer = -1 To 1
                            Dim kIdx = (x + kx) + kLine
                            avgR += linR(kIdx) : avgG += linG(kIdx) : avgB += linB(kIdx)
                        Next
                    Next
                    avgR /= 9.0 : avgG /= 9.0 : avgB /= 9.0

                    ' Mix based on brightness (denoise more in dark areas)
                    Dim mix = denoiseStrength * (1.0 - (linR(idx) + linG(idx) + linB(idx)) / 3.0)
                    r = (linR(idx) * (1 - mix)) + (avgR * mix)
                    g = (linG(idx) * (1 - mix)) + (avgG * mix)
                    b = (linB(idx) * (1 - mix)) + (avgB * mix)
                Else
                    r = linR(idx) : g = linG(idx) : b = linB(idx)
                End If

                ' Post-Processing
                r = Math.Pow(Math.Max(0, Math.Min(1, ((r - 0.5) * contrast) + 0.5)), 1.0 / 2.2)
                g = Math.Pow(Math.Max(0, Math.Min(1, ((g - 0.5) * contrast) + 0.5)), 1.0 / 2.2)
                b = Math.Pow(Math.Max(0, Math.Min(1, ((b - 0.5) * contrast) + 0.5)), 1.0 / 2.2)

                Dim di As Integer = dstRow + x * 3
                dst(di) = CByte(b * 255) : dst(di + 1) = CByte(g * 255) : dst(di + 2) = CByte(r * 255)
            Next
        Next

        Marshal.Copy(dst, 0, bmpData.Scan0, dst.Length)
        bmp.UnlockBits(bmpData)
        FreeRawBuffer(bufferPtr)
        Return bmp
    End Function

    Public Sub UpdateVertices()
        ' Use currentImage and zoomFactor to match your existing code
        If currentImage Is Nothing OrElse GlControl2.Width = 0 Then Return

        Dim vW As Single = GlControl2.ClientSize.Width
        Dim vH As Single = GlControl2.ClientSize.Height

        ' Convert pixel pan to Normalized Device Coordinates (-1 to 1)
        Dim ndcPanX As Single = (CSng(panX) * 2.0F) / vW
        Dim ndcPanY As Single = (CSng(panY) * -2.0F) / vH

        ' Aspect Ratio math to prevent stretching
        Dim windowAspect As Single = vW / vH
        Dim imageAspect As Single = CSng(currentImage.Width) / CSng(currentImage.Height)

        Dim halfW, halfH As Single
        If imageAspect > windowAspect Then
            halfW = 1.0F
            halfH = windowAspect / imageAspect
        Else
            halfW = imageAspect / windowAspect
            halfH = 1.0F
        End If

        ' The 5-float per vertex array (X, Y, Z, U, V)
        ' This setup is what removed that impossible spike in your histogram
        Dim vertices As Single() = {
            (-halfW * CSng(zoomFactor)) + ndcPanX, (-halfH * CSng(zoomFactor)) + ndcPanY, 0.0F, 0.0F, 0.0F,
             (halfW * CSng(zoomFactor)) + ndcPanX, (-halfH * CSng(zoomFactor)) + ndcPanY, 0.0F, 1.0F, 0.0F,
             (halfW * CSng(zoomFactor)) + ndcPanX, (halfH * CSng(zoomFactor)) + ndcPanY, 0.0F, 1.0F, 1.0F,
            (-halfW * CSng(zoomFactor)) + ndcPanX, (-halfH * CSng(zoomFactor)) + ndcPanY, 0.0F, 0.0F, 0.0F,
             (halfW * CSng(zoomFactor)) + ndcPanX, (halfH * CSng(zoomFactor)) + ndcPanY, 0.0F, 1.0F, 1.0F,
            (-halfW * CSng(zoomFactor)) + ndcPanX, (halfH * CSng(zoomFactor)) + ndcPanY, 0.0F, 0.0F, 1.0F
        }

        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo)
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * 4, vertices, BufferUsageHint.DynamicDraw)
    End Sub

    Private Function CreateThumbnail(img As Image, filePath As String) As Panel
        If img Is Nothing OrElse String.IsNullOrEmpty(filePath) Then Return Nothing

        ' 1. The Container (Păstrăm logica de rotunjire de mai sus)
        Dim thumbContainer As New Panel With {
        .Width = 200,
        .Height = 130,
        .Margin = New Padding(8),
        .BackColor = Color.FromArgb(45, 45, 48),
        .Cursor = Cursors.Hand,
        .Tag = filePath,
        .Padding = New Padding(3)
    }

        ' 2. The PictureBox
        Dim pb As New PictureBox With {
        .Dock = DockStyle.Fill,
        .SizeMode = PictureBoxSizeMode.Zoom,
        .Image = New Bitmap(img),
        .BackColor = Color.Black
    }

        ' 3. The Label (Numele fișierului peste poză)
        Dim lblName As New Label With {
        .Text = System.IO.Path.GetFileName(filePath),
        .Dock = DockStyle.Bottom,
        .ForeColor = Color.FromArgb(108, 210, 205),
        .BackColor = Color.FromArgb(150, 0, 0, 0), ' Negru semi-transparent (150 din 255)
        .TextAlign = ContentAlignment.MiddleCenter,
        .Height = 25,
        .Font = New Font("Century Gothic", 8, FontStyle.Regular)
    }

        ' IMPORTANT: Pentru ca transparența label-ului să funcționeze peste imagine,
        ' trebuie să îl adăugăm ca fiu al PictureBox-ului.
        pb.Controls.Add(lblName)

        ' 4. Event Handlers (Adăugăm click și pe label, altfel zona aia va fi "moartă")
        AddHandler thumbContainer.Click, AddressOf Thumbnail_Click
        AddHandler thumbContainer.MouseDown, AddressOf Thumbnail_MouseDown
        AddHandler pb.Click, AddressOf Thumbnail_Click
        AddHandler pb.MouseDown, AddressOf Thumbnail_MouseDown
        AddHandler lblName.Click, AddressOf Thumbnail_Click
        AddHandler lblName.MouseDown, AddressOf Thumbnail_MouseDown

        ' Hover effect actualizat să includă și label-ul dacă e nevoie
        AddHandler pb.MouseEnter, Sub(s, e) thumbContainer.BackColor = Color.FromArgb(80, 80, 80)
        ' ... restul handler-elor de mouse rămân la fel

        Dim hoverColor As Color = Color.FromArgb(80, 80, 80)
        Dim defaultColor As Color = Color.FromArgb(45, 45, 48)
        Dim selectionColor As Color = Color.SkyBlue

        ' Creăm o sub-rutină internă pentru a refolosi logica de refresh a culorii
        Dim updateFolderColor = Sub()
                                    If selectedThumb Is thumbContainer Then
                                        thumbContainer.BackColor = selectionColor
                                    Else
                                        thumbContainer.BackColor = defaultColor
                                    End If
                                End Sub

        ' Evenimente pentru PictureBox
        AddHandler pb.MouseEnter, Sub(s, e)
                                      If selectedThumb IsNot thumbContainer Then
                                          thumbContainer.BackColor = hoverColor
                                      End If
                                  End Sub

        AddHandler pb.MouseLeave, Sub(s, e) updateFolderColor()

        ' Evenimente pentru Label (pentru că și el acoperă o parte din panel)
        AddHandler lblName.MouseEnter, Sub(s, e)
                                           If selectedThumb IsNot thumbContainer Then
                                               thumbContainer.BackColor = hoverColor
                                           End If
                                       End Sub

        AddHandler lblName.MouseLeave, Sub(s, e) updateFolderColor()



        ' Păstrăm codul pentru colțuri rotunjite de la pasul anterior aici
        AddHandler thumbContainer.Paint, Sub(sender, e)
                                             Dim pnl = DirectCast(sender, Panel)
                                             Dim radius As Integer = 15
                                             Dim path As New Drawing2D.GraphicsPath()
                                             path.AddArc(0, 0, radius, radius, 180, 90)
                                             path.AddArc(pnl.Width - radius - 1, 0, radius, radius, 270, 90)
                                             path.AddArc(pnl.Width - radius - 1, pnl.Height - radius - 1, radius, radius, 0, 90)
                                             path.AddArc(0, pnl.Height - radius - 1, radius, radius, 90, 90)
                                             path.CloseAllFigures()
                                             pnl.Region = New Region(path)
                                         End Sub

        img.Dispose()
        thumbContainer.Controls.Add(pb)

        Return thumbContainer
    End Function

    Private Sub Thumbnail_MouseDown(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Right Then
            ' Detectăm corect Panel-ul părinte, indiferent pe ce am dat click
            If TypeOf sender Is Panel Then
                lastRightClickedThumb = DirectCast(sender, Panel)
            Else
                ' Dacă am dat click pe PictureBox sau Label, mergem la părinte
                ' Folosim .Parent pentru a ajunge la Container
                lastRightClickedThumb = DirectCast(DirectCast(sender, Control).Parent, Panel)

                ' NOTĂ: Dacă Label-ul este înăuntrul PictureBox-ului, 
                ' s-ar putea să fie nevoie de încă un .Parent dacă ierarhia e mai adâncă.
                ' O metodă mai sigură este să căutăm în sus până găsim Panel-ul:
                Dim ctrl = DirectCast(sender, Control)
                While ctrl IsNot Nothing AndAlso Not TypeOf ctrl Is Panel
                    ctrl = ctrl.Parent
                End While
                lastRightClickedThumb = DirectCast(ctrl, Panel)
            End If

            ' Afișăm meniul la poziția cursorului
            If lastRightClickedThumb IsNot Nothing Then
                thumbMenu.Show(Cursor.Position)
            End If
        End If
    End Sub

    Private Async Function SelectThumbnail(clickedPanel As Panel) As Task
        ' ... Styling logic (keeping same) ...
        btn_StraightenPic.Enabled = True
        btn_UndoRotate.Enabled = False
        currentRotation = 0.0F
        previousRotation = 0.0F

        First_Load = 0
        ' Inside your File Loading logic
        If (Math.Abs(cropRect.X - 0.1F) > 0.001F) Or (Math.Abs(cropRect.Width - 0.8F) > 0.001F) Then
            uIsCropApplied = True   ' Trigger the zoom
            uIsCropping = 0        ' Hide the marking lines
        Else
            uIsCropApplied = False  ' Don't zoom
            uIsCropping = 1        ' Show the marking lines
        End If
        GlControl2.Invalidate()

        Picture_Has_No_Changes()

        ' Reset local variables to defaults
        cropRect.X = 0.1F
        cropRect.Y = 0.1F
        cropRect.Width = 0.8F
        cropRect.Height = 0.8F


        If selectedThumb IsNot Nothing Then selectedThumb.BackColor = Color.FromArgb(45, 45, 48)
        selectedThumb = clickedPanel
        selectedThumb.BackColor = Color.SkyBlue



        Dim filePath As String = TryCast(clickedPanel.Tag, String)
        If String.IsNullOrEmpty(filePath) Then Return

        currentImagePath = filePath
        lblStatus_Change.Text = "No changes - Original"

        UpdateExifPanel(filePath)
        isUpdatingUI = True


        Dim ext As String = System.IO.Path.GetExtension(filePath).ToLower()
        Dim rawExtensions As String() = {".cr2", ".cr3", ".nef", ".arw", ".dng", ".rw2", ".raf"}
        isRawFile = rawExtensions.Contains(ext)

        If isRawFile Then
            ' --- 🟢 STEP A: FAST PREVIEW (Low Res) ---
            Dim preview As Bitmap = LoadRawThumbnail(filePath)
            If preview IsNot Nothing Then
                Me.Invoke(Sub()
                              UploadToGPU(preview)
                              ' --- 📊 HISTOGRAM (Fast Preview) ---
                              CalculateHistogram(preview)
                              glHist.Refresh()
                              ResetAllSliders()

                          End Sub)
            End If

            ' --- 🟢 STEP B: HIGH-RES EMBEDDED PREVIEW ---
            Dim fullPreview As Bitmap = Await Task.Run(Function() GetHighResEmbeddedPreview(filePath))

            ' --- 🟢 SAFE STEP C: FINAL GPU UPLOAD ---
            If selectedThumb Is clickedPanel AndAlso fullPreview IsNot Nothing Then
                Me.Invoke(Sub()
                              Try

                                  ' 1. Capture the old image to dispose it AFTER the new one is ready
                                  Dim oldImage As Bitmap = currentImage

                                  ' 2. Assign the new high-res image
                                  currentImage = fullPreview


                                  ' --- NEW: Store the original state for the Before button ---
                                  If originalTextureID <> -1 Then GL.DeleteTexture(originalTextureID)
                                  originalTextureID = CreateBackupTexture(currentImage)
                                  origW = currentImage.Width
                                  origH = currentImage.Height
                                  ' -----------------------------------------------------------


                                  ' 📊 HISTOGRAM (High Res)
                                  ' We calculate this before uploading/invalidating
                                  CalculateHistogram(currentImage)
                                  glHist.Refresh()
                                  ' 3. Standard GPU Upload
                                  GlControl2.MakeCurrent()
                                  LoadTextureFromBitmap(currentImage)
                                  ResetAllSliders()
                                  'for modified pictures
                                  'LoadSidecar(currentImagePath)
                                  ' 4. NOW it is safe to dispose the old one
                                  oldImage?.Dispose()

                                  SyncAllUniforms()
                                  GlControl2.Invalidate()
                                  UpdateStatusBar()


                              Catch ex As Exception
                                  ' Log GDI+ errors here without crashing
                                  Debug.WriteLine("GDI+ Upload Error: " & ex.Message)
                              End Try
                          End Sub)
            End If


        Else
            ' --- 🟢 STEP D: STANDARD IMAGE ---
            Dim bmp As New Bitmap(filePath)

            ' --- NEW: Store the original state for the Before button ---
            If bmp IsNot Nothing Then
                If originalTextureID <> -1 Then GL.DeleteTexture(originalTextureID)
                originalTextureID = CreateBackupTexture(bmp)
                origW = bmp.Width
                origH = bmp.Height
            End If
            ' -----------------------------------------------------------

            ' --- 📊 HISTOGRAM (Standard) ---
            CalculateHistogram(bmp)
            glHist.Refresh()
            ResetAllSliders()
            Me.Invoke(Sub()
                          ' 1. Upload the standard image to the GPU
                          UploadToGPU(bmp)

                          ' 2. Refresh the bar — this will now show the correct Width x Height 
                          ' and calculate the MP based on the 'bmp' resolution.
                          UpdateStatusBar()


                      End Sub)
        End If

        'xupdate3
        ResetParams()

        'load with modifications if they exists
        LoadSidecar(currentImagePath)
        FitToScreen()

        ' 1. Get the directory containing the current image
        Dim parentFolder As String = ""
        If Not String.IsNullOrEmpty(currentImagePath) Then
            parentFolder = System.IO.Path.GetDirectoryName(currentImagePath)
        End If

        ' 2. Safety Check: Only run the naming logic if we actually have a folder
        If Not String.IsNullOrEmpty(parentFolder) Then
            Dim folderName As String = System.IO.Path.GetFileName(parentFolder.TrimEnd(System.IO.Path.DirectorySeparatorChar))

            ' 3. Apply the Catalog vs Folder logic
            If folderName.EndsWith("_Catalog", StringComparison.OrdinalIgnoreCase) Then
                lblCatalog.Text = "CATALOG : " & folderName.Replace("_Catalog", "").ToUpper()
            Else
                lblCatalog.Text = "FOLDER : " & folderName.ToUpper()
            End If
        Else
            ' Optional: Clear the label if everything was cleared
            lblCatalog.Text = "NO FOLDER LOADED"
        End If
        Dim currentIndex As Integer = thumbnailPanel.Controls.GetChildIndex(clickedPanel) + 1
        lblLibrary.Text = $"Library : {currentIndex} / {totalLibraryItems}"

        ' 3. Unmute UI and update status
        'Check_For_Changes() ' reintialize the value of checking for changes on loading the picture in the sesion
        isUpdatingUI = False
        currentFilePath = filePath
        UpdateStatusBar()
        UpdateResetButtonState()
    End Function

    Private Function CreateBackupTexture(bmp As Bitmap) As Integer

        If bmp Is Nothing Then Return -1

        Dim texID As Integer = GL.GenTexture()
        GL.BindTexture(TextureTarget.Texture2D, texID)

        Dim data As BitmapData = bmp.LockBits(New Rectangle(0, 0, bmp.Width, bmp.Height),
                                         ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb)

        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                  OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0)

        bmp.UnlockBits(data)

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, All.Linear)
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, All.Linear)

        Return texID
    End Function

    Private Sub UpdateResetButtonState()
        Dim sidecarPath As String = currentImagePath & ".fstop"
        Dim hasChanges As Boolean = System.IO.File.Exists(sidecarPath)

        ' 1. Set the functionality
        btn_Remove_Changes.Enabled = hasChanges

        ' 2. Set the Visual Style
        If hasChanges Then
            ' Active State: Lighter Grey (90, 90, 90)
            btn_Remove_Changes.BackColor = Color.FromArgb(35, 35, 35)
            btn_Remove_Changes.ForeColor = Color.Silver ' Make text pop
            btn_Remove_Changes.Text = "REMOVE CHANGES"
        Else
            ' Inactive State: Dark/Dimmed (45, 45, 45)
            btn_Remove_Changes.BackColor = Color.FromArgb(30, 30, 35)
            btn_Remove_Changes.ForeColor = Color.LightGray ' Dim the text
            btn_Remove_Changes.Text = "NO CHANGES"
        End If
    End Sub

    Private Sub GL_Load(sender As Object, e As EventArgs)
        ' 1️⃣ Make context current FIRST (mandatory)
        GlControl2.MakeCurrent()

        ' 2️⃣ Disable sRGB framebuffer (prevents washed out/crushed colors depending on GPU)
        GL.Disable(EnableCap.FramebufferSrgb)
        GL.Disable(EnableCap.DepthTest)
        GL.Disable(EnableCap.CullFace)

        ' 3️⃣ Query and Log GPU Info
        Try
            Dim version As String = GL.GetString(StringName.Version)
            Dim renderer As String = GL.GetString(StringName.Renderer)
            Dim shortVersion As String = version.Split(" "c)(0)
            baseGpuText = "OpenGL: " & shortVersion & "    |    GPU: " & renderer
            lblGpuInfo.Text = baseGpuText
        Catch ex As Exception
            lblGpuInfo.Text = "OpenGL Init Error: " & ex.Message
        End Try

        ' 4️⃣ Viewport & Clear Color
        GL.Viewport(0, 0, GlControl2.Width, GlControl2.Height)
        GL.ClearColor(Color.FromArgb(18, 18, 18))

        ' 5️⃣ Create VBO/VAO
        ' We generate them, but we don't put final data in yet because GL_Paint handles the math
        InitGlobalGeometry()

        ' 16 byte stride (4 floats * 4 bytes): [X, Y, U, V]
        ' Attribute 0 (Position): 2 floats (X, Y), Offset 0
        ' Position: Location 0, 3 components (X,Y,Z), Stride 20, Offset 0
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, False, 20, 0)
        GL.EnableVertexAttribArray(0)

        ' TexCoord: Location 1, 2 components (U,V), Stride 20, Offset 12 (after X,Y,Z)
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, False, 20, 12)
        GL.EnableVertexAttribArray(1)

        ' ✅ Clean up / Unbind
        GL.BindVertexArray(0)
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0)

        ' 6️⃣ Shader Compilation (Simplified using your Class)
        Try
            Dim projectSourcePath As String = Application.StartupPath & "\Shaders\"

            ' Paths for Vertex Shaders
            Dim vPath As String = Path.Combine(projectSourcePath, "passthrough.vert")
            Dim vScreenPath As String = Path.Combine(projectSourcePath, "screen.vert")
            Dim vTextPath As String = Path.Combine(projectSourcePath, "Text.vert") ' NEW: Path for Text Vertex

            ' Paths for Fragment Shaders
            Dim fPath As String = Path.Combine(projectSourcePath, "correction.frag")
            Dim sPath As String = Path.Combine(projectSourcePath, "Screen.frag")
            Dim tPath As String = Path.Combine(projectSourcePath, "text.frag")

            ' 1. Load the Main "Darkroom" Shader (Process logic)
            mainShader = New ShaderProgram(vPath, fPath)

            ' 2. Load the "Gallery" Shader (Presentation logic)
            screenShader = New ShaderProgram(vScreenPath, sPath)

            ' 3. Load the Dedicated Text Shader (MATCHED PAIR)
            ' We now use vTextPath instead of vScreenPath to match the new handshake
            textShader = New ShaderProgram(vTextPath, tPath)

            ' Use the main shader initially
            mainShader.Use()
            Console.WriteLine("Shaders Loaded Successfully!")

        Catch ex As Exception
            MessageBox.Show("Error loading shaders: " & ex.Message)
        End Try

        ' 7️⃣ Cache All Uniform Locations
        Uniform_Location_Load()

        ' 8️⃣ INITIALIZE FBO AND HUD (Missing Pieces!)
        ' This creates the "Virtual Canvas" texture and the VAO for the final screen pass
        InitFBO(GlControl2.Width, GlControl2.Height)
        InitHUDResources() ' <--- THIS PREVENTS THE ACCESS VIOLATION


        ' Bind Sampler to Unit 0
        ' Ensure we are targeting the mainShader before setting its uniforms
        mainShader.Use()
        Dim imageLocation As Integer = GL.GetUniformLocation(mainShader.Handle, "image")
        If imageLocation <> -1 Then
            GL.Uniform1(imageLocation, 0) ' Bind texture unit 0 to the "image" sampler
        End If

        GlControl2.Invalidate()
    End Sub


    Private Sub Uniform_Location_Load()

        If mainShader Is Nothing OrElse textShader Is Nothing Then Return

        uExposureLoc = GL.GetUniformLocation(mainShader.Handle, "uExposure")
        uContrastLoc = GL.GetUniformLocation(mainShader.Handle, "uContrast")
        uHighlightsLoc = GL.GetUniformLocation(mainShader.Handle, "uHighlights")
        uShadowsLiftLoc = GL.GetUniformLocation(mainShader.Handle, "uShadowsLift")
        uWhitesLoc = GL.GetUniformLocation(mainShader.Handle, "uWhites")
        uBlacksLoc = GL.GetUniformLocation(mainShader.Handle, "uBlacks")
        uTempLoc = GL.GetUniformLocation(mainShader.Handle, "uTemp")
        uTintLoc = GL.GetUniformLocation(mainShader.Handle, "uTint")
        uDynConLoc = GL.GetUniformLocation(mainShader.Handle, "uDynCon")
        uClarityLoc = GL.GetUniformLocation(mainShader.Handle, "uClarity")
        uStructXLoc = GL.GetUniformLocation(mainShader.Handle, "uStructX")
        uSharpenLoc = GL.GetUniformLocation(mainShader.Handle, "uSharpen")
        uDefringeLoc = GL.GetUniformLocation(mainShader.Handle, "uDefringe")
        uDenoiseLoc = GL.GetUniformLocation(mainShader.Handle, "uDenoise")
        uNoiseLimitLoc = GL.GetUniformLocation(mainShader.Handle, "uNoiseLimit")
        uNoiseChromaLoc = GL.GetUniformLocation(mainShader.Handle, "uNoiseChroma")
        uNoiseLumaLoc = GL.GetUniformLocation(mainShader.Handle, "uNoiseLuma")
        uSaturationLoc = GL.GetUniformLocation(mainShader.Handle, "uSaturation")
        uVibranceLoc = GL.GetUniformLocation(mainShader.Handle, "uVibrance")
        uRedMixLoc = GL.GetUniformLocation(mainShader.Handle, "uRedMix")
        uGreenMixLoc = GL.GetUniformLocation(mainShader.Handle, "uGreenMix")
        uBlueMixLoc = GL.GetUniformLocation(mainShader.Handle, "uBlueMix")
        uLutIntensLoc = GL.GetUniformLocation(mainShader.Handle, "uLutIntens")
        uDeHazeLoc = GL.GetUniformLocation(mainShader.Handle, "uDeHaze")
        uVignetteLoc = GL.GetUniformLocation(mainShader.Handle, "uVignette")
        uVignettestdLoc = GL.GetUniformLocation(mainShader.Handle, "uVignetteStd")
        uFilmGrainLoc = GL.GetUniformLocation(mainShader.Handle, "uFilmGrain")
        uGrainSizeLoc = GL.GetUniformLocation(mainShader.Handle, "uGrainSize")
        uVividLoc = GL.GetUniformLocation(mainShader.Handle, "uVivid")
        uSepiaMixLoc = GL.GetUniformLocation(mainShader.Handle, "uSepiaMix")
        uNoirFactorLoc = GL.GetUniformLocation(mainShader.Handle, "uNoirFactor")
        uSplitToningLoc = GL.GetUniformLocation(mainShader.Handle, "uSplitToning")
        uFadeLoc = GL.GetUniformLocation(mainShader.Handle, "uFade")
        uFadeTintLoc = GL.GetUniformLocation(mainShader.Handle, "uFadeTint")
        uCinematicLoc = GL.GetUniformLocation(mainShader.Handle, "uCinematic")
        uCineBalanceLoc = GL.GetUniformLocation(mainShader.Handle, "uCineBalance")
        uTextureXLoc = GL.GetUniformLocation(mainShader.Handle, "uTextureX")
        uBarrelDistortionLoc = GL.GetUniformLocation(mainShader.Handle, "uBarrelDistortion")
        ' --- Inside your Shader Setup / Init function ---
        uTextTexLocation = GL.GetUniformLocation(textShader.Handle, "uTextTex")
        uUseTextLocation = GL.GetUniformLocation(textShader.Handle, "uUseText")
        uTextOpacityLocation = GL.GetUniformLocation(textShader.Handle, "uTextOpacity")
        uTextColorLocation = GL.GetUniformLocation(textShader.Handle, "uTextColor")


        uTargetRGBLoc = GL.GetUniformLocation(mainShader.Handle, "uTargetRGB")
        uReplaceRGBLoc = GL.GetUniformLocation(mainShader.Handle, "uReplaceRGB")
        uRadiusLoc = GL.GetUniformLocation(mainShader.Handle, "uRadius")
        uSmoothnessLoc = GL.GetUniformLocation(mainShader.Handle, "uSmoothness")
        uSelectionRangeLoc = GL.GetUniformLocation(mainShader.Handle, "uSelectionRange")
        uOpacityLoc = GL.GetUniformLocation(mainShader.Handle, "uOpacity")
        uRectangleSelColorLoc = GL.GetUniformLocation(mainShader.Handle, "uRectangleSelColor")

        uLevelHorizonLoc = GL.GetUniformLocation(mainShader.Handle, "uLevelHorizon")
        uRotationMatrixLoc = GL.GetUniformLocation(mainShader.Handle, "uRotationMatrix")


    End Sub

    Private Sub Thumbnail_Click(sender As Object, e As EventArgs)

        Dim clickedPanel As Panel

        If TypeOf sender Is PictureBox Then
            clickedPanel = CType(sender, PictureBox).Parent
            Renew_Edit_Mode()
        Else
            clickedPanel = CType(sender, Panel)
        End If

        SelectThumbnail(clickedPanel)

    End Sub

    Private Sub UpdateExifPanel(filePath As String)
        If txtExifData Is Nothing Then Return

        Try
            ' ✅ FIX: Use System.IO to avoid MetadataExtractor.IO conflict
            Dim directories = MetadataExtractor.ImageMetadataReader.ReadMetadata(filePath)
            Dim info As New System.Text.StringBuilder()

            ' Search variables
            Dim camModel As String = ""
            Dim exposure As String = ""
            Dim iso As String = ""
            Dim lens As String = ""

            ' Loop through ALL directories found in the RAW (Sony/Nikon/Fuji)
            For Each directory In directories
                ' Look for Camera Model
                If String.IsNullOrEmpty(camModel) Then
                    camModel = directory.GetDescription(MetadataExtractor.Formats.Exif.ExifIfd0Directory.TagModel)
                End If

                ' Look for Exposure (Shutter + F-Stop)
                If String.IsNullOrEmpty(exposure) Then
                    Dim shutter = directory.GetDescription(MetadataExtractor.Formats.Exif.ExifSubIfdDirectory.TagExposureTime)
                    Dim fstop = directory.GetDescription(MetadataExtractor.Formats.Exif.ExifSubIfdDirectory.TagFNumber)
                    If Not String.IsNullOrEmpty(shutter) Then exposure = $"{shutter} @ {fstop}"
                End If

                ' Look for ISO
                If String.IsNullOrEmpty(iso) Then
                    iso = directory.GetDescription(MetadataExtractor.Formats.Exif.ExifSubIfdDirectory.TagIsoEquivalent)
                End If

                ' Look for Lens Model (Often in "Sony Makernote" or "Nikon Makernote")
                If String.IsNullOrEmpty(lens) Then
                    lens = directory.GetDescription(MetadataExtractor.Formats.Exif.ExifSubIfdDirectory.TagLensModel)
                End If
            Next

            ' Build the display string
            If Not String.IsNullOrEmpty(camModel) Then info.AppendLine($"CAM: {camModel}")
            If Not String.IsNullOrEmpty(exposure) Then info.AppendLine($"EXP: {exposure}")
            If Not String.IsNullOrEmpty(iso) Then info.AppendLine($"ISO: {iso}")
            If Not String.IsNullOrEmpty(lens) Then info.AppendLine($"LENS: {lens}")

            ' Apply to UI
            If info.Length > 0 Then
                txtExifData.Text = info.ToString()
            Else
                txtExifData.Text = "Metadata structure not recognized."
            End If

        Catch ex As Exception
            txtExifData.Text = "Error: " & ex.Message
        End Try
    End Sub

    Private Sub txtExifData_Enter(sender As Object, e As EventArgs) Handles txtExifData.Enter
        ' When the user clicks in, move focus to the Form or the Panel
        ' This prevents the blinking vertical line from ever appearing
        thumbnailPanel.Focus()
    End Sub

    Private Function LoadRawThumbnail(path As String) As Bitmap

        Dim buffer As IntPtr = IntPtr.Zero
        Dim dataSize As Integer = 0
        Dim width As Integer = 0
        Dim height As Integer = 0
        Dim imageType As Integer = 0

        Dim result = LoadRawPreview(path, buffer, dataSize, width, height, imageType)

        If result <> 0 OrElse buffer = IntPtr.Zero Then
            Return Nothing
        End If

        Dim bmp As Bitmap = Nothing

        If imageType = 1 Then ' LIBRAW_IMAGE_JPEG

            Dim bytes(dataSize - 1) As Byte
            Marshal.Copy(buffer, bytes, 0, dataSize)

            ' ✅ FIX: Use System.IO to avoid MetadataExtractor conflict
            Using ms As New System.IO.MemoryStream(bytes)
                Using tempBmp As New Bitmap(ms)
                    ' Creating a new instance from tempBmp decouples the bitmap 
                    ' from the MemoryStream and the original file bytes.
                    bmp = New Bitmap(tempBmp)
                End Using
            End Using


        Else

            ' RAW RGB bitmap
            Dim bytes(dataSize - 1) As Byte
            Marshal.Copy(buffer, bytes, 0, dataSize)

            bmp = New Bitmap(width, height, Imaging.PixelFormat.Format24bppRgb)

            Dim bmpData = bmp.LockBits(
                New Rectangle(0, 0, width, height),
                Imaging.ImageLockMode.WriteOnly,
                Imaging.PixelFormat.Format24bppRgb)

            Marshal.Copy(bytes, 0, bmpData.Scan0, dataSize)

            bmp.UnlockBits(bmpData)
        End If


        FreeRawBuffer(buffer)
        Debug.WriteLine("Using embedded preview")
        Return bmp

    End Function

    Private Sub UploadToGPU(bmp As Bitmap)
        currentImage?.Dispose()
        currentImage = bmp
        GlControl2.MakeCurrent()

        ' LoadTextureFromBitmap should set Linear filtering for smooth upscaling
        LoadTextureFromBitmap(currentImage)

        ' Ensure the Viewport matches the current control size 
        ' This is where the "Upscale" physically happens before CAS sharpens it
        GL.Viewport(0, 0, GlControl2.Width, GlControl2.Height)

        SyncAllUniforms()
        GlControl2.Invalidate()
    End Sub

    Private Function GetHighResEmbeddedPreview(filePath As String) As System.Drawing.Bitmap
        Dim ext As String = System.IO.Path.GetExtension(filePath).ToLower()

        ' Pentru .CR3 folosim LibRaw (LoadRawPreview) pentru că știe să extragă 
        ' corect JPEG-ul de 32MP fără să îl "strice" ca Windows-ul.
        If ext = ".cr3" Then
            Dim buffer As IntPtr = IntPtr.Zero
            Dim dataSize As Integer = 0
            Dim width As Integer = 0, height As Integer = 0, imageType As Integer = 0

            ' LoadRawPreview va găsi cea mai bună imagine (Full Size) din interior
            Dim result = LoadRawPreview(filePath, buffer, dataSize, width, height, imageType)

            If result = 0 AndAlso buffer <> IntPtr.Zero Then
                Try
                    ' Dacă e JPEG (imageType = 1), îl convertim direct
                    If imageType = 1 Then
                        Dim bytes(dataSize - 1) As Byte
                        Marshal.Copy(buffer, bytes, 0, dataSize)
                        Using ms As New System.IO.MemoryStream(bytes)
                            Using tempBmp As New System.Drawing.Bitmap(ms)
                                ' Creăm copia finală și o returnăm
                                Return New System.Drawing.Bitmap(tempBmp)
                            End Using
                        End Using
                    End If
                Catch ex As Exception
                    Debug.WriteLine("Eroare conversie Full Preview: " & ex.Message)
                Finally
                    ' ACEASTA ESTE SINGURA LOCAȚIE UNDE TREBUIE SĂ FIE:
                    If buffer <> IntPtr.Zero Then
                        FreeRawBuffer(buffer)
                        buffer = IntPtr.Zero ' Resetăm pointerul ca măsură de siguranță extra
                    End If
                End Try
            End If
        End If

        ' --- FALLBACK: Logica ta originală cu WIC (pentru restul formatelor) ---
        Try
            Using stream As New System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read)
                Dim decoder = System.Windows.Media.Imaging.BitmapDecoder.Create(stream,
                System.Windows.Media.Imaging.BitmapCreateOptions.None,
                System.Windows.Media.Imaging.BitmapCacheOption.OnLoad)

                ' Căutăm frame-ul cel mai mare (cel de 32.3 MP)
                Dim bestFrame = decoder.Frames(0)
                Dim maxArea As Long = 0
                For Each frame In decoder.Frames
                    Dim area As Long = CLng(frame.PixelWidth) * CLng(frame.PixelHeight)
                    If area > maxArea Then
                        maxArea = area : bestFrame = frame
                    End If
                Next

                Dim encoder As New System.Windows.Media.Imaging.JpegBitmapEncoder()
                encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(bestFrame))
                Using ms As New System.IO.MemoryStream()
                    encoder.Save(ms)
                    Using tempBmp As New System.Drawing.Bitmap(ms)
                        Return New System.Drawing.Bitmap(tempBmp)
                    End Using
                End Using
            End Using
        Catch
            Return LoadRawThumbnail(filePath)
        End Try
    End Function

    Private Sub LoadTextureFromBitmap(bmp As Bitmap)
        If bmp Is Nothing Then Return

        GlControl2.MakeCurrent()

        ' 1. Management Memorie GPU [cite: 2026-02-14]
        If textureID <> 0 Then GL.DeleteTexture(textureID)
        textureID = GL.GenTexture()
        GL.BindTexture(TextureTarget.Texture2D, textureID)

        ' 2. Configurare Filtrare Anizotropică (Extra-Fine)
        ' Definim constantele manual pentru compatibilitate maximă
        Const TextureMaxAnisotropyExt As TextureParameterName = CType(&H84FE, TextureParameterName)
        Const MaxTextureMaxAnisotropyExt As GetPName = CType(&H84FF, GetPName)

        Dim maxAniso As Single
        GL.GetFloat(MaxTextureMaxAnisotropyExt, maxAniso)
        ' Aplicăm nivelul maxim de filtrare suportat de placa video
        GL.TexParameter(TextureTarget.Texture2D, TextureMaxAnisotropyExt, maxAniso)

        ' 3. Pregătire și Upload Pixeli
        Try
            If bmp IsNot Nothing Then
                ' Creăm o copie locală rapidă pentru a evita conflictele de acces
                Using uploadBmp As Bitmap = New Bitmap(bmp)
                    uploadBmp.RotateFlip(RotateFlipType.RotateNoneFlipY)

                    Dim data As Imaging.BitmapData = uploadBmp.LockBits(
                New Rectangle(0, 0, uploadBmp.Width, uploadBmp.Height),
                Imaging.ImageLockMode.ReadOnly,
                Imaging.PixelFormat.Format32bppArgb)

                    GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1)
                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                         uploadBmp.Width, uploadBmp.Height, 0,
                         OpenTK.Graphics.OpenGL4.PixelFormat.Bgra,
                         PixelType.UnsignedByte, data.Scan0)

                    uploadBmp.UnlockBits(data)
                End Using
            End If
        Catch ex As Exception
            Debug.WriteLine("Eroare la Upload Textură: " & ex.Message)
        End Try

        ' 4. Configurare Mipmapping și Wrap (Final Touch)
        ' LinearMipmapLinear elimină zgomotul și zimții la dimensiuni mici
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureMinFilter.LinearMipmapLinear)
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, TextureMagFilter.Linear)

        ' Păstrăm marginile curate
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, All.ClampToEdge)
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, All.ClampToEdge)

        ' CRITICAL: Generăm piramida de rezoluții după ce pixelii sunt pe GPU
        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D)

        currentImage = bmp
        FitToScreen()
    End Sub

    Private Sub SyncAllUniforms()

        ' 1. Guard clause
        If mainShader Is Nothing OrElse mainShader.Handle = 0 OrElse currentImage Is Nothing Then Return


        ' --- MAIN IMAGE (Slot 0) ---
        GL.ActiveTexture(TextureUnit.Texture0)
        GL.BindTexture(TextureTarget.Texture2D, textureID)
        GL.Uniform1(GL.GetUniformLocation(mainShader.Handle, "image"), 0)

        ' --- 3D LUT (Slot 1) ---
        GL.ActiveTexture(TextureUnit.Texture1)
        GL.Uniform1(GL.GetUniformLocation(mainShader.Handle, "lutTexture"), 1)

        If useLut AndAlso lutTextureID <> 0 Then
            GL.BindTexture(TextureTarget.Texture3D, lutTextureID)

            ' FIX: Changed "uLutEnabled" to "useLut" to match your shader's if(useLut != 0)
            Dim uEnabledLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "useLut")
            If uEnabledLoc <> -1 Then GL.Uniform1(uEnabledLoc, 1)

            ' Set Intensity (Matches uLutIntens in shader)
            Dim uIntensLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uLutIntens")
            If uIntensLoc <> -1 Then
                ' Since your internal value is already 0.0 to 1.0, just send it directly
                GL.Uniform1(uIntensLoc, CurrentParams.LutIntens)

            End If
        Else
            GL.BindTexture(TextureTarget.Texture3D, 0)
            ' Turn it off
            Dim uEnabledLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "useLut")
            If uEnabledLoc <> -1 Then GL.Uniform1(uEnabledLoc, 0)
        End If


        ' --- TEXT OVERLAY (Slot 2) ---

        GL.ActiveTexture(TextureUnit.Texture2)
        ' Only look for uniforms in mainShader.Handle
        Dim uTextTexLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uTextTex")
        GL.Uniform1(uTextTexLoc, 2)

        If useText AndAlso _textTextureID <> -1 Then
            GL.BindTexture(TextureTarget.Texture2D, _textTextureID)

            GL.Uniform1(GL.GetUniformLocation(mainShader.Handle, "uUseText"), 1)
            GL.Uniform1(GL.GetUniformLocation(mainShader.Handle, "uTextOpacity"), CSng(sldTextOpacity.Value / 100.0F))

            ' --- ADD THE COLOR HERE (No extra shaders!) ---
            Dim uTextColorLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uTextColor")
            If uTextColorLoc <> -1 Then
                GL.Uniform3(uTextColorLoc, textColor.R / 255.0F, textColor.G / 255.0F, textColor.B / 255.0F)
            End If

            GL.Uniform2(GL.GetUniformLocation(mainShader.Handle, "uTextPos"), textPos_txt.X, textPos_txt.Y)
        Else
            GL.BindTexture(TextureTarget.Texture2D, 0)
            GL.Uniform1(GL.GetUniformLocation(mainShader.Handle, "uUseText"), 0)
        End If



        ' --- 3. CROP DATA (The "Dot vs Comma" Fix) ---
        Dim imgW As Single = CSng(currentImage.Width)
        Dim imgH As Single = CSng(currentImage.Height)

        ' Default to full image
        Dim nx As Single = 0.0F
        Dim ny As Single = 0.0F
        Dim nw As Single = 1.0F
        Dim nh As Single = 1.0F

        ' Logic: Only apply if we have a valid width
        ' If your .fstop loaded "0,45" as "0", this safety keeps the image from disappearing
        If cropRect.Width > 0 Then
            If cropRect.Width > 1.0F Then
                ' It's Pixels - Normalize them
                nx = cropRect.X / imgW
                ny = cropRect.Y / imgH
                nw = cropRect.Width / imgW
                nh = cropRect.Height / imgH
            Else
                ' It's already Percentages (from .fstop)
                nx = cropRect.X
                ny = cropRect.Y
                nw = cropRect.Width
                nh = cropRect.Height
            End If
        End If

        ' FINAL SAFETY: If the result is still zero or invalid, force full-screen 
        ' to prevent the "Plain Stretched Color"
        If nw <= 0.0001F Or nh <= 0.0001F Then
            nx = 0 : ny = 0 : nw = 1 : nh = 1
        End If

        GL.Uniform4(GL.GetUniformLocation(mainShader.Handle, "uCropRect"), nx, ny, nw, nh)
        GL.Uniform1(GL.GetUniformLocation(mainShader.Handle, "uIsCropping"), uIsCropping)
        GL.Uniform1(GL.GetUniformLocation(mainShader.Handle, "uIsCropApplied"), If(uIsCropApplied, 1, 0))

        ' --- OVERLAYS & SLIDERS ---
        Dim timeLoc = GL.GetUniformLocation(mainShader.Handle, "uTime")
        If timeLoc <> -1 Then GL.Uniform1(timeLoc, CSng(DateTime.Now.TimeOfDay.TotalSeconds))

        Slider_Adjustments()

        GL.ActiveTexture(TextureUnit.Texture0)
    End Sub


    Private Sub Slider_Adjustments()
        ' 2. BASIC ADJUSTMENTS
        GL.Uniform1(uExposureLoc, CurrentParams.Exposure)
        GL.Uniform1(uContrastLoc, CurrentParams.Contrast)
        GL.Uniform1(uSaturationLoc, CurrentParams.Saturation)
        GL.Uniform1(uVibranceLoc, CurrentParams.Vibrance)
        GL.Uniform1(uHighlightsLoc, CurrentParams.Highlights)
        GL.Uniform1(uShadowsLiftLoc, CurrentParams.ShadowsLift)
        GL.Uniform1(uWhitesLoc, CurrentParams.Whites)
        GL.Uniform1(uBlacksLoc, CurrentParams.Blacks)
        GL.Uniform1(uTempLoc, CurrentParams.Temp)
        GL.Uniform1(uTintLoc, CurrentParams.Tint)
        GL.Uniform1(uBarrelDistortionLoc, CurrentParams.BarrelDistortion)

        ' 3. DETAIL & UTILITY
        GL.Uniform1(uDenoiseLoc, CurrentParams.Denoise)
        GL.Uniform1(uNoiseLimitLoc, CurrentParams.NoiseLimit)
        GL.Uniform1(uNoiseChromaLoc, CurrentParams.NoiseChroma)
        GL.Uniform1(uNoiseLumaLoc, CurrentParams.NoiseLuma)

        GL.Uniform1(uSharpenLoc, CurrentParams.Sharpen)
        GL.Uniform1(uDeHazeLoc, CurrentParams.DeHaze)
        GL.Uniform1(uClarityLoc, CurrentParams.Clarity)
        GL.Uniform1(uDynConLoc, CurrentParams.DynCon)
        GL.Uniform1(uStructXLoc, CurrentParams.StructX)
        GL.Uniform1(uTextureXLoc, CurrentParams.TextureX)
        GL.Uniform1(uDefringeLoc, CurrentParams.Defringe)

        ' 4. COLOR MIXING
        GL.Uniform1(uRedMixLoc, CurrentParams.RedMix)
        GL.Uniform1(uGreenMixLoc, CurrentParams.GreenMix)
        GL.Uniform1(uBlueMixLoc, CurrentParams.BlueMix)

        ' 5. EFFECTS
        GL.Uniform1(uLutIntensLoc, CurrentParams.LutIntens)
        GL.Uniform1(uVignetteLoc, CurrentParams.Vignette)
        GL.Uniform1(uVignettestdLoc, CurrentParams.VignetteStd)
        GL.Uniform1(uFilmGrainLoc, CurrentParams.FilmGrain)
        GL.Uniform1(uGrainSizeLoc, CurrentParams.GrainSize)
        GL.Uniform1(uVividLoc, CurrentParams.Vivid)
        GL.Uniform1(uSepiaMixLoc, CurrentParams.SepiaMix)
        GL.Uniform1(uNoirFactorLoc, CurrentParams.NoirFactor)
        GL.Uniform1(uSplitToningLoc, CurrentParams.SplitToning)
        GL.Uniform1(uFadeLoc, CurrentParams.Fade)
        GL.Uniform1(uFadeTintLoc, CurrentParams.FadeTint)
        GL.Uniform1(uCinematicLoc, CurrentParams.Cinematic)
        GL.Uniform1(uCineBalanceLoc, CurrentParams.CineBalance)

        ' Update the shader with the new slider values
        GL.Uniform1(uRadiusLoc, CurrentParams.HexaRadius)
        GL.Uniform1(uSmoothnessLoc, CurrentParams.HexaSmoothness)
        GL.Uniform1(uSelectionRangeLoc, CurrentParams.HexaRange)
        GL.Uniform1(uOpacityLoc, CurrentParams.HexaOpacity)

        ' And for the colors (using the X, Y, Z components to avoid the ambiguity error)
        GL.Uniform3(uTargetRGBLoc, targetCol.X, targetCol.Y, targetCol.Z)
        GL.Uniform3(uReplaceRGBLoc, replaceCol.X, replaceCol.Y, replaceCol.Z)

        ' --- 6. TEXT & WATERMARK ---
        ' Dim uTextColLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uTextColor")
        ' If uTextColLoc <> -1 Then
        ' GL.Uniform3(uTextColLoc, textColor.R / 255.0F, textColor.G / 255.0F, textColor.B / 255.0F)
        '   End If

        'Dim uTextOpLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uTextOpacity")
        'If uTextOpLoc <> -1 Then
        ' GL.Uniform1(uTextOpLoc, CSng(sldTextOpacity.Value / 100.0F))
        'End If

    End Sub

    Public Sub UpdateStatusBar()

        If currentImage Is Nothing OrElse String.IsNullOrEmpty(currentFilePath) Then
            lblGpuInfo.Text = baseGpuText
            Return
        End If

        ' 1. Get File Info
        Dim fileName As String = System.IO.Path.GetFileName(currentFilePath)
        Dim width As Integer = currentImage.Width
        Dim height As Integer = currentImage.Height
        Dim megaPixels As Double = (width * height) / 1000000.0

        ' 2. REAL ZOOM MATH (Local to this sub)
        ' We compare the physical control size to the actual image pixels
        Dim viewportW As Single = CSng(GlControl2.ClientSize.Width)
        Dim viewportH As Single = CSng(GlControl2.ClientSize.Height)

        ' This ratio represents what "Fit to Screen" looks like in decimals
        Dim ratioX As Single = viewportW / CSng(width)
        Dim ratioY As Single = viewportH / CSng(height)
        Dim baseFitRatio As Single = Math.Min(ratioX, ratioY)

        ' Multiply the base fit by your internal zoomFactor 
        ' (e.g., if Fit is 0.15 and zoomFactor is 1.0, result is 15%)
        Dim realZoomPercent As Double = (zoomFactor * baseFitRatio) * 100.0
        Dim zoomText As String = realZoomPercent.ToString("0") & "%"

        ' 3. Detect RAW bit depth
        Dim ext As String = System.IO.Path.GetExtension(currentFilePath).ToLower()
        Dim rawExtensions As String() = {".cr2", ".cr3", ".nef", ".arw", ".dng", ".rw2", ".raf"}
        Dim bitInfo As String = If(rawExtensions.Contains(ext), "RAW", "8-bit")

        ' 4. Update Labels
        lblGpuInfo.Text = baseGpuText
        lblFileName.Text = fileName
        lblWH.Text = width.ToString("N0") & " X " & height.ToString("N0") & " px"
        lblSizeMp.Text = megaPixels.ToString("0.0") & " Mpx"
        lblBit.Text = bitInfo
        lblZoom.Text = "Zoom : " & zoomText

    End Sub

    Private Sub UpdateSliderValue(paramName As String, internalVal As Single)
        If _isInternalUpdating Then Exit Sub

        If sliderLookup.ContainsKey(paramName) Then
            _isInternalUpdating = True
            Try
                Dim ms = sliderLookup(paramName)
                Dim uiValue As Integer = InternalToSlider(paramName, internalVal)
                Dim safeValue = Math.Clamp(uiValue, ms.Minimum, ms.Maximum)

                ms.Value = safeValue

                ' Triggers GPU sync and all related logic
                OnSliderScroll(ms, EventArgs.Empty)
                ms.Invalidate()
            Finally
                _isInternalUpdating = False
            End Try
        End If
    End Sub

    Private Function LoadTextFile(relativePath As String) As String
        Dim fullPath = System.IO.Path.Combine(Application.StartupPath, relativePath)
        Return System.IO.File.ReadAllText(fullPath)
    End Function

    Public Sub FitToScreen()
        If currentImage Is Nothing Then Return

        zoomFactor = 1.0

        '1. Reset Pan
        panX = 0
        panY = 0

        ClampPan()
        UpdateStatusBar()
        GlControl2.Invalidate()

    End Sub


    Private Sub MainForm_Resize(sender As Object, e As EventArgs) Handles Me.Resize

        FitToScreen()

    End Sub

    Private Sub ClampPan()

        If currentImage Is Nothing Then Return

        Dim viewW As Double = GlControl2.Width
        Dim viewH As Double = GlControl2.Height

        Dim imgW As Double = currentImage.Width * zoomFactor
        Dim imgH As Double = currentImage.Height * zoomFactor

        Dim maxPanX As Double = Math.Max(0, (imgW - viewW) / 2)
        Dim maxPanY As Double = Math.Max(0, (imgH - viewH) / 2)

        panX = Math.Max(-maxPanX, Math.Min(maxPanX, panX))
        panY = Math.Max(-maxPanY, Math.Min(maxPanY, panY))

    End Sub


    Private Sub btn_LoadFolder_Click(sender As Object, e As EventArgs) Handles btn_LoadFolder.Click
        OpenFolderPic(sender)
    End Sub

    Private Async Sub OpenFolderPic(sender As Object)
        Panel1.Focus()
        Using fbd As New FolderBrowserDialog()
            fbd.Description = "Select a folder containing images"

            If fbd.ShowDialog() <> DialogResult.OK Then
                Return
            End If

            Dim folderPath As String = fbd.SelectedPath
            Dim folderName As String = System.IO.Path.GetFileName(folderPath.TrimEnd(System.IO.Path.DirectorySeparatorChar))

            ' --- Folder/Catalog Label Logic ---
            If folderName.EndsWith("_Catalog", StringComparison.OrdinalIgnoreCase) Then
                Dim cleanName As String = folderName.Replace("_Catalog", "")
                lblCatalog.Text = "CATALOG : " & cleanName
            Else
                lblCatalog.Text = "FOLDER : " & folderName
            End If

            Dim supportedExtensions As String() =
            {".jpg", ".jpeg", ".png", ".bmp", ".cr2", ".cr3", ".nef", ".arw", ".dng", ".rw2", ".raf"}

            ' 1. Get files and filter
            Dim files = System.IO.Directory.GetFiles(folderPath)
            Dim imageFiles = files.
            Where(Function(f) supportedExtensions.Contains(System.IO.Path.GetExtension(f).ToLower())).
            OrderBy(Function(f) f).
            ToList()

            If imageFiles.Count = 0 Then
                MessageBox.Show("No supported images found in this folder.")
                Return
            End If

            ' Update the global total for the library count [cite: 2026-02-14]
            totalLibraryItems += imageFiles.Count

            ' Disable button while loading
            CType(sender, Button).Enabled = False

            Await Task.Run(Sub()
                               Dim rawExtensions As String() = {".cr2", ".cr3", ".nef", ".arw", ".dng", ".rw2", ".raf"}

                               Parallel.ForEach(imageFiles,
                               New ParallelOptions With {
                                   .MaxDegreeOfParallelism = Environment.ProcessorCount
                               },
                               Sub(file)
                                   ' Skip duplicates early
                                   If loadedFiles.Contains(file) Then
                                       ' If skipped, subtract from total so the math stays correct
                                       Threading.Interlocked.Decrement(totalLibraryItems)
                                       Return
                                   End If

                                   Dim bmp As Bitmap = Nothing
                                   Dim ext = System.IO.Path.GetExtension(file).ToLower()

                                   Try
                                       If rawExtensions.Contains(ext) Then
                                           bmp = LoadRawThumbnail(file)
                                       Else
                                           Using fs As New System.IO.FileStream(file, System.IO.FileMode.Open, System.IO.FileAccess.Read)
                                               ' Use GetThumbnailImage to load a tiny version without loading the whole 50MB file into memory
                                               Using fullSizeBmp As New Bitmap(fs)
                                                   ' Calculate the best fit within 200x130
                                                   Dim ratioX As Double = 200.0 / fullSizeBmp.Width
                                                   Dim ratioY As Double = 130.0 / fullSizeBmp.Height
                                                   Dim ratio As Double = Math.Min(ratioX, ratioY) ' Keep the smaller ratio to fit

                                                   Dim newWidth As Integer = CInt(fullSizeBmp.Width * ratio)
                                                   Dim newHeight As Integer = CInt(fullSizeBmp.Height * ratio)

                                                   ' Create the thumbnail with the CORRECT proportions
                                                   bmp = New Bitmap(fullSizeBmp, newWidth, newHeight)
                                               End Using
                                           End Using
                                       End If
                                   Catch ex As Exception
                                       Threading.Interlocked.Decrement(totalLibraryItems)
                                       Return
                                   End Try

                                   If bmp Is Nothing Then
                                       Threading.Interlocked.Decrement(totalLibraryItems)
                                       Return
                                   End If

                                   ' Switch to UI thread to add thumb and update label
                                   Me.Invoke(Sub()
                                                 If loadedFiles.Contains(file) Then
                                                     bmp.Dispose()
                                                     Return
                                                 End If

                                                 ' Add to path lists
                                                 If Not stripFilePaths.Contains(file) Then
                                                     stripFilePaths.Add(file)
                                                 End If

                                                 Dim thumbPanel = CreateThumbnail(bmp, file)
                                                 thumbnailPanel.Controls.Add(thumbPanel)
                                                 loadedFiles.Add(file)
                                                 If galleryMonitor IsNot Nothing Then
                                                     galleryMonitor.ForceUpdate()
                                                 End If
                                                 Application.DoEvents()

                                                 lblLibrary.Text = $"Library : {loadedFiles.Count} / {totalLibraryItems}"
                                             End Sub)
                               End Sub)
                           End Sub)

            CType(sender, Button).Enabled = True


        End Using



        GC.Collect()
        GC.WaitForPendingFinalizers()
        GC.Collect()


    End Sub

    Public Sub ResetParams()
        isUpdatingUI = True

        ' 1. Reset Internal Math Parameters
        CurrentParams.Exposure = 0.0F
        CurrentParams.Contrast = 0.0F
        CurrentParams.Highlights = 0.0F
        CurrentParams.ShadowsLift = 0.0F
        CurrentParams.Whites = 0.0F
        CurrentParams.Blacks = 0.0F
        CurrentParams.Temp = 0.0F
        CurrentParams.Tint = 0.0F
        CurrentParams.BarrelDistortion = 0.0F

        CurrentParams.DynCon = 0.0F
        CurrentParams.Clarity = 0.0F
        CurrentParams.StructX = 0.0F
        CurrentParams.TextureX = 0.0F
        CurrentParams.Sharpen = 0.0F
        CurrentParams.Defringe = 0.0F
        CurrentParams.Denoise = 0.0F
        CurrentParams.NoiseLimit = 0.0F
        CurrentParams.NoiseChroma = 0.0F
        CurrentParams.NoiseLuma = 0.0F

        ' Multipliers default to 1.0
        CurrentParams.Saturation = 1.0F
        CurrentParams.Vibrance = 1.0F
        CurrentParams.RedMix = 1.0F
        CurrentParams.GreenMix = 1.0F
        CurrentParams.BlueMix = 1.0F

        CurrentParams.LutIntens = 0.0F ' Note: If 100 on slider, 1.0 internally
        CurrentParams.DeHaze = 0.0F
        CurrentParams.Vignette = 0.0F
        CurrentParams.VignetteStd = 0.0F
        CurrentParams.FilmGrain = 0.0F
        CurrentParams.GrainSize = 0.0F
        CurrentParams.Vivid = 0.0F
        CurrentParams.SepiaMix = 0.0F
        CurrentParams.NoirFactor = 0.0F
        CurrentParams.SplitToning = 0.0F
        CurrentParams.Fade = 0.0F
        CurrentParams.FadeTint = 0.0F
        CurrentParams.Cinematic = 0.0F
        CurrentParams.CineBalance = 0.0F


        UpdateSliderValue("Exposure", 50)
        UpdateSliderValue("Contrast", 50)
        UpdateSliderValue("Highlights", 50)
        UpdateSliderValue("ShadowsLift", 50)
        UpdateSliderValue("Whites", 50)
        UpdateSliderValue("Blacks", 50)
        UpdateSliderValue("Temp", 50)
        UpdateSliderValue("Tint", 50)
        UpdateSliderValue("BarrelDistortion", 50)
        UpdateSliderValue("DynCon", 0)
        UpdateSliderValue("Clarity", 50)
        UpdateSliderValue("StructX", 0)
        UpdateSliderValue("TextureX", 0)
        UpdateSliderValue("Sharpen", 0)
        UpdateSliderValue("Defringe", 0)
        UpdateSliderValue("Denoise", 0)
        UpdateSliderValue("NoiseLimit", 0)
        UpdateSliderValue("NoiseChroma", 0)
        UpdateSliderValue("NoiseLuma", 0)
        UpdateSliderValue("Saturation", 50)
        UpdateSliderValue("Vibrance", 50)
        UpdateSliderValue("RedMix", 50)
        UpdateSliderValue("GreenMix", 50)
        UpdateSliderValue("BlueMix", 50)
        UpdateSliderValue("DeHaze", 0)
        UpdateSliderValue("Vignette", 0)
        UpdateSliderValue("VignetteStd", 0)
        UpdateSliderValue("FilmGrain", 0)
        UpdateSliderValue("GrainSize", 0)
        UpdateSliderValue("Vivid", 0)
        UpdateSliderValue("SepiaMix", 0)
        UpdateSliderValue("NoirFactor", 0)
        UpdateSliderValue("SplitToning", 0)
        UpdateSliderValue("Fade", 0)
        UpdateSliderValue("FadeTint", 50)
        UpdateSliderValue("Cinematic", 0)
        UpdateSliderValue("CineBalance", 50)
        UpdateSliderValue("LutIntens", 0)

        ' 2. Sync UI Sliders
        For Each kvp In sliderLookup
            Dim tag As String = kvp.Key
            Dim ms As ModernSlider = kvp.Value
            Dim txt = ms.Parent.Controls.OfType(Of TextBox)().FirstOrDefault()

            ' --- CATEGORY 1: Bipolar Sliders (Middle 50, Value 0.00) ---
            ' These move from -Max to +Max
            If tag = "Exposure" Or tag = "Contrast" Or tag = "Highlights" Or tag = "ShadowsLift" Or
               tag = "Whites" Or tag = "Blacks" Or tag = "Temp" Or tag = "Tint" Or tag = "BarrelDistortion" Or
               tag = "Clarity" Then

                ms.Value = 50
                If txt IsNot Nothing Then txt.Text = "0.00"

                ' --- CATEGORY 2: Multiplier Sliders (Middle 50, Value 1.00) ---
                ' These move from 0.0 to 2.0
            ElseIf tag = "Saturation" Or tag = "Vibrance" Or tag.EndsWith("Mix") Then

                ms.Value = 50
                If txt IsNot Nothing Then txt.Text = "1.00"

                ' --- CATEGORY 3: Special Cases (Lut Intensity 100) ---
            ElseIf tag = "LutIntens" Then

                ms.Value = 0
                If txt IsNot Nothing Then txt.Text = "0.00"

                ' --- CATEGORY 4: Unipolar Sliders (Start 0, Value 0.00) ---
                ' All others (Sharpen, DeHaze, Grain, etc.)
            Else
                ms.Value = 0
                If txt IsNot Nothing Then txt.Text = "0.00"
            End If

            ms.Invalidate()
        Next

        isUpdatingUI = False

        ' 3. Refresh GPU and Viewport

        SyncAllUniforms()
        GlControl2.Invalidate()
    End Sub

    Private Sub ResetAllSliders()
        ResetCorrectionSliders()
        ResetEnhancementSliders()
        ResetStylizationSliders()

        CurrentParams.Exposure = 0.0F
        CurrentParams.Temp = 0.0F
        CurrentParams.Tint = 0.0F
        CurrentParams.Highlights = 0.0F
        CurrentParams.ShadowsLift = 0.0F



        SyncAllUniforms()
        GlControl2.Invalidate()
    End Sub
    Private Sub ResetCorrectionSliders()
        Me.sldExposure.Value = 50
        Me.sldContrast.Value = 50
        Me.sldHighlights.Value = 50
        Me.sldShadowsLift.Value = 50
        Me.sldWhites.Value = 50
        Me.sldBlacks.Value = 50
        Me.sldTemp.Value = 50
        Me.sldTint.Value = 50
        Me.sldBarrelDistortion.Value = 50

        Me.sldHexaCorrect.Value = 0
        Me.sldSmoothnesCorrect.Value = 0
        Me.sldAreaCorrect.Value = 0
        Me.sldOpacity.Value = 0
        Me.cb_UseReplace.Checked = False
        Me.txtHexa_Corect.Text = ""
        Me.txtCorrectedHexa_Correct.Text = ""
        Me.txt_HEXA_READ.Text = ""
    End Sub
    Private Sub ResetEnhancementSliders()
        Me.sldDynCon.Value = 0
        Me.sldClarity.Value = 50
        Me.sldStructX.Value = 0
        Me.sldTextureX.Value = 0
        Me.sldSharpen.Value = 0
        Me.sldDefringe.Value = 0
        Me.sldDenoise.Value = 0
        Me.sldNoiseLimit.Value = 0
        Me.sldNoiseChroma.Value = 0
        Me.sldNoiseLuma.Value = 0
        Me.sldSaturation.Value = 50
        Me.sldVibrance.Value = 50
        Me.sldRedMix.Value = 50
        Me.sldGreenMix.Value = 50
        Me.sldBlueMix.Value = 50

        'Me.txtWatermark.Text = ""
        Me.sldTextOpacity.Value = 0
    End Sub
    Private Sub ResetStylizationSliders()
        Me.sldLutIntens.Value = 0
        Me.sldDeHaze.Value = 0
        Me.sldVignette.Value = 0
        Me.sldVignetteStd.Value = 0
        Me.sldFilmGrain.Value = 0
        Me.sldGrainSize.Value = 0
        Me.sldVivid.Value = 0
        Me.sldSepiaMix.Value = 0
        Me.sldNoirFactor.Value = 0
        Me.sldSplitToning.Value = 0
        Me.sldFade.Value = 0
        Me.sldFadeTint.Value = 50
        Me.sldCinematic.Value = 0
        Me.sldCineBalance.Value = 50

    End Sub
    Private Sub ResetCorrectionForAutoCorr()
        Me.sldExposure.Value = 50
        Me.sldContrast.Value = 50
        Me.sldHighlights.Value = 50
        Me.sldShadowsLift.Value = 50
        Me.sldWhites.Value = 50
        Me.sldBlacks.Value = 50
        Me.sldTemp.Value = 50
        Me.sldTint.Value = 50

    End Sub

    ' Mouse Actions in the GL Viewport ================================
    Private Sub GL_MouseWheel(sender As Object, e As MouseEventArgs) Handles GlControl2.MouseWheel
        ' 1. Guard Clause
        ' We keep the check to prevent zooming while cropping to avoid coordinate shifts
        If currentImage Is Nothing OrElse isSelectingCrop Then Return

        Dim oldZoom As Double = zoomFactor
        Dim zoomSpeed As Double = 1.1

        ' 2. Zoom Calculation
        If e.Delta > 0 Then
            zoomFactor *= zoomSpeed
        Else
            zoomFactor /= zoomSpeed
        End If

        ' Clamp zoom range
        zoomFactor = Math.Max(0.05, Math.Min(zoomFactor, 20.0))

        ' 3. Anchor Logic
        ' This keeps the image "glued" to the mouse cursor position while zooming
        Dim mouseX As Double = e.X - GlControl2.Width / 2.0
        Dim mouseY As Double = e.Y - GlControl2.Height / 2.0

        Dim ratio As Double = (zoomFactor / oldZoom) - 1.0
        panX -= CInt(mouseX * ratio)
        panY -= CInt(mouseY * ratio)

        ' 4. Sync and Redraw
        ' Ensure your UpdateVertices() still correctly recalculates the 
        ' main image quad based on the new zoomFactor and panX/Y
        UpdateVertices()
        UpdateStatusBar()

        ' Triggers GL_Paint which now handles the shader-based crop lines
        GlControl2.Invalidate()
    End Sub

    Private Sub GL_MouseDown(sender As Object, e As MouseEventArgs) Handles GlControl2.MouseDown


        ' --- 1. WATERMARK PRIORITY ---
        If My.Computer.Keyboard.CtrlKeyDown AndAlso txtWatermark.Text.Length > 0 Then
            isWatermarkMoving = True
            watermarkRefPt = e.Location
            Return
        End If

        ' --- 2. CROP SELECTION LOGIC ---
        If isSelectingCrop Then
            activeHandle = -1

            Dim imgPos As PointF = GetMouseInImageSpace(e.X, e.Y)
            Dim mouseNormX As Single = imgPos.X
            Dim mouseNormY As Single = imgPos.Y

            Dim viewW As Single = CSng(GlControl2.ClientSize.Width)
            Dim viewH As Single = CSng(GlControl2.ClientSize.Height)
            Dim imgAspect As Single = CSng(currentImage.Width) / CSng(currentImage.Height)
            Dim viewAspect As Single = viewW / viewH

            Dim baseScaleX As Single = If(viewAspect > imgAspect, imgAspect / viewAspect, 1.0F)
            Dim baseScaleY As Single = If(viewAspect > imgAspect, 1.0F, viewAspect / imgAspect)

            Dim actualDrawW As Single = viewW * baseScaleX * CSng(zoomFactor)
            Dim actualDrawH As Single = viewH * baseScaleY * CSng(zoomFactor)

            ' Normal tolerance (corners)
            Dim tol As Single = 20.0F / Math.Max(actualDrawW, 1.0F)
            ' Bigger tolerance for thin edge middles (this fixes the "stuck middle handles")
            Dim tolEdge As Single = 20.0F / Math.Max(actualDrawW, 1.0F)

            ' Always use sorted bounds (handles inverted rectangles perfectly)
            Dim _left As Single = Math.Min(cropRect.X, cropRect.X + cropRect.Width)
            Dim _right As Single = Math.Max(cropRect.X, cropRect.X + cropRect.Width)
            Dim _top As Single = Math.Min(cropRect.Y, cropRect.Y + cropRect.Height)
            Dim _bottom As Single = Math.Max(cropRect.Y, cropRect.Y + cropRect.Height)
            Dim midX As Single = (_left + _right) / 2.0F
            Dim midY As Single = (_top + _bottom) / 2.0F

            ' === HIT TEST (corners first, then edges, then move) ===
            If Math.Abs(mouseNormX - _left) < tol AndAlso Math.Abs(mouseNormY - _top) < tol Then
                activeHandle = 3 ' top-left

            ElseIf Math.Abs(mouseNormX - _right) < tol AndAlso Math.Abs(mouseNormY - _top) < tol Then
                activeHandle = 2 ' top-right
            ElseIf Math.Abs(mouseNormX - _right) < tol AndAlso Math.Abs(mouseNormY - _bottom) < tol Then
                activeHandle = 1 ' bottom-right
            ElseIf Math.Abs(mouseNormX - _left) < tol AndAlso Math.Abs(mouseNormY - _bottom) < tol Then
                activeHandle = 0 ' bottom-left

            ElseIf Math.Abs(mouseNormX - midX) < tolEdge AndAlso Math.Abs(mouseNormY - _top) < tol Then
                activeHandle = 6

                ' Bottom middle (should affect Y larger value)
            ElseIf Math.Abs(mouseNormX - midX) < tolEdge AndAlso Math.Abs(mouseNormY - _bottom) < tol Then
                activeHandle = 4

                ' Right middle
            ElseIf Math.Abs(mouseNormX - _right) < tolEdge AndAlso Math.Abs(mouseNormY - midY) < tolEdge Then
                activeHandle = 5

                ' Left middle
            ElseIf Math.Abs(mouseNormX - _left) < tolEdge AndAlso Math.Abs(mouseNormY - midY) < tolEdge Then
                activeHandle = 7

                ' Move area — slightly inset so it doesn't steal edge clicks
            ElseIf mouseNormX > _left + tol * 1.1F AndAlso mouseNormX < _right - tol * 1.1F AndAlso
               mouseNormY > _top + tol * 1.1F AndAlso mouseNormY < _bottom - tol * 1.1F Then
                activeHandle = 8
            End If


            If activeHandle <> -1 Then
                GlControl2.Capture = True          ' ← VERY IMPORTANT
                lastMousePos = e.Location
                Return
            End If

        Else

            ' ─── NEW ADDITION: HORIZON STRAIGHTEN START TRACKING ───────
            If isStraighteningMode AndAlso e.Button = MouseButtons.Left Then
                lineStartPoint = e.Location
                GlControl2.Capture = True ' Lock focus to draw properly
                Return ' Exit early so we don't activate standard panning at the same time
            End If
            ' ────────────────────────────────────────────────────────────


            ' --- 3. STANDARD PANNING ---
            If e.Button = MouseButtons.Left Then
                isPanning = True
                lastMousePos = e.Location
                GlControl2.Cursor = Cursors.Hand

            End If

        End If

        If e.Button = MouseButtons.Right Then
            pos_for_colors = GMI_Space(e.X, e.Y)
        End If
    End Sub

    Private Sub GL_MouseMove(sender As Object, e As MouseEventArgs) Handles GlControl2.MouseMove

        Dim imgPos As PointF = GetMouseInImageSpace(e.X, e.Y)

        ' Display: Screen(X,Y) | Image(0.0-1.0)
        ' We use "F3" for 3 decimal places to see fine movement
        If currentImage IsNot Nothing Then
            ' Dim debugPos As PointF = GetMouseInImageSpace(e.X, e.Y)
            Dim debugPos As PointF = GMI_Space(e.X, e.Y)

            ' --- 🟢 NEW: COLOR PICKER LOGIC ---
            ' Convert 0.0-1.0 (UV) to actual Pixel Coordinates
            Dim px As Integer = CInt(debugPos.X * currentImage.Width)
            Dim py As Integer = CInt(debugPos.Y * currentImage.Height)

            ' Safety check: ensure we are within the actual bitmap bounds
            If px >= 0 AndAlso px < currentImage.Width AndAlso py >= 0 AndAlso py < currentImage.Height Then
                Try
                    ' Get the color from the bitmap
                    Dim c As Color = currentImage.GetPixel(px, py)

                    ' Update your UI (Example labels)
                    lbl_RGB.Text = $"RGB: {c.R}, {c.G}, {c.B}"
                    lbl_Hex.Text = $"HEX: #{c.R:X2}{c.G:X2}{c.B:X2}"

                    ' If you have a small panel to show the color preview:
                    pnlColorPreview.BackColor = c
                Catch
                    ' Avoid crashing if there's a threading/locking race condition
                End Try
            End If

            ' Professional "Commercial" Format
            ' Screen uses 0-padding for stability; Image uses 3 decimal precision
            lbl_XY.Text = String.Format("SCREEN: X={0,4} Y={1,4}  |  IMAGE: U={2:F2} V={3:F2}",
                                e.X,
                                e.Y,
                                debugPos.X,
                                debugPos.Y)
        End If

        ' --- 1. WATERMARK PRIORITY ---
        If isWatermarkMoving Then
            ' 1. Get current and previous mouse positions in 0.0 - 1.0 Image Space
            Dim currentImgPos As PointF = GMI_Space(e.X, e.Y)
            Dim previousImgPos As PointF = GMI_Space(watermarkRefPt.X, watermarkRefPt.Y)

            ' 2. Calculate the Delta in "Image Units"
            ' This math automatically accounts for Zoom and Panning
            Dim dx_norm As Single = currentImgPos.X - previousImgPos.X
            Dim dy_norm As Single = currentImgPos.Y - previousImgPos.Y

            ' 3. Apply the movement to your text position
            textPos_txt.X += dx_norm
            textPos_txt.Y += dy_norm

            ' 4. Standard updates
            watermarkRefPt = e.Location
            RenderTextToBitmap(txtWatermark.Text, currentFont, textColor)
            GlControl2.Invalidate()
            Return
        End If

        If isSelectingCrop Then
            UpdateCropCursor(e.Location)

            If e.Button = MouseButtons.Left AndAlso activeHandle <> -1 Then
                Dim viewW As Single = CSng(GlControl2.ClientSize.Width)
                Dim viewH As Single = CSng(GlControl2.ClientSize.Height)
                Dim imgAspect As Single = CSng(currentImage.Width) / CSng(currentImage.Height)
                Dim viewAspect As Single = viewW / viewH
                Dim baseScaleX As Single = If(viewAspect > imgAspect, imgAspect / viewAspect, 1.0F)
                Dim baseScaleY As Single = If(viewAspect > imgAspect, 1.0F, viewAspect / imgAspect)

                Dim actualDrawW As Single = viewW * baseScaleX * CSng(zoomFactor)
                Dim actualDrawH As Single = viewH * baseScaleY * CSng(zoomFactor)

                Dim dx As Single = (e.X - lastMousePos.X) / actualDrawW
                Dim dy As Single = (e.Y - lastMousePos.Y) / actualDrawH


                Select Case activeHandle
                    Case 0
                        cropRect.X += dx : cropRect.Width -= dx
                        cropRect.Height -= dy          ' flip
                    Case 1
                        cropRect.Width += dx
                        cropRect.Height -= dy          ' flip
                    Case 4
                        cropRect.Height -= dy          ' flip

                    Case 2
                        cropRect.Width += dx
                        cropRect.Y -= dy : cropRect.Height += dy   ' flip both
                    Case 3
                        cropRect.X += dx : cropRect.Width -= dx
                        cropRect.Y -= dy : cropRect.Height += dy   ' flip both
                    Case 6
                        cropRect.Y -= dy : cropRect.Height += dy   ' flip both

                    Case 5 : cropRect.Width += dx
                    Case 7 : cropRect.X += dx : cropRect.Width -= dx

                    Case 8 : cropRect.X += dx : cropRect.Y -= dy   ' flip move too
                End Select

                cropRect.Width = Math.Max(0.01F, cropRect.Width)
                cropRect.Height = Math.Max(0.01F, cropRect.Height)
                ConstrainCropRect()

                lastMousePos = e.Location
                GlControl2.Invalidate()
                Return
            End If

        Else

            ' ─── NEW ADDITION: TRACK DRAWN HORIZON LINE ───────────────────
            If isStraighteningMode AndAlso e.Button = MouseButtons.Left Then
                ' 1. Convert our tracking points from Control coordinates to screen-wide pixels
                Dim startScreen As Point = GlControl2.PointToScreen(lineStartPoint)
                Dim currentScreen As Point = GlControl2.PointToScreen(lineCurrentPoint)
                Dim newScreen As Point = GlControl2.PointToScreen(e.Location)

                ' 2. If we already drew a line on the previous pixel move, erase it first!
                ' (ReversibleLine works by flipping pixels; drawing it twice in the exact same spot erases it)
                If lineCurrentPoint <> Point.Empty Then
                    ControlPaint.DrawReversibleLine(startScreen, currentScreen, Color.Black)
                End If

                ' 3. Save the brand new mouse position
                lineCurrentPoint = e.Location

                ' 4. Draw the new line live right onto the screen!
                ControlPaint.DrawReversibleLine(startScreen, newScreen, Color.Black)

                Return ' Exit early to explicitly bypass and suppress standard panning
            End If
            ' ───────────────────────────────────────────────────────────────


            ' --- 3. STANDARD PANNING ---
            If isPanning Then
                panX += (e.X - lastMousePos.X)
                panY += (e.Y - lastMousePos.Y)
                lastMousePos = e.Location
                GlControl2.Invalidate()
            End If

        End If

    End Sub

    Private Function ScreenToNormalized(pt As Point) As PointF
        ' 1. Identify the center of the control
        Dim midX As Single = GlControl2.Width / 2.0F
        Dim midY As Single = GlControl2.Height / 2.0F

        ' 2. Determine the "On-Screen" dimensions of the image
        ' This must match your GL rendering logic exactly [cite: 2026-02-14]
        Dim imgWidth As Single = GlControl2.Width * currentZoom
        Dim imgHeight As Single = GlControl2.Height * currentZoom

        ' 3. Find the Top-Left corner of the image in Screen Pixels
        ' We use panX/panY (the pixel offsets) to find where the image starts
        Dim startX As Single = midX + panX - (imgWidth / 2.0F)
        Dim startY As Single = midY + panY - (imgHeight / 2.0F)

        ' 4. Calculate the relative (Normalized) position
        ' result = (MousePos - ImageStart) / ImageSize
        Dim normX As Single = (pt.X - startX) / imgWidth
        Dim normY As Single = (pt.Y - startY) / imgHeight

        ' 5. Clamp to 0-1 range so the crop stays inside the image boundaries
        Return New PointF(Math.Clamp(normX, 0.0F, 1.0F), Math.Clamp(normY, 0.0F, 1.0F))
    End Function

    Private Sub ResizeCropRect(normPos As PointF)
        ' Clamp the mouse position to the image boundaries
        Dim nX As Single = Math.Clamp(normPos.X, 0.0F, 1.0F)
        Dim nY As Single = Math.Clamp(normPos.Y, 0.0F, 1.0F)

        Select Case activeHandle
            Case 0 ' Top-Left
                cropRect = RectangleF.FromLTRB(nX, nY, cropRect.Right, cropRect.Bottom)
            Case 1 ' Top-Middle
                cropRect = RectangleF.FromLTRB(cropRect.Left, nY, cropRect.Right, cropRect.Bottom)
            Case 2 ' Top-Right
                cropRect = RectangleF.FromLTRB(cropRect.Left, nY, nX, cropRect.Bottom)
            Case 3 ' Right-Middle
                cropRect = RectangleF.FromLTRB(cropRect.Left, cropRect.Top, nX, cropRect.Bottom)
            Case 4 ' Bottom-Right
                cropRect = RectangleF.FromLTRB(cropRect.Left, cropRect.Top, nX, nY)
            Case 5 ' Bottom-Middle
                cropRect = RectangleF.FromLTRB(cropRect.Left, cropRect.Top, cropRect.Right, nY)
            Case 6 ' Bottom-Left
                cropRect = RectangleF.FromLTRB(nX, cropRect.Top, cropRect.Right, nY)
            Case 7 ' Left-Middle
                cropRect = RectangleF.FromLTRB(nX, cropRect.Top, cropRect.Right, cropRect.Bottom)
            Case 8 ' Move Entire Box (Optional Logic)
                ' This would require calculating the delta of the normalized mouse
        End Select

        ' Sanity Check: Prevent the rectangle from flipping inside out
        If cropRect.Width < 0.01F Then cropRect.Width = 0.01F
        If cropRect.Height < 0.01F Then cropRect.Height = 0.01F
    End Sub


    Private Sub GL_MouseUp(sender As Object, e As MouseEventArgs) Handles GlControl2.MouseUp
        ' ────────────────────────────────────────────────────────────────
        ' Release mouse capture FIRST — prevents many "stuck drag" issues
        ' ────────────────────────────────────────────────────────────────
        GlControl2.Capture = False

        ' ────────────────────────────────────────────────────────────────
        ' Reset all drag-related states
        ' ────────────────────────────────────────────────────────────────
        activeHandle = -1
        isWatermarkMoving = False
        isPanning = False

        ' Reset cursor to default (important visual feedback)
        GlControl2.Cursor = Cursors.Default

        ' ────────────────────────────────────────────────────────────────
        ' Optional: If you want to force crop mode off on right-click or similar,
        ' you can add conditional logic here — but usually not needed.
        ' ────────────────────────────────────────────────────────────────


        ' ─── NEW ADDITION: HORIZON STRAIGHTEN CALCULATION ───────────────
        ' ─── FINAL BULLETPROOF STRAGHTEN CALCULATION ───
        If isStraighteningMode Then
            Dim dx As Single = e.X - lineStartPoint.X
            Dim dy As Single = e.Y - lineStartPoint.Y

            If Math.Abs(dx) > 2 OrElse Math.Abs(dy) > 2 Then
                Dim radians As Single = CSng(Math.Atan2(dy, dx))
                Dim angleDegrees As Single = radians * (180.0F / CSng(Math.PI))

                previousRotation = currentRotation
                currentRotation = -angleDegrees

                ' --- MAX INNER BOX MATH ---
                Dim absRad As Double = Math.Abs(radians)
                Dim aspect As Double = CSng(currentImage.Width) / CSng(currentImage.Height)

                Dim sinA As Double = Math.Sin(absRad)
                Dim cosA As Double = Math.Cos(absRad)

                ' Calculate the tight scale factor required for true inward corner trapping
                If aspect >= 1.0 Then
                    currentAutoFillScale = CSng(cosA + (sinA * aspect))
                Else
                    currentAutoFillScale = CSng(cosA + (sinA / aspect))
                End If

                ' Safety threshold
                If currentAutoFillScale < 1.0F Then currentAutoFillScale = 1.0F

                ' 🟢 Note: We do NOT touch cropRect here anymore! This stops the over-zooming.

                lineStartPoint = Point.Empty
                lineCurrentPoint = Point.Empty
                isStraighteningMode = False

                btn_StraightenPic.Enabled = False
                btn_UndoRotate.Enabled = True

            End If
        End If
        ' ────────────────────────────────────────────────────────────────



        ' ────────────────────────────────────────────────────────────────
        ' Your existing color picker copy logic
        ' ────────────────────────────────────────────────────────────────
        Dim cleanHex As String = lbl_Hex.Text.Replace("HEX:", "").Trim()
        If cleanHex.StartsWith("#") Then cleanHex = cleanHex.Substring(1)  ' safer

        txt_HEXA_READ.Text = cleanHex

        If txt_HEXA_READ.Text.Length = 6 OrElse txt_HEXA_READ.Text.Length = 8 Then
            Try
                pb_HEXA_READ.BackColor = ColorTranslator.FromHtml("#" & txt_HEXA_READ.Text)
            Catch ex As Exception
                ' Silent fail or log — don't crash on bad hex
                pb_HEXA_READ.BackColor = Color.Empty
            End Try
        Else
            pb_HEXA_READ.BackColor = Color.Empty
        End If

        ' ────────────────────────────────────────────────────────────────
        ' Final render/update
        ' ────────────────────────────────────────────────────────────────
        GlControl2.Invalidate()   ' Tell OpenTK to repaint → shader sees everything
    End Sub

    Private Sub GL_MouseDoubleClick(sender As Object, e As MouseEventArgs)
        If currentImage Is Nothing Then Return

        zoomFactor = 1.0
        panX = 0
        panY = 0

        ClampPan()
        ' SEND TO GPU
        UpdateVertices()
        UpdateStatusBar()
        GlControl2.Invalidate()
    End Sub

    Private Sub GL_KeyDown(sender As Object, e As KeyEventArgs)

        If e.KeyCode = Keys.F Then
            FitToScreen()
        End If

    End Sub

    ' Mouse Actions in the GL Viewport ================================

    Private Sub GL_Resize(sender As Object, e As EventArgs) Handles GlControl2.Resize
        If GlControl2.ClientSize.Width = 0 Or GlControl2.ClientSize.Height = 0 Then Return

        GlControl2.MakeCurrent()

        ' 1. Re-run your FBO initialization with the new dimensions.
        ' This correctly resizes fboTexture1 and fboTexture2.
        InitFBO(GlControl2.ClientSize.Width, GlControl2.ClientSize.Height)

        ' 2. Update Viewport
        GL.Viewport(0, 0, GlControl2.ClientSize.Width, GlControl2.ClientSize.Height)

        ' 3. Recalculate your FitToScreen logic
        If Not _isInitializing Then
            FitToScreen()
        End If

        GlControl2.Invalidate()
    End Sub

    Private Sub GL_Paint(sender As Object, e As PaintEventArgs) Handles GlControl2.Paint
        If mainShader Is Nothing Then Return


        Check_For_Changes()

        GlControl2.MakeCurrent()

        ' 1. Clear background
        GL.ClearColor(Color.FromArgb(30, 30, 30))
        GL.Clear(ClearBufferMask.ColorBufferBit Or ClearBufferMask.DepthBufferBit)

        If currentImage IsNot Nothing Then
            ' --- STEP 1: RENDER THE PHOTO ---
            ' This will now use uBypass internally to skip colors/crop
            RenderPhotoToBuffer1()

            ' --- STEP 2: CONDITIONAL TEXT RENDER ---
            If isShowingOriginal Then
                ' If we are showing "Before", we clear the Text FBO so it's empty
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, fboTextBuffer)
                GL.ClearColor(0, 0, 0, 0)
                GL.Clear(ClearBufferMask.ColorBufferBit)
            Else
                ' Only render text if we are in "After" (Edited) mode
                RenderTextToBuffer2()
            End If

            ' --- STEP 3: COMBINE ---
            DrawFinalComposite()

            ' --- STEP 4: REFRESH DATA ---
            ' Usually, we don't update the histogram during a "Before" hold 
            ' to save performance and prevent flickering.
            If Not isShowingOriginal Then RefreshHistogramUI()
        End If

        GlControl2.SwapBuffers()

        ' ─── NEW ADDITION: DRAW THE INTERRUPTED GUIDELINE LIVE ─────────────────
        ' ─── FIX: USE THE NATIVE PaintEventArgs TO OVERLAY THE LINE ───────────
        If isStraighteningMode AndAlso lineStartPoint <> Point.Empty AndAlso lineCurrentPoint <> Point.Empty Then
            ' 🟢 Using e.Graphics ensures WinForms draws the line reliably over the control
            Dim g As Graphics = e.Graphics
            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

            Using linePen As New Pen(Color.Yellow, 2.0F)
                linePen.DashStyle = Drawing2D.DashStyle.Dash
                g.DrawLine(linePen, lineStartPoint, lineCurrentPoint)
            End Using

            Using anchorBrush As New SolidBrush(Color.Yellow)
                g.FillEllipse(anchorBrush, lineStartPoint.X - 3, lineStartPoint.Y - 3, 6, 6)
            End Using
        End If
        ' ───────────────────────────────────────────────────────────────────────
    End Sub

    Private Sub RenderPhotoToBuffer1()
        If mainShader Is Nothing OrElse currentImage Is Nothing Then Return

        GL.BindFramebuffer(FramebufferTarget.Framebuffer, fboPhoto)
        ' Viewport must match the FBO size (usually GlControl size)
        GL.Viewport(0, 0, GlControl2.Width, GlControl2.Height)
        GL.ClearColor(Color.Black)
        GL.Clear(ClearBufferMask.ColorBufferBit Or ClearBufferMask.DepthBufferBit)




        ' 1. Get Viewport size
        Dim viewW As Single = CSng(GlControl2.ClientSize.Width)
        Dim viewH As Single = CSng(GlControl2.ClientSize.Height)

        ' 2. DECLARE and ASSIGN (Dynamic Choice)
        ' We use the names the rest of your code expects: imgW and imgH
        Dim imgW, imgH As Single

        If isShowingOriginal AndAlso originalTextureID <> -1 Then
            ' Use the "Time Machine" dimensions for the vertex box
            imgW = CSng(origW)
            imgH = CSng(origH)
        Else
            ' Use the current edited/cropped dimensions for the vertex box
            imgW = CSng(currentImage.Width)
            imgH = CSng(currentImage.Height)
        End If

        ' 3. ASPECT RATIO MATH (Now using the dynamic imgW/imgH)
        Dim viewAspect As Single = viewW / viewH
        Dim imgAspect As Single = imgW / imgH

        ' --- Fit-to-screen logic ---
        Dim baseScaleX As Single = 1.0F
        Dim baseScaleY As Single = 1.0F

        If viewAspect > imgAspect Then
            baseScaleX = imgAspect / viewAspect
        Else
            baseScaleY = viewAspect / imgAspect
        End If

        ' 4. VERTICES (halfW/halfH)
        Dim halfW As Single = baseScaleX * CSng(zoomFactor)
        Dim halfH As Single = baseScaleY * CSng(zoomFactor)


        Dim ndcPanX As Single = (CSng(panX) * 2.0F) / viewW
        Dim ndcPanY As Single = (CSng(panY) * -2.0F) / viewH

        Dim sharedVertices As Single() = {
        (-halfW) + ndcPanX, (-halfH) + ndcPanY, 0.0F, 0.0F, 0.0F,
         (halfW) + ndcPanX, (-halfH) + ndcPanY, 0.0F, 1.0F, 0.0F,
         (halfW) + ndcPanX, (halfH) + ndcPanY, 0.0F, 1.0F, 1.0F,
        (-halfW) + ndcPanX, (-halfH) + ndcPanY, 0.0F, 0.0F, 0.0F,
         (halfW) + ndcPanX, (halfH) + ndcPanY, 0.0F, 1.0F, 1.0F,
        (-halfW) + ndcPanX, (halfH) + ndcPanY, 0.0F, 0.0F, 1.0F
    }

        mainShader.Use()

        ' Texture Slot 0: Base Image
        GL.ActiveTexture(TextureUnit.Texture0)


        If isShowingOriginal AndAlso originalTextureID <> -1 Then
            GL.BindTexture(TextureTarget.Texture2D, originalTextureID)
            GL.Uniform1(GL.GetUniformLocation(mainShader.Handle, "uFlipY"), 1)
            ' IMPORTANT: Turn off crop in shader for "Before"
            GL.Uniform1(GL.GetUniformLocation(mainShader.Handle, "uIsCropApplied"), 0)
        Else
            GL.BindTexture(TextureTarget.Texture2D, textureID)
            GL.Uniform1(GL.GetUniformLocation(mainShader.Handle, "uFlipY"), 0)
            ' Re-enable crop for "After"
            GL.Uniform1(GL.GetUniformLocation(mainShader.Handle, "uIsCropApplied"), If(uIsCropApplied, 1, 0))
        End If



        GL.Uniform1(GL.GetUniformLocation(mainShader.Handle, "mainTexture"), 0)

        ' Texture Slot 1: 3D LUT
        If lutTextureID <> 0 Then
            GL.ActiveTexture(TextureUnit.Texture1)
            GL.BindTexture(TextureTarget.Texture3D, lutTextureID)
            GL.Uniform1(GL.GetUniformLocation(mainShader.Handle, "lutTexture"), 1)
            GL.Uniform1(GL.GetUniformLocation(mainShader.Handle, "useLut"), If(useLut, 1, 0))
        End If

        ' Crop Uniforms
        Dim aspectMultiplierW As Single = If(uIsCropApplied, If(cropRect.Width > 1.0F, cropRect.Width / imgW, cropRect.Width), 1.0F)
        Dim aspectMultiplierH As Single = If(uIsCropApplied, If(cropRect.Height > 1.0F, cropRect.Height / imgH, cropRect.Height), 1.0F)

        GL.Uniform1(GL.GetUniformLocation(mainShader.Handle, "uIsCropping"), uIsCropping)
        GL.Uniform1(GL.GetUniformLocation(mainShader.Handle, "uIsCropApplied"), If(uIsCropApplied, 1, 0))
        GL.Uniform4(GL.GetUniformLocation(mainShader.Handle, "uCropRect"),
       If(cropRect.X > 1.0F, cropRect.X / imgW, cropRect.X),
       If(cropRect.Y > 1.0F, cropRect.Y / imgH, cropRect.Y),
       aspectMultiplierW, aspectMultiplierH)

        UseTheUniforms(mainShader.Handle)


        '' ─── NEW REGISTERED MATRIX PASS ──────────────────────────────────────────
        ' 1. Calculate the rotation matrix using OpenTK's math library
        Dim rotationMatrix As Matrix4 = Matrix4.Identity
        Dim shouldRotate As Boolean = (Not isShowingOriginal AndAlso currentRotation <> 0.0F)

        If shouldRotate Then
            ' Convert degrees to radians safely
            Dim radians As Single = currentRotation * (CSng(Math.PI) / 180.0F)
            rotationMatrix = Matrix4.CreateRotationZ(radians)
        End If

        ' 2. Send the boolean flag to the fragment shader
        If uLevelHorizonLoc <> -1 Then
            GL.Uniform1(uLevelHorizonLoc, If(shouldRotate, 1, 0))
        End If

        ' 3. Send the transformation matrix to the fragment shader
        If uRotationMatrixLoc <> -1 Then
            GL.UniformMatrix4(uRotationMatrixLoc, False, rotationMatrix)
        End If
        ' ─────────────────────────────────────────────────────────────────────────


        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo)
        GL.BufferData(BufferTarget.ArrayBuffer, sharedVertices.Length * 4, sharedVertices, BufferUsageHint.DynamicDraw)
        GL.BindVertexArray(vao)
        GL.DrawArrays(PrimitiveType.Triangles, 0, 6)

        'MsgBox("no.1")
        ' Inside RenderPhotoToBuffer1, at the bottom:
        'System.Diagnostics.Debug.WriteLine($"Buffer1")
        Dim p1(3) As Byte
        GL.ReadPixels(GlControl2.Width / 2, GlControl2.Height / 2, 1, 1,
                      OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, p1)
        'System.Diagnostics.Debug.WriteLine($"[PHOTO BUFFER] Center Pixel -> R:{p1(0)} G:{p1(1)} B:{p1(2)} A:{p1(3)}")


    End Sub

    Private Sub RenderTextToBuffer2()
        ' 1. Target the Text FBO
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, fboTextBuffer)
        GL.Viewport(0, 0, GlControl2.Width, GlControl2.Height)

        ' Clear to total transparency (0,0,0,0)
        GL.ClearColor(0.0F, 0.0F, 0.0F, 0.0F)
        GL.Clear(ClearBufferMask.ColorBufferBit)

        ' Guard Clause
        If Not useText OrElse _textTextureID = -1 Then Return

        ' 2. RE-CALCULATE POSITIONING (Matches Photo)
        Dim viewW As Single = CSng(GlControl2.ClientSize.Width)
        Dim viewH As Single = CSng(GlControl2.ClientSize.Height)
        Dim imgW As Single = CSng(currentImage.Width)
        Dim imgH As Single = CSng(currentImage.Height)


        ' 1. Calculate the ratios
        Dim viewAspect As Single = viewW / viewH
        Dim imgAspect As Single = imgW / imgH

        ' 2. Determine base scale to prevent stretching (Pillarbox/Letterbox logic)
        Dim baseScaleX As Single = 1.0F
        Dim baseScaleY As Single = 1.0F

        If viewAspect > imgAspect Then
            ' Window is wider than photo (Pillarbox)
            baseScaleX = imgAspect / viewAspect
        Else
            ' Window is taller than photo (Letterbox)
            baseScaleY = viewAspect / imgAspect
        End If

        ' 3. Calculate final half-dimensions using baseScale and zoomFactor
        ' This ensures zoomFactor 1.0 = Proportional Fit
        Dim halfW As Single = baseScaleX * CSng(zoomFactor)
        Dim halfH As Single = baseScaleY * CSng(zoomFactor)



        Dim ndcPanX As Single = (CSng(panX) * 2.0F) / viewW
        Dim ndcPanY As Single = (CSng(panY) * -2.0F) / viewH


        ' Convert your 0-1 text position to -1 to 1 space for the shader
        ' We multiply by 2.0 and subtract 1.0 to map 0.5 (center) to 0.0 (center)


        ' --- REVERSED Y-AXIS PAN FOR TEXT LAYER ---
        Dim sharedVertices As Single() = {
            (-halfW) + ndcPanX, (-halfH) - ndcPanY, 0.0F, 0.0F, 0.0F,
             (halfW) + ndcPanX, (-halfH) - ndcPanY, 0.0F, 1.0F, 0.0F,
             (halfW) + ndcPanX, (halfH) - ndcPanY, 0.0F, 1.0F, 1.0F,
            (-halfW) + ndcPanX, (-halfH) - ndcPanY, 0.0F, 0.0F, 0.0F,
             (halfW) + ndcPanX, (halfH) - ndcPanY, 0.0F, 1.0F, 1.0F,
            (-halfW) + ndcPanX, (halfH) - ndcPanY, 0.0F, 0.0F, 1.0F
        }


        ' 3. SETUP THE TEXT SHADER
        textShader.Use()

        ' --- NEW: PASS THE MVP MATRIX ---
        ' This ensures the text scales/fits exactly like the photo
        Dim uMvpLocation As Integer = GL.GetUniformLocation(textShader.Handle, "uFinalMvp")
        If uMvpLocation <> -1 Then
            ' If you are using the Identity matrix in DrawFinalComposite, use Identity here too.
            ' If you are using CreateScale in the final pass, match it here!
            GL.UniformMatrix4(uMvpLocation, False, Matrix4.Identity)
        End If

        ' Texture Slot 0: The Text Bitmap
        GL.ActiveTexture(TextureUnit.Texture0)
        GL.BindTexture(TextureTarget.Texture2D, _textTextureID)
        GL.Uniform1(GL.GetUniformLocation(textShader.Handle, "uTextTex"), 0)

        ' Text Properties
        GL.Uniform1(GL.GetUniformLocation(textShader.Handle, "uTextOpacity"), CSng(sldTextOpacity.Value / 100.0F))

        ' 4. DRAW WITH BLENDING
        ' Essential so the text doesn't have a black box around it inside the FBO
        GL.Enable(EnableCap.Blend)
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha)

        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo)
        GL.BufferData(BufferTarget.ArrayBuffer, sharedVertices.Length * 4, sharedVertices, BufferUsageHint.DynamicDraw)
        GL.BindVertexArray(vao)
        GL.DrawArrays(PrimitiveType.Triangles, 0, 6)

        GL.Disable(EnableCap.Blend)
        GL.BindVertexArray(0)

        ' --- DEBUG LOGGING ---
        'System.Diagnostics.Debug.WriteLine($"[TEXT BUFFER] Rendered with ID: {_textTextureID}")
    End Sub


    Private Sub DrawFinalComposite()
        ' 1. Target the actual screen
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0)
        GL.Viewport(0, 0, GlControl2.Width, GlControl2.Height)

        ' 2. Clear the screen once
        GL.ClearColor(0.07F, 0.07F, 0.07F, 1.0F)
        GL.Clear(ClearBufferMask.ColorBufferBit Or ClearBufferMask.DepthBufferBit)

        ' 3. Use Shader
        screenShader.Use()
        Dim sH = screenShader.Handle

        ' --- FIX STRETCHING ---
        ' We send an Identity matrix for now, or you can use the same 
        ' aspect-ratio math we use for the photo to keep it perfectly centered.
        Dim uMvpLocation As Integer = GL.GetUniformLocation(sH, "uFinalMvp")
        If uMvpLocation <> -1 Then
            Dim identity As Matrix4 = Matrix4.Identity
            GL.UniformMatrix4(uMvpLocation, False, identity)
        End If

        ' 4. Bind the "Sandwich" Layers
        ' Photo -> Unit 0
        GL.ActiveTexture(TextureUnit.Texture0)
        GL.BindTexture(TextureTarget.Texture2D, fboTexture1)
        GL.Uniform1(GL.GetUniformLocation(sH, "photoTex"), 0)

        ' Text -> Unit 1 
        GL.ActiveTexture(TextureUnit.Texture1)
        GL.BindTexture(TextureTarget.Texture2D, fboTexture2)
        GL.Uniform1(GL.GetUniformLocation(sH, "textTex"), 1)

        ' 5. Draw the Full-Screen Quad using the Global VAO
        GL.BindVertexArray(vao)

        ' Ensure the Quad vertices are sent (Standard -1 to 1)
        Dim quadVertices As Single() = {
        -1.0F, -1.0F, 0.0F, 0.0F, 0.0F,
         1.0F, -1.0F, 0.0F, 1.0F, 0.0F,
         1.0F, 1.0F, 0.0F, 1.0F, 1.0F,
        -1.0F, -1.0F, 0.0F, 0.0F, 0.0F,
         1.0F, 1.0F, 0.0F, 1.0F, 1.0F,
        -1.0F, 1.0F, 0.0F, 0.0F, 1.0F
    }
        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo)
        GL.BufferData(BufferTarget.ArrayBuffer, quadVertices.Length * 4, quadVertices, BufferUsageHint.StreamDraw)

        GL.DrawArrays(PrimitiveType.Triangles, 0, 6)

        ' IMPORTANT: Do NOT call SwapBuffers here. Let GL_Paint do it at the very end.
    End Sub



    Private Sub UseTheUniforms(ByVal progID As Integer)
        'xupdate4
        ' This is the fix for the loading exposure:
        ' We force the current exposureValue into the shader before drawing.
        Dim uExposureLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uExposure")
        GL.Uniform1(uExposureLoc, CSng(CurrentParams.Exposure))

        Dim uContrastLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uContrast")
        GL.Uniform1(uContrastLoc, CSng(CurrentParams.Contrast))

        Dim uHighlightsLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uHighlights")
        GL.Uniform1(uHighlightsLoc, CSng(CurrentParams.Highlights))

        Dim uShadowsLiftLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uShadowsLift")
        GL.Uniform1(uShadowsLiftLoc, CSng(CurrentParams.ShadowsLift))

        Dim uWhitesLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uWhites")
        GL.Uniform1(uWhitesLoc, CSng(CurrentParams.Whites))

        Dim uBlacksLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uBlacks")
        GL.Uniform1(uBlacksLoc, CSng(CurrentParams.Blacks))

        Dim uTempLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uTemp")
        GL.Uniform1(uTempLoc, CSng(CurrentParams.Temp))

        Dim uTintLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uTint")
        GL.Uniform1(uTintLoc, CSng(CurrentParams.Tint))

        Dim uBarrelDistortionLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uBarrelDistortion")
        GL.Uniform1(uBarrelDistortionLoc, CSng(CurrentParams.BarrelDistortion))

        Dim uDynConLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uDynCon")
        GL.Uniform1(uDynConLoc, CSng(CurrentParams.DynCon))

        Dim uClarityLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uClarity")
        GL.Uniform1(uClarityLoc, CSng(CurrentParams.Clarity))

        Dim uStructXLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uStructX")
        GL.Uniform1(uStructXLoc, CSng(CurrentParams.StructX))

        Dim uSharpenLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uSharpen")
        GL.Uniform1(uSharpenLoc, CSng(CurrentParams.Sharpen))

        Dim uDefringeLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uDefringe")
        GL.Uniform1(uDefringeLoc, CSng(CurrentParams.Defringe))

        Dim uDenoiseLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uDenoise")
        GL.Uniform1(uDenoiseLoc, CSng(CurrentParams.Denoise))

        Dim uNoiseLimitLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uNoiseLimit")
        GL.Uniform1(uNoiseLimitLoc, CSng(CurrentParams.NoiseLimit))

        Dim uNoiseChromaLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uNoiseChroma")
        GL.Uniform1(uNoiseChromaLoc, CSng(CurrentParams.NoiseChroma))

        Dim uNoiseLumaLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uNoiseLuma")
        GL.Uniform1(uNoiseLumaLoc, CSng(CurrentParams.NoiseLuma))

        Dim uSaturationLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uSaturation")
        GL.Uniform1(uSaturationLoc, CSng(CurrentParams.Saturation))

        Dim uVibranceLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uVibrance")
        GL.Uniform1(uVibranceLoc, CSng(CurrentParams.Vibrance))

        Dim uRedMixLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uRedMix")
        GL.Uniform1(uRedMixLoc, CSng(CurrentParams.RedMix))

        Dim uGreenMixLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uGreenMix")
        GL.Uniform1(uGreenMixLoc, CSng(CurrentParams.GreenMix))

        Dim uBlueMixLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uBlueMix")
        GL.Uniform1(uBlueMixLoc, CSng(CurrentParams.BlueMix))

        Dim uDeHazeLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uDeHaze")
        GL.Uniform1(uDeHazeLoc, CSng(CurrentParams.DeHaze))

        Dim uVignetteLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uVignette")
        GL.Uniform1(uVignetteLoc, CSng(CurrentParams.Vignette))

        Dim uVignetteStdLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uVignetteStd")
        GL.Uniform1(uVignetteStdLoc, CSng(CurrentParams.VignetteStd))

        Dim uFilmGrainLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uFilmGrain")
        GL.Uniform1(uFilmGrainLoc, CSng(CurrentParams.FilmGrain))

        Dim uGrainSizeLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uGrainSize")
        GL.Uniform1(uGrainSizeLoc, CSng(CurrentParams.GrainSize))

        Dim uVividLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uVivid")
        GL.Uniform1(uVividLoc, CSng(CurrentParams.Vivid))

        Dim uSepiaMixLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uSepiaMix")
        GL.Uniform1(uSepiaMixLoc, CSng(CurrentParams.SepiaMix))

        Dim uNoirFactorLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uNoirFactor")
        GL.Uniform1(uNoirFactorLoc, CSng(CurrentParams.NoirFactor))

        Dim uSplitToningLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uSplitToning")
        GL.Uniform1(uSplitToningLoc, CSng(CurrentParams.SplitToning))

        Dim uFadeLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uFade")
        GL.Uniform1(uFadeLoc, CSng(CurrentParams.Fade))

        Dim uFadeTintLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uFadeTint")
        GL.Uniform1(uFadeTintLoc, CSng(CurrentParams.FadeTint))

        Dim uCinematicLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uCinematic")
        GL.Uniform1(uCinematicLoc, CSng(CurrentParams.Cinematic))

        Dim uCineBalanceLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uCineBalance")
        GL.Uniform1(uCineBalanceLoc, CSng(CurrentParams.CineBalance))

        Dim uTextureXLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uTextureX")
        GL.Uniform1(uTextureXLoc, CSng(CurrentParams.TextureX))

        Dim uLutIntensLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uLutIntens")
        GL.Uniform1(uLutIntensLoc, CSng(CurrentParams.LutIntens))


        ' --- 1. Colors (Using your conversion logic) ---
        ' Convert text to vectors first
        Dim vTarget = HexToVec3(txtHexa_Corect.Text)
        Dim vReplace = HexToVec3(txtCorrectedHexa_Correct.Text)

        ' Send them to the GPU
        Dim uTargetRGBLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uTargetRGB")
        GL.Uniform3(uTargetRGBLoc, vTarget.X, vTarget.Y, vTarget.Z)

        Dim uReplaceRGBLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uReplaceRGB")
        GL.Uniform3(uReplaceRGBLoc, vReplace.X, vReplace.Y, vReplace.Z)

        ' --- 2. Sliders (Using CurrentParams) ---
        Dim uRadiusLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uRadius")
        GL.Uniform1(uRadiusLoc, CSng(CurrentParams.HexaRadius))

        Dim uSmoothnessLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uSmoothness")
        GL.Uniform1(uSmoothnessLoc, CSng(CurrentParams.HexaSmoothness))

        Dim uSelectionRangeLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uSelectionRange")
        GL.Uniform1(uSelectionRangeLoc, CSng(CurrentParams.HexaRange))

        Dim uOpacityLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uOpacity")
        GL.Uniform1(uOpacityLoc, CSng(CurrentParams.HexaOpacity))


        GL.Uniform3(uRectangleSelColorLoc, selR, selG, selB)


        ' 1. Send the U,V center of the click
        Dim uCenterLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uSelectionCenter")
        GL.Uniform2(uCenterLoc, selectionU, selectionV)

        ' 2. Send the Selection Area Radius (The "Limit")
        ' You can use a fixed value like 0.15 for a medium circle, 
        ' or eventually link this to a 3rd slider!





        ' --- CLIPPING OVERLAY UNIFORMS ---
        ' These sync with the boolean variables toggled by your triangle click events
        Dim uShowShadowsLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uShowShadows")
        GL.Uniform1(uShowShadowsLoc, If(showShadowClipping, 1, 0))

        Dim uShowHighlightsLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uShowHighlights")
        GL.Uniform1(uShowHighlightsLoc, If(showHighlightClipping, 1, 0))

        ' --- BLINKING TIME SYNC ---
        Dim uTimeLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uTime")
        ' Convert milliseconds to seconds for a smooth float value
        GL.Uniform1(uTimeLoc, CSng(Environment.TickCount / 1000.0F))

        Dim uIsCropLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uIsCropping")
        GL.Uniform1(uIsCropLoc, If(isSelectingCrop, 1, 0))

        If isSelectingCrop Then
            Dim uCropLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uCropRect")
            ' Pass the RectangleF values: X, Y, Width, Height
            GL.Uniform4(uCropLoc, cropRect.X, cropRect.Y, cropRect.Width, cropRect.Height)
        End If




        ' --- Text System Locations & Uniforms ---
        ' 1. uUseText: The On/Off switch based on if there is text
        Dim uUseTextLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uUseText")


        GL.Uniform1(uUseTextLoc, If(txtWatermark.Text.Length > 0, 1, 0))

        ' 2. uTextOpacity: The visibility (1.0 = solid)
        Dim uTextOpacityLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uTextOpacity")
        If uTextOpacityLoc <> -1 Then
            ' Convert 0-100 slider to 0.0-1.0 float
            Dim opValue As Single = CSng(sldTextOpacity.Value / 100.0F)
            GL.Uniform1(uTextOpacityLoc, opValue)
        End If

        ' 3. uTextTex: Telling the shader to look at Texture Slot 1
        Dim uTextTexLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uTextTex")
        GL.ActiveTexture(TextureUnit.Texture1)
        GL.BindTexture(TextureTarget.Texture2D, _textTextureID)
        GL.Uniform1(uTextTexLoc, 1) ' '1' refers to TextureUnit1



        ' --- BEFORE/AFTER BYPASS ---
        Dim uBypassLoc As Integer = GL.GetUniformLocation(progID, "uBypass")
        If uBypassLoc <> -1 Then
            GL.Uniform1(uBypassLoc, If(isShowingOriginal, 1, 0))
        End If



        ' ─── 🟢 EXACT INJECTION POSITION: HORIZON LEVELING UNIFORMS ─────────────
        ' 1. Declare and calculate the matrix transformations locally
        Dim rotationMatrix As Matrix4 = Matrix4.Identity
        Dim shouldRotate As Boolean = (Not isShowingOriginal AndAlso currentRotation <> 0.0F)

        If shouldRotate Then
            ' Convert degrees to radians smoothly
            Dim radians As Single = currentRotation * (CSng(Math.PI) / 180.0F)
            rotationMatrix = Matrix4.CreateRotationZ(radians)
        End If

        ' 2. Send the boolean execution flag to the shader
        Dim uLevelHorizonLoc As Integer = GL.GetUniformLocation(progID, "uLevelHorizon")
        If uLevelHorizonLoc <> -1 Then
            GL.Uniform1(uLevelHorizonLoc, If(shouldRotate, 1, 0))
        End If

        ' 3. Send the complete transformation matrix to the shader
        Dim uRotationMatrixLoc As Integer = GL.GetUniformLocation(progID, "uRotationMatrix")
        If uRotationMatrixLoc <> -1 Then
            GL.UniformMatrix4(uRotationMatrixLoc, False, rotationMatrix)
        End If

        ' 4. Send the auto-fill scale uniform to the shader
        Dim uAutoFillScaleLoc As Integer = GL.GetUniformLocation(progID, "uAutoFillScale")
        If uAutoFillScaleLoc <> -1 Then
            ' Pass the calculated scale if rotating, otherwise keep it at 1.0F (normal size)
            GL.Uniform1(uAutoFillScaleLoc, If(shouldRotate, currentAutoFillScale, 1.0F))
        End If
        ' ─────────────────────────────────────────────────────────────────────────

    End Sub


    Private Sub RefreshHistogramUI()
        Using tempBmp As Bitmap = GetProcessedBitmap()
            If tempBmp IsNot Nothing Then

                CalculateHistogram(tempBmp) ' Apply your 0.8F logic here [cite: 2026-03-03]
                UpdateHistogramFromViewport()
                Me.glHist.Refresh()      ' Force the histogram GL control to repaint

            End If
        End Using
    End Sub


    Private Sub itemCenter_Click(sender As Object, e As EventArgs) Handles itemCenter.Click
        panX = 0
        panY = 0

        ClampPan()
        GlControl2.Invalidate()
    End Sub

    Private Sub itemFit_Click(sender As Object, e As EventArgs) Handles itemFit.Click
        FitToScreen()
    End Sub

    Private Sub itemOriginal_Click(sender As Object, e As EventArgs) Handles itemOriginal.Click
        If currentImage Is Nothing Then Return

        ' 1. Calculate what Zoom is needed to achieve 1:1 Pixel Mapping
        ' Since your shader automatically "fits" the image to the window, 
        ' we need to reverse that fit to get to 1:1.
        Dim viewW As Single = CSng(GlControl2.ClientSize.Width)
        Dim viewH As Single = CSng(GlControl2.ClientSize.Height)
        Dim imgW As Single = CSng(currentImage.Width)
        Dim imgH As Single = CSng(currentImage.Height)

        Dim ratioX As Single = viewW / imgW
        Dim ratioY As Single = viewH / imgH
        Dim baseFitRatio As Single = Math.Min(ratioX, ratioY)

        ' To get 1:1, we divide 1.0 by the base fit ratio.
        ' Example: If Fit is 0.15, 1.0 / 0.15 = 6.66 (The zoom needed for 1:1)
        zoomFactor = 1.0F / baseFitRatio

        ' 2. Reset Pan
        panX = 0
        panY = 0


        UpdateStatusBar() ' This will now correctly show "Zoom : 100%"
        SyncAllUniforms()
        GlControl2.Invalidate()
    End Sub


    '====================================================================================================
    Private Sub CalculateHistogram(bmp As Bitmap)
        If bmp Is Nothing Or Save_All_PicText = True Then Return

        Dim hR(255), hG(255), hB(255), hL(255) As Integer
        Dim rect As New Rectangle(0, 0, bmp.Width, bmp.Height)
        Dim data = bmp.LockBits(rect, Imaging.ImageLockMode.ReadOnly, Imaging.PixelFormat.Format32bppArgb)

        Dim currentMax As Integer = 1

        Try
            Dim stride = data.Stride
            Dim bytes(stride * bmp.Height - 1) As Byte
            Marshal.Copy(data.Scan0, bytes, 0, bytes.Length)

            ' STEP 2: We sample 1/4 of the pixels (Every 2nd pixel in X and Y)
            ' This is visually identical but 4x faster.
            For y As Integer = 0 To bmp.Height - 1 Step 2
                Dim row = y * stride
                For x As Integer = 0 To bmp.Width - 1 Step 2
                    Dim i = row + (x * 4)
                    Dim b = bytes(i) : Dim g = bytes(i + 1) : Dim r = bytes(i + 2)

                    ' --- UI BACKGROUND SPIKE FIX ---

                    If r = 0 AndAlso g = 0 AndAlso b = 0 Then Continue For
                    If (r = 45 AndAlso g = 45 AndAlso b = 45) OrElse (r = 30 AndAlso g = 30 AndAlso b = 30) Then Continue For

                    hR(r) += 1 : hG(g) += 1 : hB(b) += 1

                    ' Rec. 601 Luma
                    Dim luma = CInt(0.299 * r + 0.587 * g + 0.114 * b)
                    hL(luma) += 1

                    ' --- FAST MAX TRACKING ---
                    ' We check the mid-range (5-250) right here to save a second loop
                    If r > 0 AndAlso r < 251 Then If hR(r) > currentMax Then currentMax = hR(r)
                    If g > 0 AndAlso g < 251 Then If hG(g) > currentMax Then currentMax = hG(g)
                    If b > 0 AndAlso b < 251 Then If hB(b) > currentMax Then currentMax = hB(b)
                    If luma > 0 AndAlso luma < 251 Then If hL(luma) > currentMax Then currentMax = hL(luma)
                Next
            Next
        Finally
            bmp.UnlockBits(data)
        End Try

        ' Apply the "98th Percentile" feel without the slow Sort
        ' Scaling by 0.85-0.9 makes the bars look full even with small spikes.
        Me.histMaxL = Math.Max(CInt(currentMax * 0.9), 1)

        ' Apply your [2026-03-03] Smoothing
        Me.histR = SmoothHistogram(hR, 5)
        Me.histG = SmoothHistogram(hG, 5)
        Me.histB = SmoothHistogram(hB, 5)
        Me.histL = SmoothHistogram(hL, 5)

        glHist.Invalidate()
    End Sub


    Private Sub DrawHistogramCurve(g As Graphics, hist() As Integer, color As Color, w As Integer, h As Integer, globalMax As Integer)
        If globalMax <= 0 Then Return

        ' Professional Margins
        Dim leftMargin As Integer = 6
        Dim rightMargin As Integer = 6
        Dim topMargin As Integer = 25
        Dim bottomMargin As Integer = 6

        Dim drawWidth As Integer = w - leftMargin - rightMargin
        Dim drawHeight As Integer = h - topMargin - bottomMargin
        Dim pts(255) As PointF

        For i As Integer = 0 To 255
            Dim x As Single = leftMargin + (i / 255.0F) * drawWidth

            ' 1. Calculate the raw ratio using Model 1's "Realistic" globalMax
            Dim ratio As Double = hist(i) / CDbl(globalMax)

            ' 2. Apply the 0.85 Gamma lift for midtone visibility
            ' 3. Apply the * 0.8F factor to keep the curve within the limits [cite: 2026-03-03]
            Dim norm As Single = CSng(Math.Pow(ratio, 0.85)) * 0.8F

            ' Clamp to ensure it never exceeds the topMargin
            norm = Math.Clamp(norm, 0.0F, 1.0F)

            ' 4. Calculate Y: topMargin is the "ceiling", drawHeight is the room available
            Dim y As Single = topMargin + (1.0F - norm) * drawHeight
            pts(i) = New PointF(x, y)
        Next

        Dim baselineY As Single = h - bottomMargin

        ' Fill and Stroke logic remains the same...
        Using brush As New SolidBrush(color)
            Dim fillPts = pts.Concat({
            New PointF(leftMargin + drawWidth, baselineY),
            New PointF(leftMargin, baselineY)
        }).ToArray()
            g.FillPolygon(brush, fillPts)
        End Using

        Using pen As New Pen(Color.FromArgb(220, color.R, color.G, color.B), 1.0F)
            g.DrawLines(pen, pts)
        End Using
    End Sub


    ' Helper function to find the stable peak for each channel
    Private Function GetChannelPercentile(hist() As Integer) As Integer
        ' Ignore edges (0-4 and 251-255) to avoid spike walls [cite: 2026-03-03]
        Dim values As New List(Of Integer)
        For i As Integer = 5 To 250
            values.Add(hist(i))
        Next

        Dim sorted = values.OrderBy(Function(v) v).ToArray()
        If sorted.Length = 0 Then Return 1

        ' Use 98th percentile [cite: 2026-03-03]
        Return Math.Max(sorted(CInt(sorted.Length * 0.98)), 1)
    End Function

    Private Function GetPercentile(hist() As Integer, percentile As Double) As Integer
        If hist Is Nothing Then Return 128
        Dim total As Long = 0
        For i As Integer = 0 To 255 : total += hist(i) : Next

        Dim target As Double = total * percentile
        Dim cumulative As Long = 0
        For i As Integer = 0 To 255
            cumulative += hist(i)
            If cumulative >= target Then Return i
        Next
        Return 255
    End Function

    Private Sub DrawFilledHistogram(g As Graphics, hist() As Integer, globalMax As Integer, fillColor As Color, lineColor As Color, w As Integer, h As Integer)
        If hist Is Nothing OrElse globalMax <= 0 Then Return

        ' Professional Margins to prevent hitting the edges
        Dim leftMargin As Integer = 4
        Dim topMargin As Integer = 15
        Dim drawWidth As Integer = w - (leftMargin * 2)
        Dim drawHeight As Integer = h - topMargin - 4

        Dim curvePoints As New List(Of PointF)
        For i As Integer = 0 To 255
            Dim x As Single = leftMargin + (i / 255.0F) * drawWidth

            ' Use GlobalMax for proportional heights (Reality Check)
            ' Apply 0.85 Power lift and 0.8F scaling [cite: 2026-03-03]
            Dim ratio As Double = hist(i) / CDbl(globalMax)
            Dim norm As Single = CSng(Math.Pow(ratio, 0.85)) * 0.8F
            norm = Math.Min(norm, 1.0F)

            Dim y As Single = h - 4 - (norm * drawHeight)
            curvePoints.Add(New PointF(x, y))
        Next

        ' Closing points for the fill [cite: 2026-02-14]
        Dim fillPoints = New List(Of PointF)(curvePoints)
        fillPoints.Add(New PointF(leftMargin + drawWidth, h - 4))
        fillPoints.Add(New PointF(leftMargin, h - 4))

        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        ' Fill with transparency to allow color overlapping (as in your goal image)
        Using brush As New SolidBrush(Color.FromArgb(90, fillColor))
            g.FillPolygon(brush, fillPoints.ToArray())
        End Using

        Using pen As New Pen(lineColor, 1.1F)
            g.DrawLines(pen, curvePoints.ToArray())
        End Using
    End Sub

    Private Function GetStableMax(hist() As Integer) As Integer
        ' Skip indices 0-5 and 250-255 to ignore edge spikes [cite: 2026-03-03]
        Dim midValues = hist.Skip(5).Take(245).OrderBy(Function(v) v).ToArray()
        If midValues.Length = 0 Then Return 1
        ' Return the 98th percentile value [cite: 2026-03-03]
        Return Math.Max(midValues(CInt(midValues.Length * 0.98)), 1)
    End Function

    Private Sub glHist_Paint(sender As Object, e As PaintEventArgs) Handles glHist.Paint
        Dim g As Graphics = e.Graphics
        Dim w As Integer = glHist.Width
        Dim h As Integer = glHist.Height

        ' Ensure we have a clean background
        g.Clear(Color.FromArgb(30, 30, 30))

        ' 2. DRAW LINES FIRST
        Using p As New Pen(Color.FromArgb(100, Color.Gray), 1.0F)
            Dim x1 As Single = w / 3.0F
            g.DrawLine(p, x1, 0, x1, h)

            Dim x2 As Single = (w * 2.0F) / 3.0F
            g.DrawLine(p, x2, 0, x2, h)
        End Using

        ' Guard: Exit if data isn't ready
        If histL Is Nothing OrElse histR Is Nothing Then Return

        ' --- DRAW CHANNELS BASED ON MODE ---
        ' histMode mapping: 0=All, 1=Luma(Sun), 2=Red, 3=Green, 4=Blue
        Select Case histMode
            Case 0 ' The Color Wheel (All Channels Overlapping)
                ' We draw them with some transparency so they mix visually
                DrawFilledHistogram(g, histR, Me.histMaxL, Color.FromArgb(120, Color.Red), Color.Red, w, h)
                DrawFilledHistogram(g, histG, Me.histMaxL, Color.FromArgb(120, Color.Green), Color.Green, w, h)
                DrawFilledHistogram(g, histB, Me.histMaxL, Color.FromArgb(120, Color.Blue), Color.Blue, w, h)

            Case 1 ' The Sun Icon (Luma)
                DrawFilledHistogram(g, histL, Me.histMaxL, Color.DimGray, Color.White, w, h)

            Case 2 ' Red Icon
                DrawFilledHistogram(g, histR, Me.histMaxL, Color.FromArgb(100, Color.Red), Color.Red, w, h)

            Case 3 ' Green Icon
                DrawFilledHistogram(g, histG, Me.histMaxL, Color.FromArgb(100, Color.Green), Color.Green, w, h)

            Case 4 ' Blue Icon
                DrawFilledHistogram(g, histB, Me.histMaxL, Color.FromArgb(100, Color.Blue), Color.DeepSkyBlue, w, h)
        End Select
    End Sub

    Private Function SmoothHistogram(hist() As Integer, windowSize As Integer) As Integer()
        Dim smoothed(255) As Integer
        Dim radius As Integer = windowSize \ 2

        For i As Integer = 0 To 255
            Dim sum As Long = 0
            Dim count As Integer = 0

            ' Average the neighbors [cite: 2026-02-14]
            For j As Integer = -radius To radius
                Dim idx As Integer = i + j
                If idx >= 0 AndAlso idx <= 255 Then
                    sum += hist(idx)
                    count += 1
                End If
            Next
            smoothed(i) = CInt(sum / count)
        Next
        Return smoothed
    End Function



    '=========================================================




    ' --- Hover effect for the Shadows Triangle (Left) ---
    Private Sub picShadows_MouseEnter(sender As Object, e As EventArgs) Handles picShadows.MouseEnter
        ' Lighten the background slightly or change to a discrete gray
        picShadows.BackColor = Color.FromArgb(60, 60, 60)
    End Sub

    Private Sub picShadows_MouseLeave(sender As Object, e As EventArgs) Handles picShadows.MouseLeave
        ' Only reset to transparent if the clipping is NOT active
        ' This prevents the "Active" color from being erased when moving the mouse away
        If Not showShadowClipping Then
            picShadows.BackColor = Color.Transparent
        Else
            picShadows.BackColor = Color.Blue ' Keep it lit if active
        End If
    End Sub

    ' --- Hover effect for the Highlights Triangle (Right) ---
    Private Sub picHighlights_MouseEnter(sender As Object, e As EventArgs) Handles picHighlights.MouseEnter
        picHighlights.BackColor = Color.FromArgb(60, 60, 60)
    End Sub

    Private Sub picHighlights_MouseLeave(sender As Object, e As EventArgs) Handles picHighlights.MouseLeave
        ' Same logic for the highlights button
        If Not showHighlightClipping Then
            picHighlights.BackColor = Color.Transparent
        Else
            picHighlights.BackColor = Color.Magenta ' Keep it lit if active
        End If
    End Sub

    Private Sub picShadows_Click(sender As Object, e As EventArgs) Handles picShadows.Click
        showShadowClipping = Not showShadowClipping

        ' High-visibility Shadow (Left Triangle)
        If showShadowClipping Then
            picShadows.BackColor = Color.Blue ' Or a bright Blue
        Else
            picShadows.BackColor = Color.Transparent
        End If

        GlControl2.Invalidate()
    End Sub

    Private Sub picHighlights_Click(sender As Object, e As EventArgs) Handles picHighlights.Click
        showHighlightClipping = Not showHighlightClipping

        ' High-visibility Highlight (Right Triangle)
        If showHighlightClipping Then
            picHighlights.BackColor = Color.Magenta ' Or a bright Red
        Else
            picHighlights.BackColor = Color.Transparent
        End If

        GlControl2.Invalidate()
    End Sub

    Private Sub tmrAnimate_Tick(sender As Object, e As EventArgs) Handles tmrAnimate.Tick
        ' Only redraw if we actually need to see the blinking animation
        If showShadowClipping OrElse showHighlightClipping Then
            ' This triggers your GL_Paint, which now uses uTime
            GlControl2.Invalidate()
        End If
    End Sub

    Private Sub OnSliderScroll(sender As Object, e As EventArgs)
        ' 1. Identity Check
        If TypeOf sender IsNot ModernSlider Then Return
        Dim slider As ModernSlider = DirectCast(sender, ModernSlider)

        ' 2. Tag Check
        Dim tag As String = If(slider.Tag?.ToString(), String.Empty)
        If String.IsNullOrWhiteSpace(tag) Then Return

        ' 3. Value Calculation
        Dim internalValue As Single = SliderToInternal(tag, slider.Value)

        ' 4. Update Parameters (Consistency: Keep existing additions) [cite: 2026-02-14]
        Select Case tag
            Case "Exposure" : CurrentParams.Exposure = internalValue
            Case "Contrast" : CurrentParams.Contrast = internalValue
            Case "Highlights" : CurrentParams.Highlights = internalValue
            Case "ShadowsLift" : CurrentParams.ShadowsLift = internalValue
            Case "Whites" : CurrentParams.Whites = internalValue
            Case "Blacks" : CurrentParams.Blacks = internalValue
            Case "Temp" : CurrentParams.Temp = internalValue
            Case "Tint" : CurrentParams.Tint = internalValue
            Case "BarrelDistortion" : CurrentParams.BarrelDistortion = internalValue
            Case "DynCon" : CurrentParams.DynCon = internalValue
            Case "Clarity" : CurrentParams.Clarity = internalValue
            Case "StructX" : CurrentParams.StructX = internalValue
            Case "TextureX" : CurrentParams.TextureX = internalValue
            Case "Sharpen" : CurrentParams.Sharpen = internalValue
            Case "Defringe" : CurrentParams.Defringe = internalValue
            Case "Denoise" : CurrentParams.Denoise = internalValue
            Case "NoiseLimit" : CurrentParams.NoiseLimit = internalValue
            Case "NoiseChroma" : CurrentParams.NoiseChroma = internalValue
            Case "NoiseLuma" : CurrentParams.NoiseLuma = internalValue
            Case "Saturation" : CurrentParams.Saturation = internalValue
            Case "Vibrance" : CurrentParams.Vibrance = internalValue
            Case "RedMix" : CurrentParams.RedMix = internalValue
            Case "GreenMix" : CurrentParams.GreenMix = internalValue
            Case "BlueMix" : CurrentParams.BlueMix = internalValue
            Case "LutIntens" : CurrentParams.LutIntens = internalValue
            Case "DeHaze" : CurrentParams.DeHaze = internalValue
            Case "Vignette" : CurrentParams.Vignette = internalValue
            Case "VignetteStd" : CurrentParams.VignetteStd = internalValue
            Case "SepiaMix" : CurrentParams.SepiaMix = internalValue
            Case "NoirFactor" : CurrentParams.NoirFactor = internalValue
            Case "Vivid" : CurrentParams.Vivid = internalValue
            Case "FilmGrain" : CurrentParams.FilmGrain = internalValue
            Case "GrainSize" : CurrentParams.GrainSize = internalValue
            Case "SplitToning" : CurrentParams.SplitToning = internalValue
            Case "Fade" : CurrentParams.Fade = internalValue
            Case "FadeTint" : CurrentParams.FadeTint = internalValue
            Case "Cinematic" : CurrentParams.Cinematic = internalValue
            Case "CineBalance" : CurrentParams.CineBalance = internalValue
            Case Else
                Return
        End Select

        ' 5. Update Text Label
        Dim txt = Me.Controls.Find("txt_" & tag, True).FirstOrDefault()
        If TypeOf txt Is TextBox Then
            DirectCast(txt, TextBox).Text = CInt(internalValue.ToString(ParamMappings(tag).Format) * 50)
        End If

        ' 6. GPU & UI Sync [cite: 2026-02-14]
        SyncAllUniforms()
        Me.GlControl2.Invalidate()

        ' 7. Histogram Refresh [cite: 2026-03-03]
        ' Recalculate the histogram so it reflects the new slider position
        'CalculateHistogramFromCurrentState()
    End Sub


    Private Sub UpdateHistogramFromViewport()
        ' 1. Get the current processed bitmap (the one affected by your sliders)
        ' If you are using a shader, you might need to grab the frame buffer.
        ' If you have a function that returns the processed bitmap:
        Dim processedBmp As Bitmap = GetProcessedBitmap()

        If processedBmp IsNot Nothing Then
            ' 2. Run your existing math logic [cite: 2026-03-03]
            ' This updates histL, histR, etc., and applies the 0.8F scaling factor
            CalculateHistogram(processedBmp)
        End If
    End Sub

    Private Function GetProcessedBitmap() As Bitmap
        ' 1. Ensure the OpenGL context is current for the viewport [cite: 2026-02-14]
        Me.GlControl2.MakeCurrent()

        ' 2. Create a bitmap the size of your viewport
        Dim bmp As New Bitmap(Me.GlControl2.Width, Me.GlControl2.Height)

        ' 3. Lock the bitmap bits so OpenGL can write to them
        Dim data As Imaging.BitmapData = bmp.LockBits(New Rectangle(0, 0, bmp.Width, bmp.Height),
                                     Imaging.ImageLockMode.WriteOnly,
                                     Imaging.PixelFormat.Format32bppArgb)

        ' 4. Read the pixels from the GPU back to the CPU [cite: 2026-02-14]
        ' OpenTK call: ReadPixels(x, y, width, height, format, type, pointer)
        OpenTK.Graphics.OpenGL.GL.ReadPixels(0, 0, bmp.Width, bmp.Height,
                                        OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                                        OpenTK.Graphics.OpenGL.PixelType.UnsignedByte,
                                        data.Scan0)

        bmp.UnlockBits(data)

        ' 5. OpenGL coordinates are upside down compared to GDI+, so flip it
        bmp.RotateFlip(RotateFlipType.RotateNoneFlipY)

        Return bmp
    End Function

    Private Function SliderToInternal(tag As String, sliderValue As Integer) As Single
        If Not ParamMappings.ContainsKey(tag) Then Return 0.0F

        Dim m = ParamMappings(tag)
        Dim norm As Single = (sliderValue - m.NeutralSlider) / m.ScaleFactor

        If tag.EndsWith("Mix") OrElse tag = "Saturation" OrElse tag = "Vibrance" Then
            Return CSng(sliderValue / 50.0F)
        End If

        Return Math.Max(m.MinInternal, Math.Min(m.MaxInternal, norm))
    End Function

    Private Function InternalToSlider(tag As String, internalValue As Single) As Integer
        If Not ParamMappings.ContainsKey(tag) Then Return 50

        Dim m = ParamMappings(tag)

        If tag.EndsWith("Mix") OrElse tag = "Saturation" OrElse tag = "Vibrance" Then
            Return CInt(internalValue * 50.0F)
        End If

        Dim sliderPos As Single = m.NeutralSlider + (internalValue * m.ScaleFactor)
        Return CInt(Math.Max(m.NeutralSlider + (m.MinInternal * m.ScaleFactor),
                             Math.Min(m.NeutralSlider + (m.MaxInternal * m.ScaleFactor), sliderPos)))
    End Function

    ' Add this structure / dictionary at class level
    Private Structure ParamMapping
        Public NeutralSlider As Integer
        Public ScaleFactor As Single     ' slider units per 1.0 internal unit
        Public MinInternal As Single
        Public MaxInternal As Single
        Public Format As String          ' "F2", "F1", etc.
    End Structure


    Private ReadOnly ParamMappings As New Dictionary(Of String, ParamMapping) From {
        {"Exposure", New ParamMapping With {.NeutralSlider = 50, .ScaleFactor = 25.0F, .MinInternal = -4.0F, .MaxInternal = 4.0F, .Format = "F2"}},
        {"Contrast", New ParamMapping With {.NeutralSlider = 50, .ScaleFactor = 25.0F, .MinInternal = -2.0F, .MaxInternal = 2.0F, .Format = "F2"}},
        {"Highlights", New ParamMapping With {.NeutralSlider = 50, .ScaleFactor = 50.0F, .MinInternal = -1.0F, .MaxInternal = 1.0F, .Format = "F2"}},   ' tighter range
        {"ShadowsLift", New ParamMapping With {.NeutralSlider = 50, .ScaleFactor = 50.0F, .MinInternal = -1.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"Whites", New ParamMapping With {.NeutralSlider = 50, .ScaleFactor = 50.0F, .MinInternal = -1.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"Blacks", New ParamMapping With {.NeutralSlider = 50, .ScaleFactor = 50.0F, .MinInternal = -1.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"Temp", New ParamMapping With {.NeutralSlider = 50, .ScaleFactor = 25.0F, .MinInternal = -2.0F, .MaxInternal = 2.0F, .Format = "F2"}},
        {"Tint", New ParamMapping With {.NeutralSlider = 50, .ScaleFactor = 25.0F, .MinInternal = -2.0F, .MaxInternal = 2.0F, .Format = "F2"}},
        {"BarrelDistortion", New ParamMapping With {.NeutralSlider = 50, .ScaleFactor = 50.0F, .MinInternal = -1.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"DynCon", New ParamMapping With {.NeutralSlider = 0, .ScaleFactor = 100.0F, .MinInternal = 0.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"Clarity", New ParamMapping With {.NeutralSlider = 50, .ScaleFactor = 25.0F, .MinInternal = -2.0F, .MaxInternal = 2.0F, .Format = "F2"}},
        {"Saturation", New ParamMapping With {.NeutralSlider = 50, .ScaleFactor = 50.0F, .MinInternal = 0.0F, .MaxInternal = 2.0F, .Format = "F2"}},   ' multiplier
        {"Vibrance", New ParamMapping With {.NeutralSlider = 50, .ScaleFactor = 50.0F, .MinInternal = 0.0F, .MaxInternal = 2.0F, .Format = "F2"}},
        {"RedMix", New ParamMapping With {.NeutralSlider = 50, .ScaleFactor = 100.0F, .MinInternal = 0.0F, .MaxInternal = 2.0F, .Format = "F2"}},
        {"GreenMix", New ParamMapping With {.NeutralSlider = 50, .ScaleFactor = 100.0F, .MinInternal = 0.0F, .MaxInternal = 2.0F, .Format = "F2"}},
        {"BlueMix", New ParamMapping With {.NeutralSlider = 50, .ScaleFactor = 100.0F, .MinInternal = 0.0F, .MaxInternal = 2.0F, .Format = "F2"}},
        {"LutIntens", New ParamMapping With {.NeutralSlider = 0, .ScaleFactor = 100.0F, .MinInternal = 0.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"Sharpen", New ParamMapping With {.NeutralSlider = 0, .ScaleFactor = 100.0F, .MinInternal = 0.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"Defringe", New ParamMapping With {.NeutralSlider = 0, .ScaleFactor = 50.0F, .MinInternal = 0.0F, .MaxInternal = 2.0F, .Format = "F2"}},
        {"Denoise", New ParamMapping With {.NeutralSlider = 0, .ScaleFactor = 100.0F, .MinInternal = 0.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"NoiseLimit", New ParamMapping With {.NeutralSlider = 0, .ScaleFactor = 100.0F, .MinInternal = 0.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"NoiseChroma", New ParamMapping With {.NeutralSlider = 0, .ScaleFactor = 100.0F, .MinInternal = 0.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"NoiseLuma", New ParamMapping With {.NeutralSlider = 0, .ScaleFactor = 100.0F, .MinInternal = 0.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"DeHaze", New ParamMapping With {.NeutralSlider = 0, .ScaleFactor = 100.0F, .MinInternal = 0.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"Vignette", New ParamMapping With {.NeutralSlider = 0, .ScaleFactor = 100.0F, .MinInternal = 0.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"VignetteStd", New ParamMapping With {.NeutralSlider = 0, .ScaleFactor = 100.0F, .MinInternal = 0.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"FilmGrain", New ParamMapping With {.NeutralSlider = 0, .ScaleFactor = 100.0F, .MinInternal = 0.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"GrainSize", New ParamMapping With {.NeutralSlider = 0, .ScaleFactor = 100.0F, .MinInternal = 0.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"TextureX", New ParamMapping With {.NeutralSlider = 0, .ScaleFactor = 100.0F, .MinInternal = 0.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"StructX", New ParamMapping With {.NeutralSlider = 0, .ScaleFactor = 100.0F, .MinInternal = 0.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"Vivid", New ParamMapping With {.NeutralSlider = 0, .ScaleFactor = 100.0F, .MinInternal = 0.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"SepiaMix", New ParamMapping With {.NeutralSlider = 0, .ScaleFactor = 100.0F, .MinInternal = 0.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"NoirFactor", New ParamMapping With {.NeutralSlider = 0, .ScaleFactor = 100.0F, .MinInternal = 0.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"SplitToning", New ParamMapping With {.NeutralSlider = 0, .ScaleFactor = 100.0F, .MinInternal = 0.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"Fade", New ParamMapping With {.NeutralSlider = 0, .ScaleFactor = 100.0F, .MinInternal = 0.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"FadeTint", New ParamMapping With {.NeutralSlider = 50, .ScaleFactor = 50.0F, .MinInternal = -1.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"Cinematic", New ParamMapping With {.NeutralSlider = 0, .ScaleFactor = 100.0F, .MinInternal = 0.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"CineBalance", New ParamMapping With {.NeutralSlider = 50, .ScaleFactor = 50.0F, .MinInternal = -1.0F, .MaxInternal = 1.0F, .Format = "F2"}},
        {"TextOpacity", New ParamMapping With {.NeutralSlider = 100, .ScaleFactor = 100.0F, .MinInternal = 0.0F, .MaxInternal = 1.0F, .Format = "F0"}}
    }

    Private Sub Exposure_Scroll(sender As Object, e As EventArgs) Handles sldExposure.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub Contrast_Scroll(sender As Object, e As EventArgs) Handles sldContrast.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub Highlights_Scroll(sender As Object, e As EventArgs) Handles sldHighlights.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub ShadowsLift_Scroll(sender As Object, e As EventArgs) Handles sldShadowsLift.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub Whites_Scroll(sender As Object, e As EventArgs) Handles sldWhites.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub Blacks_Scroll(sender As Object, e As EventArgs) Handles sldBlacks.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub Temp_Scroll(sender As Object, e As EventArgs) Handles sldTemp.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub Tint_Scroll(sender As Object, e As EventArgs) Handles sldTint.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldClarity_Scroll(sender As Object, e As EventArgs) Handles sldClarity.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldDynCon_Scroll(sender As Object, e As EventArgs) Handles sldDynCon.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldStructX_Scroll(sender As Object, e As EventArgs) Handles sldStructX.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldSharpen_Scroll(sender As Object, e As EventArgs) Handles sldSharpen.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldDefringe_Scroll(sender As Object, e As EventArgs) Handles sldDefringe.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldDenoise_Scroll(sender As Object, e As EventArgs) Handles sldDenoise.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldNoiseLimit_Scroll(sender As Object, e As EventArgs) Handles sldNoiseLimit.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldNoiseChroma_Scroll(sender As Object, e As EventArgs) Handles sldNoiseChroma.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldNoiseLuma_Scroll(sender As Object, e As EventArgs) Handles sldNoiseLuma.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldTextureX_Scroll(sender As Object, e As EventArgs) Handles sldTextureX.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldSaturation_Scroll(sender As Object, e As EventArgs) Handles sldSaturation.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldVibrance_Scroll(sender As Object, e As EventArgs) Handles sldVibrance.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldRedMix_Scroll(sender As Object, e As EventArgs) Handles sldRedMix.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldGreenMix_Scroll(sender As Object, e As EventArgs) Handles sldGreenMix.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldBlueMix_Scroll(sender As Object, e As EventArgs) Handles sldBlueMix.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldDeHaze_Scroll(sender As Object, e As EventArgs) Handles sldDeHaze.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldVignette_Scroll(sender As Object, e As EventArgs) Handles sldVignette.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldVignetteStd_Scroll(sender As Object, e As EventArgs) Handles sldVignetteStd.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldSepiaMix_Scroll(sender As Object, e As EventArgs) Handles sldSepiaMix.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldNoirFactor_Scroll(sender As Object, e As EventArgs) Handles sldNoirFactor.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldVivid_Scroll(sender As Object, e As EventArgs) Handles sldVivid.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldFilmGrain_Scroll(sender As Object, e As EventArgs) Handles sldFilmGrain.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldGrainSize_Scroll(sender As Object, e As EventArgs) Handles sldGrainSize.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldSplitToning_Scroll(sender As Object, e As EventArgs) Handles sldSplitToning.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldFade_Scroll(sender As Object, e As EventArgs) Handles sldFade.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldFadeTint_Scroll(sender As Object, e As EventArgs) Handles sldFadeTint.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldCinematic_Scroll(sender As Object, e As EventArgs) Handles sldCinematic.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldBarrelDistortion_Scroll(sender As Object, e As EventArgs) Handles sldBarrelDistortion.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldCineBalance_Scroll(sender As Object, e As EventArgs) Handles sldCineBalance.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub sldLutIntens_Scroll(sender As Object, e As EventArgs) Handles sldLutIntens.Scroll
        OnSliderScroll(sender, e)
    End Sub
    Private Sub BtnResetCorrectionSliders_Click(sender As Object, e As EventArgs) Handles BtnResetCorrectionSliders.Click
        Panel1.Focus()
        ResetCorrectionSliders()
    End Sub
    Private Sub btn_ResetEnchentment_Click(sender As Object, e As EventArgs) Handles btn_ResetEnchentment.Click
        ResetEnhancementSliders()
    End Sub

    Private Sub btn_ResetStylization_Click(sender As Object, e As EventArgs) Handles btn_ResetStylization.Click
        ResetStylizationSliders()
    End Sub
    'Private Sub flow_Correction_MouseEnter(sender As Object, e As EventArgs) Handles flow_Correction.MouseEnter
    'If Not flow_Correction.Focused Then
    '       flow_Correction.Focus()
    'End If
    'End Sub

    Private Sub btn_LoadLut_Click(sender As Object, e As EventArgs) Handles btn_LoadLut.Click
        Panel1.Focus()
        Using ofd As New OpenFileDialog()
            ofd.Filter = "Adobe LUT Files (*.cube)|*.cube|All Files (*.*)|*.*"
            ofd.Title = "Select a 3D LUT File"

            If ofd.ShowDialog() = DialogResult.OK Then
                ' 1. Update the UI Label
                If lblLutName IsNot Nothing Then
                    lblLutName.Text = "LUT: " & System.IO.Path.GetFileNameWithoutExtension(ofd.FileName).ToUpper()
                    lblLutName.ForeColor = Color.Cyan
                End If

                ' 2. Process the file and Upload to GPU
                GlControl2.MakeCurrent()
                UpdateLutTexture(ofd.FileName)

                ' 3. Set intensity to 100% so the effect is visible [cite: 2026-02-14]
                UpdateSliderValue("LutIntens", 100)
                CurrentParams.LutIntens = 1.0F ' Ensure internal value is synced

                ' 4. SYNC & REFRESH (Consistency Fix) [cite: 2026-03-06]
                SyncAllUniforms()
                ' GlControl2.Refresh() ' Force immediate draw before histogram [cite: 2026-02-14]
                GlControl2.Invalidate()
                ' 5. Update Histogram with new LUT effect [cite: 2026-03-03]
                UpdateHistogramFromViewport()

                GL.ActiveTexture(TextureUnit.Texture0)

                glHist.Refresh()

                UpdateStatusBar()
            End If
        End Using
    End Sub


    Private Sub UpdateLutTexture(filePath As String)
        If Not System.IO.File.Exists(filePath) Then Return

        Dim lines = System.IO.File.ReadAllLines(filePath)
        Dim lutSize As Integer = 0
        Dim data As New List(Of Single)()

        ' Parse the .cube file structure
        For Each line In lines
            line = line.Trim()
            If String.IsNullOrEmpty(line) OrElse line.StartsWith("#") Then Continue For

            If line.StartsWith("LUT_3D_SIZE") Then
                lutSize = Integer.Parse(line.Split(" "c).Last())
            ElseIf Char.IsDigit(line(0)) OrElse line.StartsWith("-") OrElse line.StartsWith(".") Then
                Dim parts = line.Split({" "c, ControlChars.Tab}, StringSplitOptions.RemoveEmptyEntries)
                If parts.Length = 3 Then
                    data.Add(Single.Parse(parts(0), System.Globalization.CultureInfo.InvariantCulture))
                    data.Add(Single.Parse(parts(1), System.Globalization.CultureInfo.InvariantCulture))
                    data.Add(Single.Parse(parts(2), System.Globalization.CultureInfo.InvariantCulture))
                End If
            End If
        Next

        If lutSize = 0 OrElse data.Count = 0 Then Return



        ' --- FIX START: Switch to Texture Unit 1 specifically for the LUT ---
        GL.ActiveTexture(TextureUnit.Texture1)

        ' GPU Memory Management [cite: 2026-02-14]
        If lutTextureID = 0 Then lutTextureID = GL.GenTexture()
        GL.BindTexture(TextureTarget.Texture3D, lutTextureID)

        GL.TexParameter(TextureTarget.Texture3D, TextureParameterName.TextureMinFilter, All.Linear)
        GL.TexParameter(TextureTarget.Texture3D, TextureParameterName.TextureMagFilter, All.Linear)
        GL.TexParameter(TextureTarget.Texture3D, TextureParameterName.TextureWrapS, All.ClampToEdge)
        GL.TexParameter(TextureTarget.Texture3D, TextureParameterName.TextureWrapT, All.ClampToEdge)
        GL.TexParameter(TextureTarget.Texture3D, TextureParameterName.TextureWrapR, All.ClampToEdge)

        ' ***** CRITICAL FIX *****
        GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1)

        GL.TexImage3D(TextureTarget.Texture3D, 0, PixelInternalFormat.Rgb32f,
              lutSize, lutSize, lutSize, 0,
              OpenTK.Graphics.OpenGL.PixelFormat.Rgb,
              PixelType.Float, data.ToArray())




        ' Reset to Unit 0 (standard for main image) just in case
        GL.ActiveTexture(TextureUnit.Texture0)
        ' --- NEW CLEANUP CODE END ---

        useLut = True

    End Sub

    Private Sub PopulateLutCombo()
        Try
            If Not System.IO.Directory.Exists(lutFolderPath) Then
                System.IO.Directory.CreateDirectory(lutFolderPath)
            End If

            cmbLut.Items.Clear()
            cmbLut.Items.Add("None") ' Default option

            Dim files = System.IO.Directory.GetFiles(lutFolderPath, "*.cube")
            For Each file In files
                cmbLut.Items.Add(System.IO.Path.GetFileName(file))
            Next

            cmbLut.SelectedIndex = 0
        Catch ex As Exception
            Debug.WriteLine("Error populating LUTs: " & ex.Message)
        End Try
    End Sub

    Private Sub cmbLut_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLut.SelectedIndexChanged
        Dim selectedName As String = cmbLut.SelectedItem.ToString()

        If selectedName = "None" Then
            useLut = False
            lblLutName.Text = "LUT: NONE"

            ' CRITICAL: Clear the binding so the shader doesn't get confused
            GlControl2.MakeCurrent()
            GL.ActiveTexture(TextureUnit.Texture1)
            GL.BindTexture(TextureTarget.Texture3D, 0)
            GL.ActiveTexture(TextureUnit.Texture0)
        Else
            Dim fullPath As String = System.IO.Path.Combine(lutFolderPath, selectedName)

            GlControl2.MakeCurrent()
            UpdateLutTexture(fullPath) ' This already handles ActiveTexture(Texture1)

            ' Update Label
            lblLutName.Text = "LUT: " & System.IO.Path.GetFileNameWithoutExtension(fullPath).ToUpper()
        End If

        ' Refresh the entire pipeline
        SyncAllUniforms()
        GlControl2.Invalidate()
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        If txtWatermark.Focused Then
            Return ' Exit immediately so the key goes to the TextBox
        End If

        ' 1. CYCLE LOGIC (Left/Right Arrows)
        If cmbLut IsNot Nothing AndAlso cmbLut.Items.Count > 1 Then
            lblLutName.ForeColor = Color.Cyan
            If e.KeyCode = Keys.Right OrElse e.KeyCode = Keys.Left Then
                If e.KeyCode = Keys.Right Then
                    If cmbLut.SelectedIndex < cmbLut.Items.Count - 1 Then
                        cmbLut.SelectedIndex += 1
                    Else
                        cmbLut.SelectedIndex = 0
                    End If
                ElseIf e.KeyCode = Keys.Left Then

                    If cmbLut.SelectedIndex > 0 Then
                        cmbLut.SelectedIndex -= 1
                    Else
                        cmbLut.SelectedIndex = cmbLut.Items.Count - 1
                    End If
                End If
                e.Handled = True
                Return ' Exit after cycling so we don't trigger other keys
            End If
        End If

        ' 2. TOGGLE LOGIC (Spacebar for Before/After)
        If e.KeyCode = Keys.Space Then
            If lutTextureID <> 0 Then
                useLut = Not useLut ' Flip the switch

                ' Update the label to show current state
                If useLut Then
                    ' Restore the name from the ComboBox
                    lblLutName.Text = "LUT: " & System.IO.Path.GetFileNameWithoutExtension(cmbLut.SelectedItem.ToString()).ToUpper()
                    lblLutName.ForeColor = Color.Cyan
                Else
                    ' Show it is bypassed
                    lblLutName.Text = "LUT: OFF (Bypassed)"
                    lblLutName.ForeColor = Color.Yellow
                End If

                SyncAllUniforms()
                GlControl2.Invalidate()
            End If
            e.Handled = True
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub ShowOriginal()
        isShowingOriginal = True
        ' 1. Specifically update the bypass uniform
        UpdateShaderBypass(True)
        ' 2. Forces redraw
        GlControl2.Invalidate()
    End Sub

    Private Sub ShowEdited()
        isShowingOriginal = False
        ' 1. Turn off bypass
        UpdateShaderBypass(False)
        ' 2. Re-apply all current slider/LUT values
        SyncAllUniforms()
        ' 3. Forces redraw
        GlControl2.Invalidate()
    End Sub

    Private Sub UpdateShaderBypass(bypass As Boolean)
        ' Use the variable that was working (shaderProgram or mainShader.Handle)
        ' I'll use shaderProgram here since you mentioned it was the one working before
        Dim bypassLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uBypass")

        If bypassLoc <> -1 Then
            GL.Uniform1(bypassLoc, If(bypass, 1, 0))
            Debug.Print("Bypass sent to GPU: " & bypass.ToString())
        End If
    End Sub


    Private Sub btn_OrigEdit_Click(sender As Object, e As EventArgs) Handles btn_OrigEdit.Click
        Panel1.Focus()
        ' If current state is original, flip to EDIT
        If btn_OrigEdit.Tag Is "original" Then
            btn_OrigEdit.Tag = "edit"
            btn_OrigEdit.Text = "MODIFY - ( EDIT MODE )"
            btn_OrigEdit.BackColor = Color.FromArgb(50, 207, 92, 118)
            ShowEdited()
        Else
            ' Default/Current is Edit, so flip to ORIGINAL
            btn_OrigEdit.Tag = "original"
            btn_OrigEdit.Text = "ORIGINAL - ( EDIT OFF )"
            btn_OrigEdit.BackColor = Color.FromArgb(50, 64, 64, 64)
            ShowOriginal()
        End If
    End Sub

    Private Sub Renew_Edit_Mode()
        isShowingOriginal = False
        btn_OrigEdit.Tag = "edit"
        btn_OrigEdit.Text = "NEW PICTURE ( EDIT ON )"
        btn_OrigEdit.BackColor = Color.FromArgb(50, 64, 64, 64)


    End Sub

    Private Sub btn_AutoCorrect_Click(sender As Object, e As EventArgs) Handles btn_AutoCorrect.Click
        Panel1.Focus()
        If currentImage Is Nothing OrElse String.IsNullOrEmpty(currentImagePath) Then Return
        ' --- TUNE THESE VARIABLES ---
        Dim var_ExpTarget = 125.0
        Dim var_ExpStrength = 0.2
        Dim var_ConStrength = 0.05

        Dim var_HighSens = 0.05    ' Scade sub 0.2 pentru senzitivitate mai mare
        Dim var_HighPower = 40.0   ' Crește de la 12.0 la 25.0 sau 40.0 pentru PUTERE
        ' --- PARAMETRI DE CONTROL PENTRU SHADOWS ---
        Dim var_ShadSens = 0.4     ' Sensibilitate (0.1 = f. sensibil, 0.4 = permisiv)
        Dim var_ShadPower = 5.0    ' Puterea de "ridicare" (Lift)
        ' ----------------------------
        ' --- PARAMETRI WHITES & BLACKS (SOFT) ---
        Dim var_EdgeSens = 0.005  ' 0.5% prag de clipping
        Dim var_EdgePower = 8.0   ' Putere mică pentru un efect "Soft"
        ' ----------------------------------------
        ' --- PARAMETRI COLOR BALANCE ---
        Dim var_ColorStrength = 0.3 ' 0.1 subtil, 0.5 agresiv
        ' -------------------------------

        Dim tempPos = 50
        Dim tintPos = 50

        ' 1. RESET TOTAL ȘI REPROCESARE [cite: 2026-02-14]
        ResetCorrectionForAutoCorr()

        ' IMPORTANT: Trebuie să forțăm shader-ul să randeze imaginea NEUTRALĂ 
        ' și să așteptăm ca histograma să fie extrasă din nou din acea imagine neutră.
        'UpdateShaderParameters()
        GlControl2.Invalidate()
        Application.DoEvents() ' Permite UI-ului și proceselor de fundal să termine resetarea

        ' 2. RE-VERIFICARE HISTOGRAMĂ
        ' Dacă histL se actualizează asincron, s-ar putea să ai nevoie de o mică logică 
        ' care să asigure că citești histograma imaginii RESETATE.
        If histL Is Nothing Then Return


        ' 1. INITIALIZARE CORECTĂ (Rezolvă eroarea de Cast din imaginea ta)
        Dim virtualHist(255) As Integer
        Array.Copy(histL, virtualHist, 256)

        ' Declarăm variabilele aici sus pentru a rezolva erorile "not declared"
        Dim totalPixels = CDbl(currentImage.Width) * currentImage.Height
        Dim expPos = 50
        Dim conPos = 50

        ' --- PAS 1: EXPUNEREA ---
        Dim currentBright = CalculateAvgLuma(virtualHist)
        Dim expShift = (var_ExpTarget - currentBright) * var_ExpStrength
        expPos = Math.Clamp(50 + CInt(expShift), 35, 65)

        ' Simulăm mutarea histogramei
        ShiftVirtualHist(virtualHist, expShift)

        ' --- PAS 2: CONTRASTUL ---
        Dim newBright = CalculateAvgLuma(virtualHist)
        conPos = 50 + CInt(Math.Abs(var_ExpTarget - newBright) * var_ConStrength)
        conPos = Math.Clamp(conPos, 50, 75)

        ' Simulăm contrastul (Rezolvă eroarea de tip Single() din imagine)
        StretchVirtualHist(virtualHist, (conPos - 50) / 50.0)

        ' --- PAS 3: HIGHLIGHTS & SHADOWS (Analiză Chirurgicală) ---

        ' 1. ANALIZA HIGHLIGHTS (Fereastra/Becul)
        ' 1. Calculăm volumul de pixeli în Midtones (Zona 60-150)
        Dim midVol As Double = 0
        For i = 60 To 150 : midVol += virtualHist(i) : Next
        Dim midAvg = midVol / 91.0 ' Media pe bin în mijloc

        ' 2. Calculăm volumul în zona de Highlights Arse (210-255)
        Dim highVol As Double = 0
        For i = 210 To 255 : highVol += virtualHist(i) : Next
        Dim highAvg = highVol / 46.0 ' Media pe bin în zona luminoasă

        ' 3. COMPARAȚIA DIRECTĂ
        ' Dacă media din zona albă este mai mare decât media din zona de detaliu,
        ' înseamnă că avem "Highlight Clipping" (exact ca în poza cu mașina).
        Dim highPos = 50

        If highAvg > midAvg * var_HighSens Then ' Prag foarte sensibil: 20% din densitatea medie
            ' Calculăm raportul. În poza ta, highAvg va fi probabil mult mai mare decât midAvg.
            Dim ratio = highAvg / (midAvg + 1.0)

            ' Gain-ul: dacă ratio e 2.0 (highlights duble față de medie), scadem 20 unități.
            ' Dacă ratio e 0.5 (highlights la jumătate), scadem 5 unități.
            highPos = 50 - CInt(ratio * var_HighPower)
        End If

        ' 4. SIGURANȚĂ ȘI APLICARE
        highPos = Math.Clamp(highPos, 5, 50)
        sldHighlights.Value = highPos

        ' 2. ANALIZA SHADOWS (Colțurile întunecate)
        ' 1. Volumul în zona de Umbre Adânci (0-45)
        Dim lowVol As Double = 0
        For i = 0 To 45 : lowVol += virtualHist(i) : Next
        Dim lowAvg = lowVol / 46.0 ' Media pe bin în zona neagră

        ' 2. Folosim același midAvg calculat anterior (60-150)
        ' (Asigură-te că midAvg este declarat deasupra acestui bloc)

        ' 3. COMPARAȚIA PENTRU LIFT
        Dim shadPos = 50

        If lowAvg > midAvg * var_ShadSens Then
            ' Calculăm raportul de "întuneric"
            Dim shadRatio = lowAvg / (midAvg + 1.0)

            ' Spre deosebire de Highlights (unde scădeam sub 50), 
            ' aici ADĂUGĂM peste 50 pentru a ridica (Lift) umbrele.
            shadPos = 50 + CInt(shadRatio * var_ShadPower)
        End If

        ' 4. LIMITARE ȘI APLICARE
        ' Limităm la un maxim de 85-90 pentru a nu spăla imaginea (vibrance loss)
        shadPos = Math.Clamp(shadPos, 50, 85)
        sldShadowsLift.Value = shadPos



        ' 1. ANALIZA WHITES (Punctul de Alb)
        Dim whiteEdge As Double = 0
        For i = 245 To 255 : whiteEdge += virtualHist(i) : Next
        Dim whiteDensity = whiteEdge / totalPixels

        Dim whitePos = 50
        ' Dacă avem prea puțini pixeli în alb pur, împingem slider-ul la dreapta (>50) 
        ' pentru a da strălucire. Dacă avem prea mulți (clipping), îl tragem la stânga.
        whitePos = 50 + CInt((var_EdgeSens - whiteDensity) * 100.0 * var_EdgePower)

        ' 2. ANALIZA BLACKS (Punctul de Negru)
        Dim blackEdge As Double = 0
        For i = 0 To 10 : blackEdge += virtualHist(i) : Next
        Dim blackDensity = blackEdge / totalPixels

        Dim blackPos = 50
        ' Dacă avem spațiu gol în negru, tragem slider-ul la stânga (<50) pentru contrast.
        ' Dacă avem deja prea mult negru zdrobit, îl împingem spre dreapta.
        blackPos = 50 - CInt((var_EdgeSens - blackDensity) * 100.0 * var_EdgePower)


        ' 1. Calculăm mediile pe canale (folosind histR, histG, histB originale sau simulate)
        ' Ideal ar fi să avem aceste sume deja calculate din zona 60-190
        Dim sumR As Double = 0, sumG As Double = 0, sumB As Double = 0
        Dim countC As Double = 0

        For i = 60 To 190
            sumR += histR(i) * i
            sumG += histG(i) * i
            sumB += histB(i) * i
            countC += (histR(i) + histG(i) + histB(i)) / 3.0
        Next

        ' 2. CALCULUL PENTRU COLOR BALANCE (Fără aplicare directă pe slider încă)
        If countC > 0 Then
            Dim avgR = sumR / countC
            Dim avgG = sumG / countC
            Dim avgB = sumB / countC

            Dim tempDiff = avgB - avgR
            tempPos = 50 + CInt(tempDiff * var_ColorStrength)

            Dim avgRB = (avgR + avgB) / 2.0
            Dim tintDiff = avgRB - avgG
            tintPos = 50 + CInt(tintDiff * var_ColorStrength)
        End If


        ' --- PAS 4: APLICARE FINALĂ ---
        ' 1. Fundația (Lumina globală)
        sldExposure.Value = expPos
        sldContrast.Value = conPos

        ' 2. Recuperarea (Detaliile din zonele critice)
        sldHighlights.Value = Math.Clamp(highPos, 20, 80)
        sldShadowsLift.Value = Math.Clamp(shadPos, 50, 85)

        ' 3. Dinamica (Punctele de clipping)
        sldWhites.Value = Math.Clamp(whitePos, 25, 75)
        sldBlacks.Value = Math.Clamp(blackPos, 25, 75)

        ' 4. Culoarea (Ultimul strat)
        ' Verificăm dacă variabilele au fost calculate în IF-ul de mai sus
        ' Dacă nu, rămân la 50 (neutru)
        If tempPos = 0 Then tempPos = 50
        If tintPos = 0 Then tintPos = 50
        ' Acum tempPos și tintPos sunt vizibile aici!
        sldTemp.Value = Math.Clamp(tempPos, 20, 80)
        sldTint.Value = Math.Clamp(tintPos, 20, 80)


        ' Handshake secvențial [cite: 2026-02-14]
        OnSliderScroll(sldExposure, EventArgs.Empty)
        OnSliderScroll(sldContrast, EventArgs.Empty)
        OnSliderScroll(sldHighlights, EventArgs.Empty)
        OnSliderScroll(sldShadowsLift, EventArgs.Empty)
        OnSliderScroll(sldWhites, EventArgs.Empty)
        OnSliderScroll(sldBlacks, EventArgs.Empty)
        OnSliderScroll(sldTemp, EventArgs.Empty)
        OnSliderScroll(sldTint, EventArgs.Empty)

        GlControl2.Invalidate()
    End Sub

    Private Function CalculateAvgLuma(h As Integer()) As Double
        Dim sum As Double = 0
        Dim count As Double = 0
        For i As Integer = 0 To 255
            sum += (CDbl(h(i)) * i)
            count += h(i)
        Next
        Return If(count > 0, sum / count, 128.0)
    End Function

    Private Sub ShiftVirtualHist(ByRef h As Integer(), shift As Double)
        Dim newH(255) As Integer
        Dim s As Integer = CInt(shift)
        For i As Integer = 0 To 255
            Dim newIdx As Integer = Math.Clamp(i + s, 0, 255)
            newH(newIdx) += h(i)
        Next
        h = newH
    End Sub

    Private Sub StretchVirtualHist(ByRef h As Integer(), factor As Single)
        Dim newH(255) As Integer
        For i As Integer = 0 To 255
            Dim diff As Double = i - 128
            ' Extindem sau comprimăm indexul față de mijloc
            Dim newIdx As Integer = Math.Clamp(128 + CInt(diff * (1.0 + factor)), 0, 255)
            newH(newIdx) += h(i)
        Next
        h = newH
    End Sub

    Private Sub SetSliderAndTrigger(tag As String, internalVal As Single)

        If Not sliderLookup.ContainsKey(tag) Then Return
        Dim ms As ModernSlider = sliderLookup(tag)

        ' Convert and set UI
        ms.Value = InternalToSlider(tag, internalVal)

        ' MANUALLY update the parameter class
        Select Case tag
            Case "Exposure" : CurrentParams.Exposure = internalVal
            Case "Contrast" : CurrentParams.Contrast = internalVal
            Case "Whites" : CurrentParams.Whites = internalVal
            Case "Blacks" : CurrentParams.Blacks = internalVal
            Case "Temp" : CurrentParams.Temp = internalVal
            Case "Tint" : CurrentParams.Tint = internalVal
            Case "Highlights" : CurrentParams.Highlights = internalVal
            Case "ShadowsLift" : CurrentParams.ShadowsLift = internalVal
        End Select

        ' Trigger the sync to GPU
        SyncAllUniforms()
        Me.GlControl2.Invalidate()
        ms.Invalidate() ' Redraw the slider itself
    End Sub
    Private Function GetRangeRatio(hist() As Integer, startBin As Integer, endBin As Integer, totalPixels As Long) As Double
        Dim count As Long = 0
        ' Sum up the pixels within the specified range
        For i As Integer = startBin To endBin
            count += hist(i)
        Next
        ' Return the fraction of the total image this range represents
        Return count / CDbl(totalPixels)
    End Function

    Private Sub btn_Crop_Click(sender As Object, e As EventArgs) Handles btn_Crop.Click
        Panel1.Focus()
        If currentImage Is Nothing OrElse String.IsNullOrEmpty(currentImagePath) Then Return

        If Not isSelectingCrop Then
            ' --- ENTERING CROP MODE ---
            isSelectingCrop = True
            btn_Crop.Text = "CONFIRM CROP"
            btn_Crop.BackColor = Color.DarkOrange

            ' Initialize cropRect to a centered 50% square if it's empty
            If cropRect.Width = 0 Then
                cropRect = New RectangleF(0.25F, 0.25F, 0.5F, 0.5F)
            End If
        Else
            ' --- EXECUTING CROP ---
            PerformPhysicalCrop()
            isSelectingCrop = False
            btn_Crop.Text = "CROP"
            btn_Crop.BackColor = Color.FromArgb(30, 30, 35)

        End If
        GlControl2.Invalidate()
    End Sub

    Private Function GetScreenRect(normRect As RectangleF) As Rectangle
        ' 1. Calculate the image's current size on screen
        Dim drawW As Single = currentImage.Width * CSng(zoomFactor)
        Dim drawH As Single = currentImage.Height * CSng(zoomFactor)

        ' 2. Find the top-left corner of the image in screen pixels
        ' This matches your vertex math: Center of screen + Pan - Half of DrawSize
        Dim midX As Single = GlControl2.ClientSize.Width / 2.0F
        Dim midY As Single = GlControl2.ClientSize.Height / 2.0F

        Dim imgStartX As Single = midX + CSng(panX) - (drawW / 2.0F)
        Dim imgStartY As Single = midY + CSng(panY) - (drawH / 2.0F)

        ' 3. Map the normalized cropRect (0-1) to those pixel dimensions
        Dim x As Integer = CInt(imgStartX + (normRect.X * drawW))
        Dim y As Integer = CInt(imgStartY + (normRect.Y * drawH))
        Dim w As Integer = CInt(normRect.Width * drawW)
        Dim h As Integer = CInt(normRect.Height * drawH)


        Return New Rectangle(x, y, w, h)
    End Function


    Private Sub PerformPhysicalCrop()
        ' 1. SAVE FOR UNDO: Store current image before modification
        If lastImageBeforeCrop IsNot Nothing Then lastImageBeforeCrop.Dispose()
        lastImageBeforeCrop = DirectCast(currentImage.Clone(), Bitmap)

        ' 2. Calculate pixel coordinates with the Y-flip
        Dim pixelX As Integer = CInt(Math.Floor(cropRect.X * currentImage.Width))
        Dim pixelY As Integer = CInt(Math.Floor((1.0F - cropRect.Y - cropRect.Height) * currentImage.Height))
        Dim pixelW As Integer = CInt(Math.Ceiling(cropRect.Width * currentImage.Width))
        Dim pixelH As Integer = CInt(Math.Ceiling(cropRect.Height * currentImage.Height))

        ' 3. REPLACEMENT FOR CLAMP: Manual Boundary Validation
        ' This prevents the "min cannot be greater than max" exception

        ' Fix X and Y starting points
        If pixelX < 0 Then pixelX = 0
        If pixelY < 0 Then pixelY = 0
        If pixelX >= currentImage.Width Then pixelX = currentImage.Width - 1
        If pixelY >= currentImage.Height Then pixelY = currentImage.Height - 1

        ' Fix Width and Height relative to the validated X and Y
        Dim finalW As Integer = pixelW
        Dim finalH As Integer = pixelH

        If pixelX + finalW > currentImage.Width Then finalW = currentImage.Width - pixelX
        If pixelY + finalH > currentImage.Height Then finalH = currentImage.Height - pixelY

        ' Ensure we have at least 1 pixel
        If finalW < 1 Then finalW = 1
        If finalH < 1 Then finalH = 1

        Dim srcRect As New Rectangle(pixelX, pixelY, finalW, finalH)

        ' 4. Perform the actual extraction
        Dim newBmp As New Bitmap(srcRect.Width, srcRect.Height)
        Using g As Graphics = Graphics.FromImage(newBmp)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
            g.DrawImage(currentImage, New Rectangle(0, 0, newBmp.Width, newBmp.Height),
                    srcRect, GraphicsUnit.Pixel)
        End Using

        ' 5. Refresh State [cite: 2026-02-14]
        currentImage.Dispose()
        currentImage = newBmp
        LoadTextureFromBitmap(currentImage)
        ResetView()

        isSelectingCrop = False
        GlControl2.Invalidate()
    End Sub


    Private Sub ResetView()
        ' 1. Reset Zoom to "Fit to Screen" (1.0 or calculated based on Aspect Ratio)
        ' If 1.0 makes the image fill the screen exactly:
        currentZoom = 1.0F

        ' 2. Center the Pan offsets
        ' Usually, 0,0 is the center of your shader's coordinate system
        offsetX = 0.0F
        offsetY = 0.0F

        ' 3. Calculate Aspect Ratio Fit (Optional)
        ' If you want the forest image to fit perfectly inside your GlControl:
        Dim imageAspect As Single = currentImage.Width / currentImage.Height
        Dim controlAspect As Single = GlControl2.Width / GlControl2.Height

        If imageAspect > controlAspect Then
            ' Image is wider than control
            currentZoom = 1.0F
        Else
            ' Image is taller than control
            currentZoom = controlAspect / imageAspect
        End If

        ' 4. Update the Uniforms immediately
        UpdateCameraUniforms()

        ' 5. Redraw
        GlControl2.Invalidate()
    End Sub

    Private Sub UpdateCameraUniforms()
        mainShader.Use()

        ' Pass the reset values to your shader
        ' Make sure these uniform names match your .vert or .frag file exactly [cite: 2026-02-14]
        ' GL.Uniform1(uZoomLocation, currentZoom)
        ' GL.Uniform2(uOffsetLocation, offsetX, offsetY)
    End Sub

    Private Sub UpdateHandlePositions(screenRect As Rectangle)
        Dim hSize As Integer = HANDLE_SIZE
        Dim half As Integer = hSize / 2

        ' Order: 0:TopLeft, 1:TopMiddle, 2:TopRight, 3:RightMiddle, 
        '        4:BottomRight, 5:BottomMiddle, 6:BottomLeft, 7:LeftMiddle

        ' Corners
        cropHandles(0) = New RectangleF(screenRect.Left - half, screenRect.Top - half, hSize, hSize)
        cropHandles(2) = New RectangleF(screenRect.Right - half, screenRect.Top - half, hSize, hSize)
        cropHandles(4) = New RectangleF(screenRect.Right - half, screenRect.Bottom - half, hSize, hSize)
        cropHandles(6) = New RectangleF(screenRect.Left - half, screenRect.Bottom - half, hSize, hSize)

        ' Midpoints
        cropHandles(1) = New RectangleF(screenRect.Left + (screenRect.Width / 2) - half, screenRect.Top - half, hSize, hSize)
        cropHandles(3) = New RectangleF(screenRect.Right - half, screenRect.Top + (screenRect.Height / 2) - half, hSize, hSize)
        cropHandles(5) = New RectangleF(screenRect.Left + (screenRect.Width / 2) - half, screenRect.Bottom - half, hSize, hSize)
        cropHandles(7) = New RectangleF(screenRect.Left - half, screenRect.Top + (screenRect.Height / 2) - half, hSize, hSize)
    End Sub




    Private Sub ConstrainCropRect()
        ' 1. Fix negative dimensions if user dragged past the origin
        If cropRect.Width < 0 Then
            cropRect.X += cropRect.Width
            cropRect.Width = Math.Abs(cropRect.Width)
        End If
        If cropRect.Height < 0 Then
            cropRect.Y += cropRect.Height
            cropRect.Height = Math.Abs(cropRect.Height)
        End If

        ' 2. Clamp dimensions
        Dim w As Single = Math.Clamp(cropRect.Width, 0.01F, 1.0F)
        Dim h As Single = Math.Clamp(cropRect.Height, 0.01F, 1.0F)

        ' 3. Clamp Position (Ensure the box stays inside 0.0-1.0)
        Dim x As Single = Math.Clamp(cropRect.X, 0.0F, 1.0F - w)
        Dim y As Single = Math.Clamp(cropRect.Y, 0.0F, 1.0F - h)

        cropRect = New RectangleF(x, y, w, h)
    End Sub

    Private Sub UpdateCropCursor(pt As Point)

        If currentImage Is Nothing Then Return

        If Not isSelectingCrop Then
            GlControl2.Cursor = Cursors.Default
            Return
        End If

        ' ────────────────────────────────────────────────
        ' 1. Get mouse in normalized 0..1 image space
        '    (same logic as in MouseDown / MouseMove)
        ' ────────────────────────────────────────────────
        Dim imgPos As PointF = GetMouseInImageSpace(pt.X, pt.Y)
        Dim mX As Single = imgPos.X
        Dim mY As Single = imgPos.Y

        ' Use same tolerance calculation as in MouseDown for consistency
        Dim viewW As Single = CSng(GlControl2.ClientSize.Width)
        Dim viewH As Single = CSng(GlControl2.ClientSize.Height)
        Dim imgAspect As Single = CSng(currentImage.Width) / CSng(currentImage.Height)
        Dim viewAspect As Single = viewW / viewH

        Dim baseScaleX As Single = If(viewAspect > imgAspect, imgAspect / viewAspect, 1.0F)
        Dim baseScaleY As Single = If(viewAspect > imgAspect, 1.0F, viewAspect / imgAspect)

        Dim actualDrawW As Single = viewW * baseScaleX * CSng(zoomFactor)
        Dim actualDrawH As Single = viewH * baseScaleY * CSng(zoomFactor)

        Dim tol As Single = 30.0F / actualDrawW           ' normal for corners
        Dim tolEdge As Single = 55.0F / actualDrawW       ' generous for edges

        ' ────────────────────────────────────────────────
        ' 2. Always use sorted boundaries (handles flip)
        ' ────────────────────────────────────────────────
        Dim left = Math.Min(cropRect.X, cropRect.X + cropRect.Width)
        Dim right = Math.Max(cropRect.X, cropRect.X + cropRect.Width)
        Dim top = Math.Min(cropRect.Y, cropRect.Y + cropRect.Height)
        Dim bottom = Math.Max(cropRect.Y, cropRect.Y + cropRect.Height)

        Dim midX = (left + right) / 2.0F
        Dim midY = (top + bottom) / 2.0F

        ' ────────────────────────────────────────────────
        ' 3. Hit test – corners first (highest priority)
        ' ────────────────────────────────────────────────
        If Math.Abs(mX - left) < tol AndAlso Math.Abs(mY - top) < tol Then
            GlControl2.Cursor = Cursors.SizeNWSE   ' top-left
            Return
        End If
        If Math.Abs(mX - right) < tol AndAlso Math.Abs(mY - top) < tol Then
            GlControl2.Cursor = Cursors.SizeNESW   ' top-right
            Return
        End If
        If Math.Abs(mX - right) < tol AndAlso Math.Abs(mY - bottom) < tol Then
            GlControl2.Cursor = Cursors.SizeNWSE   ' bottom-right
            Return
        End If
        If Math.Abs(mX - left) < tol AndAlso Math.Abs(mY - bottom) < tol Then
            GlControl2.Cursor = Cursors.SizeNESW   ' bottom-left
            Return
        End If

        ' ────────────────────────────────────────────────
        ' 4. Then edges (middles) – more forgiving tolerance
        ' ────────────────────────────────────────────────
        If Math.Abs(mX - midX) < tolEdge AndAlso Math.Abs(mY - top) < tol Then
            GlControl2.Cursor = Cursors.SizeNS     ' top-middle
            Return
        End If
        If Math.Abs(mX - midX) < tolEdge AndAlso Math.Abs(mY - bottom) < tol Then
            GlControl2.Cursor = Cursors.SizeNS     ' bottom-middle
            Return
        End If
        If Math.Abs(mX - right) < tolEdge AndAlso Math.Abs(mY - midY) < tolEdge Then
            GlControl2.Cursor = Cursors.SizeWE     ' right-middle
            Return
        End If
        If Math.Abs(mX - left) < tolEdge AndAlso Math.Abs(mY - midY) < tolEdge Then
            GlControl2.Cursor = Cursors.SizeWE     ' left-middle
            Return
        End If

        ' ────────────────────────────────────────────────
        ' 5. Interior – move cursor
        ' ────────────────────────────────────────────────
        If mX >= left AndAlso mX <= right AndAlso mY >= top AndAlso mY <= bottom Then
            GlControl2.Cursor = Cursors.SizeAll
            Return
        End If

        ' ────────────────────────────────────────────────
        ' 6. Nothing hit → default
        ' ────────────────────────────────────────────────
        GlControl2.Cursor = Cursors.Default
    End Sub

    Private Function GetMouseNormalized(e As MouseEventArgs) As PointF
        Dim drawW As Single = currentImage.Width * CSng(zoomFactor)
        Dim drawH As Single = currentImage.Height * CSng(zoomFactor)

        Dim imgStartX As Single = (GlControl2.Width / 2.0F) + CSng(panX) - (drawW / 2.0F)
        Dim imgStartY As Single = (GlControl2.Height / 2.0F) + CSng(panY) - (drawH / 2.0F)

        ' SYNCED: Now matches UpdateCropCursor inversion
        Return New PointF((e.X - imgStartX) / drawW, 1.0F - ((e.Y - imgStartY) / drawH))
    End Function

    Private Sub btn_UndoCrop_Click(sender As Object, e As EventArgs) Handles btn_UndoCrop.Click
        Panel1.Focus()
        If lastImageBeforeCrop IsNot Nothing Then
            ' 1. Swap images
            Dim tempImage = currentImage
            currentImage = DirectCast(lastImageBeforeCrop.Clone, Bitmap)

            ' 2. Dispose of the "old" cropped version to free memory
            tempImage.Dispose()

            ' 3. Refresh OpenGL Texture and View [cite: 2026-02-14]
            LoadTextureFromBitmap(currentImage)
            ResetView()

            ' 4. Clear the undo buffer so we don't undo twice to the same state
            ' Or keep it if you want to toggle back and forth
            lastImageBeforeCrop.Dispose()
            lastImageBeforeCrop = Nothing

            GlControl2.Invalidate()
        Else
            MessageBox.Show("No previous state to revert to.", "Undo", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btn_SaveToCatalog_Click(sender As Object, e As EventArgs) Handles btn_SaveToCatalog.Click
        Panel1.Focus()

        ' 1. Open Dialog to pick the base location
        Using fbd As New FolderBrowserDialog()
            fbd.Description = "Select the location where you want to create the Catalog folder"

            If fbd.ShowDialog() = DialogResult.OK Then
                ' 2. Ask the user for their specific name
                Dim baseName As String = InputBox("Enter the name for your catalog:", "Catalog Name", "MyProject")

                If String.IsNullOrWhiteSpace(baseName) Then Exit Sub

                ' 3. Create the final folder path: Name_Catalog
                ' We use System.IO to avoid MetadataExtractor conflicts [cite: 2026-02-14]
                Dim catalogPath As String = System.IO.Path.Combine(fbd.SelectedPath, baseName & "_Catalog")

                Try
                    ' Create directory if it doesn't exist
                    If Not System.IO.Directory.Exists(catalogPath) Then
                        System.IO.Directory.CreateDirectory(catalogPath)
                    End If

                    ' 4. Copy all memorized paths to the new folder
                    Dim copyCount As Integer = 0
                    For Each filePath In stripFilePaths
                        If System.IO.File.Exists(filePath) Then
                            Dim fileName As String = System.IO.Path.GetFileName(filePath)
                            Dim destination As String = System.IO.Path.Combine(catalogPath, fileName)

                            ' Copy the file (True allows overwriting)
                            System.IO.File.Copy(filePath, destination, True)
                            copyCount += 1
                        End If
                    Next

                    MessageBox.Show($"Success! {copyCount} images copied to: {catalogPath}", "Catalog Created")

                Catch ex As Exception
                    MessageBox.Show("Error creating catalog: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub


    Private Sub SaveSidecar(imagePath As String)
        Try
            Dim sidecarPath As String = imagePath & ".fstop"

            ' Use System.IO to avoid MetadataExtractor conflicts [cite: 2026-02-14]
            Using sw As New System.IO.StreamWriter(sidecarPath, False)
                ' 1. Write Date/Time Header
                sw.WriteLine($"Saved={DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")};")

                ' 3. Write All Slider Parameters
                sw.WriteLine("Exposure=" & sldExposure.Value & ";")
                sw.WriteLine("Contrast=" & sldContrast.Value & ";")
                sw.WriteLine("Highlights=" & sldHighlights.Value & ";")
                sw.WriteLine("ShadowsLift=" & sldShadowsLift.Value & ";")
                sw.WriteLine("Whites=" & sldWhites.Value & ";")
                sw.WriteLine("Blacks=" & sldBlacks.Value & ";")
                sw.WriteLine("Temp=" & sldTemp.Value & ";")
                sw.WriteLine("Tint=" & sldTint.Value & ";")
                sw.WriteLine("BarrelDistortion=" & sldBarrelDistortion.Value & ";")
                sw.WriteLine("DynCon=" & sldDynCon.Value & ";")
                sw.WriteLine("StructX=" & sldStructX.Value & ";")
                sw.WriteLine("Sharpen=" & sldSharpen.Value & ";")
                sw.WriteLine("Defringe=" & sldDefringe.Value & ";")
                sw.WriteLine("Denoise=" & sldDenoise.Value & ";")
                sw.WriteLine("NoiseLimit=" & sldNoiseLimit.Value & ";")
                sw.WriteLine("NoiseChroma=" & sldNoiseChroma.Value & ";")
                sw.WriteLine("NoiseLuma=" & sldNoiseLuma.Value & ";")
                sw.WriteLine("TextureX=" & sldTextureX.Value & ";")
                sw.WriteLine("Clarity=" & sldClarity.Value & ";")
                sw.WriteLine("Saturation=" & sldSaturation.Value & ";")
                sw.WriteLine("Vibrance=" & sldVibrance.Value & ";")
                sw.WriteLine("RedMix=" & sldRedMix.Value & ";")
                sw.WriteLine("GreenMix=" & sldGreenMix.Value & ";")
                sw.WriteLine("BlueMix=" & sldBlueMix.Value & ";")
                sw.WriteLine("LutIntens=" & sldLutIntens.Value & ";")
                sw.WriteLine("DeHaze=" & sldDeHaze.Value & ";")
                sw.WriteLine("Vignette=" & sldVignette.Value & ";")
                sw.WriteLine("VignetteStd=" & sldVignetteStd.Value & ";")
                sw.WriteLine("SepiaMix=" & sldSepiaMix.Value & ";")
                sw.WriteLine("NoirFactor=" & sldNoirFactor.Value & ";")
                sw.WriteLine("Vivid=" & sldVivid.Value & ";")
                sw.WriteLine("FilmGrain=" & sldFilmGrain.Value & ";")
                sw.WriteLine("GrainSize=" & sldGrainSize.Value & ";")
                sw.WriteLine("SplitToning=" & sldSplitToning.Value & ";")
                sw.WriteLine("Fade=" & sldFade.Value & ";")
                sw.WriteLine("FadeTint=" & sldFadeTint.Value & ";")
                sw.WriteLine("Cinematic=" & sldCinematic.Value & ";")
                sw.WriteLine("CineBalance=" & sldCineBalance.Value & ";")

                ' Force the use of a Dot (.) instead of a Comma (,) regardless of Windows settings
                Dim inv As System.Globalization.CultureInfo = System.Globalization.CultureInfo.InvariantCulture

                ' NEW: Save the View State [cite: 2026-02-14]
                ' --- 1. Calculate Real Zoom for Saving ---
                Dim viewW As Single = CSng(GlControl2.ClientSize.Width)
                Dim viewH As Single = CSng(GlControl2.ClientSize.Height)
                Dim imgW As Single = CSng(imageWidth)
                Dim imgH As Single = CSng(imageHeight)

                ' This is the "Fit" ratio (e.g., 0.15 for a 7k image)
                Dim baseFitRatio As Single = Math.Min(viewW / imgW, viewH / imgH)

                ' This is the Actual Pixel Scale (e.g., 1.0 * 0.15 = 0.15)
                ' We save THIS so the sidecar knows the true physical scale.
                Dim realSaveZoom As Double = zoomFactor * baseFitRatio

                ' --- 2. Write to Sidecar ---
                sw.WriteLine("Zoom=" & realSaveZoom.ToString("G", inv) & ";")
                sw.WriteLine("PanX=" & panX.ToString("G", inv) & ";")
                sw.WriteLine("PanY=" & panY.ToString("G", inv) & ";")

                ' Save Crop Coordinates
                sw.WriteLine("CropX=" & cropRect.X.ToString("G", inv) & ";")
                sw.WriteLine("CropY=" & cropRect.Y.ToString("G", inv) & ";")
                sw.WriteLine("CropW=" & cropRect.Width.ToString("G", inv) & ";")
                sw.WriteLine("CropH=" & cropRect.Height.ToString("G", inv) & ";")

                ' Save the Image Aspect so we can handle window resizing later
                Dim currentAspect As Single = imgW / imgH
                sw.WriteLine("ImgAspect=" & currentAspect.ToString("G", inv) & ";")


                ' --- 3. NEW: WATERMARK & TEXT PARAMETERS ---
                sw.WriteLine("UseText=" & If(useText, "1", "0") & ";")
                sw.WriteLine("WatermarkText=" & txtWatermark.Text & ";")

                ' Save Font details
                sw.WriteLine("FontName=" & currentFont.Name & ";")
                sw.WriteLine("FontSize=" & numFontSize.Value.ToString(inv) & ";")
                sw.WriteLine("FontStyle=" & CInt(currentFont.Style) & ";")

                ' Save Color (as ARGB integer)
                sw.WriteLine("TextColor=" & textColor.ToArgb().ToString() & ";")
                sw.WriteLine("TextOpacity=" & sldTextOpacity.Value & ";")

                ' Save Position (0.0 to 1.0)
                sw.WriteLine("TextPosX=" & textPos_txt.X.ToString("G", inv) & ";")
                sw.WriteLine("TextPosY=" & textPos_txt.Y.ToString("G", inv) & ";")



                ' --- 5. NEW: COLOR CHANGE & REPLACEMENT PARAMETERS ---
                ' Target Color (Culoarea selectată pentru schimbare)
                ' --- COLOR CHANGE & REPLACEMENT PARAMETERS ---
                ' 1. Vectorii de culoare (necesari pentru Shader)
                sw.WriteLine("TargetColX=" & targetCol.X.ToString("G", inv) & ";")
                sw.WriteLine("TargetColY=" & targetCol.Y.ToString("G", inv) & ";")
                sw.WriteLine("TargetColZ=" & targetCol.Z.ToString("G", inv) & ";")

                sw.WriteLine("ReplaceColX=" & replaceCol.X.ToString("G", inv) & ";")
                sw.WriteLine("ReplaceColY=" & replaceCol.Y.ToString("G", inv) & ";")
                sw.WriteLine("ReplaceColZ=" & replaceCol.Z.ToString("G", inv) & ";")

                ' 2. Valorile Sliderelor (declanșează actualizarea logică la Load)
                sw.WriteLine("sldAreaCorrect=" & sldAreaCorrect.Value & ";")
                sw.WriteLine("sldSmoothnesCorrect=" & sldSmoothnesCorrect.Value & ";")
                sw.WriteLine("sldHexaCorrect=" & sldHexaCorrect.Value & ";")
                sw.WriteLine("sldOpacity=" & sldOpacity.Value & ";")

                ' 3. Textbox-uri și Checkbox
                sw.WriteLine("txtHexa_Corect=" & txtHexa_Corect.Text & ";")
                sw.WriteLine("txtCorrectedHexa=" & txtCorrectedHexa_Correct.Text & ";")
                sw.WriteLine("UseReplace=" & If(cb_UseReplace.Checked, "1", "0") & ";")
                sw.WriteLine("SelectionU=" & selectionU.ToString("G", inv) & ";")
                sw.WriteLine("SelectionV=" & selectionV.ToString("G", inv) & ";")


            End Using

            lblStatus_Change.Text = "Sidecar saved successfully."
        Catch ex As Exception
            MessageBox.Show("Error saving .fstop: " & ex.Message)
        End Try
    End Sub


    Private Sub LoadSidecar(imagePath As String, Optional isBatchExport As Boolean = False, Optional targetImg As Bitmap = Nothing)

        ' ADD THIS at the very start of LoadSidecar
        'txtWatermark.Text = ""
        'textColor = Color.White ' Default
        'useText = False

        Dim sidecarPath As String = imagePath & ".fstop"

        ' Default values before loading
        uIsCropApplied = False
        uIsCropping = 1
        cropRect = New RectangleF(0.1F, 0.1F, 0.8F, 0.8F)

        ' Reset View Defaults (So a clean load doesn't inherit old pan/zoom)
        zoomFactor = 1.0
        panX = 0
        panY = 0

        If Not System.IO.File.Exists(sidecarPath) Then
            ResetAllSliders()

            Exit Sub
        End If

        Dim inv As System.Globalization.CultureInfo = System.Globalization.CultureInfo.InvariantCulture

        Try
            Using sr As New System.IO.StreamReader(sidecarPath)
                While Not sr.EndOfStream
                    Dim line As String = sr.ReadLine().Trim()
                    If line.StartsWith("#") AndAlso Not line.Contains("Saved") Then Continue While

                    Dim parts = line.Replace(";", "").Replace("#", "").Split("="c)
                    If parts.Length < 2 Then Continue While

                    Dim key As String = parts(0).Trim()
                    Dim rawValue As String = parts(1).Trim()

                    Select Case key
                        Case "Saved", "Saved:"
                            lblStatus_Change.Text = "Changed on: " & rawValue
                            Continue While

                        Case "WatermarkText"
                            txtWatermark.Text = rawValue
                            Continue While ' Move to the next line in the file immediately

                        Case "FontName"
                            ' Rebuild font with the same size/style but the new name
                            currentFont = New Font(rawValue, currentFont.Size, currentFont.Style)
                            lblFontName.Text = currentFont.Name
                            Continue While
                        Case "txtHexa_Corect"
                            txtHexa_Corect.Text = rawValue
                            Continue While
                        Case "txtCorrectedHexa"
                            txtCorrectedHexa_Correct.Text = rawValue
                            Continue While

                    End Select

                    Dim value As Single
                    Dim cleanValue As String = rawValue.Replace(",", ".")

                    If Single.TryParse(cleanValue, System.Globalization.NumberStyles.Any, inv, value) Then
                        Select Case key
                            Case "Exposure" : sldExposure.Value = CInt(value)
                            Case "Contrast" : sldContrast.Value = CInt(value)
                            Case "Highlights" : sldHighlights.Value = CInt(value)
                            Case "ShadowsLift" : sldShadowsLift.Value = CInt(value)
                            Case "Whites" : sldWhites.Value = CInt(value)
                            Case "Blacks" : sldBlacks.Value = CInt(value)
                            Case "Temp" : sldTemp.Value = CInt(value)
                            Case "Tint" : sldTint.Value = CInt(value)
                            Case "DynCon" : sldDynCon.Value = CInt(value)
                            Case "StructX" : sldStructX.Value = CInt(value)
                            Case "Sharpen" : sldSharpen.Value = CInt(value)
                            Case "Defringe" : sldDefringe.Value = CInt(value)
                            Case "Denoise" : sldDenoise.Value = CInt(value)
                            Case "NoiseLimit" : sldNoiseLimit.Value = CInt(value)
                            Case "NoiseChroma" : sldNoiseChroma.Value = CInt(value)
                            Case "NoiseLuma" : sldNoiseLuma.Value = CInt(value)
                            Case "TextureX" : sldTextureX.Value = CInt(value)
                            Case "BarrelDistortion" : sldBarrelDistortion.Value = CInt(value)
                            Case "Clarity" : sldClarity.Value = CInt(value)
                            Case "Saturation" : sldSaturation.Value = CInt(value)
                            Case "Vibrance" : sldVibrance.Value = CInt(value)
                            Case "RedMix" : sldRedMix.Value = CInt(value)
                            Case "GreenMix" : sldGreenMix.Value = CInt(value)
                            Case "BlueMix" : sldBlueMix.Value = CInt(value)
                            Case "LutIntens" : sldLutIntens.Value = CInt(value)
                            Case "DeHaze" : sldDeHaze.Value = CInt(value)
                            Case "Vignette" : sldVignette.Value = CInt(value)
                            Case "VignetteStd" : sldVignetteStd.Value = CInt(value)
                            Case "SepiaMix" : sldSepiaMix.Value = CInt(value)
                            Case "NoirFactor" : sldNoirFactor.Value = CInt(value)
                            Case "Vivid" : sldVivid.Value = CInt(value)
                            Case "FilmGrain" : sldFilmGrain.Value = CInt(value)
                            Case "GrainSize" : sldGrainSize.Value = CInt(value)
                            Case "SplitToning" : sldSplitToning.Value = CInt(value)
                            Case "Fade" : sldFade.Value = CInt(value)
                            Case "FadeTint" : sldFadeTint.Value = CInt(value)
                            Case "Cinematic" : sldCinematic.Value = CInt(value)
                            Case "CineBalance" : sldCineBalance.Value = CInt(value)

                                ' --- NOILE CASE-URI PENTRU COLOR REPLACEMENT ---
                            Case "TargetColX" : targetCol.X = value
                            Case "TargetColY" : targetCol.Y = value
                            Case "TargetColZ" : targetCol.Z = value

                            Case "ReplaceColX" : replaceCol.X = value
                            Case "ReplaceColY" : replaceCol.Y = value
                            Case "ReplaceColZ" : replaceCol.Z = value

                            Case "sldAreaCorrect" : sldAreaCorrect.Value = CInt(value)
                            Case "sldSmoothnesCorrect" : sldSmoothnesCorrect.Value = CInt(value)
                            Case "sldHexaCorrect" : sldHexaCorrect.Value = CInt(value)
                            Case "sldOpacity" : sldOpacity.Value = CInt(value)

                            Case "SelectionU"
                                selectionU = value
                                ' Opțional: dacă ai un punct vizual pe ecran (pos_for_colors), îl poți recalcula aici
                                pos_for_colors.X = selectionU

                            Case "SelectionV"
                                selectionV = value
                                pos_for_colors.Y = selectionV

                            Case "UseReplace" : cb_UseReplace.Checked = (value = 1)


                                ' --- NEW: Text & Watermark Restoration ---

                            Case "UseText" : useText = (value = 1)
                            Case "FontSize"
                                numFontSize.Value = CDec(value)
                                sld_FNT_Size.Value = CInt(value)
                            Case "FontStyle"
                                ' Re-apply style to current font
                                currentFont = New Font(currentFont.Name, currentFont.Size, DirectCast(CInt(value), FontStyle))
                            Case "TextColor"
                                Dim colorInt As Integer = CInt(value)
                                textColor = Color.FromArgb(colorInt)
                                btnSelectColor.BackColor = textColor



                                SyncAllUniforms()
                                GlControl2.Invalidate()

                            Case "TextOpacity" : sldTextOpacity.Value = CInt(value)
                            Case "TextPosX" : textPos_txt.X = CSng(Val(value))
                            Case "TextPosY" : textPos_txt.Y = CSng(Val(value))
                                ' You MUST pass the variables here so the "clr" argument gets the new color


                            ' --- ADDED: View State Restoration ---
                            Case "Zoom" : zoomFactor = value
                            Case "PanX" : panX = value
                            Case "PanY" : panY = value
                            Case "ImgAspect"
                                ' If the loaded image aspect differs significantly from the saved one, 
                                ' we might need to adjust the zoom to prevent black bars.
                                Dim currentAspect As Single = CSng(currentImage.Width) / CSng(currentImage.Height)
                                If Math.Abs(currentAspect - value) > 0.01F Then
                                    System.Diagnostics.Debug.WriteLine("Warning: Sidecar aspect ratio mismatch.")
                                End If
                            ' --- Crop Rect ---
                            Case "CropX" : cropRect.X = value
                            Case "CropY" : cropRect.Y = value
                            Case "CropW" : cropRect.Width = value
                            Case "CropH" : cropRect.Height = value
                        End Select
                    End If
                End While
            End Using


            ' --- THE LOGIC GATE ---
            Dim isDefaultX As Boolean = Math.Abs(cropRect.X - 0.1F) < 0.001F
            Dim isDefaultY As Boolean = Math.Abs(cropRect.Y - 0.1F) < 0.001F
            Dim isDefaultW As Boolean = Math.Abs(cropRect.Width - 0.8F) < 0.001F
            Dim isDefaultH As Boolean = Math.Abs(cropRect.Height - 0.8F) < 0.001F

            ' Check if the loaded crop is not the default
            If Not (isDefaultX AndAlso isDefaultY AndAlso isDefaultW AndAlso isDefaultH) Then
                uIsCropApplied = True
                uIsCropping = 1

                ' Only physically crop the image for preview, NOT for batch export
                If Not isBatchExport Then
                    ' This modifies currentImage so preview is correct
                    PerformPhysicalCrop()

                    ' Adjust zoom so the cropped preview fits the screen
                    Dim viewW As Single = CSng(GlControl2.ClientSize.Width)
                    Dim viewH As Single = CSng(GlControl2.ClientSize.Height)

                    Dim newImgW As Single = CSng(currentImage.Width)
                    Dim newImgH As Single = CSng(currentImage.Height)

                    Dim currentBaseFit As Single = Math.Min(viewW / newImgW, viewH / newImgH)
                    If currentBaseFit > 0 Then
                        zoomFactor = zoomFactor / currentBaseFit
                    Else
                        zoomFactor = 1.0F
                    End If

                    ' Physical crop is done, so we mark uIsCropApplied False for shaders
                    uIsCropApplied = False
                End If

            Else
                ' Default load (no crop)
                uIsCropApplied = False
                uIsCropping = 1
                zoomFactor = 1.0F
                panX = 0
                panY = 0
                FitToScreen()
            End If




            UpdateStatusBar()



            ' Sync the color button to the loaded color
            btnSelectColor.BackColor = Color.FromArgb(255, textColor.R, textColor.G, textColor.B)

            ' Sync the font button text
            ' btnFont.Text = currentFont.Name & ", " & currentFont.Size

            RenderTextToBitmap(txtWatermark.Text, currentFont, textColor)
            SyncAllUniforms()
            GlControl2.Invalidate()

        Catch ex As Exception
            MessageBox.Show("Error loading .fstop: " & ex.Message)
        End Try
    End Sub





    Private Sub Apply_Physical_Crop(ByVal finalize As Boolean)
        ' Update the local variable
        currentIsCroppingState = If(finalize, 2, 1)

        ' Trigger the Redraw (The Paint event will handle the Shader)
        GlControl2.Invalidate()
    End Sub



    Private Sub ResetCropToDefault()
        cropRect = New RectangleF(0.1F, 0.1F, 0.8F, 0.8F)
        isSelectingCrop = False
    End Sub

    Private Sub btn_SaveChanges_Click(sender As Object, e As EventArgs) Handles btn_SaveChanges.Click
        Panel1.Focus()
        ' 1. Check if we have an image currently loaded
        If String.IsNullOrEmpty(currentImagePath) OrElse Not File.Exists(currentImagePath) Then
            MessageBox.Show("No active image found to save changes.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' 2. Trigger the SaveSidecar logic
        ' This is centralized so it can be called by the button or an auto-save routine
        SaveSidecar(currentImagePath)
        btn_SaveChanges.Enabled = False
        btn_Remove_Changes.Text = "REMOVE CHANGES"
        btn_Remove_Changes.Enabled = True
        First_Load = 0
        Check_For_Changes()
    End Sub

    Private Sub Picture_Has_No_Changes()
        ' Reset the "No Changes" flag at the start of every check


        ' 1. Define the sliders and their neutral points
        Dim sliders50 = {sldExposure, sldContrast, sldHighlights, sldShadowsLift, sldWhites, sldBlacks, sldTemp, sldTint, sldBarrelDistortion, sldClarity, sldSaturation, sldVibrance, sldRedMix, sldGreenMix, sldBlueMix}
        Dim sliders0 = {sldDynCon, sldStructX, sldSharpen, sldDefringe, sldDenoise, sldNoiseLimit, sldNoiseChroma, sldNoiseLuma, sldTextureX, sldLutIntens, sldDeHaze, sldVignette, sldVignetteStd, sldSepiaMix, sldNoirFactor, sldVivid, sldFilmGrain, sldGrainSize, sldSplitToning, sldFade, sldFadeTint, sldCinematic, sldCineBalance}

        ' 2. Use "Any" to see if a single slider has moved [cite: 2026-02-14]
        Dim slidersChanged As Boolean = sliders50.Any(Function(s) s.Value <> 50) OrElse
                                        sliders0.Any(Function(s) s.Value <> 0)


        ' 2. Check Crop [New Logic]
        ' Assuming cropRect uses normalized coordinates (0 to 1)
        ' Default is X=0, Y=0, Width=1, Height=1
        Dim cropChanged As Boolean = (Math.Abs(cropRect.X - 0.1F) > 0.001F) OrElse
                                 (Math.Abs(cropRect.Y - 0.1F) > 0.001F) OrElse
                                 (Math.Abs(cropRect.Width - 0.8F) > 0.001F) OrElse
                                 (Math.Abs(cropRect.Height - 0.8F) > 0.001F)


        ' 3. Set your flag
        Pic_No_Changes = Not (slidersChanged OrElse cropChanged)



    End Sub

    Private Sub btn_ExportJPG_Click(sender As Object, e As EventArgs) Handles btn_ExportJPG.Click
        Panel1.Focus()
        If currentImage Is Nothing OrElse String.IsNullOrEmpty(currentImagePath) Then Return

        ' Creăm instanța SaveForm trimițând lățimea și înălțimea pozei curente
        Using frm As New SaveForm(currentImage.Width, currentImage.Height)
            If frm.ShowDialog() = DialogResult.OK AndAlso frm.IsAccepted Then

                ' Aici ai dimensiunile finale alese de user
                Dim finalW As Integer = frm.ExportWidth
                Dim finalH As Integer = frm.ExportHeight


                If currentImage Is Nothing OrElse textureID = 0 Then Return

                Dim sfd As New SaveFileDialog
                sfd.Filter = "JPEG Image|*.jpg"

                ' Pre-populate the input field with the original image's actual file name
                If Not String.IsNullOrEmpty(currentImagePath) Then
                    Dim originalName As String = System.IO.Path.GetFileName(currentImagePath)
                    sfd.FileName = "copy_" & originalName
                End If

                If sfd.ShowDialog <> DialogResult.OK Then Return


                GlControl2.MakeCurrent()
                Debug.WriteLine($"CropRect: {cropRect.X} {cropRect.Y} {cropRect.Width} {cropRect.Height}")
                ' 1. Dimensions
                Dim exportW = If(uIsCropApplied, CInt(finalW * cropRect.Width), finalW)
                Dim exportH = If(uIsCropApplied, CInt(finalH * cropRect.Height), finalH)

                ' 2. CREATE FBO
                Dim fbo = GL.GenFramebuffer
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, fbo)


                ' --- ASIGURĂ CALITATEA PENTRU PHOTO ---
                GL.ActiveTexture(TextureUnit.Texture0)
                GL.BindTexture(TextureTarget.Texture2D, textureID)
                ' Forțăm filtrarea cu Mipmaps pentru a evita zimții la export redus
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureMinFilter.LinearMipmapLinear)
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, TextureMagFilter.Linear)
                ' Ne asigurăm că piramida Mipmap este actualizată
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D)

                ' --- ASIGURĂ CALITATEA PENTRU TEXT ---
                If useText AndAlso _textTextureID <> -1 Then
                    GL.ActiveTexture(TextureUnit.Texture2) ' Sau unitatea folosită pentru text
                    GL.BindTexture(TextureTarget.Texture2D, _textTextureID)
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureMinFilter.LinearMipmapLinear)
                    GL.GenerateMipmap(GenerateMipmapTarget.Texture2D)
                End If



                Dim renderTex = GL.GenTexture
                GL.BindTexture(TextureTarget.Texture2D, renderTex)
                'GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, exportW, exportH, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero)

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba16f, exportW, exportH, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.HalfFloat, IntPtr.Zero)
                GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, renderTex, 0)

                ' 3. SETUP DRAW
                GL.Viewport(0, 0, exportW, exportH)
                GL.ClearColor(Color.Black)
                GL.Clear(ClearBufferMask.ColorBufferBit)

                ' --- PASS 1: DRAW THE PHOTO ---
                mainShader.Use()
                GL.ActiveTexture(TextureUnit.Texture0)
                GL.BindTexture(TextureTarget.Texture2D, textureID)
                GL.Uniform1(GL.GetUniformLocation(mainShader.Handle, "image"), 0)

                GL.Uniform1(GL.GetUniformLocation(mainShader.Handle, "uIsCropApplied"), If(uIsCropApplied, 1, 0))
                GL.Uniform4(GL.GetUniformLocation(mainShader.Handle, "uCropRect"), cropRect.X, cropRect.Y, cropRect.Width, cropRect.Height)
                UseTheUniforms(mainShader.Handle)

                GL.BindVertexArray(vao)
                GL.DrawArrays(PrimitiveType.Triangles, 0, 6)


                ' --------------------------------------------------------------------

                ' --- PASS 2: DRAW THE TEXT (Using your Buffer2 Logic) ---
                If useText AndAlso _textTextureID <> -1 Then
                    GL.Enable(EnableCap.Blend)
                    GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha)
                    textShader.Use()

                    ' In Export, the "View" is the export size
                    Dim exportAspect = exportW / CSng(exportH)
                    ' The text texture aspect (from GDI+ Bitmap)
                    Dim imgAspect = currentImage.Width / CSng(currentImage.Height)

                    ' 1. Logic from your Buffer2 to prevent stretching
                    Dim baseScaleX = 1.0F
                    Dim baseScaleY = 1.0F

                    If exportAspect > imgAspect Then
                        baseScaleX = imgAspect / exportAspect
                    Else
                        baseScaleY = exportAspect / imgAspect
                    End If

                    ' 2. Determine Scale 
                    ' Since we are already cropped, the text should be 1.0 scale relative 
                    ' to the original image proportions. 
                    Dim exportHalfW = baseScaleX
                    Dim exportHalfH = baseScaleY

                    ' 3. Calculate Vertices using your sharedVertices structure
                    ' Note: We don't use panX/panY here because the Export FBO is static
                    Dim exportVertices = {
                -exportHalfW, -exportHalfH, 0.0F, 0.0F, 1.0F, ' Bottom Left
                 exportHalfW, -exportHalfH, 0.0F, 1.0F, 1.0F, ' Bottom Right
                 exportHalfW, exportHalfH, 0.0F, 1.0F, 0.0F, ' Top Right
                -exportHalfW, -exportHalfH, 0.0F, 0.0F, 1.0F, ' Bottom Left
                 exportHalfW, exportHalfH, 0.0F, 1.0F, 0.0F, ' Top Right
                -exportHalfW, exportHalfH, 0.0F, 0.0F, 0.0F  ' Top Left
            }

                    ' 4. Upload and Draw
                    GL.BindBuffer(BufferTarget.ArrayBuffer, vbo)
                    GL.BufferData(BufferTarget.ArrayBuffer, exportVertices.Length * 4, exportVertices, BufferUsageHint.StaticDraw)

                    GL.ActiveTexture(TextureUnit.Texture0)
                    GL.BindTexture(TextureTarget.Texture2D, _textTextureID)
                    GL.Uniform1(GL.GetUniformLocation(textShader.Handle, "uTextTex"), 0)


                    ' Calculate base opacity (0.0 to 1.0)
                    Dim baseOpacity = sldTextOpacity.Value / 100.0F
                    ' Apply a "Visual Correction" factor. 
                    ' Squaring the value (Math.Pow(x, 2)) makes lower opacities 
                    ' appear thinner/lighter, matching the viewport's look.
                    Dim correctedOpacity As Single = Math.Pow(baseOpacity, 2.8) ' Adjust 1.25 to 1.5 if 10% is still too strong
                    GL.Uniform1(GL.GetUniformLocation(textShader.Handle, "uTextOpacity"), correctedOpacity)

                    ' Set Identity for Export MVP
                    Dim uMvpLocation = GL.GetUniformLocation(textShader.Handle, "uFinalMvp")
                    If uMvpLocation <> -1 Then
                        GL.UniformMatrix4(uMvpLocation, False, Matrix4.Identity)
                    End If

                    GL.BindVertexArray(vao)
                    GL.DrawArrays(PrimitiveType.Triangles, 0, 6)
                    GL.Disable(EnableCap.Blend)
                End If


                ' 4. READ PIXELS
                Dim bmp As New Bitmap(exportW, exportH, Imaging.PixelFormat.Format32bppArgb)
                Dim data = bmp.LockBits(New Rectangle(0, 0, exportW, exportH),
                 ImageLockMode.WriteOnly, Imaging.PixelFormat.Format32bppArgb)

                GL.ReadPixels(0, 0, exportW, exportH, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0)

                bmp.UnlockBits(data)
                bmp.RotateFlip(RotateFlipType.RotateNoneFlipY)
                'bmp.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg)

                Dim jpgEncoder = ImageCodecInfo.GetImageDecoders.FirstOrDefault(Function(c) c.FormatID = Imaging.ImageFormat.Jpeg.Guid)
                Dim encoderParams As New EncoderParameters(1)
                encoderParams.Param(0) = New EncoderParameter(Imaging.Encoder.Quality, 95L) ' 95% calitate

                'bmp.Save(sfd.FileName, jpgEncoder, encoderParams)
                ' ─── FIX: BYPASS GDI+ FILE LOCK FOR SAME-FILE OVERWRITES ─────────
                ' If overwriting the currently loaded file, GDI+ holds a lock on it.
                ' Saving via an intermediate MemoryStream or temporary file avoids the crash.
                Try
                    Using ms As New System.IO.MemoryStream()
                        bmp.Save(ms, jpgEncoder, encoderParams)
                        System.IO.File.WriteAllBytes(sfd.FileName, ms.ToArray())
                    End Using
                Catch ex As Exception
                    MessageBox.Show("Export write failed: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try



                ' 5. CLEANUP
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0)
                GL.DeleteTexture(renderTex)
                GL.DeleteFramebuffer(fbo)

                GL.Viewport(0, 0, GlControl2.ClientSize.Width, GlControl2.ClientSize.Height)
                SyncAllUniforms() ' Restore UI state
                GlControl2.Invalidate()

                'MessageBox.Show("Saved: " & exportW & " x " & exportH)
                ' ─── NEW ADDITION: CUSTOM FormInfo DIALOG WITH AUTO-ELLIPSIS HANDLING ────
                Using frmInfo As New Form()
                    ' Configure Form properties
                    frmInfo.Text = "Export Info"
                    frmInfo.Size = New Size(320, 170) ' Expanded slightly for a cleaner layout space
                    frmInfo.FormBorderStyle = FormBorderStyle.FixedDialog
                    frmInfo.MaximizeBox = False
                    frmInfo.MinimizeBox = False
                    frmInfo.StartPosition = FormStartPosition.CenterParent
                    frmInfo.BackColor = Color.FromArgb(35, 35, 35) ' Matches your dark theme
                    frmInfo.ForeColor = Color.White

                    ' Create and configure the Label
                    Dim lblMessage As New Label()
                    ' Stack the filename on top and dimensions safely underneath
                    lblMessage.Text = "Saved: " & System.IO.Path.GetFileName(sfd.FileName) & Environment.NewLine & "(" & exportW & " x " & exportH & ")"
                    lblMessage.Location = New Point(15, 20)
                    lblMessage.Size = New Size(275, 45) ' Taller box to support dual-line presentation cleanly
                    lblMessage.TextAlign = ContentAlignment.MiddleCenter
                    lblMessage.Font = New Font("Segoe UI", 10.0F, FontStyle.Regular)

                    ' 🟢 THE KEY FIX FOR LONG NAMES: Automatically insert "..." if text hits boundaries
                    lblMessage.AutoEllipsis = True

                    ' Create and configure the Close Button
                    Dim btnClose As New Button()
                    btnClose.Text = "Close"
                    btnClose.Location = New Point(102, 85) ' Re-centered perfectly to account for new width
                    btnClose.Size = New Size(100, 30)
                    btnClose.FlatStyle = FlatStyle.Flat
                    btnClose.FlatAppearance.BorderColor = Color.Gray
                    btnClose.BackColor = Color.FromArgb(50, 50, 50)
                    btnClose.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)

                    ' Set up button behavior
                    AddHandler btnClose.Click, Sub(btnSender, btnE) frmInfo.Close()
                    frmInfo.AcceptButton = btnClose ' Pressing Enter hits the Close button automatically

                    ' Assemble components and display
                    frmInfo.Controls.Add(lblMessage)
                    frmInfo.Controls.Add(btnClose)
                    frmInfo.ShowDialog(GlControl2.FindForm()) ' Lock input to the application until dismissed
                End Using
                ' ─────────────────────────────────────────────────────────────────────────

                lblStatus_Change.Text = $"Exporting at {finalW}x{finalH}..."
            End If
        End Using
    End Sub

    Private Sub btn_Remove_Changes_Click(sender As Object, e As EventArgs) Handles btn_Remove_Changes.Click
        Panel1.Focus()
        ' 1. Guard Clause: Make sure there's an image loaded
        If currentImage Is Nothing OrElse String.IsNullOrEmpty(currentImagePath) Then Return

        ' 2. Confirm with the user (Optional but safer for their nerves!)
        Dim result = MessageBox.Show("Delete all edits and revert to original?", "Confirm Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result <> DialogResult.Yes Then Return

        Try
            ' 3. Physically delete the .fstop file if it exists
            Dim sidecarPath = currentImagePath & ".fstop"
            If File.Exists(sidecarPath) Then
                File.Delete(sidecarPath)

            End If

            ' 4. Reset the UI Sliders
            ' Assuming you have a sub that sets all sldExposure.Value = 0 etc.
            ResetAllSliders()

            ' 5. Reset the Crop state
            uIsCropApplied = False
            uIsCropping = 1 ' Go back to showing the crop lines
            cropRect = New RectangleF(0.1F, 0.1F, 0.8F, 0.8F)

            ' 6. Clear the "Saved on" label
            btn_Remove_Changes.Enabled = False
            btn_Remove_Changes.Text = "Original Image"

            ' 7. Force the GPU to redraw the clean version
            ShowOriginal()
            btn_OrigEdit.Tag = "original"
            btn_OrigEdit.Text = "ORIGINAL - ( EDIT OFF )"
            btn_OrigEdit.BackColor = Color.FromArgb(50, 64, 64, 64)

        Catch ex As Exception
            MessageBox.Show("Error removing changes: " & ex.Message)
        End Try
    End Sub

    Private Sub DeletePictureFromStripToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeletePictureFromStripToolStripMenuItem.Click
        ' 1. Guard Clause: If for some reason we lost the reference, exit
        If clickedThumbnail Is Nothing Then Return

        ' 2. Get the path from the tag
        Dim pathToRemove As String = clickedThumbnail.Tag.ToString()

        ' 3. Reset the Editor if the removed image is the one being edited
        If pathToRemove = currentImagePath Then
            currentImage = Nothing
            currentImagePath = ""

            ' Clear the GPU Texture
            If textureID <> 0 Then
                GL.DeleteTexture(textureID)
                textureID = 0
            End If

            ' Reset Editor State
            uIsCropApplied = False
            lblStatus_Change.Text = "Image removed from strip."
            GlControl2.Invalidate()
        End If

        ' 4. Physically remove the control from the FlowLayoutPanel
        thumbnailPanel.Controls.Remove(clickedThumbnail)

        ' 5. Memory Cleanup
        clickedThumbnail.Dispose()
        clickedThumbnail = Nothing

        ' 6. Force the panel to rearrange the remaining thumbnails
        thumbnailPanel.PerformLayout()
    End Sub

    Private Sub DeletePictureFromStrip_Click(sender As Object, e As EventArgs) Handles DeletePictureFromStripToolStripMenuItem.Click
        If lastRightClickedThumb Is Nothing Then Return

        Dim pathToRemove As String = TryCast(lastRightClickedThumb.Tag, String)

        ' 1. If we are currently editing the photo we are deleting from the strip, clear the UI
        If pathToRemove = currentImagePath Then
            currentImage = Nothing
            currentImagePath = ""
            If textureID <> 0 Then
                GL.DeleteTexture(textureID)
                textureID = 0
            End If
            lblStatus_Change.Text = "No image loaded."
            GlControl2.Invalidate()
        End If

        ' 2. Remove the thumbnail from the panel
        thumbnailPanel.Controls.Remove(lastRightClickedThumb)

        ' 3. Cleanup memory
        lastRightClickedThumb.Dispose()
        lastRightClickedThumb = Nothing

        ' 4. Tell the FlowLayoutPanel to rearrange the remaining thumbnails
        thumbnailPanel.PerformLayout()
    End Sub

    Private Async Sub btn_ExportAll_Click(sender As Object, e As EventArgs) Handles btn_ExportAll.Click
        Panel1.Focus()
        If currentImage Is Nothing OrElse String.IsNullOrEmpty(currentImagePath) Then Return

        Using frm As New SaveMulti()
            If frm.ShowDialog() = DialogResult.OK AndAlso frm.IsAccepted Then

                ' Aici ai dimensiunile finale alese de user
                Dim finalRapScale As Integer = frm.Raport_Scale



                If thumbnailPanel.Controls.Count = 0 Then Return

                Save_All_PicText = True
                pbExportAll.Visible = True
                btn_ExportAll.Enabled = False
                pbExportAll.Value = 0
                pbExportAll.Maximum = thumbnailPanel.Controls.Count

                Dim firstPath = thumbnailPanel.Controls(0).Tag.ToString
                Dim exportDir = Path.Combine(Path.GetDirectoryName(firstPath), "EXPORT")

                ' Folosim System.IO pentru siguranță
                If Not System.IO.Directory.Exists(exportDir) Then System.IO.Directory.CreateDirectory(exportDir)

                ' --- ASIGURĂM CONTEXTUL GRAFIC ---
                GlControl2.MakeCurrent()

                ' Definim extensiile RAW acceptate
                Dim rawExtensions = {".cr2", ".cr3", ".nef", ".arw", ".dng", ".rw2", ".raf"}

                For i = 0 To thumbnailPanel.Controls.Count - 1
                    Dim ctrl = thumbnailPanel.Controls(i)
                    Dim filePath = ctrl.Tag.ToString

                    ' --- LOGICĂ NOUĂ DE DENUMIRE (Evită coliziunea nume.arw vs nume.jpg) ---
                    Dim fileNameOnly = Path.GetFileNameWithoutExtension(filePath)
                    Dim originalExtLabel = Path.GetExtension(filePath).Replace(".", "").ToUpper

                    ' Rezultat: "nume_ARW.jpg" sau "nume_JPG.jpg"
                    Dim newFileName = $"{fileNameOnly}_{originalExtLabel}.jpg"
                    Dim savePath = Path.Combine(exportDir, newFileName)

                    Dim ext = Path.GetExtension(filePath).ToLower

                    Try
                        Dim imgForUI As Bitmap = Nothing
                        Dim imgForExport As Bitmap = Nothing

                        ' --- LOGICĂ DE ÎNCĂRCARE (JPG vs RAW) ---
                        If rawExtensions.Contains(ext) Then
                            ' CAZUL RAW: Extragem preview-ul high-res
                            Dim rawPreview = GetHighResEmbeddedPreview(filePath)
                            If rawPreview IsNot Nothing Then
                                imgForUI = New Bitmap(rawPreview)
                                imgForExport = New Bitmap(rawPreview)
                                rawPreview.Dispose()
                            End If
                        Else
                            ' CAZUL JPG/Altele: Citim fără să blocăm fișierul folosind ReadAllBytes
                            Dim rawBytes = File.ReadAllBytes(filePath)
                            Using ms As New MemoryStream(rawBytes)
                                Using temp As New Bitmap(ms)
                                    imgForUI = New Bitmap(temp)
                                    imgForExport = New Bitmap(temp)
                                End Using
                            End Using
                        End If

                        ' --- PROCESARE DACĂ AVEM IMAGINE ---
                        If imgForUI IsNot Nothing Then
                            ' 3. Sincronizăm UI-ul (Referința curentă)
                            currentImage = imgForUI

                            ' Încărcăm setările (sidecar) în mod Batch
                            LoadSidecar(filePath, isBatchExport:=True)

                            ' Trimitem pixelii la GPU
                            LoadTextureFromBitmap(imgForExport)

                            ' 4. Forțăm desenarea pe ecran (Feedback vizual în timpul exportului)
                            GlControl2.Invalidate()
                            GlControl2.Update()
                            Application.DoEvents()

                            ' 5. EXPORT (Salvarea pe disc)
                            AutomatedMultiExport(imgForExport, savePath, finalRapScale)

                            ' 6. Curățenie imediată a resurselor
                            imgForExport.Dispose()
                            imgForUI.Dispose()
                            currentImage = Nothing
                        End If

                    Catch ex As Exception
                        Debug.WriteLine("Sărit peste poza: " & newFileName & " din cauza: " & ex.Message)
                    End Try

                    ' Update UI Progress
                    pbExportAll.Value = i + 1
                    lblStatus_Change.Text = "Procesat: " & i + 1 & "/" & thumbnailPanel.Controls.Count
                    Application.DoEvents()
                Next

                ' --- RE-LOAD FIRST IMAGE AFTER EXPORT ---
                ' Această secțiune restabilește mediul de lucru pe prima imagine din listă
                If thumbnailPanel.Controls.Count > 0 Then
                    Dim firstImgPath = thumbnailPanel.Controls(0).Tag.ToString
                    Dim firstExt = Path.GetExtension(firstImgPath).ToLower

                    Try
                        If rawExtensions.Contains(firstExt) Then
                            currentImage = GetHighResEmbeddedPreview(firstImgPath)
                        Else
                            Dim b = File.ReadAllBytes(firstImgPath)
                            Using ms As New MemoryStream(b)
                                currentImage = New Bitmap(ms)
                            End Using
                        End If

                        If currentImage IsNot Nothing Then
                            ' Resetăm modul batch pentru a reveni la editare normală
                            LoadSidecar(firstImgPath, isBatchExport:=False)
                            LoadTextureFromBitmap(currentImage)

                            ' Forțăm reîncadrarea și desenarea
                            FitToScreen()
                            GlControl2.Invalidate()
                            GlControl2.Update()
                        End If
                    Catch ex As Exception
                        Debug.WriteLine("Eroare la reîncărcarea primei poze: " & ex.Message)
                    End Try
                End If

                ' Restaurăm starea butoanelor
                btn_ExportAll.Enabled = True
                pbExportAll.Visible = False
                Save_All_PicText = False

                MessageBox.Show("Export finalizat în: " & exportDir, "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Deschidem folderul automat la final
                Process.Start("explorer.exe", exportDir)



            End If

        End Using

    End Sub

    Private Sub AutomatedMultiExport(ByVal img As Bitmap, ByVal savePath As String, ByVal FRS As Integer)
        If img Is Nothing Then Return

        GlControl2.MakeCurrent()

        ' 1. Dimensions
        Dim exportW As Integer = If(uIsCropApplied, CInt(currentImage.Width * cropRect.Width / FRS), currentImage.Width / FRS)
        Dim exportH As Integer = If(uIsCropApplied, CInt(currentImage.Height * cropRect.Height / FRS), currentImage.Height / FRS)

        ' 2. FBO Setup
        Dim fbo As Integer = GL.GenFramebuffer()
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, fbo)

        Dim renderTex As Integer = GL.GenTexture()
        GL.BindTexture(TextureTarget.Texture2D, renderTex)
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, exportW, exportH, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero)
        GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, renderTex, 0)

        ' 3. Upload source image
        Dim sourceTex As Integer = GL.GenTexture()
        GL.ActiveTexture(TextureUnit.Texture0)
        GL.BindTexture(TextureTarget.Texture2D, sourceTex)

        Dim bmpData As Imaging.BitmapData = img.LockBits(New Rectangle(0, 0, img.Width, img.Height), Imaging.ImageLockMode.ReadOnly, Imaging.PixelFormat.Format32bppArgb)
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, img.Width, img.Height, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, bmpData.Scan0)
        img.UnlockBits(bmpData)

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, All.Linear)
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, All.Linear)

        ' 4. Render
        GL.Viewport(0, 0, exportW, exportH)
        GL.ClearColor(Color.Black)
        GL.Clear(ClearBufferMask.ColorBufferBit)

        mainShader.Use()
        GL.ActiveTexture(TextureUnit.Texture0)
        GL.BindTexture(TextureTarget.Texture2D, sourceTex)

        Dim correctedY As Single = If(uIsCropApplied, 1.0F - cropRect.Y - cropRect.Height, cropRect.Y)
        GL.Uniform1(GL.GetUniformLocation(mainShader.Handle, "uIsCropApplied"), If(uIsCropApplied, 1, 0))
        GL.Uniform4(GL.GetUniformLocation(mainShader.Handle, "uCropRect"), cropRect.X, correctedY, cropRect.Width, cropRect.Height)


        'flip the color of replacement
        ' 1. Salvăm valoarea originală (cea bună pentru ecran/UI)
        Dim originalV As Single = selectionV

        ' 2. Inversăm axa Y special pentru randarea în Framebuffer (FBO)
        ' Dacă pe ecran punctul e la 0.2 (sus), în FBO OpenGL el trebuie să fie la 0.8
        selectionV = 1.0F - selectionV


        UseTheUniforms(mainShader.Handle)

        GL.BindVertexArray(vao)
        GL.DrawArrays(PrimitiveType.Triangles, 0, 6)


        ' --- PASS 2: DRAW THE TEXT (Using your Buffer2 Logic) ---
        If useText AndAlso _textTextureID <> -1 Then
            GL.Enable(EnableCap.Blend)
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha)
            textShader.Use()

            ' In Export, the "View" is the export size
            Dim exportAspect As Single = CSng(exportW) / CSng(exportH)
            ' The text texture aspect (from GDI+ Bitmap)
            Dim imgAspect As Single = CSng(currentImage.Width) / CSng(currentImage.Height)

            ' 1. Logic from your Buffer2 to prevent stretching
            Dim baseScaleX As Single = 1.0F
            Dim baseScaleY As Single = 1.0F

            If exportAspect > imgAspect Then
                baseScaleX = imgAspect / exportAspect
            Else
                baseScaleY = exportAspect / imgAspect
            End If

            ' 2. Determine Scale 
            ' Since we are already cropped, the text should be 1.0 scale relative 
            ' to the original image proportions. 
            Dim exportHalfW As Single = baseScaleX
            Dim exportHalfH As Single = baseScaleY

            ' 3. Calculate Vertices using your sharedVertices structure
            ' Note: We don't use panX/panY here because the Export FBO is static
            Dim exportVertices As Single() = {
                -exportHalfW, -exportHalfH, 0.0F, 0.0F, 0.0F, ' Bottom Left
                 exportHalfW, -exportHalfH, 0.0F, 1.0F, 0.0F, ' Bottom Right
                 exportHalfW, exportHalfH, 0.0F, 1.0F, 1.0F, ' Top Right
                -exportHalfW, -exportHalfH, 0.0F, 0.0F, 0.0F, ' Bottom Left
                 exportHalfW, exportHalfH, 0.0F, 1.0F, 1.0F, ' Top Right
                -exportHalfW, exportHalfH, 0.0F, 0.0F, 1.0F  ' Top Left
            }

            ' 4. Upload and Draw
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo)
            GL.BufferData(BufferTarget.ArrayBuffer, exportVertices.Length * 4, exportVertices, BufferUsageHint.StaticDraw)

            GL.ActiveTexture(TextureUnit.Texture0)
            GL.BindTexture(TextureTarget.Texture2D, _textTextureID)
            GL.Uniform1(GL.GetUniformLocation(textShader.Handle, "uTextTex"), 0)


            ' Calculate base opacity (0.0 to 1.0)
            Dim baseOpacity As Single = CSng(sldTextOpacity.Value / 100.0F)
            ' Apply a "Visual Correction" factor. 
            ' Squaring the value (Math.Pow(x, 2)) makes lower opacities 
            ' appear thinner/lighter, matching the viewport's look.
            Dim correctedOpacity As Single = CSng(Math.Pow(baseOpacity, 2.8)) ' Adjust 1.25 to 1.5 if 10% is still too strong
            GL.Uniform1(GL.GetUniformLocation(textShader.Handle, "uTextOpacity"), correctedOpacity)

            ' Set Identity for Export MVP
            Dim uMvpLocation As Integer = GL.GetUniformLocation(textShader.Handle, "uFinalMvp")
            If uMvpLocation <> -1 Then
                GL.UniformMatrix4(uMvpLocation, False, Matrix4.Identity)
            End If

            GL.BindVertexArray(vao)
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6)
            GL.Disable(EnableCap.Blend)
        End If


        ' 5. Save
        GL.Finish() ' FORȚĂM GPU să termine randarea înainte de citire

        Using bmpOut As New Bitmap(exportW, exportH, Imaging.PixelFormat.Format32bppArgb)
            Dim dataOut As Imaging.BitmapData = bmpOut.LockBits(New Rectangle(0, 0, exportW, exportH), Imaging.ImageLockMode.WriteOnly, Imaging.PixelFormat.Format32bppArgb)
            GL.ReadPixels(0, 0, exportW, exportH, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, dataOut.Scan0)
            bmpOut.UnlockBits(dataOut)

            'bmpOut.RotateFlip(RotateFlipType.RotateNoneFlipY)

            ' SALVARE ANTI-GDI-ERROR: Salvăm într-un Stream, apoi scriem fișierul
            Try
                Using ms As New System.IO.MemoryStream()
                    bmpOut.Save(ms, Imaging.ImageFormat.Jpeg)
                    Dim bmpBytes As Byte() = ms.ToArray()

                    ' Ștergem dacă există și scriem bytes direct (metoda cea mai sigură)
                    If System.IO.File.Exists(savePath) Then System.IO.File.Delete(savePath)
                    System.IO.File.WriteAllBytes(savePath, bmpBytes)
                End Using
            Catch ex As Exception
                Debug.WriteLine("Eroare critică la scriere disc: " & ex.Message)
            End Try
        End Using

        ' 6. Cleanup
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0)
        GL.DeleteTexture(renderTex)
        GL.DeleteTexture(sourceTex)
        GL.DeleteFramebuffer(fbo)

    End Sub


    Private Sub tsRGB_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles tsRGB.ItemClicked
        ' 1. Logic to make buttons act like Radio Buttons
        For Each item As ToolStripItem In tsRGB.Items
            If TypeOf item Is ToolStripButton Then
                DirectCast(item, ToolStripButton).Checked = (item Is e.ClickedItem)
            End If
        Next

        ' 2. Assign the mode based on the button clicked
        ' Check the "Name" property of your buttons in the designer
        Select Case e.ClickedItem.Name
            Case "btnAll" : histMode = 0
            Case "btnLuma" : histMode = 1
            Case "btnRed" : histMode = 2
            Case "btnGreen" : histMode = 3
            Case "btnBlue" : histMode = 4
        End Select

        ' 3. Refresh the Histogram GLControl
        GlControl2.Invalidate()
    End Sub

    ' Call this in Form_Load to "Unlock" them and set the style
    Private Sub PrepareTextBoxes()
        Dim allTxt = {txt_Exposure, txt_Contrast, txt_Highlights} ' Add all 29 here
        For Each txt In allTxt
            txt.BackColor = Color.FromArgb(45, 45, 48) ' Match your sidebar
            txt.ForeColor = Color.White
            txt.BorderStyle = BorderStyle.None
            txt.TextAlign = HorizontalAlignment.Right
        Next
    End Sub

    ' ONE Sub to handle ALL 29 Textboxes
    Private Sub All_TextBoxes_KeyDown(sender As Object, e As KeyEventArgs) _
    Handles txt_Exposure.KeyDown, txt_Contrast.KeyDown, txt_Highlights.KeyDown, txt_ShadowsLift.KeyDown, txt_Whites.KeyDown, txt_Blacks.KeyDown, txt_Temp.KeyDown, txt_Tint.KeyDown, txt_DynCon.KeyDown, txt_StructX.KeyDown, txt_Sharpen.KeyDown, txt_Defringe.KeyDown, txt_Denoise.KeyDown, txt_NoiseLimit.KeyDown, txt_NoiseChroma.KeyDown, txt_NoiseLuma.KeyDown, txt_TextureX.KeyDown, txt_Clarity.KeyDown, txt_Saturation.KeyDown, txt_Vibrance.KeyDown, txt_RedMix.KeyDown, txt_GreenMix.KeyDown, txt_BlueMix.KeyDown, txt_LutIntens.KeyDown, txt_DeHaze.KeyDown, txt_Vignette.KeyDown, txt_VignetteStd.KeyDown


        If e.KeyCode = Keys.Enter Then
            Dim box = DirectCast(sender, TextBox)
            Dim val As Double

            If Double.TryParse(box.Text, val) Then
                ' 1. Find the matching slider (assuming naming convention: txt_Name -> trk_Name)
                Dim sliderName = box.Name.Replace("txt_", "sld")
                Dim slider = DirectCast(Controls.Find(sliderName, True).FirstOrDefault, ModernSlider)

                If slider IsNot Nothing Then
                    ' 2. Convert text value back to slider scale (e.g., 0.76 -> slider 88)
                    ' Adjust this math to match your specific slider ranges

                    ' from 0 to 50
                    If sliderName = "sldDynCon" Or sliderName = "sldStructX" Or sliderName = "sldSharpen" Or sliderName = "sldDenoise" Or sliderName = "sldNoiseLimit" _
                       Or sliderName = "sldNoiseChroma" Or sliderName = "sldNoiseLuma" Or sliderName = "sldTextureX" Or sliderName = "sldLutIntens" Or sliderName = "sldDeHaze" Or sliderName = "sldVignette" Or sliderName = "sldVignetteStd" Then
                        slider.Value = CInt(val * 2)
                    End If

                    ' from 0 to 100
                    If sliderName = "sldDefringe" Or sliderName = "sldSaturation" Or sliderName = "sldVibrance" Or sliderName = "sldRedMix" Or sliderName = "sldGreenMix" Or sliderName = "sldBlueMix" Then
                        slider.Value = CInt(val)
                    End If

                    'from  -100 to 100
                    If sliderName = "sldExposure" Or sliderName = "sldContrast" Or sliderName = "sldClarity" Or sliderName = "sldTemp" Or sliderName = "sldTint" Then
                        slider.Value = CInt((val + 100) / 2)
                    End If

                    'from  -50 to 50
                    If sliderName = "sldHighlights" Or sliderName = "sldShadowsLift" Or sliderName = "sldWhites" Or sliderName = "sldBlacks" Then
                        slider.Value = CInt(val + 50)
                    End If


                End If

                ' 4. Clean up UI focus
                ActiveControl = Nothing ' Removes focus from textbox 
                e.SuppressKeyPress = True ' Stops the "Ding" sound
            End If
        End If
    End Sub
    Private Sub txt_MouseEnter(sender As Object, e As EventArgs) Handles txt_Exposure.MouseEnter
        ' DirectCast(sender, TextBox).BorderStyle = BorderStyle.FixedSingle
    End Sub

    Private Sub txt_MouseLeave(sender As Object, e As EventArgs) Handles txt_Exposure.MouseLeave
        ' If Not DirectCast(sender, TextBox).Focused Then
        ' DirectCast(sender, TextBox).BorderStyle = BorderStyle.None
        ' End If
    End Sub


    Private Sub RenderTextToBitmap(text As String, fnt As Font, clr As Color)
        If String.IsNullOrEmpty(text) OrElse currentImage Is Nothing Then Return

        ' Use the ACTUAL image dimensions. 
        ' This ensures the 0.0-1.0 coordinates match the photo perfectly.
        Dim canvasW As Integer = currentImage.Width
        Dim canvasH As Integer = currentImage.Height

        Using bmp As New Bitmap(canvasW, canvasH)
            Using g As Graphics = Graphics.FromImage(bmp)
                g.Clear(Color.Transparent)

                ' Scale the font so it doesn't look tiny on big RAW files
                Dim scaleFactor As Single = canvasH / 1000.0F
                Using scaledFont As New Font(fnt.FontFamily, fnt.Size * scaleFactor, fnt.Style)
                    Using brsh As New SolidBrush(clr)
                        ' Draw the text
                        g.DrawString(text, scaledFont, brsh,
                                 textPos_txt.X * canvasW,
                                 textPos_txt.Y * canvasH)
                    End Using
                End Using
            End Using
            UploadBitmapToGPU(bmp)
        End Using
    End Sub


    Private Sub UploadBitmapToGPU(bmp As Bitmap)
        On Error Resume Next
        ' 1. If an old texture exists, delete it to prevent memory leaks
        If _textTextureID > 0 Then
            GL.DeleteTexture(_textTextureID)
        End If

        ' 2. Generate the new ID
        _textTextureID = GL.GenTexture()
        GL.BindTexture(TextureTarget.Texture2D, _textTextureID)

        ' 3. Set parameters so the text looks sharp
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, All.Linear)
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, All.Linear)
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, All.ClampToEdge)
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, All.ClampToEdge)

        ' 4. Lock the Bitmap bits to copy to GPU
        Dim data As Imaging.BitmapData = bmp.LockBits(New Rectangle(0, 0, bmp.Width, bmp.Height),
                                                Imaging.ImageLockMode.ReadOnly,
                                                Imaging.PixelFormat.Format32bppArgb)

        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                  data.Width, data.Height, 0,
                  OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0)

        bmp.UnlockBits(data)
        GL.BindTexture(TextureTarget.Texture2D, 0) ' Unbind
    End Sub


    ' --- Button: Select Font ---
    Private Sub btnFont_Click(sender As Object, e As EventArgs) Handles btnFont.Click
        Using fd As New FontDialog
            fd.Font = currentFont
            If fd.ShowDialog() = DialogResult.OK Then
                ' 1. Store the new font
                currentFont = fd.Font

                ' 2. Update your UI
                lblFontName.Text = currentFont.Name
                numFontSize.Value = CDec(currentFont.SizeInPoints)

                ' 3. Re-render the text for the GPU
                RenderTextToBitmap(txtWatermark.Text, currentFont, currentColor)
                GlControl2.Invalidate()
            End If
        End Using
    End Sub

    ' --- Button: Select Color ---
    Private Sub btnSelectColor_Click(sender As Object, e As EventArgs) Handles btnSelectColor.Click
        Using cd As New ColorDialog
            cd.Color = textColor
            If cd.ShowDialog() = DialogResult.OK Then
                textColor = cd.Color
                currentColor = cd.Color


                ' Update the button color so you can see what you picked
                btnSelectColor.BackColor = textColor

                ' Re-render and Redraw
                RenderTextToBitmap(txtWatermark.Text, currentFont, textColor)
                GlControl2.Invalidate()
            End If
        End Using
    End Sub

    Private Sub numFontSize_ValueChanged(sender As Object, e As EventArgs) Handles numFontSize.ValueChanged
        ' 1. Update the font object
        currentFont = New Font(currentFont.FontFamily, CSng(numFontSize.Value), currentFont.Style)

        ' 2. PASS THE ARGUMENTS to the drawing function
        ' This ensures the bitmap is redrawn with the NEW size and the CURRENT color
        RenderTextToBitmap(txtWatermark.Text, currentFont, textColor)

        ' 3. UI and Viewport update
        GlControl2.Invalidate()
        sld_FNT_Size.Value = CInt(numFontSize.Value)
    End Sub

    Private Sub RenderTextToBitmap()
        If String.IsNullOrEmpty(txtWatermark.Text) Then Return

        ' Create a transparent bitmap matching the viewport
        Using bmp As New Bitmap(GlControl2.Width, GlControl2.Height)
            Using g As Graphics = Graphics.FromImage(bmp)
                g.Clear(Color.Transparent)
                g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

                Using brush As New SolidBrush(currentColor) ' Use currentColor here
                    ' Draw text at our custom position
                    g.DrawString(txtWatermark.Text, currentFont, brush,
                             textPos_txt.X * GlControl2.Width,
                             textPos_txt.Y * GlControl2.Height)
                End Using
            End Using

            ' Call the GPU upload helper
            UploadBitmapToGPU(bmp)
        End Using
    End Sub

    Private Sub txtWatermark_TextChanged(sender As Object, e As EventArgs) Handles txtWatermark.TextChanged
        ' 1. Render the text to bitmap and upload to GPU (this will assign _textTextureID)
        RenderTextToBitmap(txtWatermark.Text, currentFont, textColor)

        ' 2. Now _textTextureID is valid, we can update the useText flag
        useText = (_textTextureID > 0 AndAlso Not String.IsNullOrEmpty(txtWatermark.Text))

        ' 3. Update all uniforms in the shader
        SyncAllUniforms()
        GlControl2.Invalidate()
    End Sub


    Private Sub BindTextureSafe(unit As TextureUnit, target As TextureTarget, id As Integer)
        GL.ActiveTexture(unit)

        ' Only clear the type you are about to bind
        GL.BindTexture(target, 0)

        ' Bind the actual texture if valid
        If id > 0 Then
            GL.BindTexture(target, id)
        End If
    End Sub



    Private Sub sldTextOpacity_Scroll(sender As Object, e As EventArgs) Handles sldTextOpacity.Scroll
        ' 1. Update the display label if you have one (e.g., "85%")
        lblTextOpacityValue.Text = sldTextOpacity.Value.ToString() & "%"

        ' 2. Trigger the full sync
        ' This ensures that while you change opacity, the LUT (Slot 1) 
        ' and the Image (Slot 0) stay bound to their textures.
        SyncAllUniforms()

        ' 3. Redraw the viewport
        GlControl2.Invalidate()
    End Sub

    '/////////////////////////////////////////////////////////TEXT//////////////////////////////////////////////////////////////
    Private Sub UpdateTextTexture()
        If String.IsNullOrEmpty(txtWatermark.Text) Then
            useText = False
            Return
        End If

        ' 1. Create a transparent bitmap (Match it to your image or a fixed high-res)
        ' Using 2048x2048 provides crisp text even when zooming
        Using bmp As New Bitmap(2048, 2048)
            Using g As Graphics = Graphics.FromImage(bmp)
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit
                g.Clear(Color.Transparent)

                ' Draw the text (using your variables)
                Using brush As New SolidBrush(textColor)
                    ' Center the text in our transparent canvas for easier rotation/scaling later
                    Dim size = g.MeasureString(txtWatermark.Text, currentFont)
                    g.DrawString(txtWatermark.Text, currentFont, brush, (bmp.Width - size.Width) / 2, (bmp.Height - size.Height) / 2)
                End Using
            End Using

            ' 2. Upload to GPU (Slot 2)
            GL.ActiveTexture(TextureUnit.Texture2)
            If _textTextureID = -1 Then _textTextureID = GL.GenTexture()
            GL.BindTexture(TextureTarget.Texture2D, _textTextureID)

            Dim data = bmp.LockBits(New Rectangle(0, 0, bmp.Width, bmp.Height),
                                Imaging.ImageLockMode.ReadOnly, Imaging.PixelFormat.Format32bppPArgb)

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                      bmp.Width, bmp.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                      PixelType.UnsignedByte, data.Scan0)

            bmp.UnlockBits(data)

            ' Standard filtering for text
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, All.Linear)
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, All.Linear)
        End Using

        useText = True
        GlControl2.Invalidate()
    End Sub









    '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////








    Private Sub InitHUDFrameBuffer(w As Integer, h As Integer)
        ' 1. Cleanup old buffer if it exists
        If fboHUD <> 0 Then
            GL.DeleteFramebuffer(fboHUD)
            GL.DeleteTexture(fboTexture)
            GL.DeleteRenderbuffer(rboDepth)
        End If

        ' 2. Create the Framebuffer (The Easel)
        fboHUD = GL.GenFramebuffer()
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, fboHUD)

        ' 3. Create the Texture (The Canvas)
        fboTexture = GL.GenTexture()
        GL.BindTexture(TextureTarget.Texture2D, fboTexture)
        OpenTK.Graphics.OpenGL.GL.TexImage2D(TextureTarget.Texture2D, 0, OpenTK.Graphics.OpenGL.PixelInternalFormat.Rgba, w, h, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, OpenTK.Graphics.OpenGL.PixelType.UnsignedByte, IntPtr.Zero)
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureMinFilter.Linear)
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, TextureMagFilter.Linear)

        ' Attach Texture to Framebuffer
        GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, fboTexture, 0)

        ' 4. Create Depth Buffer (The Support)
        rboDepth = GL.GenRenderbuffer()
        GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, rboDepth)
        GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.DepthComponent24, w, h)
        GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, rboDepth)

        ' Check if it worked
        If GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) <> FramebufferStatus.FramebufferComplete Then
            Debug.WriteLine("ERROR: Framebuffer is not complete!")
        End If

        ' Unbind to return to normal screen
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0)
    End Sub

    Private Sub DrawFBToScreen()
        If vaoHUD = 0 OrElse screenShader Is Nothing Then Return

        screenShader.Use()

        ' Bind the Virtual Canvas texture to Unit 0
        GL.ActiveTexture(TextureUnit.Texture0)
        GL.BindTexture(TextureTarget.Texture2D, fboTexture)

        ' Explicitly tell the screen shader that the texture is on Unit 0
        GL.Uniform1(GL.GetUniformLocation(screenShader.Handle, "screenTexture"), 0)

        GL.BindVertexArray(vaoHUD)
        GL.DrawArrays(PrimitiveType.Triangles, 0, 6)
        GL.BindVertexArray(0)
    End Sub

    Private Sub DrawHUDOverlay()
        ' 1. We are already using screenShader from the previous step in GL_Paint
        ' 2. We are already bound to Framebuffer 0 (the screen)

        ' Disable Depth Testing so the text always stays on top
        GL.Disable(EnableCap.DepthTest)
        GL.Enable(EnableCap.Blend)
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha)

        ' --- EXAMPLE: DRAW YOUR FILENAME OR EXPOSURE ---
        ' If you use a texture-based text (the uTextTex method):
        ' GL.BindTexture(TextureTarget.Texture2D, textTextureID)
        ' GL.DrawArrays(...) 

        ' --- EXAMPLE: DRAW A SIMPLE BORDER OR CROSSHAIR ---
        ' This is where you put any technical info that shouldn't be 
        ' affected by the photo's 3D LUT or Cropping.

        GL.Disable(EnableCap.Blend)
        GL.Enable(EnableCap.DepthTest)
    End Sub

    '=====GEOMETRY
    ' These must be declared at the TOP of your Class (Global)


    Private Sub InitFBO(width As Integer, height As Integer)
        ' --- Setup Buffer 1 (Photo) ---
        fboPhoto = GL.GenFramebuffer()
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, fboPhoto)

        fboTexture1 = GL.GenTexture()
        GL.BindTexture(TextureTarget.Texture2D, fboTexture1)
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
              width, height, 0,
              OpenTK.Graphics.OpenGL.PixelFormat.Rgba, ' <--- Fully qualified name
              PixelType.UnsignedByte, IntPtr.Zero)
        ' Important: Set filters or it will be black!
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, All.Linear)
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, All.Linear)
        GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, fboTexture1, 0)

        ' --- Setup Buffer 2 (Text) ---
        fboTextBuffer = GL.GenFramebuffer()
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, fboTextBuffer)

        fboTexture2 = GL.GenTexture()
        GL.BindTexture(TextureTarget.Texture2D, fboTexture2)
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
              width, height, 0,
              OpenTK.Graphics.OpenGL.PixelFormat.Rgba, ' <--- Fully qualified name
              PixelType.UnsignedByte, IntPtr.Zero)
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, All.Linear)
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, All.Linear)
        GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, fboTexture2, 0)

        GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0)
    End Sub

    Private Sub InitHUDResources()
        ' 1. Generate the IDs
        vaoHUD = GL.GenVertexArray()
        vboHUD = GL.GenBuffer()

        ' 2. Bind and Fill with data
        GL.BindVertexArray(vaoHUD)
        GL.BindBuffer(BufferTarget.ArrayBuffer, vboHUD)

        ' Data layout: X, Y, Z, U, V (Matching your main photo's 20-byte stride)
        Dim quadVertices As Single() = {
            -1.0F, 1.0F, 0.0F, 0.0F, 1.0F, ' Top Left
            -1.0F, -1.0F, 0.0F, 0.0F, 0.0F, ' Bottom Left
             1.0F, -1.0F, 0.0F, 1.0F, 0.0F, ' Bottom Right
            -1.0F, 1.0F, 0.0F, 0.0F, 1.0F, ' Top Left
             1.0F, -1.0F, 0.0F, 1.0F, 0.0F, ' Bottom Right
             1.0F, 1.0F, 0.0F, 1.0F, 1.0F  ' Top Right
        }

        GL.BufferData(BufferTarget.ArrayBuffer, quadVertices.Length * 4, quadVertices, BufferUsageHint.StaticDraw)

        ' 3. Set Attributes (Must match passthrough.vert locations)

        ' Location 0: Position (3 components: X, Y, Z)
        GL.EnableVertexAttribArray(0)
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, False, 20, 0)

        ' Location 1: UV/TexCoords (2 components: U, V)
        ' Offset is 12 because X,Y,Z take up 12 bytes (3 * 4)
        GL.EnableVertexAttribArray(1)
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, False, 20, 12)

        ' 4. Clean up binding
        GL.BindVertexArray(0)
    End Sub

    ' Run this ONCE at startup
    Private Sub InitGlobalGeometry()
        vao = GL.GenVertexArray()
        vbo = GL.GenBuffer()

        GL.BindVertexArray(vao)
        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo)

        ' Attribute 0: Position (3 floats: x, y, z)
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, False, 5 * 4, 0)
        GL.EnableVertexAttribArray(0)

        ' Attribute 1: TexCoords (2 floats: u, v)
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, False, 5 * 4, 3 * 4)
        GL.EnableVertexAttribArray(1)

        GL.BindVertexArray(0) ' Unbind for safety
    End Sub


    Private Function GetMouseInImageSpace(mx As Integer, my As Integer) As PointF
        If currentImage Is Nothing Then Return New PointF(0, 0)
        Dim viewW As Single = CSng(GlControl2.ClientSize.Width)
        Dim viewH As Single = CSng(GlControl2.ClientSize.Height)

        Dim imgW As Single = CSng(currentImage.Width)
        Dim imgH As Single = CSng(currentImage.Height)

        Dim imgAspect As Single = imgW / imgH
        Dim viewAspect As Single = viewW / viewH

        ' Calculate actual displayed dimensions (letterboxing)
        Dim baseScaleX As Single = If(viewAspect > imgAspect, imgAspect / viewAspect, 1.0F)
        Dim baseScaleY As Single = If(viewAspect > imgAspect, 1.0F, viewAspect / imgAspect)

        Dim actualDrawW As Single = viewW * baseScaleX * CSng(zoomFactor)
        Dim actualDrawH As Single = viewH * baseScaleY * CSng(zoomFactor)

        ' Calculate start positions (centering + pan)
        Dim imgStartX As Single = (viewW / 2.0F) + CSng(panX) - (actualDrawW / 2.0F)
        Dim imgStartY As Single = (viewH / 2.0F) + CSng(panY) - (actualDrawH / 2.0F)

        ' Normalized Coordinates (0.0 to 1.0)
        Dim u As Single = (mx - imgStartX) / actualDrawW
        Dim v As Single = 1.0F - ((my - imgStartY) / actualDrawH) ' CRITICAL FLIP

        Return New PointF(u, v)
    End Function

    Private Function GMI_Space(screenX As Integer, screenY As Integer) As PointF

        If currentImage Is Nothing Then Return New PointF(0, 0)

        Dim viewW As Single = CSng(GlControl2.ClientSize.Width)
        Dim viewH As Single = CSng(GlControl2.ClientSize.Height)

        ' 1. Use CURRENT image dimensions (the physically cropped ones)
        Dim imgW As Single = CSng(currentImage.Width)
        Dim imgH As Single = CSng(currentImage.Height)

        Dim imgAspect As Single = imgW / imgH
        Dim viewAspect As Single = viewW / viewH

        ' 2. Calculate the base scale (matches UpdateVertices exactly)
        Dim baseScaleX As Single = If(viewAspect > imgAspect, imgAspect / viewAspect, 1.0F)
        Dim baseScaleY As Single = If(viewAspect > imgAspect, 1.0F, viewAspect / imgAspect)

        ' 3. Calculate the actual pixel size of the image on screen
        Dim actualDrawW As Single = viewW * baseScaleX * CSng(zoomFactor)
        Dim actualDrawH As Single = viewH * baseScaleY * CSng(zoomFactor)

        ' 4. Find the image start point (Center + Pan - HalfSize)
        Dim imgStartX As Single = (viewW / 2.0F) + CSng(panX) - (actualDrawW / 2.0F)
        Dim imgStartY As Single = (viewH / 2.0F) + CSng(panY) - (actualDrawH / 2.0F)

        ' 5. Convert mouse click to 0.0 - 1.0 range
        Dim normX As Single = (CSng(screenX) - imgStartX) / actualDrawW
        Dim normY As Single = (CSng(screenY) - imgStartY) / actualDrawH

        Return New PointF(normX, normY)
    End Function


    Private Sub sld_FNT_Size_ValueChanged(sender As Object, e As EventArgs) Handles sld_FNT_Size.ValueChanged
        numFontSize.Value = sld_FNT_Size.Value
    End Sub


    Private Function HexToVec3(hex As String) As Vector3
        Try
            ' Handle the case where user forgets the '#'
            If Not hex.StartsWith("#") Then hex = "#" & hex

            Dim c As Color = ColorTranslator.FromHtml(hex)
            ' OpenGL needs 0.0 to 1.0, so we divide by 255
            Return New Vector3(c.R / 255.0F, c.G / 255.0F, c.B / 255.0F)
        Catch
            Return New Vector3(0, 0, 0)
        End Try
    End Function

    Private Sub txt_HEXA_READ_TextChanged(sender As Object, e As EventArgs) Handles txt_HEXA_READ.TextChanged
        Try
            Dim hex As String = txt_HEXA_READ.Text.Trim()

            ' Ensure it starts with #
            If Not hex.StartsWith("#") AndAlso hex.Length >= 6 Then
                hex = "#" & hex
            End If

            ' Only update if it's a valid hex length
            If hex.Length = 7 Then
                pb_HEXA_READ.BackColor = ColorTranslator.FromHtml(hex)
            End If
        Catch
            ' Don't do anything if the hex is invalid (user still typing)
        End Try
    End Sub

    Private Sub txt_HEXA_READ_DoubleClick(sender As Object, e As EventArgs) Handles txt_HEXA_READ.DoubleClick
        ' 1. If both have content, reset: first gets the new color, second is cleared
        If Not String.IsNullOrWhiteSpace(txtHexa_Corect.Text) AndAlso Not String.IsNullOrWhiteSpace(txtCorrectedHexa_Correct.Text) Then
            txtHexa_Corect.Text = txt_HEXA_READ.Text
            txtCorrectedHexa_Correct.Text = ""
            pbCorrectedHexa_Correct.BackColor = Color.FromArgb(32, 32, 32)

            ' Reset the label because the "pair" is broken
            lblSwitchCol.Text = ".."

            ' 2. If the first box is empty, fill it
        ElseIf String.IsNullOrWhiteSpace(txtHexa_Corect.Text) Then
            txtHexa_Corect.Text = txt_HEXA_READ.Text

            ' 3. If the first is full but the second is empty, fill the second
        ElseIf String.IsNullOrWhiteSpace(txtCorrectedHexa_Correct.Text) Then
            txtCorrectedHexa_Correct.Text = txt_HEXA_READ.Text

            ' Now both are full! Set the arrow
            lblSwitchCol.Text = "->"
        End If

        ' --- SYNC SHADER VARIABLES ---
        ' Update targetCol and replaceCol so the Render loop doesn't use old data
        targetCol = HexToVec3(txtHexa_Corect.Text)
        replaceCol = HexToVec3(txtCorrectedHexa_Correct.Text)

        ' Optional: Immediate GPU push if you want instant feedback
        mainShader.Use()
        GL.Uniform3(GL.GetUniformLocation(mainShader.Handle, "uTargetRGB"), targetCol.X, targetCol.Y, targetCol.Z)
        GL.Uniform3(GL.GetUniformLocation(mainShader.Handle, "uReplaceRGB"), replaceCol.X, replaceCol.Y, replaceCol.Z)

        GlControl2.Invalidate()
    End Sub



    Private Sub cb_UseReplace_CheckedChanged(sender As Object, e As EventArgs) Handles cb_UseReplace.CheckedChanged
        If cb_UseReplace.Checked = True Then
            cb_UseReplace.Text = "ON"
            selectionU = 0.5F
            selectionV = 0.5F

            If txtHexa_Corect.Text <> "" AndAlso txtCorrectedHexa_Correct.Text <> "" Then
                'sldHexaCorrect.Value = 50
                'sldAreaCorrect.Value = 50
            End If
        Else
            cb_UseReplace.Text = "OFF"
            sldHexaCorrect.Value = 0
            sldAreaCorrect.Value = 0
            GlControl2.Invalidate()
        End If
    End Sub

    Private Sub txtHexa_Corect_TextChanged(sender As Object, e As EventArgs) Handles txtHexa_Corect.TextChanged
        Try
            Dim hex As String = txtHexa_Corect.Text.Trim()

            ' Ensure it starts with #
            If Not hex.StartsWith("#") AndAlso hex.Length >= 6 Then
                hex = "#" & hex
            End If

            ' Only update if it's a valid hex length
            If hex.Length = 7 Then
                pbHexa_Corect.BackColor = ColorTranslator.FromHtml(hex)
            End If
        Catch
            ' Don't do anything if the hex is invalid (user still typing)
        End Try
    End Sub

    Private Sub txtCorrectedHexa_Correct_TextChanged(sender As Object, e As EventArgs) Handles txtCorrectedHexa_Correct.TextChanged
        Try
            Dim hex As String = txtCorrectedHexa_Correct.Text.Trim()

            ' Ensure it starts with #
            If Not hex.StartsWith("#") AndAlso hex.Length >= 6 Then
                hex = "#" & hex
            End If

            ' Only update if it's a valid hex length
            If hex.Length = 7 Then
                pbCorrectedHexa_Correct.BackColor = ColorTranslator.FromHtml(hex)
            End If
        Catch
            ' Don't do anything if the hex is invalid (user still typing)
        End Try
    End Sub


    Private Sub sldHexaCorrect_ValueChanged(sender As Object, e As EventArgs) Handles sldHexaCorrect.ValueChanged
        lblHexaCorrect.Text = sldHexaCorrect.Value

        ' CORRECTED: Hexa (Radius) slider updates Radius variable
        CurrentParams.HexaRadius = CSng(sldHexaCorrect.Value) / 400.0F

        If cb_UseReplace.Checked = True Then
            targetCol = HexToVec3(txtHexa_Corect.Text)
            replaceCol = HexToVec3(txtCorrectedHexa_Correct.Text)
            GlControl2.Invalidate()
        End If
    End Sub


    Private Sub sldSmoothnesCorrect_ValueChanged(sender As Object, e As EventArgs) Handles sldSmoothnesCorrect.ValueChanged
        lblSmoothnesCorrect.Text = sldSmoothnesCorrect.Value

        ' CORRECTED: Smoothness slider updates Smoothness variable
        CurrentParams.HexaSmoothness = CSng(sldSmoothnesCorrect.Value) / 1000.0F

        ' Live update if unchecked
        If cb_UseReplace.Checked = True Then
            ' Important: Ensure colors are updated too, otherwise it's comparing against black (0,0,0)
            targetCol = HexToVec3(txtHexa_Corect.Text)
            replaceCol = HexToVec3(txtCorrectedHexa_Correct.Text)
            GlControl2.Invalidate()
        End If
    End Sub

    Private Sub sldAreaCorrect_ValueChanged(sender As Object, e As EventArgs) Handles sldAreaCorrect.ValueChanged
        ' Normalize slider (0-100) to a usable radius (0.0 to 1.5)
        ' 1.5 ensures that even if you click a corner, the circle can cover the whole image
        lblAreaCorrect.Text = sldAreaCorrect.Value

        ' Convert 0-100 to 0.0-1.0
        Dim pct = sldAreaCorrect.Value / 100.0F

        CurrentParams.HexaRange = pct * pct * 1.5F ' 1.5 allows coverage of the whole screen

        ' Live update logic
        If cb_UseReplace.Checked = True Then
            ' Ensure the colors stay synced during the live adjustment
            targetCol = HexToVec3(txtHexa_Corect.Text)
            replaceCol = HexToVec3(txtCorrectedHexa_Correct.Text)
            GlControl2.Invalidate()
        End If
    End Sub

    Private Sub FixPositionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FixPositionToolStripMenuItem.Click
        ' 1. Use the "Snapshot" we took during the Right-Click
        selectionU = pos_for_colors.X
        selectionV = pos_for_colors.Y

        ' 2. Update the Shader
        mainShader.Use()
        Dim uCenterLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uSelectionCenter")

        ' Note: If your shader has the (1.0 - TexCoord.y) fix, send selectionV directly.
        ' If not, use (1.0F - selectionV) here.
        GL.Uniform2(uCenterLoc, selectionU, selectionV)

        ' 3. Redraw to see the change
        GlControl2.Invalidate()

        ' Optional: Pin the HEXA for reference
        ' pinnedHEXA = txtHexa_Corect.Text 
    End Sub



    Private Sub lblSwitchCol_Click(sender As Object, e As EventArgs) Handles lblSwitchCol.Click

        If cb_UseReplace.Checked = True Then
            ' 1. Snapshot and Swap Textboxes
            Dim tempTarget As String = txtHexa_Corect.Text
            Dim tempReplace As String = txtCorrectedHexa_Correct.Text

            txtHexa_Corect.Text = tempReplace
            txtCorrectedHexa_Correct.Text = tempTarget

            ' 2. Toggle the Label Caption
            ' If it was -> it becomes <- and vice-versa
            If lblSwitchCol.Text = ">>" Then
                lblSwitchCol.Text = "<<"
            Else
                lblSwitchCol.Text = ">>"
            End If

            ' 3. Update Vector3 variables for Shader
            targetCol = HexToVec3(txtHexa_Corect.Text)
            replaceCol = HexToVec3(txtCorrectedHexa_Correct.Text)

            ' 4. Update Shader Uniforms
            mainShader.Use()
            Dim uTargetLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uTargetRGB")
            Dim uReplaceLoc As Integer = GL.GetUniformLocation(mainShader.Handle, "uReplaceRGB")

            ' Using .X, .Y, .Z to ensure OpenTK/OpenGL compatibility
            GL.Uniform3(uTargetLoc, targetCol.X, targetCol.Y, targetCol.Z)
            GL.Uniform3(uReplaceLoc, replaceCol.X, replaceCol.Y, replaceCol.Z)

            ' 5. Redraw
            GlControl2.Invalidate()
        End If
    End Sub

    ' Label Hover Events
    Private Sub lblSwitchCol_MouseEnter(sender As Object, e As EventArgs) Handles lblSwitchCol.MouseEnter
        lblSwitchCol.Cursor = Cursors.Hand
        lblSwitchCol.Invalidate() ' Triggers the Paint event for the highlight
    End Sub

    Private Sub lblSwitchCol_MouseLeave(sender As Object, e As EventArgs) Handles lblSwitchCol.MouseLeave
        lblSwitchCol.Cursor = Cursors.Default
        lblSwitchCol.Invalidate()
    End Sub
    Private Sub lblSwitchCol_Paint(sender As Object, e As PaintEventArgs) Handles lblSwitchCol.Paint
        Dim mousePos = lblSwitchCol.PointToClient(Control.MousePosition)

        ' Only draw the highlight if the mouse is actually over the label
        If lblSwitchCol.ClientRectangle.Contains(mousePos) Then
            Using p As New Pen(Color.FromArgb(200, 200, 200), 1) ' Subtle light gray
                ' Draw a rectangle just inside the label's edge
                e.Graphics.DrawRectangle(p, 0, 0, lblSwitchCol.Width - 1, lblSwitchCol.Height - 1)
            End Using
        End If
    End Sub
    Private Sub sldOpacity_ValueChanged(sender As Object, e As EventArgs) Handles sldOpacity.ValueChanged
        lblOpacity.Text = sldOpacity.Value

        ' CORRECTED: Smoothness slider updates Smoothness variable
        CurrentParams.HexaOpacity = CSng(sldOpacity.Value) / 100.0F

        ' Live update if unchecked
        If cb_UseReplace.Checked = True Then
            ' Important: Ensure colors are updated too, otherwise it's comparing against black (0,0,0)
            targetCol = HexToVec3(txtHexa_Corect.Text)
            replaceCol = HexToVec3(txtCorrectedHexa_Correct.Text)
            GlControl2.Invalidate()
        End If
    End Sub
    Private Sub pbCorrectedHexa_Correct_Click(sender As Object, e As EventArgs) Handles pbCorrectedHexa_Correct.Click
        Using cd As New ColorDialog
            If cd.ShowDialog() = DialogResult.OK Then
                ' 1. Update the logical color variables
                textColor = cd.Color
                currentColor = cd.Color

                ' 2. FORCE HEX FORMAT: This prevents "Red" and gives you "#FF0000"
                txtCorrectedHexa_Correct.Text = String.Format("#{0:X2}{1:X2}{2:X2}",
                                        cd.Color.R, cd.Color.G, cd.Color.B)

                ' 3. Sync the Replace color variable for the shader
                replaceCol = HexToVec3(txtCorrectedHexa_Correct.Text)

                ' 4. Redraw
                If cb_UseReplace.Checked = True Then
                    targetCol = HexToVec3(txtHexa_Corect.Text)
                    replaceCol = HexToVec3(txtCorrectedHexa_Correct.Text)
                    GlControl2.Invalidate()
                End If
            End If
        End Using
    End Sub
    ' When the mouse enters, show the hand cursor and a highlight effect
    Private Sub pbCorrectedHexa_Correct_MouseEnter(sender As Object, e As EventArgs) Handles pbCorrectedHexa_Correct.MouseEnter
        pbCorrectedHexa_Correct.Cursor = Cursors.Hand
        ' Optional: Add a highlight border by changing the BorderStyle if available
        ' Or simply change a property to trigger a redraw
        pbCorrectedHexa_Correct.Invalidate()
    End Sub

    ' When the mouse leaves, reset the cursor
    Private Sub pbCorrectedHexa_Correct_MouseLeave(sender As Object, e As EventArgs) Handles pbCorrectedHexa_Correct.MouseLeave
        pbCorrectedHexa_Correct.Cursor = Cursors.Default
        pbCorrectedHexa_Correct.Invalidate()
    End Sub



    Private Sub pb_HEXA_READ_Click(sender As Object, e As EventArgs) Handles pb_HEXA_READ.Click
        If Not String.IsNullOrWhiteSpace(txtHexa_Corect.Text) AndAlso Not String.IsNullOrWhiteSpace(txtCorrectedHexa_Correct.Text) Then
            txtHexa_Corect.Text = txt_HEXA_READ.Text
            txtCorrectedHexa_Correct.Text = ""
            pbCorrectedHexa_Correct.BackColor = Color.FromArgb(32, 32, 32)

            ' Reset the label because the "pair" is broken
            lblSwitchCol.Text = ".."

            ' 2. If the first box is empty, fill it
        ElseIf String.IsNullOrWhiteSpace(txtHexa_Corect.Text) Then
            txtHexa_Corect.Text = txt_HEXA_READ.Text

            ' 3. If the first is full but the second is empty, fill the second
        ElseIf String.IsNullOrWhiteSpace(txtCorrectedHexa_Correct.Text) Then
            txtCorrectedHexa_Correct.Text = txt_HEXA_READ.Text

            ' Now both are full! Set the arrow
            lblSwitchCol.Text = ">>"
        End If

        ' --- SYNC SHADER VARIABLES ---
        ' Update targetCol and replaceCol so the Render loop doesn't use old data
        targetCol = HexToVec3(txtHexa_Corect.Text)
        replaceCol = HexToVec3(txtCorrectedHexa_Correct.Text)

        ' Optional: Immediate GPU push if you want instant feedback
        mainShader.Use()
        GL.Uniform3(GL.GetUniformLocation(mainShader.Handle, "uTargetRGB"), targetCol.X, targetCol.Y, targetCol.Z)
        GL.Uniform3(GL.GetUniformLocation(mainShader.Handle, "uReplaceRGB"), replaceCol.X, replaceCol.Y, replaceCol.Z)

        GlControl2.Invalidate()
    End Sub

    ' PictureBox Target Color Hover Events
    Private Sub pb_HEXA_READ_MouseEnter(sender As Object, e As EventArgs) Handles pb_HEXA_READ.MouseEnter
        pb_HEXA_READ.Cursor = Cursors.Hand
        pb_HEXA_READ.Invalidate()
    End Sub

    Private Sub pb_HEXA_READ_MouseLeave(sender As Object, e As EventArgs) Handles pb_HEXA_READ.MouseLeave
        pb_HEXA_READ.Cursor = Cursors.Default
        pb_HEXA_READ.Invalidate()
    End Sub

    Private Sub pb_HEXA_READ_Paint(sender As Object, e As PaintEventArgs) Handles pb_HEXA_READ.Paint
        ' Check if mouse is over this specific control
        Dim mousePos = pb_HEXA_READ.PointToClient(Control.MousePosition)

        If pb_HEXA_READ.ClientRectangle.Contains(mousePos) Then
            ' Draw the "Active" border
            Using p As New Pen(Color.White, 2)
                ' Draw slightly inside the edges (1 pixel offset) to ensure it's visible
                e.Graphics.DrawRectangle(p, 1, 1, pb_HEXA_READ.Width - 3, pb_HEXA_READ.Height - 3)
            End Using
        End If
    End Sub

    Private Sub pbCorrectedHexa_Correct_Paint(sender As Object, e As PaintEventArgs) Handles pbCorrectedHexa_Correct.Paint
        ' Check if mouse is over this specific control
        Dim mousePos = pbCorrectedHexa_Correct.PointToClient(Control.MousePosition)

        If pbCorrectedHexa_Correct.ClientRectangle.Contains(mousePos) Then
            ' Draw the "Active" border
            Using p As New Pen(Color.White, 2)
                ' Draw slightly inside the edges (1 pixel offset) to ensure it's visible
                e.Graphics.DrawRectangle(p, 1, 1, pbCorrectedHexa_Correct.Width - 3, pbCorrectedHexa_Correct.Height - 3)
            End Using
        End If
    End Sub

    Private Sub lblHelpColor_Click(sender As Object, e As EventArgs) Handles lblHelpColor.Click
        ' 1. Create the Form
        Dim helpForm As New Form
        helpForm.Text = "Editor Help"
        helpForm.Size = New Size(540, 530)
        helpForm.StartPosition = FormStartPosition.CenterParent
        helpForm.BackColor = Color.FromArgb(28, 28, 28) ' Match your dark UI
        helpForm.ForeColor = Color.White
        helpForm.FormBorderStyle = FormBorderStyle.FixedDialog
        helpForm.MaximizeBox = False
        helpForm.MinimizeBox = False

        ' 2. Apply the Dark Title Bar (The fix we just did!)
        ' Note: You might need to make ApplyDarkModeTitle accept a Handle as a parameter
        ' Or just call the API directly here:
        DwmSetWindowAttribute(helpForm.Handle, 20, 1, 4)

        ' 3. Add the Big Text
        Dim lblInfo As New Label
        lblInfo.Text = "Color Replacement Pro Guide" & vbCrLf & vbCrLf &
                   "• Pick Target: Click the Target on the image to select the" & vbCrLf &
                   "  color you want to change and the color will be set in the" & vbCrLf &
                   "  lower corner in the C.PICKER, ready for selection by double " & vbCrLf &
                   "  clicking the C.PICKER or by cliking the color box and the" & vbCrLf &
                   "  color will be assigned to C.TARGET automatically." & vbCrLf &
                   "• Now choose the replacement color from the picture or from" & vbCrLf &
                   "  the associated color box of the C.REPLACE." & vbCrLf &
                   "  Activate the color replace function ON/OFF and use the sliders." & vbCrLf &
                   "• Following options are available: [F]= Fuzziness, [S]= Smoothness," & vbCrLf &
                   "  [A]= Area affected and [O]= Opacity.The color boxes are swapable" & vbCrLf &
                   "• IMPORTANT! You have to set the center of the area where the" & vbCrLf &
                   "  color will be replaced by right clicking on the picture and selecting" & vbCrLf &
                   "  'Fix psn. Color replace'. This will lock the desired position." & vbCrLf &
                   "• Texture Preservation: The tool automatically maintains the" & vbCrLf &
                   "  original highlights and shadows of your object, ensuring the" & vbCrLf &
                   "  new color looks integrated in the environment."


        lblInfo.Font = New Font("Segoe UI", 12.0F, FontStyle.Regular) ' Big text
        lblInfo.Dock = DockStyle.Fill
        lblInfo.Padding = New Padding(20)
        lblInfo.TextAlign = ContentAlignment.TopLeft

        ' 4. Add the CLOSE Button
        Dim btnClose As New Button
        btnClose.Text = "CLOSE"
        btnClose.Dock = DockStyle.Bottom
        btnClose.Height = 50
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.FlatAppearance.BorderSize = 0
        btnClose.BackColor = Color.FromArgb(45, 45, 48)
        btnClose.Cursor = Cursors.Hand

        ' Add the click event to close the form
        AddHandler btnClose.Click, Sub() helpForm.Close()

        ' 5. Assemble and Show
        helpForm.Controls.Add(lblInfo)
        helpForm.Controls.Add(btnClose)
        helpForm.ShowDialog(Me) ' Opens as a popup
    End Sub

    Private Sub lblLUThelp_Click(sender As Object, e As EventArgs) Handles lblLUThelp.Click
        ' 1. Create the Form
        Dim helpForm As New Form
        helpForm.Text = "Editor Help"
        helpForm.Size = New Size(510, 300)
        helpForm.StartPosition = FormStartPosition.CenterParent
        helpForm.BackColor = Color.FromArgb(28, 28, 28) ' Match your dark UI
        helpForm.ForeColor = Color.White
        helpForm.FormBorderStyle = FormBorderStyle.FixedDialog
        helpForm.MaximizeBox = False
        helpForm.MinimizeBox = False

        ' 2. Apply the Dark Title Bar (The fix we just did!)
        ' Note: You might need to make ApplyDarkModeTitle accept a Handle as a parameter
        ' Or just call the API directly here:
        DwmSetWindowAttribute(helpForm.Handle, 20, 1, 4)

        ' 3. Add the Big Text
        Dim lblInfo As New Label
        lblInfo.Text = "Welcome to the F-Stop Pro Editor!" & vbCrLf & vbCrLf &
                   "• The LUT list is using the Arrows Keys, so You can " & vbCrLf &
                   "  easely choose the LUTs with Left and Right keys." & vbCrLf &
                   "• The LUT list is using also the Space bar for comapre" & vbCrLf &
                   "  the 'Before' and 'After' or disble/enable the LUT effect." & vbCrLf &
                   "• You can choose the LUTs from the list or load separate" & vbCrLf &
                   "  from your files. ATTENTION! Only .cube files will work."


        lblInfo.Font = New Font("Segoe UI", 12.0F, FontStyle.Regular) ' Big text
        lblInfo.Dock = DockStyle.Fill
        lblInfo.Padding = New Padding(20)
        lblInfo.TextAlign = ContentAlignment.TopLeft

        ' 4. Add the CLOSE Button
        Dim btnClose As New Button
        btnClose.Text = "CLOSE"
        btnClose.Dock = DockStyle.Bottom
        btnClose.Height = 50
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.FlatAppearance.BorderSize = 0
        btnClose.BackColor = Color.FromArgb(45, 45, 48)
        btnClose.Cursor = Cursors.Hand

        ' Add the click event to close the form
        AddHandler btnClose.Click, Sub() helpForm.Close()

        ' 5. Assemble and Show
        helpForm.Controls.Add(lblInfo)
        helpForm.Controls.Add(btnClose)
        helpForm.ShowDialog(Me) ' Opens as a popup
    End Sub

    Private Sub TS_White_Click(sender As Object, e As EventArgs) Handles TS_White.Click
        selR = 1.0F : selG = 1.0F : selB = 1.0F
        'GL.UseProgram(mainShader.Handle)
        'GL.Uniform3(uRectangleSelColorLoc, 1.0F, 1.0F, 1.0F) ' White
        GlControl2.Invalidate()
    End Sub

    Private Sub TS_Yellow_Click(sender As Object, e As EventArgs) Handles TS_Yellow.Click
        selR = 1.0F : selG = 1.0F : selB = 0.0F
        'GL.UseProgram(mainShader.Handle)
        'GL.Uniform3(uRectangleSelColorLoc, 1.0F, 1.0F, 0.0F) ' Yellow
        GlControl2.Invalidate()
    End Sub

    Private Sub TS_Red_Click(sender As Object, e As EventArgs) Handles TS_Red.Click
        selR = 1.0F : selG = 0.0F : selB = 0.0F
        'GL.UseProgram(mainShader.Handle)
        'GL.Uniform3(uRectangleSelColorLoc, 1.0F, 0.0F, 0.0F) ' Red
        GlControl2.Invalidate()
    End Sub

    Private Sub TS_Blue_Click(sender As Object, e As EventArgs) Handles TS_Blue.Click
        selR = 0.0F : selG = 0.0F : selB = 1.0F
        'GL.UseProgram(mainShader.Handle)
        'GL.Uniform3(uRectangleSelColorLoc, 0.0F, 0.0F, 1.0F) ' Blue
        GlControl2.Invalidate()
    End Sub

    Private Sub Check_For_Changes()

        If currentImage Is Nothing OrElse String.IsNullOrEmpty(currentImagePath) Then Return

        vl_check_for_change = False
        ' check everytime for changes if changes are made to original picture

        Dim local_str_effects As String, local_str_crop As String, local_str_color As String, local_str_text As String, pls_Save As String
        Dim loc_effects_ch As Boolean, loc_crop_ch As Boolean, loc_color_ch As Boolean, loc_text_ch As Boolean

        ' check if the slider effects were changed
        If sldExposure.Value <> 50 Then loc_effects_ch = True
        If sldContrast.Value <> 50 Then loc_effects_ch = True
        If sldHighlights.Value <> 50 Then loc_effects_ch = True
        If sldShadowsLift.Value <> 50 Then loc_effects_ch = True
        If sldWhites.Value <> 50 Then loc_effects_ch = True
        If sldBlacks.Value <> 50 Then loc_effects_ch = True
        If sldTemp.Value <> 50 Then loc_effects_ch = True
        If sldTint.Value <> 50 Then loc_effects_ch = True
        If sldDynCon.Value <> 0 Then loc_effects_ch = True
        If sldClarity.Value <> 50 Then loc_effects_ch = True
        If sldStructX.Value <> 0 Then loc_effects_ch = True
        If sldTextureX.Value <> 0 Then loc_effects_ch = True
        If sldBarrelDistortion.Value <> 50 Then loc_effects_ch = True
        If sldSharpen.Value <> 0 Then loc_effects_ch = True
        If sldDefringe.Value <> 0 Then loc_effects_ch = True
        If sldDenoise.Value <> 0 Then loc_effects_ch = True
        If sldNoiseLimit.Value <> 0 Then loc_effects_ch = True
        If sldNoiseChroma.Value <> 0 Then loc_effects_ch = True
        If sldNoiseLuma.Value <> 0 Then loc_effects_ch = True
        If sldSaturation.Value <> 50 Then loc_effects_ch = True
        If sldVibrance.Value <> 50 Then loc_effects_ch = True
        If sldRedMix.Value <> 50 Then loc_effects_ch = True
        If sldGreenMix.Value <> 50 Then loc_effects_ch = True
        If sldBlueMix.Value <> 50 Then loc_effects_ch = True
        If sldDeHaze.Value <> 0 Then loc_effects_ch = True
        If sldVignette.Value <> 0 Then loc_effects_ch = True
        If sldVignetteStd.Value <> 0 Then loc_effects_ch = True
        If sldFilmGrain.Value <> 0 Then loc_effects_ch = True
        If sldGrainSize.Value <> 0 Then loc_effects_ch = True
        If sldVivid.Value <> 0 Then loc_effects_ch = True
        If sldSepiaMix.Value <> 0 Then loc_effects_ch = True
        If sldNoirFactor.Value <> 0 Then loc_effects_ch = True
        If sldSplitToning.Value <> 0 Then loc_effects_ch = True
        If sldFade.Value <> 0 Then loc_effects_ch = True
        If sldFadeTint.Value <> 50 Then loc_effects_ch = True
        If sldCinematic.Value <> 0 Then loc_effects_ch = True
        If sldCineBalance.Value <> 50 Then loc_effects_ch = True

        If loc_effects_ch = True Then
            vl_check_for_change = True
            local_str_effects = "[ Has Effects ] "
        Else

            local_str_effects = ""
        End If


        ' check if the image is cropped
        If cropRect.X <> 0.1F Then loc_crop_ch = True
        If cropRect.Y <> 0.1F Then loc_crop_ch = True
        If cropRect.Width <> 0.8F Then loc_crop_ch = True
        If cropRect.Height <> 0.8F Then loc_crop_ch = True

        If loc_crop_ch = True Then
            vl_check_for_change = True
            local_str_crop = "[ Is cropped ] "
        Else

            local_str_crop = ""
        End If



        ' check if the color were changed
        If cb_UseReplace.Checked = True AndAlso sldHexaCorrect.Value <> 0 AndAlso txtHexa_Corect.Text <> "" Then loc_color_ch = True

        If loc_color_ch = True Then
            vl_check_for_change = True
            local_str_color = "[ Color replaced ] "
        Else

            local_str_color = ""
        End If


        ' check if text is applied to the picture
        If txtWatermark.Text <> "" AndAlso sldTextOpacity.Value <> 0 Then loc_text_ch = True

        If loc_text_ch = True Then
            vl_check_for_change = True
            local_str_text = "[ Has Text Layer ] "
        Else

            local_str_text = ""
        End If


        First_Load += 1
        If First_Load > 10 Then First_Load = 10
        pls_Save = ""

        If vl_check_for_change = False Then
            btn_SaveChanges.Enabled = False
            lbl_Check_for_Original.ForeColor = Color.FromArgb(192, 255, 192)
            lbl_Check_for_Original.Text = "Picture Status"

        Else

            If First_Load / 2 > 2 Then
                btn_SaveChanges.Enabled = True
                pls_Save = " - Not Saved"
            End If

            lbl_Check_for_Original.ForeColor = Color.FromArgb(255, 192, 192)
            lbl_Check_for_Original.Text = "Picture is Modified: " & local_str_effects & local_str_crop & local_str_color & local_str_text & pls_Save

        End If


    End Sub

    Private Sub ClearLibrary()
        ' 1. Stop any current UI selection
        selectedThumb = Nothing
        currentImagePath = ""

        ' 2. Clear the GPU Texture to free up VRAM
        If textureID <> 0 Then
            GL.DeleteTexture(textureID)
            textureID = 0
        End If

        ' 3. Dispose of all Thumbnail Bitmaps to free up RAM
        ' We loop through every panel in the thumbnail list
        For Each ctrl As Control In thumbnailPanel.Controls
            If TypeOf ctrl Is Panel Then
                Dim pb As PictureBox = ctrl.Controls.OfType(Of PictureBox)().FirstOrDefault()
                If pb IsNot Nothing AndAlso pb.Image IsNot Nothing Then
                    pb.Image.Dispose() ' This kills the bitmap in RAM
                    pb.Image = Nothing
                End If
            End If
        Next

        ' 4. Clear the UI and Lists
        thumbnailPanel.Controls.Clear()
        loadedFiles.Clear()
        stripFilePaths.Clear()

        ' 5. Reset Counters
        totalLibraryItems = 0
        lblLibrary.Text = "Library : 0 / 0"

        ' 6. Force Windows to reclaim the memory immediately
        GC.Collect()
        GC.WaitForPendingFinalizers()

        ' 7. Clear the GL Screen
        If GlControl2 IsNot Nothing Then
            GlControl2.MakeCurrent()
            GL.ClearColor(Color.FromArgb(30, 30, 30))
            GL.Clear(ClearBufferMask.ColorBufferBit)
            GlControl2.SwapBuffers()
        End If
    End Sub

    Private Sub ClearAllStripToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearAllStripToolStripMenuItem.Click

        selectedThumb = Nothing
        currentImagePath = ""

        ' 2. Clear the GPU Texture to free up VRAM
        If textureID <> 0 Then
            GL.DeleteTexture(textureID)
            textureID = 0
        End If

        ' 3. Dispose of all Thumbnail Bitmaps to free up RAM
        ' We loop through every panel in the thumbnail list
        For Each ctrl As Control In thumbnailPanel.Controls
            If TypeOf ctrl Is Panel Then
                Dim pb As PictureBox = ctrl.Controls.OfType(Of PictureBox)().FirstOrDefault()
                If pb IsNot Nothing AndAlso pb.Image IsNot Nothing Then
                    pb.Image.Dispose() ' This kills the bitmap in RAM
                    pb.Image = Nothing
                End If
            End If
        Next


        ResetAllSliders()
        btn_SaveChanges.Enabled = False

        ' 4. Clear the UI and Lists
        thumbnailPanel.Controls.Clear()
        loadedFiles.Clear()
        stripFilePaths.Clear()

        ' 5. Reset Counters
        totalLibraryItems = 0
        lblLibrary.Text = "Library : 0 / 0"

        ' 6. Force Windows to reclaim the memory immediately
        GC.Collect()
        GC.WaitForPendingFinalizers()

        ' 7. Clear the GL Screen
        If GlControl2 IsNot Nothing Then
            GlControl2.MakeCurrent()
            GL.ClearColor(Color.FromArgb(30, 30, 30))
            GL.Clear(ClearBufferMask.ColorBufferBit)
            GlControl2.SwapBuffers()
        End If


        lbl_Check_for_Original.ForeColor = Color.FromArgb(192, 255, 192)
        lbl_Check_for_Original.Text = "Picture Status"

    End Sub


    ' Creează o metodă mică de ajutor



    Private Class ScrollHider
        Inherits NativeWindow

        Private Declare Function ShowScrollBar Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal wBar As Integer, ByVal bShow As Boolean) As Boolean
        Private _parentPanel As FlowLayoutPanel

        Public Sub New(ctrl As FlowLayoutPanel)
            _parentPanel = ctrl
            Me.AssignHandle(ctrl.Handle)
        End Sub
        Public Sub ForceUpdate()
            If _parentPanel IsNot Nothing AndAlso _parentPanel.IsHandleCreated Then
                ' Forțăm ascunderea barei native
                ShowScrollBar(Me.Handle, 1, False)

                ' Curățăm zona și desenăm bara noastră
                Dim rectToRefresh As New Rectangle(_parentPanel.Width - 20, 0, 20, _parentPanel.Height)
                _parentPanel.Invalidate(rectToRefresh)
                _parentPanel.Update()

                DrawCustomScroll()
            End If
        End Sub
        Protected Overrides Sub WndProc(ByRef m As Message)
            ' 1. Ascundem scrollbar-ul nativ (Standard)
            If m.Msg = &H115 OrElse m.Msg = &H114 OrElse m.Msg = &H85 Then
                ShowScrollBar(Me.Handle, 1, False)
            End If

            MyBase.WndProc(m)

            ' 2. Interceptăm Scroll-ul (Vertical & MouseWheel)
            If m.Msg = &H115 OrElse m.Msg = &H20A Then ' &H20A este WM_MOUSEWHEEL
                ' Definim zona scrollbar-ului (ultimii 15 pixeli pentru siguranță)
                Dim rectToRefresh As New Rectangle(_parentPanel.Width - 15, 0, 15, _parentPanel.Height)

                ' Forțăm Windows să șteargă zona ACUM, nu mai târziu
                _parentPanel.Invalidate(rectToRefresh)
                _parentPanel.Update() ' Aceasta este cheia: forțează desenarea imediată

                ' Desenăm noua bară peste zona curățată
                DrawCustomScroll()
            End If

            ' 3. Desenăm și la Paint-ul normal (redimensionare fereastră, etc.)
            If m.Msg = &HF Then
                DrawCustomScroll()
            End If
        End Sub

        Private Sub DrawCustomScroll()
            ' Folosim un Graphics obiect direct din Paint context dacă e posibil, 
            ' dar pentru simplitate rămânem la CreateGraphics cu un mic truc:
            Using g As Graphics = _parentPanel.CreateGraphics()
                Dim viewHeight As Single = _parentPanel.ClientSize.Height
                Dim totalHeight As Single = _parentPanel.DisplayRectangle.Height

                If totalHeight > viewHeight Then
                    Dim scrollWidth As Integer = 4 ' O facem și mai subțire pentru eleganță
                    Dim thumbHeight As Single = (viewHeight / totalHeight) * viewHeight
                    ' Calculăm Y-ul inversat (AutoScrollPosition e negativ)
                    Dim scrollPos As Single = Math.Abs(_parentPanel.AutoScrollPosition.Y)
                    Dim thumbY As Single = (scrollPos / totalHeight) * viewHeight

                    ' Desenăm indicatorul
                    Dim rect As New RectangleF(_parentPanel.Width - scrollWidth - 3, thumbY, scrollWidth, thumbHeight)

                    ' Folosim o culoare discretă
                    Using b As New SolidBrush(Color.FromArgb(120, 152, 225, 134))
                        g.FillRectangle(b, rect)
                    End Using
                End If
            End Using
        End Sub
    End Class

    Private Sub lblHelpEnhancement_Click(sender As Object, e As EventArgs) Handles lblHelpEnhancement.Click

        ' 1. Create the Form
        Dim helpForm As New Form
        helpForm.Text = "Editor Help"
        helpForm.Size = New Size(520, 410)
        helpForm.StartPosition = FormStartPosition.CenterParent
        helpForm.BackColor = Color.FromArgb(28, 28, 28) ' Match your dark UI
        helpForm.ForeColor = Color.White
        helpForm.FormBorderStyle = FormBorderStyle.FixedDialog
        helpForm.MaximizeBox = False
        helpForm.MinimizeBox = False

        ' 2. Apply the Dark Title Bar (The fix we just did!)
        ' Note: You might need to make ApplyDarkModeTitle accept a Handle as a parameter
        ' Or just call the API directly here:
        DwmSetWindowAttribute(helpForm.Handle, 20, 1, 4)

        ' 3. Add the Big Text
        Dim lblInfo As New Label
        lblInfo.Text = "                  Welcome to the F-Stop Pro Editor!" & vbCrLf & vbCrLf &
                   "• STRUCTURE, SHARPEN, DENOISE, TEXTURE and CLARITY" & vbCrLf &
                   "  are very subtle effects and may not be visible on a" & vbCrLf &
                   "  file over 15 MB, which is also very good quality." & vbCrLf &
                   "  However, the effects work perfectly when applied to files" & vbCrLf &
                   "  that need such adjustments, on small files, or on cropped" & vbCrLf &
                   "  sections of large files. The effects are there to help you." & vbCrLf &
                   "• Be patient and learn how and when to use the effects," & vbCrLf &
                   "   with the same patience I had when I made this program." & vbCrLf &
                   "  If you really want to see what's happening, zoom in on the" & vbCrLf &
                   "  picture, move the sliders, and you will see them in real time."

        lblInfo.Font = New Font("Segoe UI", 12.0F, FontStyle.Regular) ' Big text
        lblInfo.Dock = DockStyle.Fill
        lblInfo.Padding = New Padding(20)
        lblInfo.TextAlign = ContentAlignment.TopLeft

        ' 4. Add the CLOSE Button
        Dim btnClose As New Button
        btnClose.Text = "CLOSE"
        btnClose.Dock = DockStyle.Bottom
        btnClose.Height = 50
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.FlatAppearance.BorderSize = 0
        btnClose.BackColor = Color.FromArgb(45, 45, 48)
        btnClose.Cursor = Cursors.Hand

        ' Add the click event to close the form
        AddHandler btnClose.Click, Sub() helpForm.Close()

        ' 5. Assemble and Show
        helpForm.Controls.Add(lblInfo)
        helpForm.Controls.Add(btnClose)
        helpForm.ShowDialog(Me) ' Opens as a popup

    End Sub

    Private Sub lblOwner_MouseHover(sender As Object, e As EventArgs) Handles lblOwner.MouseHover
        lblOwner.ForeColor = Color.Gold
    End Sub

    Private Sub lblOwner_MouseLeave(sender As Object, e As EventArgs) Handles lblOwner.MouseLeave
        lblOwner.ForeColor = Color.FromArgb(64, 64, 64)
    End Sub

    Private Sub btn_StraightenPic_Click(sender As Object, e As EventArgs) Handles btn_StraightenPic.Click
        btn_StraightenPic.Enabled = False
        btn_UndoRotate.Enabled = False

        isStraighteningMode = True
        GlControl2.Cursor = Cursors.Cross
        lineStartPoint = Point.Empty
        lineCurrentPoint = Point.Empty

        ' 🟢 Save the baseline crop area before this rotation session begins
        preRotationCropRect = cropRect
    End Sub

    Private Sub btn_UndoRotate_Click(sender As Object, e As EventArgs) Handles btn_UndoRotate.Click
        btn_StraightenPic.Enabled = True
        btn_UndoRotate.Enabled = False

        ' Swap current and previous positions for a true single-step revert
        Dim temp = currentRotation
        currentRotation = previousRotation
        previousRotation = temp

        ' Instantly redraw the OpenTK pipeline
        GlControl2.Invalidate()
    End Sub





    '=============================================================================================================================================================================
End Class


Public Class MyToolStripRenderer
    Inherits ToolStripProfessionalRenderer

    Protected Overrides Sub OnRenderToolStripBorder(e As ToolStripRenderEventArgs)
        ' Do nothing here to remove that white horizontal line!
    End Sub

    Protected Overrides Sub OnRenderGrip(e As ToolStripGripRenderEventArgs)
        ' Do nothing here to ensure the "dots" on the left are gone
    End Sub
End Class

'=====================TheShaderProgram==================================
Public Class ShaderProgram
    Public Property Handle As Integer

    Public Sub New(vertexPath As String, fragmentPath As String)

        Dim vertexSource = File.ReadAllText(vertexPath)
        Dim fragmentSource = File.ReadAllText(fragmentPath)

        Dim vertexShader = CompileShader(ShaderType.VertexShader, vertexSource)
        Dim fragmentShader = CompileShader(ShaderType.FragmentShader, fragmentSource)

        Handle = GL.CreateProgram()
        GL.AttachShader(Handle, vertexShader)
        GL.AttachShader(Handle, fragmentShader)
        GL.LinkProgram(Handle)

        Dim status As Integer
        GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, status)
        If status = 0 Then
            Throw New Exception("Program link error: " & GL.GetProgramInfoLog(Handle))
        End If

        GL.UseProgram(Handle)

        GL.DeleteShader(vertexShader)
        GL.DeleteShader(fragmentShader)

    End Sub

    Private Function CompileShader(type As ShaderType, source As String) As Integer
        ' 1. Create and Compile the individual shader
        Dim shader = GL.CreateShader(type)
        GL.ShaderSource(shader, source)
        GL.CompileShader(shader)

        ' 2. Check for COMPILE errors (typos in the GLSL code)
        Dim status As Integer
        GL.GetShader(shader, ShaderParameter.CompileStatus, status)

        If status = 0 Then
            ' If this happens, your GLSL has a syntax error!
            Dim infoLog As String = GL.GetShaderInfoLog(shader)
            Throw New Exception($"{type} compile error: {infoLog}")
            Throw New Exception($"{type} compile error: {GL.GetShaderInfoLog(shader)}")
        End If

        Return shader
    End Function

    Public Sub Use()
        GL.UseProgram(Handle)
    End Sub

    Public Sub SetInt(name As String, value As Integer)
        GL.Uniform1(GL.GetUniformLocation(Handle, name), value)
    End Sub

    Public Sub SetFloat(name As String, value As Single)
        GL.Uniform1(GL.GetUniformLocation(Handle, name), value)
    End Sub

End Class


' MODERN SLIDER CLASS ===================================
Public Class ModernSlider
    Inherits Control

    ' 1. Properties (Added missing Minimum, Maximum, and AccentColor)
    Public Property Minimum As Integer = 0
    Public Property Maximum As Integer = 100
    Public Property AccentColor As Color = Color.DodgerBlue
    Public Event Scroll As EventHandler

    Private _value As Integer = 50
    Private _isDragging As Boolean = False ' Track if mouse is held down

    Public Property Value As Integer
        Get
            Return _value
        End Get
        Set(val As Integer)
            ' Clamp using the properties we just added [cite: 2026-02-14]
            Dim clampedVal = Math.Clamp(val, Minimum, Maximum)

            If _value <> clampedVal Then
                _value = clampedVal
                Me.Invalidate() ' Visually move the knob

                ' Only raise the scroll event if it's a code-change or a deliberate move
                RaiseEvent Scroll(Me, EventArgs.Empty)
            End If
        End Set
    End Property

    Sub New()
        Me.DoubleBuffered = True
        Me.Height = 28
        Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        Me.BackColor = Color.Transparent
    End Sub

    ' 2. The Final Refresh Logic [cite: 2026-02-14]
    ' This ensures the histogram "catches up" if the drag was too fast
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        _isDragging = False
        MyBase.OnMouseUp(e)
        ' Force one final, high-priority update to sync the 0.8F histogram [cite: 2026-03-03]
        RaiseEvent Scroll(Me, EventArgs.Empty)
        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        _isDragging = True
        UpdateVal(e.X)
        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        If _isDragging Then UpdateVal(e.X)
        MyBase.OnMouseMove(e)
    End Sub

    Private Sub UpdateVal(x As Integer)
        Dim drawWidth As Integer = Me.Width - 20
        If drawWidth <= 0 Then Return

        Dim relX = Math.Min(Math.Max(x - 10, 0), drawWidth)
        Dim newValue = Minimum + CInt((relX / drawWidth) * (Maximum - Minimum))

        ' Only update if the value is different
        If newValue <> _value Then
            _value = newValue

            ' 1. CRITICAL: Repaint the slider IMMEDIATELY so the knob follows the mouse
            Me.Invalidate()
            Me.Update() ' Forces the slider to redraw now, before the event starts

            ' 2. Raise the event for the Histogram/Viewport
            RaiseEvent Scroll(Me, EventArgs.Empty)
        End If
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim g = e.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        ' Draw the Rail (Background)
        Dim railY As Integer = Me.Height / 2
        Using p As New Pen(Color.FromArgb(65, 65, 65), 2)
            g.DrawLine(p, 10, railY, Me.Width - 10, railY)
        End Using

        ' Position Calculation
        Dim range As Integer = Maximum - Minimum
        Dim ratio As Single = If(range > 0, (_value - Minimum) / range, 0)
        Dim knobX As Integer = 10 + CInt(ratio * (Me.Width - 20))

        ' Draw the Active Rail (Color)
        Using p As New Pen(AccentColor, 2)
            g.DrawLine(p, 10, railY, knobX, railY)
        End Using

        ' Draw the Knob
        Dim rect As New Rectangle(knobX - 6, railY - 6, 12, 12)
        Using b As New SolidBrush(AccentColor)
            g.FillEllipse(b, rect)
        End Using
    End Sub

    Protected Overrides Sub OnMouseDoubleClick(e As MouseEventArgs)
        MyBase.OnMouseDoubleClick(e)
        Dim tagStr As String = If(Me.Tag?.ToString(), "")

        ' Reset logic remains the same [cite: 2026-02-14]
        If tagStr = "Exposure" Or tagStr = "Contrast" Or tagStr = "Highlights" Or
           tagStr = "ShadowsLift" Or tagStr = "Whites" Or tagStr = "Blacks" Or
           tagStr = "Temp" Or tagStr = "Tint" Or tagStr = "CineBalance" Or tagStr = "FadeTint" Or
            tagStr = "Clarity" Or tagStr = "Saturation" Or tagStr = "Vibrance" Or
            tagStr = "RedMix" Or tagStr = "GreenMix" Or tagStr = "BlueMix" Or tagStr = "BarrelDistortion" Then
            Me.Value = 50
        Else
            Me.Value = 0
        End If
    End Sub
End Class


Public Class ModernTabControl
    Inherits TabControl

    ' Properties that will appear in the "Appearance" category in Design Mode
    <Category("Appearance")>
    Public Property HeaderBackColor As Color = Color.FromArgb(32, 32, 32)

    <Category("Appearance")>
    Public Property TabActiveColor As Color = Color.DodgerBlue

    <Category("Appearance")>
    Public Property TextActiveColor As Color = Color.White

    <Category("Appearance")>
    Public Property TextInactiveColor As Color = Color.Gray

    Public Sub New()
        Me.SetStyle(ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint Or
                   ControlStyles.DoubleBuffer Or ControlStyles.OptimizedDoubleBuffer, True)
        Me.SizeMode = TabSizeMode.Fixed
        Me.ItemSize = New Size(100, 30)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim g = e.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        ' 1. Fill the Header Background
        Using b As New SolidBrush(HeaderBackColor)
            g.FillRectangle(b, Me.ClientRectangle)
        End Using

        ' 2. Draw each Tab
        For i As Integer = 0 To Me.TabCount - 1
            Dim tabRect = Me.GetTabRect(i)
            Dim isSelected As Boolean = (Me.SelectedIndex = i)

            ' Active Indicator Line
            If isSelected Then
                Using p As New Pen(TabActiveColor, 3)
                    g.DrawLine(p, tabRect.X, tabRect.Bottom - 1, tabRect.Right, tabRect.Bottom - 1)
                End Using
            End If

            ' Tab Text
            Dim textColor = If(isSelected, TextActiveColor, TextInactiveColor)

            ' Draw the Tab Name (TabPage.Text)
            TextRenderer.DrawText(g, Me.TabPages(i).Text, Me.Font, tabRect, textColor,
                                TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
        Next
    End Sub

    ' Fix for the white background inside the tabs
    Protected Overrides Sub OnControlAdded(e As ControlEventArgs)
        MyBase.OnControlAdded(e)
        If TypeOf e.Control Is TabPage Then
            ' Automatically set the interior color to match the header when a tab is added
            e.Control.BackColor = HeaderBackColor
        End If
    End Sub
End Class


' This attribute helps the Toolbox find and load the component correctly [cite: 2026-02-14]
<System.ComponentModel.ToolboxItem(True)>
Public Class ModernVerticalSlider
    Inherits Control

    ' 1. The Event Declaration
    Public Event ValueChanged As EventHandler

    ' 2. Backing Fields
    Private _value As Integer = 0
    Private _minimum As Integer = 5
    Private _maximum As Integer = 100

    ' --- COLOR BACKING FIELDS ---
    Private _railColor As Color = Color.FromArgb(70, 70, 70)
    Private _thumbColor As Color = Color.White
    Private _thumbBorderColor As Color = Color.Black

    ' --- OPTIMIZATION: GDI+ CACHE ---
    Private _railPen As Pen
    Private _thumbBrush As SolidBrush
    Private _thumbPen As Pen

    ' Method to refresh cached drawing objects only when colors change
    Private Sub UpdateGDIResources()
        _railPen?.Dispose()
        _thumbBrush?.Dispose()
        _thumbPen?.Dispose()

        _railPen = New Pen(_railColor, 2)
        _thumbBrush = New SolidBrush(_thumbColor)
        _thumbPen = New Pen(_thumbBorderColor)
    End Sub

    ' --- COLOR PROPERTIES ---
    <System.ComponentModel.Category("Appearance")>
    Public Property RailColor As Color
        Get
            Return _railColor
        End Get
        Set(val As Color)
            _railColor = val
            UpdateGDIResources()
            Me.Invalidate()
        End Set
    End Property

    <System.ComponentModel.Category("Appearance")>
    Public Property ThumbColor As Color
        Get
            Return _thumbColor
        End Get
        Set(val As Color)
            _thumbColor = val
            UpdateGDIResources()
            Me.Invalidate()
        End Set
    End Property

    <System.ComponentModel.Category("Appearance")>
    Public Property ThumbBorderColor As Color
        Get
            Return _thumbBorderColor
        End Get
        Set(val As Color)
            _thumbBorderColor = val
            UpdateGDIResources()
            Me.Invalidate()
        End Set
    End Property

    ' --- EXISTING PROPERTIES ---
    <System.ComponentModel.Category("Behavior")>
    Public Property Minimum As Integer
        Get
            Return _minimum
        End Get
        Set(val As Integer)
            _minimum = val
            Me.Invalidate()
        End Set
    End Property

    <System.ComponentModel.Category("Behavior")>
    Public Property Maximum As Integer
        Get
            Return _maximum
        End Get
        Set(val As Integer)
            _maximum = val
            Me.Invalidate()
        End Set
    End Property

    <System.ComponentModel.Category("Appearance")>
    Public Property Value As Integer
        Get
            Return _value
        End Get
        Set(ByVal val As Integer)
            Dim clampedVal = Math.Max(_minimum, Math.Min(val, _maximum))
            If _value <> clampedVal Then
                _value = clampedVal
                Me.Invalidate()
                RaiseEvent ValueChanged(Me, EventArgs.Empty)
            End If
        End Set
    End Property

    Sub New()
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.DoubleBuffer Or ControlStyles.OptimizedDoubleBuffer, True)
        Me.Size = New Size(20, 200)
        UpdateGDIResources()
    End Sub

    ' --- MOUSE LOGIC ---
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then UpdateScroll(e.Y)
        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then UpdateScroll(e.Y)
        MyBase.OnMouseMove(e)
    End Sub

    Private Sub UpdateScroll(y As Integer)
        Dim span As Integer = Maximum - Minimum
        If span <= 0 Then Return
        ' Improvement: Ensure pixel-perfect calculation using the control's actual height
        Dim pct = 1.0 - (Math.Max(0, Math.Min(y, Me.Height)) / Me.Height)
        Me.Value = CInt(Minimum + (pct * span))
    End Sub

    ' --- OPTIMIZED PAINT LOGIC ---
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        ' Safety check for cached objects
        If _railPen Is Nothing Then UpdateGDIResources()

        Dim g = e.Graphics
        ' We use HighQuality for smoothness, but since we use cached pens, the CPU hit is minimal
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

        ' 1. Draw the Rail (No Using block = no allocation)
        g.DrawLine(_railPen, Me.Width \ 2, 5, Me.Width \ 2, Me.Height - 5)

        ' 2. Calculate Thumb Position
        Dim thumbSize As Integer = 12
        Dim span As Integer = Maximum - Minimum
        Dim ratio As Double = If(span > 0, (Value - Minimum) / span, 0)

        Dim usableHeight = Me.Height - thumbSize - 10
        Dim thumbY = CInt((1.0 - ratio) * usableHeight) + 5
        Dim thumbRect As New Rectangle((Me.Width \ 2) - (thumbSize \ 2), thumbY, thumbSize, thumbSize)

        ' 3. Draw the Thumb (Cached brush)
        g.FillRectangle(_thumbBrush, thumbRect)

        ' 4. Draw the Border (Cached pen)
        g.DrawRectangle(_thumbPen, thumbRect)
    End Sub

    ' Important: Clean up unmanaged resources when control is destroyed
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            _railPen?.Dispose()
            _thumbBrush?.Dispose()
            _thumbPen?.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub
End Class

Public Class CleanFlowPanel
    Inherits FlowLayoutPanel

    Sub New()
        ' This stops the panel from trying to manage its own scrollbars [cite: 2026-02-14]
        Me.AutoScroll = False
        Me.DoubleBuffered = True
        Me.FlowDirection = FlowDirection.TopDown
        Me.WrapContents = False
    End Sub

    ' We override this to prevent the system from ever showing bars [cite: 2026-02-14]
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.Style = cp.Style And Not &H200000 ' Force No V-Scroll
            cp.Style = cp.Style And Not &H100000 ' Force No H-Scroll
            Return cp
        End Get
    End Property
End Class

Public Structure RenderParams
    Public Exposure As Single
    Public Contrast As Single
    Public Highlights As Single
    Public ShadowsLift As Single
    Public Whites As Single
    Public Blacks As Single
    Public Temp As Single
    Public Tint As Single
    Public DynCon As Single
    Public Clarity As Single
    Public StructX As Single
    Public Sharpen As Single
    Public Defringe As Single
    Public Denoise As Single
    Public NoiseLimit As Single
    Public NoiseChroma As Single
    Public NoiseLuma As Single
    Public Saturation As Single
    Public Vibrance As Single
    Public RedMix As Single
    Public GreenMix As Single
    Public BlueMix As Single
    Public LutIntens As Single
    Public DeHaze As Single
    Public Vignette As Single
    Public VignetteStd As Single
    Public FilmGrain As Single
    Public GrainSize As Single
    Public Vivid As Single
    Public SepiaMix As Single
    Public NoirFactor As Single
    Public SplitToning As Single
    Public Fade As Single
    Public FadeTint As Single
    Public Cinematic As Single
    Public CineBalance As Single
    Public TextureX As Single
    Public BarrelDistortion As Single
    Public HexaRadius As Single
    Public HexaSmoothness As Single
    Public HexaRange As Single
    Public HexaOpacity As Single


End Structure

<Serializable>
Public Class PhotoEditState
    Public Property CropArea As RectangleF
    ' Add other properties here later (e.g., Brightness, Rotation)
End Class

Public Class GlowButton
    Inherits Button

    Private _isHovered As Boolean = False

    Public Sub New()
        MyBase.New()
        FlatStyle = FlatStyle.Flat
        FlatAppearance.BorderSize = 0
        BackColor = Color.FromArgb(30, 30, 35)   ' Your dark sidebar color
        ForeColor = Color.White
        Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
        Padding = New Padding(0, 0, 0, 0)
        TextAlign = ContentAlignment.MiddleCenter
    End Sub

    ' Subtle glow intensity in normal state (0.0 - 1.0)
    Public Property NormalGlowIntensity As Single = 0.25F
    ' Stronger glow on hover
    Public Property HoverGlowIntensity As Single = 0.65F
    ' Glow color (soft cyan/blue works great with your ocean theme)
    Public Property GlowColor As Color = Color.FromArgb(100, 180, 255)
    ' Corner Radius
    Public Property CornerRadius As Integer = 6
    ' Pen Width
    Public Property PenWidth As Integer = 1

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        _isHovered = True
        Invalidate()   ' Redraw with stronger glow
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        _isHovered = False
        Invalidate()   ' Redraw with subtle glow
    End Sub

    Protected Overrides Sub OnPaint(pe As PaintEventArgs)
        MyBase.OnPaint(pe)

        Dim g As Graphics = pe.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        Dim rect As Rectangle = New Rectangle(1, 1, Width - 3, Height - 3)

        ' Choose intensity based on hover state
        Dim intensity As Single = If(_isHovered, HoverGlowIntensity, NormalGlowIntensity)

        If intensity > 0 Then
            Using glowBrush As New Drawing2D.LinearGradientBrush(rect, Color.FromArgb(CInt(80 * intensity), GlowColor), Color.Transparent, 90)   ' vertical glow feel
                ' Draw multiple blurred layers for soft glow effect
                For i As Integer = 1 To 4
                    Using p As New Pen(Color.FromArgb(CInt(40 * intensity / i), GlowColor), i * PenWidth)
                        g.DRR(p, rect, CornerRadius)   ' Rounded corners
                    End Using
                Next
            End Using
        End If
    End Sub
End Class

Public Module GraphicsExtensions
    <System.Runtime.CompilerServices.Extension()>
    Public Sub DRR(g As Graphics, pen As Pen, rect As Rectangle, radius As Integer) 'draw round rectangle
        Dim path As New Drawing2D.GraphicsPath()
        path.AddArc(rect.X, rect.Y, radius, radius, 180, 90)
        path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90)
        path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90)
        path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90)
        path.CloseFigure()
        g.DrawPath(pen, path)
    End Sub
End Module

' THE PAINTER (The one you assign to .Renderer)
Public Class DarkThemeMenuRenderer
    Inherits ToolStripProfessionalRenderer

    Public Sub New()
        ' This tells the renderer to use our custom paint colors below
        MyBase.New(New DarkColors())
    End Sub
End Class

' THE PAINT (The specific colors)
Public Class DarkColors
    Inherits ProfessionalColorTable

    ' The main background of the menu
    Public Overrides ReadOnly Property ToolStripDropDownBackground As Color
        Get
            Return Color.FromArgb(30, 30, 35)
        End Get
    End Property

    ' The border around the entire menu
    Public Overrides ReadOnly Property MenuBorder As Color
        Get
            Return Color.FromArgb(60, 60, 65)
        End Get
    End Property

    ' The background of an item when you hover over it
    Public Overrides ReadOnly Property MenuItemSelected As Color
        Get
            Return Color.FromArgb(45, 45, 50)
        End Get
    End Property

    ' The start of the hover-selection gradient
    Public Overrides ReadOnly Property MenuItemSelectedGradientBegin As Color
        Get
            Return Color.FromArgb(45, 45, 50)
        End Get
    End Property

    ' The end of the hover-selection gradient
    Public Overrides ReadOnly Property MenuItemSelectedGradientEnd As Color
        Get
            Return Color.FromArgb(45, 45, 50)
        End Get
    End Property

    ' The sidebar margin
    Public Overrides ReadOnly Property ImageMarginGradientBegin As Color
        Get
            Return Color.FromArgb(30, 30, 35)
        End Get
    End Property
End Class

