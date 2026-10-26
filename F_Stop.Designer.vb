<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_Stop
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_Stop))
        Panel1 = New Panel()
        btn_ExportAll = New GlowButton()
        btn_ExportJPG = New GlowButton()
        btn_SaveChanges = New GlowButton()
        btn_SaveToCatalog = New GlowButton()
        btn_LoadFolder = New GlowButton()
        btn_Open = New GlowButton()
        txtExifData = New RichTextBox()
        lblStatus_Change = New Label()
        pbExportAll = New ProgressBar()
        lblCatalog = New Label()
        StatusBar = New StatusStrip()
        lblGpuInfo = New ToolStripStatusLabel()
        ToolStripStatusLabel1 = New ToolStripStatusLabel()
        lblFileName = New ToolStripStatusLabel()
        ToolStripStatusLabel2 = New ToolStripStatusLabel()
        lblWH = New ToolStripStatusLabel()
        ToolStripStatusLabel3 = New ToolStripStatusLabel()
        lblSizeMp = New ToolStripStatusLabel()
        ToolStripStatusLabel4 = New ToolStripStatusLabel()
        lblBit = New ToolStripStatusLabel()
        ToolStripStatusLabel5 = New ToolStripStatusLabel()
        lblZoom = New ToolStripStatusLabel()
        lblLibrary = New Label()
        panelCenter = New Panel()
        lbl_Check_for_Original = New Label()
        FlowLayoutPanel3 = New FlowLayoutPanel()
        lbl_RGB = New Label()
        lbl_Hex = New Label()
        lbl_XY = New Label()
        GlControl2 = New OpenTK.GLControl.GLControl()
        pnlColorPreview = New PictureBox()
        txt_HEXA_READ = New TextBox()
        thumbnailPanel = New FlowLayoutPanel()
        btn_LoadLut = New GlowButton()
        panelHistogram = New Panel()
        tsRGB = New ToolStrip()
        ToolStripLabel1 = New ToolStripLabel()
        btnRed = New ToolStripButton()
        ToolStripLabel2 = New ToolStripLabel()
        btnGreen = New ToolStripButton()
        ToolStripLabel3 = New ToolStripLabel()
        btnBlue = New ToolStripButton()
        ToolStripLabel4 = New ToolStripLabel()
        btnLuma = New ToolStripButton()
        ToolStripLabel5 = New ToolStripLabel()
        btnAll = New ToolStripButton()
        picHighlights = New PictureBox()
        picShadows = New PictureBox()
        glHist = New PictureBox()
        Panel3 = New Panel()
        ModernTabControl1 = New ModernTabControl()
        TabPage1 = New TabPage()
        flow_Correction = New FlowLayoutPanel()
        Panel2 = New Panel()
        Label84 = New Label()
        Label82 = New Label()
        txt_Exposure = New TextBox()
        Label1 = New Label()
        sldExposure = New ModernSlider()
        Panel5 = New Panel()
        Label85 = New Label()
        Label83 = New Label()
        Label2 = New Label()
        sldContrast = New ModernSlider()
        txt_Contrast = New TextBox()
        Panel6 = New Panel()
        Label88 = New Label()
        Label86 = New Label()
        Label3 = New Label()
        sldHighlights = New ModernSlider()
        txt_Highlights = New TextBox()
        Panel7 = New Panel()
        Label89 = New Label()
        Label87 = New Label()
        Label4 = New Label()
        sldShadowsLift = New ModernSlider()
        txt_ShadowsLift = New TextBox()
        Panel8 = New Panel()
        Label92 = New Label()
        Label90 = New Label()
        Label5 = New Label()
        sldWhites = New ModernSlider()
        txt_Whites = New TextBox()
        Panel9 = New Panel()
        Label93 = New Label()
        Label91 = New Label()
        Label6 = New Label()
        sldBlacks = New ModernSlider()
        txt_Blacks = New TextBox()
        Panel10 = New Panel()
        Label96 = New Label()
        Label94 = New Label()
        Label7 = New Label()
        sldTemp = New ModernSlider()
        txt_Temp = New TextBox()
        Panel11 = New Panel()
        Label97 = New Label()
        Label95 = New Label()
        Label8 = New Label()
        sldTint = New ModernSlider()
        txt_Tint = New TextBox()
        pnl_3 = New Panel()
        lblHelpColor = New Label()
        lblOpacity = New Label()
        Label40 = New Label()
        sldOpacity = New ModernVerticalSlider()
        cb_UseReplace = New CheckBox()
        Label38 = New Label()
        lblAreaCorrect = New Label()
        Label39 = New Label()
        sldAreaCorrect = New ModernVerticalSlider()
        sldSmoothnesCorrect = New ModernVerticalSlider()
        sldHexaCorrect = New ModernVerticalSlider()
        pbCorrectedHexa_Correct = New PictureBox()
        pbHexa_Corect = New PictureBox()
        lblHexaCorrect = New Label()
        lblSmoothnesCorrect = New Label()
        Panel38 = New Panel()
        Panel37 = New Panel()
        lblSwitchCol = New Label()
        Label37 = New Label()
        Label36 = New Label()
        Label34 = New Label()
        Label33 = New Label()
        txtHexa_Corect = New TextBox()
        txtCorrectedHexa_Correct = New TextBox()
        Panel24 = New Panel()
        txt_BarrelDistortion = New TextBox()
        Label51 = New Label()
        sldBarrelDistortion = New ModernSlider()
        Label32 = New Label()
        Panel40 = New Panel()
        btn_UndoRotate = New GlowButton()
        btn_StraightenPic = New GlowButton()
        Label49 = New Label()
        btn_Crop = New GlowButton()
        btn_UndoCrop = New GlowButton()
        Label50 = New Label()
        btn_AutoCorrect = New GlowButton()
        BtnResetCorrectionSliders = New GlowButton()
        Label31 = New Label()
        btn_Remove_Changes = New GlowButton()
        lblOwner = New Label()
        TabPage2 = New TabPage()
        lblHelpEnhancement = New Label()
        btn_ResetEnchentment = New GlowButton()
        sld_FNT_Size = New ModernVerticalSlider()
        lblTextOpacityValue = New Label()
        sldTextOpacity = New ModernSlider()
        btnSelectColor = New Button()
        numFontSize = New NumericUpDown()
        lblFontName = New Label()
        btnFont = New Button()
        txtWatermark = New TextBox()
        Panel23 = New Panel()
        FlowLayoutPanel1 = New FlowLayoutPanel()
        Panel12 = New Panel()
        Label53 = New Label()
        Label52 = New Label()
        lbl = New Label()
        sldDynCon = New ModernSlider()
        txt_DynCon = New TextBox()
        Panel14 = New Panel()
        Label55 = New Label()
        Label54 = New Label()
        Label11 = New Label()
        sldStructX = New ModernSlider()
        txt_StructX = New TextBox()
        Panel15 = New Panel()
        Label57 = New Label()
        Label56 = New Label()
        Label12 = New Label()
        sldSharpen = New ModernSlider()
        txt_Sharpen = New TextBox()
        Panel16 = New Panel()
        Label59 = New Label()
        Label58 = New Label()
        Label13 = New Label()
        sldDefringe = New ModernSlider()
        txt_Defringe = New TextBox()
        Panel17 = New Panel()
        Label61 = New Label()
        Label60 = New Label()
        Label14 = New Label()
        sldDenoise = New ModernSlider()
        txt_Denoise = New TextBox()
        Panel42 = New Panel()
        Label65 = New Label()
        Label62 = New Label()
        PictureBox2 = New PictureBox()
        Label42 = New Label()
        sldNoiseLimit = New ModernSlider()
        txt_NoiseLimit = New TextBox()
        Panel43 = New Panel()
        Label66 = New Label()
        Label63 = New Label()
        PictureBox3 = New PictureBox()
        Label43 = New Label()
        sldNoiseChroma = New ModernSlider()
        txt_NoiseChroma = New TextBox()
        Panel44 = New Panel()
        Label67 = New Label()
        Label64 = New Label()
        PictureBox4 = New PictureBox()
        Label44 = New Label()
        sldNoiseLuma = New ModernSlider()
        txt_NoiseLuma = New TextBox()
        Panel36 = New Panel()
        Label69 = New Label()
        Label68 = New Label()
        Label30 = New Label()
        sldTextureX = New ModernSlider()
        txt_TextureX = New TextBox()
        Panel13 = New Panel()
        Label71 = New Label()
        Label70 = New Label()
        Label10 = New Label()
        sldClarity = New ModernSlider()
        txt_Clarity = New TextBox()
        Panel18 = New Panel()
        Label73 = New Label()
        Label72 = New Label()
        Label15 = New Label()
        sldSaturation = New ModernSlider()
        txt_Saturation = New TextBox()
        Panel19 = New Panel()
        Label75 = New Label()
        Label74 = New Label()
        Label16 = New Label()
        sldVibrance = New ModernSlider()
        txt_Vibrance = New TextBox()
        Panel20 = New Panel()
        Label77 = New Label()
        Label76 = New Label()
        Label9 = New Label()
        sldRedMix = New ModernSlider()
        txt_RedMix = New TextBox()
        Panel21 = New Panel()
        Label79 = New Label()
        Label78 = New Label()
        Label17 = New Label()
        sldGreenMix = New ModernSlider()
        txt_GreenMix = New TextBox()
        Panel22 = New Panel()
        Label81 = New Label()
        Label80 = New Label()
        Label18 = New Label()
        sldBlueMix = New ModernSlider()
        txt_BlueMix = New TextBox()
        Label126 = New Label()
        TabPage3 = New TabPage()
        btn_ResetStylization = New GlowButton()
        lblLUThelp = New Label()
        Panel35 = New Panel()
        lblLutName = New Label()
        cmbLut = New ComboBox()
        FlowLayoutPanel2 = New FlowLayoutPanel()
        Panel25 = New Panel()
        Label99 = New Label()
        Label98 = New Label()
        Label19 = New Label()
        sldLutIntens = New ModernSlider()
        txt_LutIntens = New TextBox()
        Panel26 = New Panel()
        Label111 = New Label()
        Label100 = New Label()
        Label20 = New Label()
        sldDeHaze = New ModernSlider()
        txt_DeHaze = New TextBox()
        Panel27 = New Panel()
        Label112 = New Label()
        Label101 = New Label()
        Label21 = New Label()
        sldVignette = New ModernSlider()
        txt_Vignette = New TextBox()
        Panel28 = New Panel()
        Label113 = New Label()
        Label102 = New Label()
        Label22 = New Label()
        sldVignetteStd = New ModernSlider()
        txt_VignetteStd = New TextBox()
        Panel29 = New Panel()
        Label114 = New Label()
        Label103 = New Label()
        Label23 = New Label()
        sldSepiaMix = New ModernSlider()
        txt_SepiaMix = New TextBox()
        Panel30 = New Panel()
        Label115 = New Label()
        Label104 = New Label()
        Label24 = New Label()
        sldNoirFactor = New ModernSlider()
        txt_NoirFactor = New TextBox()
        Panel31 = New Panel()
        Label116 = New Label()
        Label105 = New Label()
        Label25 = New Label()
        sldVivid = New ModernSlider()
        txt_Vivid = New TextBox()
        Panel32 = New Panel()
        Label117 = New Label()
        Label106 = New Label()
        Label26 = New Label()
        sldFilmGrain = New ModernSlider()
        txt_FilmGrain = New TextBox()
        Panel39 = New Panel()
        Label118 = New Label()
        Label108 = New Label()
        PictureBox1 = New PictureBox()
        Label35 = New Label()
        sldGrainSize = New ModernSlider()
        txt_GrainSize = New TextBox()
        Panel33 = New Panel()
        Label119 = New Label()
        Label107 = New Label()
        Label27 = New Label()
        sldSplitToning = New ModernSlider()
        txt_SplitToning = New TextBox()
        Panel34 = New Panel()
        Label120 = New Label()
        Label109 = New Label()
        Label28 = New Label()
        sldFade = New ModernSlider()
        txt_Fade = New TextBox()
        Panel46 = New Panel()
        Label124 = New Label()
        Label121 = New Label()
        PictureBox6 = New PictureBox()
        Label47 = New Label()
        sldFadeTint = New ModernSlider()
        txt_FadeTint = New TextBox()
        Panel41 = New Panel()
        Label122 = New Label()
        Label110 = New Label()
        Label45 = New Label()
        sldCinematic = New ModernSlider()
        txt_Cinematic = New TextBox()
        Panel45 = New Panel()
        Label125 = New Label()
        Label123 = New Label()
        PictureBox5 = New PictureBox()
        Label46 = New Label()
        sldCineBalance = New ModernSlider()
        txt_CineBalance = New TextBox()
        pb_HEXA_READ = New PictureBox()
        Panel4 = New Panel()
        btn_OrigEdit = New GlowButton()
        Label41 = New Label()
        Label29 = New Label()
        viewportMenu = New ContextMenuStrip(components)
        itemCenter = New ToolStripMenuItem()
        itemFit = New ToolStripMenuItem()
        itemOriginal = New ToolStripMenuItem()
        ToolStripSeparator1 = New ToolStripSeparator()
        FixPositionToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator2 = New ToolStripSeparator()
        ChangeColorCtopToolStripMenuItem = New ToolStripMenuItem()
        TS_White = New ToolStripMenuItem()
        TS_Yellow = New ToolStripMenuItem()
        TS_Red = New ToolStripMenuItem()
        TS_Blue = New ToolStripMenuItem()
        tmrAnimate = New Timer(components)
        thumbMenu = New ContextMenuStrip(components)
        DeletePictureFromStripToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator3 = New ToolStripSeparator()
        ClearAllStripToolStripMenuItem = New ToolStripMenuItem()
        Panel1.SuspendLayout()
        StatusBar.SuspendLayout()
        panelCenter.SuspendLayout()
        FlowLayoutPanel3.SuspendLayout()
        CType(pnlColorPreview, ComponentModel.ISupportInitialize).BeginInit()
        panelHistogram.SuspendLayout()
        tsRGB.SuspendLayout()
        CType(picHighlights, ComponentModel.ISupportInitialize).BeginInit()
        CType(picShadows, ComponentModel.ISupportInitialize).BeginInit()
        CType(glHist, ComponentModel.ISupportInitialize).BeginInit()
        Panel3.SuspendLayout()
        ModernTabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        flow_Correction.SuspendLayout()
        Panel2.SuspendLayout()
        Panel5.SuspendLayout()
        Panel6.SuspendLayout()
        Panel7.SuspendLayout()
        Panel8.SuspendLayout()
        Panel9.SuspendLayout()
        Panel10.SuspendLayout()
        Panel11.SuspendLayout()
        pnl_3.SuspendLayout()
        CType(pbCorrectedHexa_Correct, ComponentModel.ISupportInitialize).BeginInit()
        CType(pbHexa_Corect, ComponentModel.ISupportInitialize).BeginInit()
        Panel24.SuspendLayout()
        Panel40.SuspendLayout()
        TabPage2.SuspendLayout()
        CType(numFontSize, ComponentModel.ISupportInitialize).BeginInit()
        FlowLayoutPanel1.SuspendLayout()
        Panel12.SuspendLayout()
        Panel14.SuspendLayout()
        Panel15.SuspendLayout()
        Panel16.SuspendLayout()
        Panel17.SuspendLayout()
        Panel42.SuspendLayout()
        CType(PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        Panel43.SuspendLayout()
        CType(PictureBox3, ComponentModel.ISupportInitialize).BeginInit()
        Panel44.SuspendLayout()
        CType(PictureBox4, ComponentModel.ISupportInitialize).BeginInit()
        Panel36.SuspendLayout()
        Panel13.SuspendLayout()
        Panel18.SuspendLayout()
        Panel19.SuspendLayout()
        Panel20.SuspendLayout()
        Panel21.SuspendLayout()
        Panel22.SuspendLayout()
        TabPage3.SuspendLayout()
        FlowLayoutPanel2.SuspendLayout()
        Panel25.SuspendLayout()
        Panel26.SuspendLayout()
        Panel27.SuspendLayout()
        Panel28.SuspendLayout()
        Panel29.SuspendLayout()
        Panel30.SuspendLayout()
        Panel31.SuspendLayout()
        Panel32.SuspendLayout()
        Panel39.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        Panel33.SuspendLayout()
        Panel34.SuspendLayout()
        Panel46.SuspendLayout()
        CType(PictureBox6, ComponentModel.ISupportInitialize).BeginInit()
        Panel41.SuspendLayout()
        Panel45.SuspendLayout()
        CType(PictureBox5, ComponentModel.ISupportInitialize).BeginInit()
        CType(pb_HEXA_READ, ComponentModel.ISupportInitialize).BeginInit()
        Panel4.SuspendLayout()
        viewportMenu.SuspendLayout()
        thumbMenu.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.FromArgb(CByte(40), CByte(40), CByte(40))
        Panel1.Controls.Add(btn_ExportAll)
        Panel1.Controls.Add(btn_ExportJPG)
        Panel1.Controls.Add(btn_SaveChanges)
        Panel1.Controls.Add(btn_SaveToCatalog)
        Panel1.Controls.Add(btn_LoadFolder)
        Panel1.Controls.Add(btn_Open)
        Panel1.Controls.Add(txtExifData)
        Panel1.Controls.Add(lblStatus_Change)
        Panel1.Controls.Add(pbExportAll)
        Panel1.Location = New Point(10, 9)
        Panel1.Margin = New Padding(3, 2, 3, 2)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(236, 198)
        Panel1.TabIndex = 0
        ' 
        ' btn_ExportAll
        ' 
        btn_ExportAll.BackColor = Color.FromArgb(CByte(40), CByte(40), CByte(40))
        btn_ExportAll.CornerRadius = 5
        btn_ExportAll.FlatAppearance.BorderSize = 0
        btn_ExportAll.FlatStyle = FlatStyle.Flat
        btn_ExportAll.Font = New Font("Segoe UI", 9F)
        btn_ExportAll.ForeColor = Color.FromArgb(CByte(170), CByte(189), CByte(189))
        btn_ExportAll.GlowColor = Color.Silver
        btn_ExportAll.HoverGlowIntensity = 0.9F
        btn_ExportAll.Location = New Point(121, 74)
        btn_ExportAll.Name = "btn_ExportAll"
        btn_ExportAll.NormalGlowIntensity = 0.5F
        btn_ExportAll.PenWidth = 1
        btn_ExportAll.Size = New Size(110, 24)
        btn_ExportAll.TabIndex = 28
        btn_ExportAll.TabStop = False
        btn_ExportAll.Text = "EXPORT ALL"
        btn_ExportAll.UseVisualStyleBackColor = False
        ' 
        ' btn_ExportJPG
        ' 
        btn_ExportJPG.BackColor = Color.FromArgb(CByte(40), CByte(40), CByte(40))
        btn_ExportJPG.CornerRadius = 5
        btn_ExportJPG.FlatAppearance.BorderSize = 0
        btn_ExportJPG.FlatStyle = FlatStyle.Flat
        btn_ExportJPG.Font = New Font("Segoe UI", 9F)
        btn_ExportJPG.ForeColor = Color.FromArgb(CByte(170), CByte(189), CByte(189))
        btn_ExportJPG.GlowColor = Color.Silver
        btn_ExportJPG.HoverGlowIntensity = 0.9F
        btn_ExportJPG.Location = New Point(7, 74)
        btn_ExportJPG.Name = "btn_ExportJPG"
        btn_ExportJPG.NormalGlowIntensity = 0.5F
        btn_ExportJPG.PenWidth = 1
        btn_ExportJPG.Size = New Size(110, 24)
        btn_ExportJPG.TabIndex = 27
        btn_ExportJPG.TabStop = False
        btn_ExportJPG.Text = "EXPORT TO JPG"
        btn_ExportJPG.UseVisualStyleBackColor = False
        ' 
        ' btn_SaveChanges
        ' 
        btn_SaveChanges.BackColor = Color.FromArgb(CByte(40), CByte(40), CByte(40))
        btn_SaveChanges.CornerRadius = 5
        btn_SaveChanges.Enabled = False
        btn_SaveChanges.FlatAppearance.BorderSize = 0
        btn_SaveChanges.FlatStyle = FlatStyle.Flat
        btn_SaveChanges.Font = New Font("Segoe UI", 9F)
        btn_SaveChanges.ForeColor = Color.FromArgb(CByte(170), CByte(189), CByte(189))
        btn_SaveChanges.GlowColor = Color.Silver
        btn_SaveChanges.HoverGlowIntensity = 0.9F
        btn_SaveChanges.Location = New Point(121, 42)
        btn_SaveChanges.Name = "btn_SaveChanges"
        btn_SaveChanges.NormalGlowIntensity = 0.5F
        btn_SaveChanges.PenWidth = 1
        btn_SaveChanges.Size = New Size(110, 24)
        btn_SaveChanges.TabIndex = 26
        btn_SaveChanges.TabStop = False
        btn_SaveChanges.Text = "SAVE CHANGES"
        btn_SaveChanges.UseVisualStyleBackColor = False
        ' 
        ' btn_SaveToCatalog
        ' 
        btn_SaveToCatalog.BackColor = Color.FromArgb(CByte(40), CByte(40), CByte(40))
        btn_SaveToCatalog.CornerRadius = 5
        btn_SaveToCatalog.FlatAppearance.BorderSize = 0
        btn_SaveToCatalog.FlatStyle = FlatStyle.Flat
        btn_SaveToCatalog.Font = New Font("Segoe UI", 9F)
        btn_SaveToCatalog.ForeColor = Color.FromArgb(CByte(170), CByte(189), CByte(189))
        btn_SaveToCatalog.GlowColor = Color.Silver
        btn_SaveToCatalog.HoverGlowIntensity = 0.9F
        btn_SaveToCatalog.Location = New Point(7, 41)
        btn_SaveToCatalog.Name = "btn_SaveToCatalog"
        btn_SaveToCatalog.NormalGlowIntensity = 0.5F
        btn_SaveToCatalog.PenWidth = 1
        btn_SaveToCatalog.Size = New Size(110, 24)
        btn_SaveToCatalog.TabIndex = 25
        btn_SaveToCatalog.TabStop = False
        btn_SaveToCatalog.Text = "SAVE CATALOG"
        btn_SaveToCatalog.UseVisualStyleBackColor = False
        ' 
        ' btn_LoadFolder
        ' 
        btn_LoadFolder.BackColor = Color.FromArgb(CByte(40), CByte(40), CByte(40))
        btn_LoadFolder.CornerRadius = 5
        btn_LoadFolder.FlatAppearance.BorderSize = 0
        btn_LoadFolder.FlatStyle = FlatStyle.Flat
        btn_LoadFolder.Font = New Font("Segoe UI", 9F)
        btn_LoadFolder.ForeColor = Color.FromArgb(CByte(170), CByte(189), CByte(189))
        btn_LoadFolder.GlowColor = Color.Silver
        btn_LoadFolder.HoverGlowIntensity = 0.9F
        btn_LoadFolder.Location = New Point(121, 9)
        btn_LoadFolder.Name = "btn_LoadFolder"
        btn_LoadFolder.NormalGlowIntensity = 0.5F
        btn_LoadFolder.PenWidth = 1
        btn_LoadFolder.Size = New Size(110, 24)
        btn_LoadFolder.TabIndex = 24
        btn_LoadFolder.TabStop = False
        btn_LoadFolder.Text = "LOAD FOLDER"
        btn_LoadFolder.UseVisualStyleBackColor = False
        ' 
        ' btn_Open
        ' 
        btn_Open.BackColor = Color.FromArgb(CByte(40), CByte(40), CByte(40))
        btn_Open.CornerRadius = 5
        btn_Open.FlatAppearance.BorderSize = 0
        btn_Open.FlatStyle = FlatStyle.Flat
        btn_Open.Font = New Font("Segoe UI", 9F)
        btn_Open.ForeColor = Color.FromArgb(CByte(170), CByte(189), CByte(189))
        btn_Open.GlowColor = Color.Silver
        btn_Open.HoverGlowIntensity = 0.9F
        btn_Open.Location = New Point(7, 9)
        btn_Open.Name = "btn_Open"
        btn_Open.NormalGlowIntensity = 0.5F
        btn_Open.PenWidth = 1
        btn_Open.Size = New Size(110, 24)
        btn_Open.TabIndex = 23
        btn_Open.TabStop = False
        btn_Open.Text = "OPEN FILE"
        btn_Open.UseVisualStyleBackColor = False
        ' 
        ' txtExifData
        ' 
        txtExifData.BackColor = Color.FromArgb(CByte(47), CByte(47), CByte(47))
        txtExifData.BorderStyle = BorderStyle.None
        txtExifData.ForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtExifData.Location = New Point(3, 112)
        txtExifData.Margin = New Padding(3, 2, 3, 2)
        txtExifData.Name = "txtExifData"
        txtExifData.ReadOnly = True
        txtExifData.ScrollBars = RichTextBoxScrollBars.None
        txtExifData.Size = New Size(230, 64)
        txtExifData.TabIndex = 4
        txtExifData.TabStop = False
        txtExifData.Text = "No data."
        ' 
        ' lblStatus_Change
        ' 
        lblStatus_Change.AutoSize = True
        lblStatus_Change.ForeColor = Color.Gray
        lblStatus_Change.Location = New Point(3, 180)
        lblStatus_Change.Name = "lblStatus_Change"
        lblStatus_Change.Size = New Size(120, 13)
        lblStatus_Change.TabIndex = 18
        lblStatus_Change.Text = "No changes - Original"
        ' 
        ' pbExportAll
        ' 
        pbExportAll.Location = New Point(127, 100)
        pbExportAll.Name = "pbExportAll"
        pbExportAll.Size = New Size(97, 6)
        pbExportAll.Style = ProgressBarStyle.Continuous
        pbExportAll.TabIndex = 20
        pbExportAll.Visible = False
        ' 
        ' lblCatalog
        ' 
        lblCatalog.AutoSize = True
        lblCatalog.ForeColor = Color.FromArgb(CByte(255), CByte(192), CByte(128))
        lblCatalog.Location = New Point(26, 0)
        lblCatalog.Name = "lblCatalog"
        lblCatalog.Size = New Size(54, 13)
        lblCatalog.TabIndex = 10
        lblCatalog.Text = "FOLDER :"
        ' 
        ' StatusBar
        ' 
        StatusBar.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        StatusBar.Items.AddRange(New ToolStripItem() {lblGpuInfo, ToolStripStatusLabel1, lblFileName, ToolStripStatusLabel2, lblWH, ToolStripStatusLabel3, lblSizeMp, ToolStripStatusLabel4, lblBit, ToolStripStatusLabel5, lblZoom})
        StatusBar.Location = New Point(0, 963)
        StatusBar.Name = "StatusBar"
        StatusBar.Padding = New Padding(1, 0, 12, 0)
        StatusBar.Size = New Size(1904, 22)
        StatusBar.TabIndex = 3
        StatusBar.Text = "StatusStrip1"
        ' 
        ' lblGpuInfo
        ' 
        lblGpuInfo.ForeColor = Color.Silver
        lblGpuInfo.Name = "lblGpuInfo"
        lblGpuInfo.Size = New Size(30, 17)
        lblGpuInfo.Text = "GPU"
        ' 
        ' ToolStripStatusLabel1
        ' 
        ToolStripStatusLabel1.ForeColor = Color.FromArgb(CByte(255), CByte(192), CByte(128))
        ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        ToolStripStatusLabel1.Size = New Size(40, 17)
        ToolStripStatusLabel1.Text = "   ||      "
        ' 
        ' lblFileName
        ' 
        lblFileName.ForeColor = Color.Silver
        lblFileName.Name = "lblFileName"
        lblFileName.Size = New Size(57, 17)
        lblFileName.Text = "FileName"
        ' 
        ' ToolStripStatusLabel2
        ' 
        ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        ToolStripStatusLabel2.Size = New Size(28, 17)
        ToolStripStatusLabel2.Text = "   |   "
        ' 
        ' lblWH
        ' 
        lblWH.ForeColor = Color.Silver
        lblWH.Name = "lblWH"
        lblWH.Size = New Size(38, 17)
        lblWH.Text = "W x H"
        ' 
        ' ToolStripStatusLabel3
        ' 
        ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        ToolStripStatusLabel3.Size = New Size(28, 17)
        ToolStripStatusLabel3.Text = "   |   "
        ' 
        ' lblSizeMp
        ' 
        lblSizeMp.ForeColor = Color.Silver
        lblSizeMp.Name = "lblSizeMp"
        lblSizeMp.Size = New Size(39, 17)
        lblSizeMp.Text = "0 Mpx"
        ' 
        ' ToolStripStatusLabel4
        ' 
        ToolStripStatusLabel4.Name = "ToolStripStatusLabel4"
        ToolStripStatusLabel4.Size = New Size(28, 17)
        ToolStripStatusLabel4.Text = "   |   "
        ' 
        ' lblBit
        ' 
        lblBit.ForeColor = Color.Silver
        lblBit.Name = "lblBit"
        lblBit.Size = New Size(30, 17)
        lblBit.Text = "0 Bit"
        ' 
        ' ToolStripStatusLabel5
        ' 
        ToolStripStatusLabel5.Name = "ToolStripStatusLabel5"
        ToolStripStatusLabel5.Size = New Size(25, 17)
        ToolStripStatusLabel5.Text = "   |  "
        ' 
        ' lblZoom
        ' 
        lblZoom.ForeColor = Color.Silver
        lblZoom.Name = "lblZoom"
        lblZoom.Size = New Size(64, 17)
        lblZoom.Text = "Zoom: 0 %"
        ' 
        ' lblLibrary
        ' 
        lblLibrary.AutoSize = True
        lblLibrary.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        lblLibrary.Location = New Point(86, 212)
        lblLibrary.Name = "lblLibrary"
        lblLibrary.Size = New Size(78, 13)
        lblLibrary.TabIndex = 4
        lblLibrary.Text = "Pictures:  0 / 0"
        ' 
        ' panelCenter
        ' 
        panelCenter.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        panelCenter.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        panelCenter.BorderStyle = BorderStyle.FixedSingle
        panelCenter.Controls.Add(lbl_Check_for_Original)
        panelCenter.Controls.Add(FlowLayoutPanel3)
        panelCenter.Controls.Add(lbl_XY)
        panelCenter.Controls.Add(GlControl2)
        panelCenter.Controls.Add(pnlColorPreview)
        panelCenter.Controls.Add(lblCatalog)
        panelCenter.Location = New Point(252, 9)
        panelCenter.Margin = New Padding(3, 2, 3, 2)
        panelCenter.Name = "panelCenter"
        panelCenter.Size = New Size(1254, 949)
        panelCenter.TabIndex = 5
        ' 
        ' lbl_Check_for_Original
        ' 
        lbl_Check_for_Original.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lbl_Check_for_Original.AutoSize = True
        lbl_Check_for_Original.ForeColor = Color.FromArgb(CByte(192), CByte(255), CByte(192))
        lbl_Check_for_Original.Location = New Point(766, -1)
        lbl_Check_for_Original.Name = "lbl_Check_for_Original"
        lbl_Check_for_Original.Size = New Size(77, 13)
        lbl_Check_for_Original.TabIndex = 15
        lbl_Check_for_Original.Text = "Picture Status"
        ' 
        ' FlowLayoutPanel3
        ' 
        FlowLayoutPanel3.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        FlowLayoutPanel3.Controls.Add(lbl_RGB)
        FlowLayoutPanel3.Controls.Add(lbl_Hex)
        FlowLayoutPanel3.Location = New Point(0, 932)
        FlowLayoutPanel3.Name = "FlowLayoutPanel3"
        FlowLayoutPanel3.Size = New Size(194, 15)
        FlowLayoutPanel3.TabIndex = 11
        ' 
        ' lbl_RGB
        ' 
        lbl_RGB.Anchor = AnchorStyles.None
        lbl_RGB.AutoSize = True
        lbl_RGB.ForeColor = Color.FromArgb(CByte(255), CByte(192), CByte(128))
        lbl_RGB.Location = New Point(3, 0)
        lbl_RGB.Name = "lbl_RGB"
        lbl_RGB.Size = New Size(32, 13)
        lbl_RGB.TabIndex = 12
        lbl_RGB.Text = "RGB:"
        ' 
        ' lbl_Hex
        ' 
        lbl_Hex.Anchor = AnchorStyles.None
        lbl_Hex.AutoSize = True
        lbl_Hex.ForeColor = Color.FromArgb(CByte(255), CByte(192), CByte(128))
        lbl_Hex.Location = New Point(41, 0)
        lbl_Hex.Name = "lbl_Hex"
        lbl_Hex.Size = New Size(30, 13)
        lbl_Hex.TabIndex = 13
        lbl_Hex.Text = "HEX:"
        ' 
        ' lbl_XY
        ' 
        lbl_XY.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        lbl_XY.AutoSize = True
        lbl_XY.ForeColor = Color.FromArgb(CByte(255), CByte(192), CByte(128))
        lbl_XY.Location = New Point(966, 932)
        lbl_XY.Name = "lbl_XY"
        lbl_XY.Size = New Size(68, 13)
        lbl_XY.TabIndex = 11
        lbl_XY.Text = "X = 0 / Y = 0"
        ' 
        ' GlControl2
        ' 
        GlControl2.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        GlControl2.API = OpenTK.Windowing.Common.ContextAPI.OpenGL
        GlControl2.APIVersion = New Version(4, 5, 0, 0)
        GlControl2.BackColor = Color.Black
        GlControl2.Flags = OpenTK.Windowing.Common.ContextFlags.Default
        GlControl2.IsEventDriven = True
        GlControl2.Location = New Point(8, 16)
        GlControl2.Margin = New Padding(3, 2, 3, 2)
        GlControl2.Name = "GlControl2"
        GlControl2.Profile = OpenTK.Windowing.Common.ContextProfile.Core
        GlControl2.SharedContext = Nothing
        GlControl2.Size = New Size(1235, 913)
        GlControl2.TabIndex = 2
        ' 
        ' pnlColorPreview
        ' 
        pnlColorPreview.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        pnlColorPreview.BackColor = Color.FromArgb(CByte(32), CByte(32), CByte(32))
        pnlColorPreview.Location = New Point(200, 933)
        pnlColorPreview.Name = "pnlColorPreview"
        pnlColorPreview.Size = New Size(14, 14)
        pnlColorPreview.TabIndex = 14
        pnlColorPreview.TabStop = False
        ' 
        ' txt_HEXA_READ
        ' 
        txt_HEXA_READ.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        txt_HEXA_READ.BorderStyle = BorderStyle.FixedSingle
        txt_HEXA_READ.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_HEXA_READ.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_HEXA_READ.Location = New Point(303, 31)
        txt_HEXA_READ.Name = "txt_HEXA_READ"
        txt_HEXA_READ.Size = New Size(54, 22)
        txt_HEXA_READ.TabIndex = 18
        ' 
        ' thumbnailPanel
        ' 
        thumbnailPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        thumbnailPanel.AutoScroll = True
        thumbnailPanel.BackColor = Color.FromArgb(CByte(44), CByte(44), CByte(44))
        thumbnailPanel.Location = New Point(10, 232)
        thumbnailPanel.Margin = New Padding(3, 2, 3, 2)
        thumbnailPanel.Name = "thumbnailPanel"
        thumbnailPanel.Size = New Size(236, 726)
        thumbnailPanel.TabIndex = 6
        ' 
        ' btn_LoadLut
        ' 
        btn_LoadLut.BackColor = Color.FromArgb(CByte(30), CByte(30), CByte(35))
        btn_LoadLut.CornerRadius = 10
        btn_LoadLut.FlatAppearance.BorderSize = 0
        btn_LoadLut.FlatStyle = FlatStyle.Flat
        btn_LoadLut.Font = New Font("Segoe UI", 9F)
        btn_LoadLut.ForeColor = Color.Silver
        btn_LoadLut.GlowColor = Color.FromArgb(CByte(255), CByte(192), CByte(255))
        btn_LoadLut.HoverGlowIntensity = 0.65F
        btn_LoadLut.Location = New Point(21, 11)
        btn_LoadLut.Name = "btn_LoadLut"
        btn_LoadLut.NormalGlowIntensity = 0.35F
        btn_LoadLut.PenWidth = 2
        btn_LoadLut.Size = New Size(308, 25)
        btn_LoadLut.TabIndex = 44
        btn_LoadLut.TabStop = False
        btn_LoadLut.Text = "LOAD LUT .CUBE"
        btn_LoadLut.UseVisualStyleBackColor = False
        ' 
        ' panelHistogram
        ' 
        panelHistogram.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        panelHistogram.BackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        panelHistogram.Controls.Add(tsRGB)
        panelHistogram.Controls.Add(picHighlights)
        panelHistogram.Controls.Add(picShadows)
        panelHistogram.Controls.Add(glHist)
        panelHistogram.Location = New Point(1512, 9)
        panelHistogram.Margin = New Padding(3, 2, 3, 2)
        panelHistogram.Name = "panelHistogram"
        panelHistogram.Size = New Size(382, 149)
        panelHistogram.TabIndex = 7
        ' 
        ' tsRGB
        ' 
        tsRGB.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        tsRGB.BackgroundImageLayout = ImageLayout.Center
        tsRGB.GripStyle = ToolStripGripStyle.Hidden
        tsRGB.Items.AddRange(New ToolStripItem() {ToolStripLabel1, btnRed, ToolStripLabel2, btnGreen, ToolStripLabel3, btnBlue, ToolStripLabel4, btnLuma, ToolStripLabel5, btnAll})
        tsRGB.Location = New Point(0, 0)
        tsRGB.Name = "tsRGB"
        tsRGB.RenderMode = ToolStripRenderMode.Professional
        tsRGB.Size = New Size(382, 25)
        tsRGB.TabIndex = 4
        tsRGB.Text = "ToolStrip1"
        ' 
        ' ToolStripLabel1
        ' 
        ToolStripLabel1.Name = "ToolStripLabel1"
        ToolStripLabel1.Size = New Size(31, 22)
        ToolStripLabel1.Text = "        "
        ' 
        ' btnRed
        ' 
        btnRed.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnRed.Image = CType(resources.GetObject("btnRed.Image"), Image)
        btnRed.ImageTransparentColor = Color.Magenta
        btnRed.Name = "btnRed"
        btnRed.Size = New Size(23, 22)
        btnRed.Text = "Red"
        ' 
        ' ToolStripLabel2
        ' 
        ToolStripLabel2.Name = "ToolStripLabel2"
        ToolStripLabel2.Size = New Size(15, 22)
        ToolStripLabel2.Text = "- "
        ' 
        ' btnGreen
        ' 
        btnGreen.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnGreen.Image = CType(resources.GetObject("btnGreen.Image"), Image)
        btnGreen.ImageTransparentColor = Color.Magenta
        btnGreen.Name = "btnGreen"
        btnGreen.Size = New Size(23, 22)
        btnGreen.Text = "Green"
        ' 
        ' ToolStripLabel3
        ' 
        ToolStripLabel3.Name = "ToolStripLabel3"
        ToolStripLabel3.Size = New Size(12, 22)
        ToolStripLabel3.Text = "-"
        ' 
        ' btnBlue
        ' 
        btnBlue.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnBlue.Image = CType(resources.GetObject("btnBlue.Image"), Image)
        btnBlue.ImageTransparentColor = Color.Magenta
        btnBlue.Name = "btnBlue"
        btnBlue.Size = New Size(23, 22)
        btnBlue.Text = "Blue"
        ' 
        ' ToolStripLabel4
        ' 
        ToolStripLabel4.Name = "ToolStripLabel4"
        ToolStripLabel4.Size = New Size(12, 22)
        ToolStripLabel4.Text = "-"
        ' 
        ' btnLuma
        ' 
        btnLuma.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnLuma.Image = CType(resources.GetObject("btnLuma.Image"), Image)
        btnLuma.ImageTransparentColor = Color.Magenta
        btnLuma.Name = "btnLuma"
        btnLuma.Size = New Size(23, 22)
        btnLuma.Text = "Luma"
        ' 
        ' ToolStripLabel5
        ' 
        ToolStripLabel5.Name = "ToolStripLabel5"
        ToolStripLabel5.Size = New Size(12, 22)
        ToolStripLabel5.Text = "-"
        ' 
        ' btnAll
        ' 
        btnAll.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnAll.Image = CType(resources.GetObject("btnAll.Image"), Image)
        btnAll.ImageTransparentColor = Color.Magenta
        btnAll.Name = "btnAll"
        btnAll.Size = New Size(23, 22)
        btnAll.Text = "RGB"
        ' 
        ' picHighlights
        ' 
        picHighlights.Cursor = Cursors.Hand
        picHighlights.Image = CType(resources.GetObject("picHighlights.Image"), Image)
        picHighlights.Location = New Point(344, 29)
        picHighlights.Margin = New Padding(3, 2, 3, 2)
        picHighlights.Name = "picHighlights"
        picHighlights.Size = New Size(19, 19)
        picHighlights.SizeMode = PictureBoxSizeMode.StretchImage
        picHighlights.TabIndex = 2
        picHighlights.TabStop = False
        ' 
        ' picShadows
        ' 
        picShadows.Cursor = Cursors.Hand
        picShadows.Image = CType(resources.GetObject("picShadows.Image"), Image)
        picShadows.Location = New Point(13, 30)
        picShadows.Margin = New Padding(3, 2, 3, 2)
        picShadows.Name = "picShadows"
        picShadows.Size = New Size(19, 19)
        picShadows.SizeMode = PictureBoxSizeMode.StretchImage
        picShadows.TabIndex = 1
        picShadows.TabStop = False
        ' 
        ' glHist
        ' 
        glHist.BackColor = Color.Black
        glHist.Location = New Point(11, 28)
        glHist.Name = "glHist"
        glHist.Size = New Size(354, 118)
        glHist.TabIndex = 3
        glHist.TabStop = False
        ' 
        ' Panel3
        ' 
        Panel3.Anchor = AnchorStyles.Right
        Panel3.BackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        Panel3.Controls.Add(ModernTabControl1)
        Panel3.Location = New Point(1512, 165)
        Panel3.Margin = New Padding(3, 2, 3, 2)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(382, 718)
        Panel3.TabIndex = 8
        ' 
        ' ModernTabControl1
        ' 
        ModernTabControl1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        ModernTabControl1.Controls.Add(TabPage1)
        ModernTabControl1.Controls.Add(TabPage2)
        ModernTabControl1.Controls.Add(TabPage3)
        ModernTabControl1.HeaderBackColor = Color.FromArgb(CByte(32), CByte(32), CByte(32))
        ModernTabControl1.ItemSize = New Size(100, 30)
        ModernTabControl1.Location = New Point(11, 5)
        ModernTabControl1.Name = "ModernTabControl1"
        ModernTabControl1.SelectedIndex = 0
        ModernTabControl1.Size = New Size(354, 706)
        ModernTabControl1.SizeMode = TabSizeMode.Fixed
        ModernTabControl1.TabActiveColor = Color.FromArgb(CByte(255), CByte(192), CByte(192))
        ModernTabControl1.TabIndex = 3
        ModernTabControl1.TextActiveColor = Color.White
        ModernTabControl1.TextInactiveColor = Color.Gray
        ' 
        ' TabPage1
        ' 
        TabPage1.BackColor = Color.FromArgb(CByte(32), CByte(32), CByte(32))
        TabPage1.Controls.Add(flow_Correction)
        TabPage1.Controls.Add(lblOwner)
        TabPage1.Location = New Point(4, 34)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(346, 668)
        TabPage1.TabIndex = 0
        TabPage1.Text = "CORRECTION"
        ' 
        ' flow_Correction
        ' 
        flow_Correction.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        flow_Correction.BackColor = Color.Transparent
        flow_Correction.Controls.Add(Panel2)
        flow_Correction.Controls.Add(Panel5)
        flow_Correction.Controls.Add(Panel6)
        flow_Correction.Controls.Add(Panel7)
        flow_Correction.Controls.Add(Panel8)
        flow_Correction.Controls.Add(Panel9)
        flow_Correction.Controls.Add(Panel10)
        flow_Correction.Controls.Add(Panel11)
        flow_Correction.Controls.Add(pnl_3)
        flow_Correction.Controls.Add(Panel24)
        flow_Correction.Controls.Add(Label32)
        flow_Correction.Controls.Add(Panel40)
        flow_Correction.Controls.Add(Label49)
        flow_Correction.Controls.Add(btn_Crop)
        flow_Correction.Controls.Add(btn_UndoCrop)
        flow_Correction.Controls.Add(Label50)
        flow_Correction.Controls.Add(btn_AutoCorrect)
        flow_Correction.Controls.Add(BtnResetCorrectionSliders)
        flow_Correction.Controls.Add(Label31)
        flow_Correction.Controls.Add(btn_Remove_Changes)
        flow_Correction.Location = New Point(21, 15)
        flow_Correction.Name = "flow_Correction"
        flow_Correction.Size = New Size(308, 635)
        flow_Correction.TabIndex = 0
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(Label84)
        Panel2.Controls.Add(Label82)
        Panel2.Controls.Add(txt_Exposure)
        Panel2.Controls.Add(Label1)
        Panel2.Controls.Add(sldExposure)
        Panel2.Location = New Point(3, 3)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(299, 29)
        Panel2.TabIndex = 3
        ' 
        ' Label84
        ' 
        Label84.AutoSize = True
        Label84.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label84.ForeColor = Color.FromArgb(CByte(89), CByte(133), CByte(136))
        Label84.Location = New Point(245, 9)
        Label84.Name = "Label84"
        Label84.Size = New Size(17, 11)
        Label84.TabIndex = 54
        Label84.Text = "100"
        ' 
        ' Label82
        ' 
        Label82.AutoSize = True
        Label82.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label82.ForeColor = Color.FromArgb(CByte(89), CByte(133), CByte(136))
        Label82.Location = New Point(68, 9)
        Label82.Name = "Label82"
        Label82.Size = New Size(20, 11)
        Label82.TabIndex = 53
        Label82.Text = "-100"
        ' 
        ' txt_Exposure
        ' 
        txt_Exposure.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_Exposure.BorderStyle = BorderStyle.None
        txt_Exposure.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_Exposure.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_Exposure.Location = New Point(263, 7)
        txt_Exposure.Margin = New Padding(3, 2, 3, 2)
        txt_Exposure.Name = "txt_Exposure"
        txt_Exposure.Size = New Size(30, 15)
        txt_Exposure.TabIndex = 1
        txt_Exposure.Text = "0"
        txt_Exposure.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label1.Location = New Point(3, 7)
        Label1.Name = "Label1"
        Label1.Size = New Size(61, 13)
        Label1.TabIndex = 2
        Label1.Text = "EXPOSURE"
        ' 
        ' sldExposure
        ' 
        sldExposure.AccentColor = Color.White
        sldExposure.BackColor = Color.Transparent
        sldExposure.Location = New Point(83, 2)
        sldExposure.Margin = New Padding(3, 2, 3, 2)
        sldExposure.Maximum = 100
        sldExposure.Minimum = 0
        sldExposure.Name = "sldExposure"
        sldExposure.Size = New Size(165, 24)
        sldExposure.TabIndex = 0
        sldExposure.Tag = "Exposure"
        sldExposure.Text = "Exposure"
        sldExposure.Value = 50
        ' 
        ' Panel5
        ' 
        Panel5.Controls.Add(Label85)
        Panel5.Controls.Add(Label83)
        Panel5.Controls.Add(Label2)
        Panel5.Controls.Add(sldContrast)
        Panel5.Controls.Add(txt_Contrast)
        Panel5.Location = New Point(3, 38)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(299, 29)
        Panel5.TabIndex = 4
        ' 
        ' Label85
        ' 
        Label85.AutoSize = True
        Label85.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label85.ForeColor = Color.FromArgb(CByte(89), CByte(133), CByte(136))
        Label85.Location = New Point(245, 9)
        Label85.Name = "Label85"
        Label85.Size = New Size(17, 11)
        Label85.TabIndex = 55
        Label85.Text = "100"
        ' 
        ' Label83
        ' 
        Label83.AutoSize = True
        Label83.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label83.ForeColor = Color.FromArgb(CByte(89), CByte(133), CByte(136))
        Label83.Location = New Point(68, 9)
        Label83.Name = "Label83"
        Label83.Size = New Size(20, 11)
        Label83.TabIndex = 54
        Label83.Text = "-100"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label2.Location = New Point(3, 7)
        Label2.Name = "Label2"
        Label2.Size = New Size(61, 13)
        Label2.TabIndex = 2
        Label2.Text = "CONTRAST"
        ' 
        ' sldContrast
        ' 
        sldContrast.AccentColor = Color.FromArgb(CByte(244), CByte(155), CByte(180))
        sldContrast.BackColor = Color.Transparent
        sldContrast.Location = New Point(83, 2)
        sldContrast.Margin = New Padding(3, 2, 3, 2)
        sldContrast.Maximum = 100
        sldContrast.Minimum = 0
        sldContrast.Name = "sldContrast"
        sldContrast.Size = New Size(165, 24)
        sldContrast.TabIndex = 0
        sldContrast.Tag = "Contrast"
        sldContrast.Text = "Contrast"
        sldContrast.Value = 50
        ' 
        ' txt_Contrast
        ' 
        txt_Contrast.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_Contrast.BorderStyle = BorderStyle.None
        txt_Contrast.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_Contrast.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_Contrast.Location = New Point(263, 7)
        txt_Contrast.Margin = New Padding(3, 2, 3, 2)
        txt_Contrast.Name = "txt_Contrast"
        txt_Contrast.Size = New Size(30, 15)
        txt_Contrast.TabIndex = 1
        txt_Contrast.Text = "0"
        txt_Contrast.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel6
        ' 
        Panel6.Controls.Add(Label88)
        Panel6.Controls.Add(Label86)
        Panel6.Controls.Add(Label3)
        Panel6.Controls.Add(sldHighlights)
        Panel6.Controls.Add(txt_Highlights)
        Panel6.Location = New Point(3, 73)
        Panel6.Name = "Panel6"
        Panel6.Size = New Size(299, 29)
        Panel6.TabIndex = 5
        ' 
        ' Label88
        ' 
        Label88.AutoSize = True
        Label88.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label88.ForeColor = Color.FromArgb(CByte(89), CByte(133), CByte(136))
        Label88.Location = New Point(249, 9)
        Label88.Name = "Label88"
        Label88.Size = New Size(13, 11)
        Label88.TabIndex = 56
        Label88.Text = "50"
        ' 
        ' Label86
        ' 
        Label86.AutoSize = True
        Label86.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label86.ForeColor = Color.FromArgb(CByte(89), CByte(133), CByte(136))
        Label86.Location = New Point(72, 9)
        Label86.Name = "Label86"
        Label86.Size = New Size(16, 11)
        Label86.TabIndex = 55
        Label86.Text = "-50"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label3.Location = New Point(3, 7)
        Label3.Name = "Label3"
        Label3.Size = New Size(69, 13)
        Label3.TabIndex = 2
        Label3.Text = "HIGHLIGHTS"
        ' 
        ' sldHighlights
        ' 
        sldHighlights.AccentColor = Color.FromArgb(CByte(239), CByte(237), CByte(156))
        sldHighlights.BackColor = Color.Transparent
        sldHighlights.Location = New Point(83, 2)
        sldHighlights.Margin = New Padding(3, 2, 3, 2)
        sldHighlights.Maximum = 100
        sldHighlights.Minimum = 0
        sldHighlights.Name = "sldHighlights"
        sldHighlights.Size = New Size(165, 24)
        sldHighlights.TabIndex = 0
        sldHighlights.Tag = "Highlights"
        sldHighlights.Text = "Highlights"
        sldHighlights.Value = 50
        ' 
        ' txt_Highlights
        ' 
        txt_Highlights.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_Highlights.BorderStyle = BorderStyle.None
        txt_Highlights.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_Highlights.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_Highlights.Location = New Point(263, 7)
        txt_Highlights.Margin = New Padding(3, 2, 3, 2)
        txt_Highlights.Name = "txt_Highlights"
        txt_Highlights.Size = New Size(30, 15)
        txt_Highlights.TabIndex = 1
        txt_Highlights.Text = "0"
        txt_Highlights.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel7
        ' 
        Panel7.Controls.Add(Label89)
        Panel7.Controls.Add(Label87)
        Panel7.Controls.Add(Label4)
        Panel7.Controls.Add(sldShadowsLift)
        Panel7.Controls.Add(txt_ShadowsLift)
        Panel7.Location = New Point(3, 108)
        Panel7.Name = "Panel7"
        Panel7.Size = New Size(299, 29)
        Panel7.TabIndex = 6
        ' 
        ' Label89
        ' 
        Label89.AutoSize = True
        Label89.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label89.ForeColor = Color.FromArgb(CByte(89), CByte(133), CByte(136))
        Label89.Location = New Point(249, 9)
        Label89.Name = "Label89"
        Label89.Size = New Size(13, 11)
        Label89.TabIndex = 57
        Label89.Text = "50"
        ' 
        ' Label87
        ' 
        Label87.AutoSize = True
        Label87.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label87.ForeColor = Color.FromArgb(CByte(89), CByte(133), CByte(136))
        Label87.Location = New Point(72, 9)
        Label87.Name = "Label87"
        Label87.Size = New Size(16, 11)
        Label87.TabIndex = 56
        Label87.Text = "-50"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label4.Location = New Point(3, 7)
        Label4.Name = "Label4"
        Label4.Size = New Size(62, 13)
        Label4.TabIndex = 2
        Label4.Text = "SHADOWS"
        ' 
        ' sldShadowsLift
        ' 
        sldShadowsLift.AccentColor = Color.FromArgb(CByte(158), CByte(158), CByte(158))
        sldShadowsLift.BackColor = Color.Transparent
        sldShadowsLift.Location = New Point(83, 2)
        sldShadowsLift.Margin = New Padding(3, 2, 3, 2)
        sldShadowsLift.Maximum = 100
        sldShadowsLift.Minimum = 0
        sldShadowsLift.Name = "sldShadowsLift"
        sldShadowsLift.Size = New Size(165, 24)
        sldShadowsLift.TabIndex = 0
        sldShadowsLift.Tag = "ShadowsLift"
        sldShadowsLift.Text = "ShadowsLift"
        sldShadowsLift.Value = 50
        ' 
        ' txt_ShadowsLift
        ' 
        txt_ShadowsLift.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_ShadowsLift.BorderStyle = BorderStyle.None
        txt_ShadowsLift.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_ShadowsLift.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_ShadowsLift.Location = New Point(263, 7)
        txt_ShadowsLift.Margin = New Padding(3, 2, 3, 2)
        txt_ShadowsLift.Name = "txt_ShadowsLift"
        txt_ShadowsLift.Size = New Size(30, 15)
        txt_ShadowsLift.TabIndex = 1
        txt_ShadowsLift.Text = "0"
        txt_ShadowsLift.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel8
        ' 
        Panel8.Controls.Add(Label92)
        Panel8.Controls.Add(Label90)
        Panel8.Controls.Add(Label5)
        Panel8.Controls.Add(sldWhites)
        Panel8.Controls.Add(txt_Whites)
        Panel8.Location = New Point(3, 143)
        Panel8.Name = "Panel8"
        Panel8.Size = New Size(299, 29)
        Panel8.TabIndex = 7
        ' 
        ' Label92
        ' 
        Label92.AutoSize = True
        Label92.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label92.ForeColor = Color.FromArgb(CByte(89), CByte(133), CByte(136))
        Label92.Location = New Point(249, 9)
        Label92.Name = "Label92"
        Label92.Size = New Size(13, 11)
        Label92.TabIndex = 58
        Label92.Text = "50"
        ' 
        ' Label90
        ' 
        Label90.AutoSize = True
        Label90.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label90.ForeColor = Color.FromArgb(CByte(89), CByte(133), CByte(136))
        Label90.Location = New Point(72, 9)
        Label90.Name = "Label90"
        Label90.Size = New Size(16, 11)
        Label90.TabIndex = 57
        Label90.Text = "-50"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label5.Location = New Point(3, 7)
        Label5.Name = "Label5"
        Label5.Size = New Size(46, 13)
        Label5.TabIndex = 2
        Label5.Text = "WHITES"
        ' 
        ' sldWhites
        ' 
        sldWhites.AccentColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        sldWhites.BackColor = Color.Transparent
        sldWhites.Location = New Point(83, 2)
        sldWhites.Margin = New Padding(3, 2, 3, 2)
        sldWhites.Maximum = 100
        sldWhites.Minimum = 0
        sldWhites.Name = "sldWhites"
        sldWhites.Size = New Size(165, 24)
        sldWhites.TabIndex = 0
        sldWhites.Tag = "Whites"
        sldWhites.Text = "Whites"
        sldWhites.Value = 50
        ' 
        ' txt_Whites
        ' 
        txt_Whites.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_Whites.BorderStyle = BorderStyle.None
        txt_Whites.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_Whites.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_Whites.Location = New Point(263, 7)
        txt_Whites.Margin = New Padding(3, 2, 3, 2)
        txt_Whites.Name = "txt_Whites"
        txt_Whites.Size = New Size(30, 15)
        txt_Whites.TabIndex = 1
        txt_Whites.Text = "0"
        txt_Whites.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel9
        ' 
        Panel9.Controls.Add(Label93)
        Panel9.Controls.Add(Label91)
        Panel9.Controls.Add(Label6)
        Panel9.Controls.Add(sldBlacks)
        Panel9.Controls.Add(txt_Blacks)
        Panel9.Location = New Point(3, 178)
        Panel9.Name = "Panel9"
        Panel9.Size = New Size(299, 29)
        Panel9.TabIndex = 8
        ' 
        ' Label93
        ' 
        Label93.AutoSize = True
        Label93.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label93.ForeColor = Color.FromArgb(CByte(89), CByte(133), CByte(136))
        Label93.Location = New Point(249, 9)
        Label93.Name = "Label93"
        Label93.Size = New Size(13, 11)
        Label93.TabIndex = 58
        Label93.Text = "50"
        ' 
        ' Label91
        ' 
        Label91.AutoSize = True
        Label91.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label91.ForeColor = Color.FromArgb(CByte(89), CByte(133), CByte(136))
        Label91.Location = New Point(72, 9)
        Label91.Name = "Label91"
        Label91.Size = New Size(16, 11)
        Label91.TabIndex = 57
        Label91.Text = "-50"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label6.Location = New Point(3, 7)
        Label6.Name = "Label6"
        Label6.Size = New Size(45, 13)
        Label6.TabIndex = 2
        Label6.Text = "BLACKS"
        ' 
        ' sldBlacks
        ' 
        sldBlacks.AccentColor = Color.FromArgb(CByte(74), CByte(81), CByte(117))
        sldBlacks.BackColor = Color.Transparent
        sldBlacks.Location = New Point(83, 2)
        sldBlacks.Margin = New Padding(3, 2, 3, 2)
        sldBlacks.Maximum = 100
        sldBlacks.Minimum = 0
        sldBlacks.Name = "sldBlacks"
        sldBlacks.Size = New Size(165, 24)
        sldBlacks.TabIndex = 0
        sldBlacks.Tag = "Blacks"
        sldBlacks.Text = "Blacks"
        sldBlacks.Value = 50
        ' 
        ' txt_Blacks
        ' 
        txt_Blacks.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_Blacks.BorderStyle = BorderStyle.None
        txt_Blacks.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_Blacks.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_Blacks.Location = New Point(263, 7)
        txt_Blacks.Margin = New Padding(3, 2, 3, 2)
        txt_Blacks.Name = "txt_Blacks"
        txt_Blacks.Size = New Size(30, 15)
        txt_Blacks.TabIndex = 1
        txt_Blacks.Text = "0"
        txt_Blacks.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel10
        ' 
        Panel10.Controls.Add(Label96)
        Panel10.Controls.Add(Label94)
        Panel10.Controls.Add(Label7)
        Panel10.Controls.Add(sldTemp)
        Panel10.Controls.Add(txt_Temp)
        Panel10.Location = New Point(3, 213)
        Panel10.Name = "Panel10"
        Panel10.Size = New Size(299, 29)
        Panel10.TabIndex = 9
        ' 
        ' Label96
        ' 
        Label96.AutoSize = True
        Label96.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label96.ForeColor = Color.FromArgb(CByte(89), CByte(133), CByte(136))
        Label96.Location = New Point(245, 9)
        Label96.Name = "Label96"
        Label96.Size = New Size(17, 11)
        Label96.TabIndex = 56
        Label96.Text = "100"
        ' 
        ' Label94
        ' 
        Label94.AutoSize = True
        Label94.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label94.ForeColor = Color.FromArgb(CByte(89), CByte(133), CByte(136))
        Label94.Location = New Point(68, 9)
        Label94.Name = "Label94"
        Label94.Size = New Size(20, 11)
        Label94.TabIndex = 55
        Label94.Text = "-100"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label7.Location = New Point(3, 7)
        Label7.Name = "Label7"
        Label7.Size = New Size(61, 13)
        Label7.TabIndex = 2
        Label7.Text = "TEMPERAT."
        ' 
        ' sldTemp
        ' 
        sldTemp.AccentColor = Color.FromArgb(CByte(237), CByte(197), CByte(167))
        sldTemp.BackColor = Color.Transparent
        sldTemp.Location = New Point(83, 2)
        sldTemp.Margin = New Padding(3, 2, 3, 2)
        sldTemp.Maximum = 100
        sldTemp.Minimum = 0
        sldTemp.Name = "sldTemp"
        sldTemp.Size = New Size(165, 24)
        sldTemp.TabIndex = 0
        sldTemp.Tag = "Temp"
        sldTemp.Text = "Temp"
        sldTemp.Value = 50
        ' 
        ' txt_Temp
        ' 
        txt_Temp.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_Temp.BorderStyle = BorderStyle.None
        txt_Temp.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_Temp.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_Temp.Location = New Point(263, 7)
        txt_Temp.Margin = New Padding(3, 2, 3, 2)
        txt_Temp.Name = "txt_Temp"
        txt_Temp.Size = New Size(30, 15)
        txt_Temp.TabIndex = 1
        txt_Temp.Text = "0"
        txt_Temp.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel11
        ' 
        Panel11.Controls.Add(Label97)
        Panel11.Controls.Add(Label95)
        Panel11.Controls.Add(Label8)
        Panel11.Controls.Add(sldTint)
        Panel11.Controls.Add(txt_Tint)
        Panel11.Location = New Point(3, 248)
        Panel11.Name = "Panel11"
        Panel11.Size = New Size(299, 29)
        Panel11.TabIndex = 10
        ' 
        ' Label97
        ' 
        Label97.AutoSize = True
        Label97.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label97.ForeColor = Color.FromArgb(CByte(89), CByte(133), CByte(136))
        Label97.Location = New Point(245, 9)
        Label97.Name = "Label97"
        Label97.Size = New Size(17, 11)
        Label97.TabIndex = 56
        Label97.Text = "100"
        ' 
        ' Label95
        ' 
        Label95.AutoSize = True
        Label95.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label95.ForeColor = Color.FromArgb(CByte(89), CByte(133), CByte(136))
        Label95.Location = New Point(68, 9)
        Label95.Name = "Label95"
        Label95.Size = New Size(20, 11)
        Label95.TabIndex = 55
        Label95.Text = "-100"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label8.Location = New Point(3, 7)
        Label8.Name = "Label8"
        Label8.Size = New Size(28, 13)
        Label8.TabIndex = 2
        Label8.Text = "TINT"
        ' 
        ' sldTint
        ' 
        sldTint.AccentColor = Color.FromArgb(CByte(152), CByte(225), CByte(134))
        sldTint.BackColor = Color.Transparent
        sldTint.Location = New Point(83, 2)
        sldTint.Margin = New Padding(3, 2, 3, 2)
        sldTint.Maximum = 100
        sldTint.Minimum = 0
        sldTint.Name = "sldTint"
        sldTint.Size = New Size(165, 24)
        sldTint.TabIndex = 0
        sldTint.Tag = "Tint"
        sldTint.Text = "Tint"
        sldTint.Value = 50
        ' 
        ' txt_Tint
        ' 
        txt_Tint.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_Tint.BorderStyle = BorderStyle.None
        txt_Tint.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_Tint.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_Tint.Location = New Point(263, 7)
        txt_Tint.Margin = New Padding(3, 2, 3, 2)
        txt_Tint.Name = "txt_Tint"
        txt_Tint.Size = New Size(30, 15)
        txt_Tint.TabIndex = 1
        txt_Tint.Text = "0"
        txt_Tint.TextAlign = HorizontalAlignment.Center
        ' 
        ' pnl_3
        ' 
        pnl_3.Controls.Add(lblHelpColor)
        pnl_3.Controls.Add(lblOpacity)
        pnl_3.Controls.Add(Label40)
        pnl_3.Controls.Add(sldOpacity)
        pnl_3.Controls.Add(cb_UseReplace)
        pnl_3.Controls.Add(Label38)
        pnl_3.Controls.Add(lblAreaCorrect)
        pnl_3.Controls.Add(Label39)
        pnl_3.Controls.Add(sldAreaCorrect)
        pnl_3.Controls.Add(sldSmoothnesCorrect)
        pnl_3.Controls.Add(sldHexaCorrect)
        pnl_3.Controls.Add(pbCorrectedHexa_Correct)
        pnl_3.Controls.Add(pbHexa_Corect)
        pnl_3.Controls.Add(lblHexaCorrect)
        pnl_3.Controls.Add(lblSmoothnesCorrect)
        pnl_3.Controls.Add(Panel38)
        pnl_3.Controls.Add(Panel37)
        pnl_3.Controls.Add(lblSwitchCol)
        pnl_3.Controls.Add(Label37)
        pnl_3.Controls.Add(Label36)
        pnl_3.Controls.Add(Label34)
        pnl_3.Controls.Add(Label33)
        pnl_3.Controls.Add(txtHexa_Corect)
        pnl_3.Controls.Add(txtCorrectedHexa_Correct)
        pnl_3.Location = New Point(3, 283)
        pnl_3.Name = "pnl_3"
        pnl_3.Size = New Size(299, 115)
        pnl_3.TabIndex = 20
        ' 
        ' lblHelpColor
        ' 
        lblHelpColor.AutoSize = True
        lblHelpColor.Cursor = Cursors.Hand
        lblHelpColor.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblHelpColor.ForeColor = Color.Silver
        lblHelpColor.Location = New Point(124, 63)
        lblHelpColor.Name = "lblHelpColor"
        lblHelpColor.Size = New Size(14, 17)
        lblHelpColor.TabIndex = 41
        lblHelpColor.Text = "?"
        ' 
        ' lblOpacity
        ' 
        lblOpacity.ForeColor = Color.Gray
        lblOpacity.Location = New Point(96, 88)
        lblOpacity.Name = "lblOpacity"
        lblOpacity.Size = New Size(22, 13)
        lblOpacity.TabIndex = 40
        lblOpacity.Text = "0"
        lblOpacity.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label40
        ' 
        Label40.AutoSize = True
        Label40.ForeColor = Color.FromArgb(CByte(192), CByte(255), CByte(255))
        Label40.Location = New Point(99, 19)
        Label40.Name = "Label40"
        Label40.Size = New Size(16, 13)
        Label40.TabIndex = 39
        Label40.Text = "O"
        ' 
        ' sldOpacity
        ' 
        sldOpacity.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        sldOpacity.Location = New Point(101, 34)
        sldOpacity.Maximum = 100
        sldOpacity.Minimum = 0
        sldOpacity.Name = "sldOpacity"
        sldOpacity.RailColor = Color.FromArgb(CByte(192), CByte(192), CByte(255))
        sldOpacity.Size = New Size(10, 55)
        sldOpacity.TabIndex = 38
        sldOpacity.Text = "ModernVerticalSlider2"
        sldOpacity.ThumbBorderColor = Color.Black
        sldOpacity.ThumbColor = Color.FromArgb(CByte(192), CByte(192), CByte(255))
        sldOpacity.Value = 0
        ' 
        ' cb_UseReplace
        ' 
        cb_UseReplace.AutoSize = True
        cb_UseReplace.ForeColor = Color.FromArgb(CByte(192), CByte(255), CByte(192))
        cb_UseReplace.Location = New Point(249, 18)
        cb_UseReplace.Name = "cb_UseReplace"
        cb_UseReplace.Size = New Size(47, 17)
        cb_UseReplace.TabIndex = 28
        cb_UseReplace.Text = "OFF"
        cb_UseReplace.UseVisualStyleBackColor = True
        ' 
        ' Label38
        ' 
        Label38.AutoSize = True
        Label38.Font = New Font("SketchFlow Print", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label38.ForeColor = Color.DimGray
        Label38.Location = New Point(128, 19)
        Label38.Name = "Label38"
        Label38.Size = New Size(107, 12)
        Label38.TabIndex = 37
        Label38.Text = "COLOR REPLACE"
        ' 
        ' lblAreaCorrect
        ' 
        lblAreaCorrect.ForeColor = Color.Gray
        lblAreaCorrect.Location = New Point(68, 88)
        lblAreaCorrect.Name = "lblAreaCorrect"
        lblAreaCorrect.Size = New Size(22, 13)
        lblAreaCorrect.TabIndex = 36
        lblAreaCorrect.Text = "0"
        lblAreaCorrect.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label39
        ' 
        Label39.AutoSize = True
        Label39.ForeColor = Color.FromArgb(CByte(192), CByte(255), CByte(255))
        Label39.Location = New Point(71, 19)
        Label39.Name = "Label39"
        Label39.Size = New Size(14, 13)
        Label39.TabIndex = 35
        Label39.Text = "A"
        ' 
        ' sldAreaCorrect
        ' 
        sldAreaCorrect.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        sldAreaCorrect.Location = New Point(73, 34)
        sldAreaCorrect.Maximum = 100
        sldAreaCorrect.Minimum = 0
        sldAreaCorrect.Name = "sldAreaCorrect"
        sldAreaCorrect.RailColor = Color.FromArgb(CByte(192), CByte(255), CByte(255))
        sldAreaCorrect.Size = New Size(10, 55)
        sldAreaCorrect.TabIndex = 34
        sldAreaCorrect.Text = "ModernVerticalSlider2"
        sldAreaCorrect.ThumbBorderColor = Color.Black
        sldAreaCorrect.ThumbColor = Color.FromArgb(CByte(192), CByte(255), CByte(255))
        sldAreaCorrect.Value = 0
        ' 
        ' sldSmoothnesCorrect
        ' 
        sldSmoothnesCorrect.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        sldSmoothnesCorrect.Location = New Point(43, 34)
        sldSmoothnesCorrect.Maximum = 100
        sldSmoothnesCorrect.Minimum = 0
        sldSmoothnesCorrect.Name = "sldSmoothnesCorrect"
        sldSmoothnesCorrect.RailColor = Color.FromArgb(CByte(255), CByte(255), CByte(192))
        sldSmoothnesCorrect.Size = New Size(10, 55)
        sldSmoothnesCorrect.TabIndex = 33
        sldSmoothnesCorrect.Text = "ModernVerticalSlider2"
        sldSmoothnesCorrect.ThumbBorderColor = Color.Black
        sldSmoothnesCorrect.ThumbColor = Color.FromArgb(CByte(255), CByte(255), CByte(192))
        sldSmoothnesCorrect.Value = 0
        ' 
        ' sldHexaCorrect
        ' 
        sldHexaCorrect.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        sldHexaCorrect.ForeColor = Color.White
        sldHexaCorrect.Location = New Point(12, 34)
        sldHexaCorrect.Maximum = 100
        sldHexaCorrect.Minimum = 0
        sldHexaCorrect.Name = "sldHexaCorrect"
        sldHexaCorrect.RailColor = Color.FromArgb(CByte(255), CByte(192), CByte(192))
        sldHexaCorrect.Size = New Size(10, 55)
        sldHexaCorrect.TabIndex = 32
        sldHexaCorrect.Text = "ModernVerticalSlider1"
        sldHexaCorrect.ThumbBorderColor = Color.Black
        sldHexaCorrect.ThumbColor = Color.FromArgb(CByte(255), CByte(192), CByte(192))
        sldHexaCorrect.Value = 0
        ' 
        ' pbCorrectedHexa_Correct
        ' 
        pbCorrectedHexa_Correct.BackColor = Color.FromArgb(CByte(32), CByte(32), CByte(32))
        pbCorrectedHexa_Correct.BorderStyle = BorderStyle.FixedSingle
        pbCorrectedHexa_Correct.Location = New Point(238, 87)
        pbCorrectedHexa_Correct.Name = "pbCorrectedHexa_Correct"
        pbCorrectedHexa_Correct.Size = New Size(55, 12)
        pbCorrectedHexa_Correct.TabIndex = 30
        pbCorrectedHexa_Correct.TabStop = False
        ' 
        ' pbHexa_Corect
        ' 
        pbHexa_Corect.BackColor = Color.FromArgb(CByte(32), CByte(32), CByte(32))
        pbHexa_Corect.BorderStyle = BorderStyle.FixedSingle
        pbHexa_Corect.Location = New Point(145, 87)
        pbHexa_Corect.Name = "pbHexa_Corect"
        pbHexa_Corect.Size = New Size(55, 12)
        pbHexa_Corect.TabIndex = 29
        pbHexa_Corect.TabStop = False
        ' 
        ' lblHexaCorrect
        ' 
        lblHexaCorrect.ForeColor = Color.Gray
        lblHexaCorrect.Location = New Point(6, 88)
        lblHexaCorrect.Name = "lblHexaCorrect"
        lblHexaCorrect.Size = New Size(22, 13)
        lblHexaCorrect.TabIndex = 27
        lblHexaCorrect.Text = "0"
        lblHexaCorrect.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblSmoothnesCorrect
        ' 
        lblSmoothnesCorrect.ForeColor = Color.Gray
        lblSmoothnesCorrect.Location = New Point(38, 88)
        lblSmoothnesCorrect.Name = "lblSmoothnesCorrect"
        lblSmoothnesCorrect.Size = New Size(22, 13)
        lblSmoothnesCorrect.TabIndex = 26
        lblSmoothnesCorrect.Text = "0"
        lblSmoothnesCorrect.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Panel38
        ' 
        Panel38.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        Panel38.Dock = DockStyle.Bottom
        Panel38.Location = New Point(0, 112)
        Panel38.Name = "Panel38"
        Panel38.Size = New Size(299, 3)
        Panel38.TabIndex = 25
        ' 
        ' Panel37
        ' 
        Panel37.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        Panel37.Dock = DockStyle.Top
        Panel37.Location = New Point(0, 0)
        Panel37.Name = "Panel37"
        Panel37.Size = New Size(299, 3)
        Panel37.TabIndex = 24
        ' 
        ' lblSwitchCol
        ' 
        lblSwitchCol.AutoSize = True
        lblSwitchCol.ForeColor = Color.FromArgb(CByte(192), CByte(255), CByte(192))
        lblSwitchCol.Location = New Point(209, 53)
        lblSwitchCol.Name = "lblSwitchCol"
        lblSwitchCol.Size = New Size(23, 13)
        lblSwitchCol.TabIndex = 23
        lblSwitchCol.Text = ">>"
        lblSwitchCol.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label37
        ' 
        Label37.AutoSize = True
        Label37.ForeColor = Color.Gray
        Label37.Location = New Point(235, 42)
        Label37.Name = "Label37"
        Label37.Size = New Size(61, 13)
        Label37.TabIndex = 22
        Label37.Text = "C.REPLACE"
        ' 
        ' Label36
        ' 
        Label36.AutoSize = True
        Label36.ForeColor = Color.Gray
        Label36.Location = New Point(145, 42)
        Label36.Name = "Label36"
        Label36.Size = New Size(54, 13)
        Label36.TabIndex = 21
        Label36.Text = "C.TARGET"
        ' 
        ' Label34
        ' 
        Label34.AutoSize = True
        Label34.ForeColor = Color.FromArgb(CByte(255), CByte(192), CByte(192))
        Label34.Location = New Point(11, 19)
        Label34.Name = "Label34"
        Label34.Size = New Size(13, 13)
        Label34.TabIndex = 20
        Label34.Text = "F"
        ' 
        ' Label33
        ' 
        Label33.AutoSize = True
        Label33.ForeColor = Color.FromArgb(CByte(255), CByte(255), CByte(192))
        Label33.Location = New Point(42, 19)
        Label33.Name = "Label33"
        Label33.Size = New Size(13, 13)
        Label33.TabIndex = 19
        Label33.Text = "S"
        ' 
        ' txtHexa_Corect
        ' 
        txtHexa_Corect.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        txtHexa_Corect.BorderStyle = BorderStyle.FixedSingle
        txtHexa_Corect.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txtHexa_Corect.Location = New Point(145, 60)
        txtHexa_Corect.Name = "txtHexa_Corect"
        txtHexa_Corect.Size = New Size(55, 22)
        txtHexa_Corect.TabIndex = 1
        ' 
        ' txtCorrectedHexa_Correct
        ' 
        txtCorrectedHexa_Correct.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        txtCorrectedHexa_Correct.BorderStyle = BorderStyle.FixedSingle
        txtCorrectedHexa_Correct.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txtCorrectedHexa_Correct.Location = New Point(238, 60)
        txtCorrectedHexa_Correct.Name = "txtCorrectedHexa_Correct"
        txtCorrectedHexa_Correct.Size = New Size(55, 22)
        txtCorrectedHexa_Correct.TabIndex = 0
        ' 
        ' Panel24
        ' 
        Panel24.Controls.Add(txt_BarrelDistortion)
        Panel24.Controls.Add(Label51)
        Panel24.Controls.Add(sldBarrelDistortion)
        Panel24.Location = New Point(3, 404)
        Panel24.Name = "Panel24"
        Panel24.Size = New Size(299, 29)
        Panel24.TabIndex = 30
        ' 
        ' txt_BarrelDistortion
        ' 
        txt_BarrelDistortion.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_BarrelDistortion.BorderStyle = BorderStyle.None
        txt_BarrelDistortion.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_BarrelDistortion.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_BarrelDistortion.Location = New Point(263, 8)
        txt_BarrelDistortion.Margin = New Padding(3, 2, 3, 2)
        txt_BarrelDistortion.Name = "txt_BarrelDistortion"
        txt_BarrelDistortion.Size = New Size(30, 15)
        txt_BarrelDistortion.TabIndex = 1
        txt_BarrelDistortion.Text = "0"
        txt_BarrelDistortion.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label51
        ' 
        Label51.AutoSize = True
        Label51.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label51.Location = New Point(3, 8)
        Label51.Name = "Label51"
        Label51.Size = New Size(111, 13)
        Label51.TabIndex = 2
        Label51.Text = "BARREL DISTORTION"
        ' 
        ' sldBarrelDistortion
        ' 
        sldBarrelDistortion.AccentColor = Color.Gray
        sldBarrelDistortion.BackColor = Color.Transparent
        sldBarrelDistortion.Location = New Point(128, 3)
        sldBarrelDistortion.Margin = New Padding(3, 2, 3, 2)
        sldBarrelDistortion.Maximum = 100
        sldBarrelDistortion.Minimum = 0
        sldBarrelDistortion.Name = "sldBarrelDistortion"
        sldBarrelDistortion.Size = New Size(133, 24)
        sldBarrelDistortion.TabIndex = 0
        sldBarrelDistortion.Tag = "BarrelDistortion"
        sldBarrelDistortion.Text = "BarrelDistortion"
        sldBarrelDistortion.Value = 50
        ' 
        ' Label32
        ' 
        Label32.Font = New Font("SketchFlow Print", 9F, FontStyle.Bold)
        Label32.ForeColor = Color.DimGray
        Label32.Location = New Point(3, 436)
        Label32.Name = "Label32"
        Label32.Size = New Size(294, 20)
        Label32.TabIndex = 34
        Label32.Text = "ALIGNMENT SECTION"
        Label32.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Panel40
        ' 
        Panel40.Controls.Add(btn_UndoRotate)
        Panel40.Controls.Add(btn_StraightenPic)
        Panel40.Location = New Point(3, 459)
        Panel40.Name = "Panel40"
        Panel40.Size = New Size(299, 29)
        Panel40.TabIndex = 33
        ' 
        ' btn_UndoRotate
        ' 
        btn_UndoRotate.BackColor = Color.FromArgb(CByte(30), CByte(30), CByte(35))
        btn_UndoRotate.CornerRadius = 10
        btn_UndoRotate.Enabled = False
        btn_UndoRotate.FlatAppearance.BorderSize = 0
        btn_UndoRotate.FlatStyle = FlatStyle.Flat
        btn_UndoRotate.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btn_UndoRotate.ForeColor = Color.Silver
        btn_UndoRotate.GlowColor = Color.FromArgb(CByte(255), CByte(192), CByte(255))
        btn_UndoRotate.HoverGlowIntensity = 0.65F
        btn_UndoRotate.Location = New Point(155, 4)
        btn_UndoRotate.Name = "btn_UndoRotate"
        btn_UndoRotate.NormalGlowIntensity = 0.35F
        btn_UndoRotate.PenWidth = 2
        btn_UndoRotate.Size = New Size(138, 20)
        btn_UndoRotate.TabIndex = 24
        btn_UndoRotate.TabStop = False
        btn_UndoRotate.Text = "UNDO ALIGN"
        btn_UndoRotate.UseVisualStyleBackColor = False
        ' 
        ' btn_StraightenPic
        ' 
        btn_StraightenPic.BackColor = Color.FromArgb(CByte(30), CByte(30), CByte(35))
        btn_StraightenPic.CornerRadius = 10
        btn_StraightenPic.FlatAppearance.BorderSize = 0
        btn_StraightenPic.FlatStyle = FlatStyle.Flat
        btn_StraightenPic.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btn_StraightenPic.ForeColor = Color.Silver
        btn_StraightenPic.GlowColor = Color.FromArgb(CByte(255), CByte(192), CByte(255))
        btn_StraightenPic.HoverGlowIntensity = 0.65F
        btn_StraightenPic.Location = New Point(7, 4)
        btn_StraightenPic.Name = "btn_StraightenPic"
        btn_StraightenPic.NormalGlowIntensity = 0.35F
        btn_StraightenPic.PenWidth = 2
        btn_StraightenPic.Size = New Size(138, 20)
        btn_StraightenPic.TabIndex = 23
        btn_StraightenPic.TabStop = False
        btn_StraightenPic.Text = "HORIZONT ALIGN"
        btn_StraightenPic.UseVisualStyleBackColor = False
        ' 
        ' Label49
        ' 
        Label49.Font = New Font("SketchFlow Print", 9F, FontStyle.Bold)
        Label49.ForeColor = Color.DimGray
        Label49.Location = New Point(3, 491)
        Label49.Name = "Label49"
        Label49.Size = New Size(294, 20)
        Label49.TabIndex = 26
        Label49.Text = "CROP SECTION"
        Label49.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' btn_Crop
        ' 
        btn_Crop.BackColor = Color.FromArgb(CByte(30), CByte(30), CByte(35))
        btn_Crop.CornerRadius = 10
        btn_Crop.FlatAppearance.BorderSize = 0
        btn_Crop.FlatStyle = FlatStyle.Flat
        btn_Crop.Font = New Font("Segoe UI", 9F)
        btn_Crop.ForeColor = Color.Silver
        btn_Crop.GlowColor = Color.FromArgb(CByte(255), CByte(192), CByte(255))
        btn_Crop.HoverGlowIntensity = 0.65F
        btn_Crop.Location = New Point(3, 514)
        btn_Crop.Name = "btn_Crop"
        btn_Crop.NormalGlowIntensity = 0.35F
        btn_Crop.PenWidth = 2
        btn_Crop.Size = New Size(145, 21)
        btn_Crop.TabIndex = 22
        btn_Crop.TabStop = False
        btn_Crop.Text = "CROP"
        btn_Crop.UseVisualStyleBackColor = False
        ' 
        ' btn_UndoCrop
        ' 
        btn_UndoCrop.BackColor = Color.FromArgb(CByte(30), CByte(30), CByte(35))
        btn_UndoCrop.CornerRadius = 10
        btn_UndoCrop.FlatAppearance.BorderSize = 0
        btn_UndoCrop.FlatStyle = FlatStyle.Flat
        btn_UndoCrop.Font = New Font("Segoe UI", 9F)
        btn_UndoCrop.ForeColor = Color.Silver
        btn_UndoCrop.GlowColor = Color.FromArgb(CByte(255), CByte(192), CByte(255))
        btn_UndoCrop.HoverGlowIntensity = 0.65F
        btn_UndoCrop.Location = New Point(154, 514)
        btn_UndoCrop.Name = "btn_UndoCrop"
        btn_UndoCrop.NormalGlowIntensity = 0.35F
        btn_UndoCrop.PenWidth = 2
        btn_UndoCrop.Size = New Size(145, 21)
        btn_UndoCrop.TabIndex = 23
        btn_UndoCrop.TabStop = False
        btn_UndoCrop.Text = "UNDO"
        btn_UndoCrop.UseVisualStyleBackColor = False
        ' 
        ' Label50
        ' 
        Label50.Font = New Font("SketchFlow Print", 9F, FontStyle.Bold)
        Label50.ForeColor = Color.DimGray
        Label50.Location = New Point(3, 538)
        Label50.Name = "Label50"
        Label50.Size = New Size(294, 20)
        Label50.TabIndex = 27
        Label50.Text = "CORRECTION SECTION"
        Label50.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' btn_AutoCorrect
        ' 
        btn_AutoCorrect.BackColor = Color.FromArgb(CByte(30), CByte(30), CByte(35))
        btn_AutoCorrect.CornerRadius = 10
        btn_AutoCorrect.FlatAppearance.BorderSize = 0
        btn_AutoCorrect.FlatStyle = FlatStyle.Flat
        btn_AutoCorrect.Font = New Font("Segoe UI", 9F)
        btn_AutoCorrect.ForeColor = Color.Silver
        btn_AutoCorrect.GlowColor = Color.FromArgb(CByte(255), CByte(192), CByte(255))
        btn_AutoCorrect.HoverGlowIntensity = 0.65F
        btn_AutoCorrect.Location = New Point(3, 561)
        btn_AutoCorrect.Name = "btn_AutoCorrect"
        btn_AutoCorrect.NormalGlowIntensity = 0.35F
        btn_AutoCorrect.PenWidth = 2
        btn_AutoCorrect.Size = New Size(145, 21)
        btn_AutoCorrect.TabIndex = 21
        btn_AutoCorrect.TabStop = False
        btn_AutoCorrect.Text = "AUTO CORRECT"
        btn_AutoCorrect.UseVisualStyleBackColor = False
        ' 
        ' BtnResetCorrectionSliders
        ' 
        BtnResetCorrectionSliders.BackColor = Color.FromArgb(CByte(30), CByte(30), CByte(35))
        BtnResetCorrectionSliders.CornerRadius = 10
        BtnResetCorrectionSliders.FlatAppearance.BorderSize = 0
        BtnResetCorrectionSliders.FlatStyle = FlatStyle.Flat
        BtnResetCorrectionSliders.Font = New Font("Segoe UI", 9F)
        BtnResetCorrectionSliders.ForeColor = Color.Silver
        BtnResetCorrectionSliders.GlowColor = Color.FromArgb(CByte(255), CByte(192), CByte(255))
        BtnResetCorrectionSliders.HoverGlowIntensity = 0.65F
        BtnResetCorrectionSliders.Location = New Point(154, 561)
        BtnResetCorrectionSliders.Name = "BtnResetCorrectionSliders"
        BtnResetCorrectionSliders.NormalGlowIntensity = 0.35F
        BtnResetCorrectionSliders.PenWidth = 2
        BtnResetCorrectionSliders.Size = New Size(145, 21)
        BtnResetCorrectionSliders.TabIndex = 28
        BtnResetCorrectionSliders.TabStop = False
        BtnResetCorrectionSliders.Text = "RESET ALL"
        BtnResetCorrectionSliders.UseVisualStyleBackColor = False
        ' 
        ' Label31
        ' 
        Label31.Font = New Font("SketchFlow Print", 9F, FontStyle.Bold)
        Label31.ForeColor = Color.DimGray
        Label31.Location = New Point(3, 585)
        Label31.Name = "Label31"
        Label31.Size = New Size(294, 20)
        Label31.TabIndex = 29
        Label31.Text = "PICTURE STATUS"
        Label31.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' btn_Remove_Changes
        ' 
        btn_Remove_Changes.BackColor = Color.FromArgb(CByte(30), CByte(30), CByte(35))
        btn_Remove_Changes.CornerRadius = 10
        btn_Remove_Changes.FlatAppearance.BorderSize = 0
        btn_Remove_Changes.FlatAppearance.MouseDownBackColor = Color.Transparent
        btn_Remove_Changes.FlatAppearance.MouseOverBackColor = Color.Transparent
        btn_Remove_Changes.FlatStyle = FlatStyle.Flat
        btn_Remove_Changes.Font = New Font("Segoe UI", 9F)
        btn_Remove_Changes.ForeColor = Color.Silver
        btn_Remove_Changes.GlowColor = Color.FromArgb(CByte(255), CByte(192), CByte(255))
        btn_Remove_Changes.HoverGlowIntensity = 0.65F
        btn_Remove_Changes.Location = New Point(3, 608)
        btn_Remove_Changes.Name = "btn_Remove_Changes"
        btn_Remove_Changes.NormalGlowIntensity = 0.35F
        btn_Remove_Changes.PenWidth = 2
        btn_Remove_Changes.Size = New Size(296, 21)
        btn_Remove_Changes.TabIndex = 25
        btn_Remove_Changes.TabStop = False
        btn_Remove_Changes.Text = "NO INFO"
        btn_Remove_Changes.UseVisualStyleBackColor = False
        ' 
        ' lblOwner
        ' 
        lblOwner.AutoSize = True
        lblOwner.BackColor = Color.Transparent
        lblOwner.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblOwner.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblOwner.Location = New Point(75, 653)
        lblOwner.Name = "lblOwner"
        lblOwner.Size = New Size(200, 13)
        lblOwner.TabIndex = 1
        lblOwner.Text = "F-Stop V1.0 @ P. Remus Adrian - 2026"
        ' 
        ' TabPage2
        ' 
        TabPage2.BackColor = Color.FromArgb(CByte(32), CByte(32), CByte(32))
        TabPage2.Controls.Add(lblHelpEnhancement)
        TabPage2.Controls.Add(btn_ResetEnchentment)
        TabPage2.Controls.Add(sld_FNT_Size)
        TabPage2.Controls.Add(lblTextOpacityValue)
        TabPage2.Controls.Add(sldTextOpacity)
        TabPage2.Controls.Add(btnSelectColor)
        TabPage2.Controls.Add(numFontSize)
        TabPage2.Controls.Add(lblFontName)
        TabPage2.Controls.Add(btnFont)
        TabPage2.Controls.Add(txtWatermark)
        TabPage2.Controls.Add(Panel23)
        TabPage2.Controls.Add(FlowLayoutPanel1)
        TabPage2.Location = New Point(4, 34)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(346, 668)
        TabPage2.TabIndex = 1
        TabPage2.Text = "ENHANCEMENT"
        ' 
        ' lblHelpEnhancement
        ' 
        lblHelpEnhancement.AutoSize = True
        lblHelpEnhancement.Cursor = Cursors.Hand
        lblHelpEnhancement.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblHelpEnhancement.ForeColor = Color.Silver
        lblHelpEnhancement.Location = New Point(297, 636)
        lblHelpEnhancement.Name = "lblHelpEnhancement"
        lblHelpEnhancement.Size = New Size(20, 25)
        lblHelpEnhancement.TabIndex = 43
        lblHelpEnhancement.Text = "?"
        ' 
        ' btn_ResetEnchentment
        ' 
        btn_ResetEnchentment.BackColor = Color.FromArgb(CByte(30), CByte(30), CByte(35))
        btn_ResetEnchentment.CornerRadius = 10
        btn_ResetEnchentment.FlatAppearance.BorderSize = 0
        btn_ResetEnchentment.FlatStyle = FlatStyle.Flat
        btn_ResetEnchentment.Font = New Font("Segoe UI", 9F)
        btn_ResetEnchentment.ForeColor = Color.Silver
        btn_ResetEnchentment.GlowColor = Color.FromArgb(CByte(255), CByte(192), CByte(255))
        btn_ResetEnchentment.HoverGlowIntensity = 0.65F
        btn_ResetEnchentment.Location = New Point(89, 639)
        btn_ResetEnchentment.Name = "btn_ResetEnchentment"
        btn_ResetEnchentment.NormalGlowIntensity = 0.35F
        btn_ResetEnchentment.PenWidth = 2
        btn_ResetEnchentment.Size = New Size(173, 25)
        btn_ResetEnchentment.TabIndex = 24
        btn_ResetEnchentment.TabStop = False
        btn_ResetEnchentment.Text = "RESET ENHANCEMENT"
        btn_ResetEnchentment.UseVisualStyleBackColor = False
        ' 
        ' sld_FNT_Size
        ' 
        sld_FNT_Size.Location = New Point(173, 582)
        sld_FNT_Size.Maximum = 100
        sld_FNT_Size.Minimum = 5
        sld_FNT_Size.Name = "sld_FNT_Size"
        sld_FNT_Size.RailColor = Color.FromArgb(CByte(70), CByte(70), CByte(70))
        sld_FNT_Size.Size = New Size(20, 40)
        sld_FNT_Size.TabIndex = 23
        sld_FNT_Size.Text = "ModernVerticalSlider1"
        sld_FNT_Size.ThumbBorderColor = Color.Black
        sld_FNT_Size.ThumbColor = Color.White
        sld_FNT_Size.Value = 24
        ' 
        ' lblTextOpacityValue
        ' 
        lblTextOpacityValue.AutoSize = True
        lblTextOpacityValue.ForeColor = Color.Silver
        lblTextOpacityValue.Location = New Point(230, 586)
        lblTextOpacityValue.Name = "lblTextOpacityValue"
        lblTextOpacityValue.Size = New Size(22, 13)
        lblTextOpacityValue.TabIndex = 22
        lblTextOpacityValue.Text = "0%"
        ' 
        ' sldTextOpacity
        ' 
        sldTextOpacity.AccentColor = Color.FromArgb(CByte(255), CByte(192), CByte(128))
        sldTextOpacity.BackColor = Color.Transparent
        sldTextOpacity.Location = New Point(206, 595)
        sldTextOpacity.Maximum = 100
        sldTextOpacity.Minimum = 0
        sldTextOpacity.Name = "sldTextOpacity"
        sldTextOpacity.Size = New Size(75, 23)
        sldTextOpacity.TabIndex = 21
        sldTextOpacity.Text = "ModernSlider1"
        sldTextOpacity.Value = 0
        ' 
        ' btnSelectColor
        ' 
        btnSelectColor.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        btnSelectColor.BackgroundImage = CType(resources.GetObject("btnSelectColor.BackgroundImage"), Image)
        btnSelectColor.BackgroundImageLayout = ImageLayout.Zoom
        btnSelectColor.FlatAppearance.BorderColor = Color.FromArgb(CByte(164), CByte(244), CByte(159))
        btnSelectColor.FlatStyle = FlatStyle.Flat
        btnSelectColor.Location = New Point(292, 591)
        btnSelectColor.Name = "btnSelectColor"
        btnSelectColor.Size = New Size(31, 22)
        btnSelectColor.TabIndex = 20
        btnSelectColor.UseVisualStyleBackColor = False
        ' 
        ' numFontSize
        ' 
        numFontSize.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        numFontSize.ForeColor = Color.White
        numFontSize.Location = New Point(133, 592)
        numFontSize.Name = "numFontSize"
        numFontSize.Size = New Size(39, 22)
        numFontSize.TabIndex = 19
        numFontSize.Value = New Decimal(New Integer() {24, 0, 0, 0})
        ' 
        ' lblFontName
        ' 
        lblFontName.Location = New Point(62, 597)
        lblFontName.Name = "lblFontName"
        lblFontName.Size = New Size(65, 13)
        lblFontName.TabIndex = 18
        lblFontName.Text = "Arial"
        ' 
        ' btnFont
        ' 
        btnFont.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        btnFont.BackgroundImage = CType(resources.GetObject("btnFont.BackgroundImage"), Image)
        btnFont.BackgroundImageLayout = ImageLayout.Zoom
        btnFont.FlatAppearance.BorderColor = Color.FromArgb(CByte(164), CByte(244), CByte(159))
        btnFont.FlatStyle = FlatStyle.Flat
        btnFont.Location = New Point(28, 592)
        btnFont.Name = "btnFont"
        btnFont.Size = New Size(31, 22)
        btnFont.TabIndex = 17
        btnFont.UseVisualStyleBackColor = False
        ' 
        ' txtWatermark
        ' 
        txtWatermark.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        txtWatermark.BorderStyle = BorderStyle.FixedSingle
        txtWatermark.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtWatermark.ForeColor = Color.White
        txtWatermark.Location = New Point(27, 558)
        txtWatermark.Name = "txtWatermark"
        txtWatermark.Size = New Size(296, 23)
        txtWatermark.TabIndex = 15
        ' 
        ' Panel23
        ' 
        Panel23.BackColor = Color.FromArgb(CByte(40), CByte(40), CByte(40))
        Panel23.Location = New Point(1, 629)
        Panel23.Name = "Panel23"
        Panel23.Size = New Size(345, 3)
        Panel23.TabIndex = 13
        ' 
        ' FlowLayoutPanel1
        ' 
        FlowLayoutPanel1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        FlowLayoutPanel1.BackColor = Color.Transparent
        FlowLayoutPanel1.Controls.Add(Panel12)
        FlowLayoutPanel1.Controls.Add(Panel14)
        FlowLayoutPanel1.Controls.Add(Panel15)
        FlowLayoutPanel1.Controls.Add(Panel16)
        FlowLayoutPanel1.Controls.Add(Panel17)
        FlowLayoutPanel1.Controls.Add(Panel42)
        FlowLayoutPanel1.Controls.Add(Panel43)
        FlowLayoutPanel1.Controls.Add(Panel44)
        FlowLayoutPanel1.Controls.Add(Panel36)
        FlowLayoutPanel1.Controls.Add(Panel13)
        FlowLayoutPanel1.Controls.Add(Panel18)
        FlowLayoutPanel1.Controls.Add(Panel19)
        FlowLayoutPanel1.Controls.Add(Panel20)
        FlowLayoutPanel1.Controls.Add(Panel21)
        FlowLayoutPanel1.Controls.Add(Panel22)
        FlowLayoutPanel1.Controls.Add(Label126)
        FlowLayoutPanel1.Location = New Point(21, 15)
        FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        FlowLayoutPanel1.Size = New Size(311, 608)
        FlowLayoutPanel1.TabIndex = 1
        ' 
        ' Panel12
        ' 
        Panel12.Controls.Add(Label53)
        Panel12.Controls.Add(Label52)
        Panel12.Controls.Add(lbl)
        Panel12.Controls.Add(sldDynCon)
        Panel12.Controls.Add(txt_DynCon)
        Panel12.Location = New Point(3, 3)
        Panel12.Name = "Panel12"
        Panel12.Size = New Size(299, 29)
        Panel12.TabIndex = 3
        ' 
        ' Label53
        ' 
        Label53.AutoSize = True
        Label53.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label53.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label53.Location = New Point(250, 8)
        Label53.Name = "Label53"
        Label53.Size = New Size(13, 11)
        Label53.TabIndex = 45
        Label53.Text = "50"
        ' 
        ' Label52
        ' 
        Label52.AutoSize = True
        Label52.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label52.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label52.Location = New Point(72, 8)
        Label52.Name = "Label52"
        Label52.Size = New Size(9, 11)
        Label52.TabIndex = 44
        Label52.Text = "0"
        ' 
        ' lbl
        ' 
        lbl.AutoSize = True
        lbl.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        lbl.Location = New Point(3, 7)
        lbl.Name = "lbl"
        lbl.Size = New Size(58, 13)
        lbl.TabIndex = 2
        lbl.Tag = "DynCon"
        lbl.Text = "DYN.CON."
        ' 
        ' sldDynCon
        ' 
        sldDynCon.AccentColor = Color.FromArgb(CByte(152), CByte(149), CByte(185))
        sldDynCon.BackColor = Color.Transparent
        sldDynCon.Location = New Point(79, 2)
        sldDynCon.Margin = New Padding(3, 2, 3, 2)
        sldDynCon.Maximum = 100
        sldDynCon.Minimum = 0
        sldDynCon.Name = "sldDynCon"
        sldDynCon.Size = New Size(170, 24)
        sldDynCon.TabIndex = 0
        sldDynCon.Tag = "DynCon"
        sldDynCon.Text = "DynCon"
        sldDynCon.Value = 0
        ' 
        ' txt_DynCon
        ' 
        txt_DynCon.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_DynCon.BorderStyle = BorderStyle.None
        txt_DynCon.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_DynCon.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_DynCon.Location = New Point(263, 7)
        txt_DynCon.Margin = New Padding(3, 2, 3, 2)
        txt_DynCon.Name = "txt_DynCon"
        txt_DynCon.Size = New Size(30, 15)
        txt_DynCon.TabIndex = 1
        txt_DynCon.Text = "0"
        txt_DynCon.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel14
        ' 
        Panel14.Controls.Add(Label55)
        Panel14.Controls.Add(Label54)
        Panel14.Controls.Add(Label11)
        Panel14.Controls.Add(sldStructX)
        Panel14.Controls.Add(txt_StructX)
        Panel14.Location = New Point(3, 38)
        Panel14.Name = "Panel14"
        Panel14.Size = New Size(299, 29)
        Panel14.TabIndex = 5
        ' 
        ' Label55
        ' 
        Label55.AutoSize = True
        Label55.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label55.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label55.Location = New Point(250, 8)
        Label55.Name = "Label55"
        Label55.Size = New Size(13, 11)
        Label55.TabIndex = 45
        Label55.Text = "50"
        ' 
        ' Label54
        ' 
        Label54.AutoSize = True
        Label54.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label54.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label54.Location = New Point(72, 9)
        Label54.Name = "Label54"
        Label54.Size = New Size(9, 11)
        Label54.TabIndex = 45
        Label54.Text = "0"
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label11.Location = New Point(3, 7)
        Label11.Name = "Label11"
        Label11.Size = New Size(66, 13)
        Label11.TabIndex = 2
        Label11.Text = "STRUCTURE"
        ' 
        ' sldStructX
        ' 
        sldStructX.AccentColor = Color.FromArgb(CByte(255), CByte(255), CByte(192))
        sldStructX.BackColor = Color.Transparent
        sldStructX.Location = New Point(79, 2)
        sldStructX.Margin = New Padding(3, 2, 3, 2)
        sldStructX.Maximum = 100
        sldStructX.Minimum = 0
        sldStructX.Name = "sldStructX"
        sldStructX.Size = New Size(170, 24)
        sldStructX.TabIndex = 0
        sldStructX.Tag = "StructX"
        sldStructX.Text = "StructX"
        sldStructX.Value = 0
        ' 
        ' txt_StructX
        ' 
        txt_StructX.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_StructX.BorderStyle = BorderStyle.None
        txt_StructX.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_StructX.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_StructX.Location = New Point(263, 7)
        txt_StructX.Margin = New Padding(3, 2, 3, 2)
        txt_StructX.Name = "txt_StructX"
        txt_StructX.Size = New Size(30, 15)
        txt_StructX.TabIndex = 1
        txt_StructX.Text = "0"
        txt_StructX.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel15
        ' 
        Panel15.Controls.Add(Label57)
        Panel15.Controls.Add(Label56)
        Panel15.Controls.Add(Label12)
        Panel15.Controls.Add(sldSharpen)
        Panel15.Controls.Add(txt_Sharpen)
        Panel15.Location = New Point(3, 73)
        Panel15.Name = "Panel15"
        Panel15.Size = New Size(299, 29)
        Panel15.TabIndex = 6
        ' 
        ' Label57
        ' 
        Label57.AutoSize = True
        Label57.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label57.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label57.Location = New Point(250, 9)
        Label57.Name = "Label57"
        Label57.Size = New Size(13, 11)
        Label57.TabIndex = 47
        Label57.Text = "50"
        ' 
        ' Label56
        ' 
        Label56.AutoSize = True
        Label56.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label56.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label56.Location = New Point(72, 9)
        Label56.Name = "Label56"
        Label56.Size = New Size(9, 11)
        Label56.TabIndex = 46
        Label56.Text = "0"
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label12.Location = New Point(3, 7)
        Label12.Name = "Label12"
        Label12.Size = New Size(55, 13)
        Label12.TabIndex = 2
        Label12.Text = "SHARPEN"
        ' 
        ' sldSharpen
        ' 
        sldSharpen.AccentColor = Color.FromArgb(CByte(255), CByte(255), CByte(192))
        sldSharpen.BackColor = Color.Transparent
        sldSharpen.Location = New Point(79, 2)
        sldSharpen.Margin = New Padding(3, 2, 3, 2)
        sldSharpen.Maximum = 100
        sldSharpen.Minimum = 0
        sldSharpen.Name = "sldSharpen"
        sldSharpen.Size = New Size(170, 24)
        sldSharpen.TabIndex = 0
        sldSharpen.Tag = "Sharpen"
        sldSharpen.Text = "Sharpen"
        sldSharpen.Value = 0
        ' 
        ' txt_Sharpen
        ' 
        txt_Sharpen.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_Sharpen.BorderStyle = BorderStyle.None
        txt_Sharpen.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_Sharpen.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_Sharpen.Location = New Point(263, 7)
        txt_Sharpen.Margin = New Padding(3, 2, 3, 2)
        txt_Sharpen.Name = "txt_Sharpen"
        txt_Sharpen.Size = New Size(30, 15)
        txt_Sharpen.TabIndex = 1
        txt_Sharpen.Text = "0"
        txt_Sharpen.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel16
        ' 
        Panel16.Controls.Add(Label59)
        Panel16.Controls.Add(Label58)
        Panel16.Controls.Add(Label13)
        Panel16.Controls.Add(sldDefringe)
        Panel16.Controls.Add(txt_Defringe)
        Panel16.Location = New Point(3, 108)
        Panel16.Name = "Panel16"
        Panel16.Size = New Size(299, 29)
        Panel16.TabIndex = 7
        ' 
        ' Label59
        ' 
        Label59.AutoSize = True
        Label59.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label59.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label59.Location = New Point(246, 8)
        Label59.Name = "Label59"
        Label59.Size = New Size(17, 11)
        Label59.TabIndex = 48
        Label59.Text = "100"
        ' 
        ' Label58
        ' 
        Label58.AutoSize = True
        Label58.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label58.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label58.Location = New Point(73, 9)
        Label58.Name = "Label58"
        Label58.Size = New Size(9, 11)
        Label58.TabIndex = 47
        Label58.Text = "0"
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label13.Location = New Point(3, 7)
        Label13.Name = "Label13"
        Label13.Size = New Size(59, 13)
        Label13.TabIndex = 2
        Label13.Text = "DEFRINGE"
        ' 
        ' sldDefringe
        ' 
        sldDefringe.AccentColor = Color.FromArgb(CByte(255), CByte(255), CByte(192))
        sldDefringe.BackColor = Color.Transparent
        sldDefringe.Location = New Point(79, 2)
        sldDefringe.Margin = New Padding(3, 2, 3, 2)
        sldDefringe.Maximum = 100
        sldDefringe.Minimum = 0
        sldDefringe.Name = "sldDefringe"
        sldDefringe.Size = New Size(170, 24)
        sldDefringe.TabIndex = 0
        sldDefringe.Tag = "Defringe"
        sldDefringe.Text = "Defringe"
        sldDefringe.Value = 0
        ' 
        ' txt_Defringe
        ' 
        txt_Defringe.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_Defringe.BorderStyle = BorderStyle.None
        txt_Defringe.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_Defringe.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_Defringe.Location = New Point(263, 7)
        txt_Defringe.Margin = New Padding(3, 2, 3, 2)
        txt_Defringe.Name = "txt_Defringe"
        txt_Defringe.Size = New Size(30, 15)
        txt_Defringe.TabIndex = 1
        txt_Defringe.Text = "0"
        txt_Defringe.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel17
        ' 
        Panel17.Controls.Add(Label61)
        Panel17.Controls.Add(Label60)
        Panel17.Controls.Add(Label14)
        Panel17.Controls.Add(sldDenoise)
        Panel17.Controls.Add(txt_Denoise)
        Panel17.Location = New Point(3, 143)
        Panel17.Name = "Panel17"
        Panel17.Size = New Size(299, 29)
        Panel17.TabIndex = 8
        ' 
        ' Label61
        ' 
        Label61.AutoSize = True
        Label61.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label61.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label61.Location = New Point(249, 9)
        Label61.Name = "Label61"
        Label61.Size = New Size(13, 11)
        Label61.TabIndex = 49
        Label61.Text = "50"
        ' 
        ' Label60
        ' 
        Label60.AutoSize = True
        Label60.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label60.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label60.Location = New Point(73, 9)
        Label60.Name = "Label60"
        Label60.Size = New Size(9, 11)
        Label60.TabIndex = 48
        Label60.Text = "0"
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label14.Location = New Point(3, 7)
        Label14.Name = "Label14"
        Label14.Size = New Size(53, 13)
        Label14.TabIndex = 2
        Label14.Text = "DENOISE"
        ' 
        ' sldDenoise
        ' 
        sldDenoise.AccentColor = Color.FromArgb(CByte(255), CByte(192), CByte(128))
        sldDenoise.BackColor = Color.Transparent
        sldDenoise.Location = New Point(79, 2)
        sldDenoise.Margin = New Padding(3, 2, 3, 2)
        sldDenoise.Maximum = 100
        sldDenoise.Minimum = 0
        sldDenoise.Name = "sldDenoise"
        sldDenoise.Size = New Size(174, 24)
        sldDenoise.TabIndex = 0
        sldDenoise.Tag = "Denoise"
        sldDenoise.Text = "Denoise"
        sldDenoise.Value = 0
        ' 
        ' txt_Denoise
        ' 
        txt_Denoise.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_Denoise.BorderStyle = BorderStyle.None
        txt_Denoise.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_Denoise.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_Denoise.Location = New Point(263, 7)
        txt_Denoise.Margin = New Padding(3, 2, 3, 2)
        txt_Denoise.Name = "txt_Denoise"
        txt_Denoise.Size = New Size(30, 15)
        txt_Denoise.TabIndex = 1
        txt_Denoise.Text = "0"
        txt_Denoise.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel42
        ' 
        Panel42.Controls.Add(Label65)
        Panel42.Controls.Add(Label62)
        Panel42.Controls.Add(PictureBox2)
        Panel42.Controls.Add(Label42)
        Panel42.Controls.Add(sldNoiseLimit)
        Panel42.Controls.Add(txt_NoiseLimit)
        Panel42.Location = New Point(3, 178)
        Panel42.Name = "Panel42"
        Panel42.Size = New Size(299, 29)
        Panel42.TabIndex = 16
        ' 
        ' Label65
        ' 
        Label65.AutoSize = True
        Label65.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label65.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label65.Location = New Point(249, 9)
        Label65.Name = "Label65"
        Label65.Size = New Size(13, 11)
        Label65.TabIndex = 50
        Label65.Text = "50"
        ' 
        ' Label62
        ' 
        Label62.AutoSize = True
        Label62.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label62.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label62.Location = New Point(135, 9)
        Label62.Name = "Label62"
        Label62.Size = New Size(9, 11)
        Label62.TabIndex = 49
        Label62.Text = "0"
        ' 
        ' PictureBox2
        ' 
        PictureBox2.BackgroundImage = CType(resources.GetObject("PictureBox2.BackgroundImage"), Image)
        PictureBox2.BackgroundImageLayout = ImageLayout.Zoom
        PictureBox2.Location = New Point(3, 2)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(25, 25)
        PictureBox2.TabIndex = 3
        PictureBox2.TabStop = False
        ' 
        ' Label42
        ' 
        Label42.AutoSize = True
        Label42.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label42.Location = New Point(50, 8)
        Label42.Name = "Label42"
        Label42.Size = New Size(70, 13)
        Label42.TabIndex = 2
        Label42.Text = "DARK NOISE"
        ' 
        ' sldNoiseLimit
        ' 
        sldNoiseLimit.AccentColor = Color.FromArgb(CByte(255), CByte(192), CByte(128))
        sldNoiseLimit.BackColor = Color.Transparent
        sldNoiseLimit.Location = New Point(141, 2)
        sldNoiseLimit.Margin = New Padding(3, 2, 3, 2)
        sldNoiseLimit.Maximum = 100
        sldNoiseLimit.Minimum = 0
        sldNoiseLimit.Name = "sldNoiseLimit"
        sldNoiseLimit.Size = New Size(110, 24)
        sldNoiseLimit.TabIndex = 0
        sldNoiseLimit.Tag = "NoiseLimit"
        sldNoiseLimit.Text = "NoiseLimit"
        sldNoiseLimit.Value = 0
        ' 
        ' txt_NoiseLimit
        ' 
        txt_NoiseLimit.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_NoiseLimit.BorderStyle = BorderStyle.None
        txt_NoiseLimit.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_NoiseLimit.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_NoiseLimit.Location = New Point(263, 7)
        txt_NoiseLimit.Margin = New Padding(3, 2, 3, 2)
        txt_NoiseLimit.Name = "txt_NoiseLimit"
        txt_NoiseLimit.Size = New Size(30, 15)
        txt_NoiseLimit.TabIndex = 1
        txt_NoiseLimit.Text = "0"
        txt_NoiseLimit.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel43
        ' 
        Panel43.Controls.Add(Label66)
        Panel43.Controls.Add(Label63)
        Panel43.Controls.Add(PictureBox3)
        Panel43.Controls.Add(Label43)
        Panel43.Controls.Add(sldNoiseChroma)
        Panel43.Controls.Add(txt_NoiseChroma)
        Panel43.Location = New Point(3, 213)
        Panel43.Name = "Panel43"
        Panel43.Size = New Size(299, 29)
        Panel43.TabIndex = 17
        ' 
        ' Label66
        ' 
        Label66.AutoSize = True
        Label66.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label66.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label66.Location = New Point(249, 9)
        Label66.Name = "Label66"
        Label66.Size = New Size(13, 11)
        Label66.TabIndex = 51
        Label66.Text = "50"
        ' 
        ' Label63
        ' 
        Label63.AutoSize = True
        Label63.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label63.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label63.Location = New Point(135, 9)
        Label63.Name = "Label63"
        Label63.Size = New Size(9, 11)
        Label63.TabIndex = 50
        Label63.Text = "0"
        ' 
        ' PictureBox3
        ' 
        PictureBox3.BackgroundImage = CType(resources.GetObject("PictureBox3.BackgroundImage"), Image)
        PictureBox3.BackgroundImageLayout = ImageLayout.Zoom
        PictureBox3.Location = New Point(3, 2)
        PictureBox3.Name = "PictureBox3"
        PictureBox3.Size = New Size(25, 25)
        PictureBox3.TabIndex = 3
        PictureBox3.TabStop = False
        ' 
        ' Label43
        ' 
        Label43.AutoSize = True
        Label43.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label43.Location = New Point(50, 8)
        Label43.Name = "Label43"
        Label43.Size = New Size(75, 13)
        Label43.TabIndex = 2
        Label43.Text = "WHITE NOISE"
        ' 
        ' sldNoiseChroma
        ' 
        sldNoiseChroma.AccentColor = Color.FromArgb(CByte(255), CByte(192), CByte(128))
        sldNoiseChroma.BackColor = Color.Transparent
        sldNoiseChroma.Location = New Point(141, 2)
        sldNoiseChroma.Margin = New Padding(3, 2, 3, 2)
        sldNoiseChroma.Maximum = 100
        sldNoiseChroma.Minimum = 0
        sldNoiseChroma.Name = "sldNoiseChroma"
        sldNoiseChroma.Size = New Size(110, 24)
        sldNoiseChroma.TabIndex = 0
        sldNoiseChroma.Tag = "NoiseChroma"
        sldNoiseChroma.Text = "NoiseChroma"
        sldNoiseChroma.Value = 0
        ' 
        ' txt_NoiseChroma
        ' 
        txt_NoiseChroma.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_NoiseChroma.BorderStyle = BorderStyle.None
        txt_NoiseChroma.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_NoiseChroma.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_NoiseChroma.Location = New Point(263, 7)
        txt_NoiseChroma.Margin = New Padding(3, 2, 3, 2)
        txt_NoiseChroma.Name = "txt_NoiseChroma"
        txt_NoiseChroma.Size = New Size(30, 15)
        txt_NoiseChroma.TabIndex = 1
        txt_NoiseChroma.Text = "0"
        txt_NoiseChroma.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel44
        ' 
        Panel44.Controls.Add(Label67)
        Panel44.Controls.Add(Label64)
        Panel44.Controls.Add(PictureBox4)
        Panel44.Controls.Add(Label44)
        Panel44.Controls.Add(sldNoiseLuma)
        Panel44.Controls.Add(txt_NoiseLuma)
        Panel44.Location = New Point(3, 248)
        Panel44.Name = "Panel44"
        Panel44.Size = New Size(299, 29)
        Panel44.TabIndex = 18
        ' 
        ' Label67
        ' 
        Label67.AutoSize = True
        Label67.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label67.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label67.Location = New Point(250, 9)
        Label67.Name = "Label67"
        Label67.Size = New Size(13, 11)
        Label67.TabIndex = 52
        Label67.Text = "50"
        ' 
        ' Label64
        ' 
        Label64.AutoSize = True
        Label64.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label64.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label64.Location = New Point(135, 9)
        Label64.Name = "Label64"
        Label64.Size = New Size(9, 11)
        Label64.TabIndex = 50
        Label64.Text = "0"
        ' 
        ' PictureBox4
        ' 
        PictureBox4.BackgroundImage = CType(resources.GetObject("PictureBox4.BackgroundImage"), Image)
        PictureBox4.BackgroundImageLayout = ImageLayout.Zoom
        PictureBox4.Location = New Point(3, 2)
        PictureBox4.Name = "PictureBox4"
        PictureBox4.Size = New Size(25, 25)
        PictureBox4.TabIndex = 3
        PictureBox4.TabStop = False
        ' 
        ' Label44
        ' 
        Label44.AutoSize = True
        Label44.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label44.Location = New Point(50, 8)
        Label44.Name = "Label44"
        Label44.Size = New Size(60, 13)
        Label44.TabIndex = 2
        Label44.Text = "STRENGHT"
        ' 
        ' sldNoiseLuma
        ' 
        sldNoiseLuma.AccentColor = Color.FromArgb(CByte(255), CByte(192), CByte(128))
        sldNoiseLuma.BackColor = Color.Transparent
        sldNoiseLuma.Location = New Point(141, 2)
        sldNoiseLuma.Margin = New Padding(3, 2, 3, 2)
        sldNoiseLuma.Maximum = 100
        sldNoiseLuma.Minimum = 0
        sldNoiseLuma.Name = "sldNoiseLuma"
        sldNoiseLuma.Size = New Size(110, 24)
        sldNoiseLuma.TabIndex = 0
        sldNoiseLuma.Tag = "NoiseLuma"
        sldNoiseLuma.Text = "NoiseLuma"
        sldNoiseLuma.Value = 0
        ' 
        ' txt_NoiseLuma
        ' 
        txt_NoiseLuma.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_NoiseLuma.BorderStyle = BorderStyle.None
        txt_NoiseLuma.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_NoiseLuma.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_NoiseLuma.Location = New Point(263, 7)
        txt_NoiseLuma.Margin = New Padding(3, 2, 3, 2)
        txt_NoiseLuma.Name = "txt_NoiseLuma"
        txt_NoiseLuma.Size = New Size(30, 15)
        txt_NoiseLuma.TabIndex = 1
        txt_NoiseLuma.Text = "0"
        txt_NoiseLuma.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel36
        ' 
        Panel36.Controls.Add(Label69)
        Panel36.Controls.Add(Label68)
        Panel36.Controls.Add(Label30)
        Panel36.Controls.Add(sldTextureX)
        Panel36.Controls.Add(txt_TextureX)
        Panel36.Location = New Point(3, 283)
        Panel36.Name = "Panel36"
        Panel36.Size = New Size(299, 29)
        Panel36.TabIndex = 14
        ' 
        ' Label69
        ' 
        Label69.AutoSize = True
        Label69.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label69.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label69.Location = New Point(250, 9)
        Label69.Name = "Label69"
        Label69.Size = New Size(13, 11)
        Label69.TabIndex = 53
        Label69.Text = "50"
        ' 
        ' Label68
        ' 
        Label68.AutoSize = True
        Label68.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label68.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label68.Location = New Point(72, 9)
        Label68.Name = "Label68"
        Label68.Size = New Size(9, 11)
        Label68.TabIndex = 51
        Label68.Text = "0"
        ' 
        ' Label30
        ' 
        Label30.AutoSize = True
        Label30.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label30.Location = New Point(3, 7)
        Label30.Name = "Label30"
        Label30.Size = New Size(50, 13)
        Label30.TabIndex = 2
        Label30.Text = "TEXTURE"
        ' 
        ' sldTextureX
        ' 
        sldTextureX.AccentColor = Color.FromArgb(CByte(255), CByte(255), CByte(192))
        sldTextureX.BackColor = Color.Transparent
        sldTextureX.Location = New Point(79, 2)
        sldTextureX.Margin = New Padding(3, 2, 3, 2)
        sldTextureX.Maximum = 100
        sldTextureX.Minimum = 0
        sldTextureX.Name = "sldTextureX"
        sldTextureX.Size = New Size(170, 24)
        sldTextureX.TabIndex = 0
        sldTextureX.Tag = "TextureX"
        sldTextureX.Text = "TextureX"
        sldTextureX.Value = 0
        ' 
        ' txt_TextureX
        ' 
        txt_TextureX.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_TextureX.BorderStyle = BorderStyle.None
        txt_TextureX.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_TextureX.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_TextureX.Location = New Point(263, 7)
        txt_TextureX.Margin = New Padding(3, 2, 3, 2)
        txt_TextureX.Name = "txt_TextureX"
        txt_TextureX.Size = New Size(30, 15)
        txt_TextureX.TabIndex = 1
        txt_TextureX.Text = "0"
        txt_TextureX.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel13
        ' 
        Panel13.Controls.Add(Label71)
        Panel13.Controls.Add(Label70)
        Panel13.Controls.Add(Label10)
        Panel13.Controls.Add(sldClarity)
        Panel13.Controls.Add(txt_Clarity)
        Panel13.Location = New Point(3, 318)
        Panel13.Name = "Panel13"
        Panel13.Size = New Size(299, 29)
        Panel13.TabIndex = 4
        ' 
        ' Label71
        ' 
        Label71.AutoSize = True
        Label71.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label71.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label71.Location = New Point(246, 9)
        Label71.Name = "Label71"
        Label71.Size = New Size(17, 11)
        Label71.TabIndex = 53
        Label71.Text = "100"
        ' 
        ' Label70
        ' 
        Label70.AutoSize = True
        Label70.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label70.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label70.Location = New Point(64, 10)
        Label70.Name = "Label70"
        Label70.Size = New Size(20, 11)
        Label70.TabIndex = 52
        Label70.Text = "-100"
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label10.Location = New Point(3, 7)
        Label10.Name = "Label10"
        Label10.Size = New Size(46, 13)
        Label10.TabIndex = 2
        Label10.Text = "CLARITY"
        ' 
        ' sldClarity
        ' 
        sldClarity.AccentColor = Color.FromArgb(CByte(255), CByte(255), CByte(192))
        sldClarity.BackColor = Color.Transparent
        sldClarity.Location = New Point(79, 2)
        sldClarity.Margin = New Padding(3, 2, 3, 2)
        sldClarity.Maximum = 100
        sldClarity.Minimum = 0
        sldClarity.Name = "sldClarity"
        sldClarity.Size = New Size(170, 24)
        sldClarity.TabIndex = 0
        sldClarity.Tag = "Clarity"
        sldClarity.Text = "Clarity"
        sldClarity.Value = 50
        ' 
        ' txt_Clarity
        ' 
        txt_Clarity.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_Clarity.BorderStyle = BorderStyle.None
        txt_Clarity.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_Clarity.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_Clarity.Location = New Point(263, 7)
        txt_Clarity.Margin = New Padding(3, 2, 3, 2)
        txt_Clarity.Name = "txt_Clarity"
        txt_Clarity.Size = New Size(30, 15)
        txt_Clarity.TabIndex = 1
        txt_Clarity.Text = "0"
        txt_Clarity.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel18
        ' 
        Panel18.Controls.Add(Label73)
        Panel18.Controls.Add(Label72)
        Panel18.Controls.Add(Label15)
        Panel18.Controls.Add(sldSaturation)
        Panel18.Controls.Add(txt_Saturation)
        Panel18.Location = New Point(3, 353)
        Panel18.Name = "Panel18"
        Panel18.Size = New Size(299, 29)
        Panel18.TabIndex = 9
        ' 
        ' Label73
        ' 
        Label73.AutoSize = True
        Label73.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label73.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label73.Location = New Point(246, 9)
        Label73.Name = "Label73"
        Label73.Size = New Size(17, 11)
        Label73.TabIndex = 54
        Label73.Text = "100"
        ' 
        ' Label72
        ' 
        Label72.AutoSize = True
        Label72.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label72.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label72.Location = New Point(75, 9)
        Label72.Name = "Label72"
        Label72.Size = New Size(9, 11)
        Label72.TabIndex = 52
        Label72.Text = "0"
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label15.Location = New Point(3, 7)
        Label15.Name = "Label15"
        Label15.Size = New Size(70, 13)
        Label15.TabIndex = 2
        Label15.Text = "SATURATION"
        ' 
        ' sldSaturation
        ' 
        sldSaturation.AccentColor = Color.FromArgb(CByte(255), CByte(255), CByte(192))
        sldSaturation.BackColor = Color.Transparent
        sldSaturation.Location = New Point(79, 2)
        sldSaturation.Margin = New Padding(3, 2, 3, 2)
        sldSaturation.Maximum = 100
        sldSaturation.Minimum = 0
        sldSaturation.Name = "sldSaturation"
        sldSaturation.Size = New Size(170, 24)
        sldSaturation.TabIndex = 0
        sldSaturation.Tag = "Saturation"
        sldSaturation.Text = "Saturation"
        sldSaturation.Value = 50
        ' 
        ' txt_Saturation
        ' 
        txt_Saturation.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_Saturation.BorderStyle = BorderStyle.None
        txt_Saturation.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_Saturation.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_Saturation.Location = New Point(263, 7)
        txt_Saturation.Margin = New Padding(3, 2, 3, 2)
        txt_Saturation.Name = "txt_Saturation"
        txt_Saturation.Size = New Size(30, 15)
        txt_Saturation.TabIndex = 1
        txt_Saturation.Text = "50"
        txt_Saturation.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel19
        ' 
        Panel19.Controls.Add(Label75)
        Panel19.Controls.Add(Label74)
        Panel19.Controls.Add(Label16)
        Panel19.Controls.Add(sldVibrance)
        Panel19.Controls.Add(txt_Vibrance)
        Panel19.Location = New Point(3, 388)
        Panel19.Name = "Panel19"
        Panel19.Size = New Size(299, 29)
        Panel19.TabIndex = 10
        ' 
        ' Label75
        ' 
        Label75.AutoSize = True
        Label75.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label75.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label75.Location = New Point(246, 9)
        Label75.Name = "Label75"
        Label75.Size = New Size(17, 11)
        Label75.TabIndex = 55
        Label75.Text = "100"
        ' 
        ' Label74
        ' 
        Label74.AutoSize = True
        Label74.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label74.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label74.Location = New Point(75, 9)
        Label74.Name = "Label74"
        Label74.Size = New Size(9, 11)
        Label74.TabIndex = 53
        Label74.Text = "0"
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label16.Location = New Point(3, 7)
        Label16.Name = "Label16"
        Label16.Size = New Size(59, 13)
        Label16.TabIndex = 2
        Label16.Text = "VIBRANCE"
        ' 
        ' sldVibrance
        ' 
        sldVibrance.AccentColor = Color.FromArgb(CByte(255), CByte(255), CByte(192))
        sldVibrance.BackColor = Color.Transparent
        sldVibrance.Location = New Point(79, 2)
        sldVibrance.Margin = New Padding(3, 2, 3, 2)
        sldVibrance.Maximum = 100
        sldVibrance.Minimum = 0
        sldVibrance.Name = "sldVibrance"
        sldVibrance.Size = New Size(170, 24)
        sldVibrance.TabIndex = 0
        sldVibrance.Tag = "Vibrance"
        sldVibrance.Text = "Vibrance"
        sldVibrance.Value = 50
        ' 
        ' txt_Vibrance
        ' 
        txt_Vibrance.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_Vibrance.BorderStyle = BorderStyle.None
        txt_Vibrance.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_Vibrance.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_Vibrance.Location = New Point(263, 7)
        txt_Vibrance.Margin = New Padding(3, 2, 3, 2)
        txt_Vibrance.Name = "txt_Vibrance"
        txt_Vibrance.Size = New Size(30, 15)
        txt_Vibrance.TabIndex = 1
        txt_Vibrance.Text = "50"
        txt_Vibrance.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel20
        ' 
        Panel20.Controls.Add(Label77)
        Panel20.Controls.Add(Label76)
        Panel20.Controls.Add(Label9)
        Panel20.Controls.Add(sldRedMix)
        Panel20.Controls.Add(txt_RedMix)
        Panel20.Location = New Point(3, 423)
        Panel20.Name = "Panel20"
        Panel20.Size = New Size(299, 29)
        Panel20.TabIndex = 11
        ' 
        ' Label77
        ' 
        Label77.AutoSize = True
        Label77.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label77.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label77.Location = New Point(246, 9)
        Label77.Name = "Label77"
        Label77.Size = New Size(17, 11)
        Label77.TabIndex = 56
        Label77.Text = "100"
        ' 
        ' Label76
        ' 
        Label76.AutoSize = True
        Label76.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label76.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label76.Location = New Point(75, 9)
        Label76.Name = "Label76"
        Label76.Size = New Size(9, 11)
        Label76.TabIndex = 54
        Label76.Text = "0"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label9.Location = New Point(3, 7)
        Label9.Name = "Label9"
        Label9.Size = New Size(50, 13)
        Label9.TabIndex = 2
        Label9.Text = "RED MIX"
        ' 
        ' sldRedMix
        ' 
        sldRedMix.AccentColor = Color.FromArgb(CByte(255), CByte(128), CByte(128))
        sldRedMix.BackColor = Color.Transparent
        sldRedMix.Location = New Point(79, 2)
        sldRedMix.Margin = New Padding(3, 2, 3, 2)
        sldRedMix.Maximum = 100
        sldRedMix.Minimum = 0
        sldRedMix.Name = "sldRedMix"
        sldRedMix.Size = New Size(170, 24)
        sldRedMix.TabIndex = 0
        sldRedMix.Tag = "RedMix"
        sldRedMix.Text = "RedMix"
        sldRedMix.Value = 50
        ' 
        ' txt_RedMix
        ' 
        txt_RedMix.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_RedMix.BorderStyle = BorderStyle.None
        txt_RedMix.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_RedMix.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_RedMix.Location = New Point(263, 7)
        txt_RedMix.Margin = New Padding(3, 2, 3, 2)
        txt_RedMix.Name = "txt_RedMix"
        txt_RedMix.Size = New Size(30, 15)
        txt_RedMix.TabIndex = 1
        txt_RedMix.Text = "50"
        txt_RedMix.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel21
        ' 
        Panel21.Controls.Add(Label79)
        Panel21.Controls.Add(Label78)
        Panel21.Controls.Add(Label17)
        Panel21.Controls.Add(sldGreenMix)
        Panel21.Controls.Add(txt_GreenMix)
        Panel21.Location = New Point(3, 458)
        Panel21.Name = "Panel21"
        Panel21.Size = New Size(299, 29)
        Panel21.TabIndex = 12
        ' 
        ' Label79
        ' 
        Label79.AutoSize = True
        Label79.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label79.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label79.Location = New Point(246, 9)
        Label79.Name = "Label79"
        Label79.Size = New Size(17, 11)
        Label79.TabIndex = 57
        Label79.Text = "100"
        ' 
        ' Label78
        ' 
        Label78.AutoSize = True
        Label78.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label78.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label78.Location = New Point(75, 9)
        Label78.Name = "Label78"
        Label78.Size = New Size(9, 11)
        Label78.TabIndex = 55
        Label78.Text = "0"
        ' 
        ' Label17
        ' 
        Label17.AutoSize = True
        Label17.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label17.Location = New Point(3, 7)
        Label17.Name = "Label17"
        Label17.Size = New Size(64, 13)
        Label17.TabIndex = 2
        Label17.Text = "GREEN MIX"
        ' 
        ' sldGreenMix
        ' 
        sldGreenMix.AccentColor = Color.FromArgb(CByte(128), CByte(255), CByte(128))
        sldGreenMix.BackColor = Color.Transparent
        sldGreenMix.Location = New Point(79, 2)
        sldGreenMix.Margin = New Padding(3, 2, 3, 2)
        sldGreenMix.Maximum = 100
        sldGreenMix.Minimum = 0
        sldGreenMix.Name = "sldGreenMix"
        sldGreenMix.Size = New Size(170, 24)
        sldGreenMix.TabIndex = 0
        sldGreenMix.Tag = "GreenMix"
        sldGreenMix.Text = "GreenMix"
        sldGreenMix.Value = 50
        ' 
        ' txt_GreenMix
        ' 
        txt_GreenMix.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_GreenMix.BorderStyle = BorderStyle.None
        txt_GreenMix.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_GreenMix.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_GreenMix.Location = New Point(263, 7)
        txt_GreenMix.Margin = New Padding(3, 2, 3, 2)
        txt_GreenMix.Name = "txt_GreenMix"
        txt_GreenMix.Size = New Size(30, 15)
        txt_GreenMix.TabIndex = 1
        txt_GreenMix.Text = "50"
        txt_GreenMix.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel22
        ' 
        Panel22.Controls.Add(Label81)
        Panel22.Controls.Add(Label80)
        Panel22.Controls.Add(Label18)
        Panel22.Controls.Add(sldBlueMix)
        Panel22.Controls.Add(txt_BlueMix)
        Panel22.Location = New Point(3, 493)
        Panel22.Name = "Panel22"
        Panel22.Size = New Size(299, 29)
        Panel22.TabIndex = 13
        ' 
        ' Label81
        ' 
        Label81.AutoSize = True
        Label81.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label81.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label81.Location = New Point(246, 9)
        Label81.Name = "Label81"
        Label81.Size = New Size(17, 11)
        Label81.TabIndex = 58
        Label81.Text = "100"
        ' 
        ' Label80
        ' 
        Label80.AutoSize = True
        Label80.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label80.ForeColor = Color.FromArgb(CByte(143), CByte(147), CByte(104))
        Label80.Location = New Point(75, 9)
        Label80.Name = "Label80"
        Label80.Size = New Size(9, 11)
        Label80.TabIndex = 56
        Label80.Text = "0"
        ' 
        ' Label18
        ' 
        Label18.AutoSize = True
        Label18.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label18.Location = New Point(3, 7)
        Label18.Name = "Label18"
        Label18.Size = New Size(55, 13)
        Label18.TabIndex = 2
        Label18.Text = "BLUE MIX"
        ' 
        ' sldBlueMix
        ' 
        sldBlueMix.AccentColor = Color.FromArgb(CByte(128), CByte(255), CByte(255))
        sldBlueMix.BackColor = Color.Transparent
        sldBlueMix.Location = New Point(79, 2)
        sldBlueMix.Margin = New Padding(3, 2, 3, 2)
        sldBlueMix.Maximum = 100
        sldBlueMix.Minimum = 0
        sldBlueMix.Name = "sldBlueMix"
        sldBlueMix.Size = New Size(170, 24)
        sldBlueMix.TabIndex = 0
        sldBlueMix.Tag = "BlueMix"
        sldBlueMix.Text = "BlueMix"
        sldBlueMix.Value = 50
        ' 
        ' txt_BlueMix
        ' 
        txt_BlueMix.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_BlueMix.BorderStyle = BorderStyle.None
        txt_BlueMix.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_BlueMix.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_BlueMix.Location = New Point(263, 7)
        txt_BlueMix.Margin = New Padding(3, 2, 3, 2)
        txt_BlueMix.Name = "txt_BlueMix"
        txt_BlueMix.Size = New Size(30, 15)
        txt_BlueMix.TabIndex = 1
        txt_BlueMix.Text = "50"
        txt_BlueMix.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label126
        ' 
        Label126.Font = New Font("SketchFlow Print", 9F, FontStyle.Bold)
        Label126.ForeColor = Color.DimGray
        Label126.Location = New Point(3, 525)
        Label126.Name = "Label126"
        Label126.Size = New Size(299, 19)
        Label126.TabIndex = 30
        Label126.Text = "TEXT TO PICTURE"
        Label126.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' TabPage3
        ' 
        TabPage3.BackColor = Color.FromArgb(CByte(32), CByte(32), CByte(32))
        TabPage3.Controls.Add(btn_LoadLut)
        TabPage3.Controls.Add(btn_ResetStylization)
        TabPage3.Controls.Add(lblLUThelp)
        TabPage3.Controls.Add(Panel35)
        TabPage3.Controls.Add(lblLutName)
        TabPage3.Controls.Add(cmbLut)
        TabPage3.Controls.Add(FlowLayoutPanel2)
        TabPage3.Location = New Point(4, 34)
        TabPage3.Name = "TabPage3"
        TabPage3.Size = New Size(346, 668)
        TabPage3.TabIndex = 2
        TabPage3.Text = "STYLIZATION"
        ' 
        ' btn_ResetStylization
        ' 
        btn_ResetStylization.BackColor = Color.FromArgb(CByte(30), CByte(30), CByte(35))
        btn_ResetStylization.CornerRadius = 10
        btn_ResetStylization.FlatAppearance.BorderSize = 0
        btn_ResetStylization.FlatStyle = FlatStyle.Flat
        btn_ResetStylization.Font = New Font("Segoe UI", 9F)
        btn_ResetStylization.ForeColor = Color.Silver
        btn_ResetStylization.GlowColor = Color.FromArgb(CByte(255), CByte(192), CByte(255))
        btn_ResetStylization.HoverGlowIntensity = 0.65F
        btn_ResetStylization.Location = New Point(93, 640)
        btn_ResetStylization.Name = "btn_ResetStylization"
        btn_ResetStylization.NormalGlowIntensity = 0.35F
        btn_ResetStylization.PenWidth = 2
        btn_ResetStylization.Size = New Size(173, 25)
        btn_ResetStylization.TabIndex = 43
        btn_ResetStylization.TabStop = False
        btn_ResetStylization.Text = "RESET STYLIZATION"
        btn_ResetStylization.UseVisualStyleBackColor = False
        ' 
        ' lblLUThelp
        ' 
        lblLUThelp.AutoSize = True
        lblLUThelp.Cursor = Cursors.Hand
        lblLUThelp.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblLUThelp.ForeColor = Color.Silver
        lblLUThelp.Location = New Point(295, 47)
        lblLUThelp.Name = "lblLUThelp"
        lblLUThelp.Size = New Size(20, 25)
        lblLUThelp.TabIndex = 42
        lblLUThelp.Text = "?"
        ' 
        ' Panel35
        ' 
        Panel35.BackColor = Color.FromArgb(CByte(40), CByte(40), CByte(40))
        Panel35.Location = New Point(1, 629)
        Panel35.Name = "Panel35"
        Panel35.Size = New Size(345, 3)
        Panel35.TabIndex = 16
        ' 
        ' lblLutName
        ' 
        lblLutName.AutoSize = True
        lblLutName.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblLutName.ForeColor = Color.Silver
        lblLutName.Location = New Point(24, 76)
        lblLutName.Name = "lblLutName"
        lblLutName.Size = New Size(107, 17)
        lblLutName.TabIndex = 15
        lblLutName.Text = "NO LUT LOADED"
        ' 
        ' cmbLut
        ' 
        cmbLut.BackColor = Color.FromArgb(CByte(37), CByte(38), CByte(39))
        cmbLut.DropDownStyle = ComboBoxStyle.DropDownList
        cmbLut.FlatStyle = FlatStyle.Flat
        cmbLut.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        cmbLut.FormattingEnabled = True
        cmbLut.Location = New Point(24, 48)
        cmbLut.Name = "cmbLut"
        cmbLut.Size = New Size(258, 21)
        cmbLut.TabIndex = 14
        ' 
        ' FlowLayoutPanel2
        ' 
        FlowLayoutPanel2.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        FlowLayoutPanel2.BackColor = Color.Transparent
        FlowLayoutPanel2.Controls.Add(Panel25)
        FlowLayoutPanel2.Controls.Add(Panel26)
        FlowLayoutPanel2.Controls.Add(Panel27)
        FlowLayoutPanel2.Controls.Add(Panel28)
        FlowLayoutPanel2.Controls.Add(Panel29)
        FlowLayoutPanel2.Controls.Add(Panel30)
        FlowLayoutPanel2.Controls.Add(Panel31)
        FlowLayoutPanel2.Controls.Add(Panel32)
        FlowLayoutPanel2.Controls.Add(Panel39)
        FlowLayoutPanel2.Controls.Add(Panel33)
        FlowLayoutPanel2.Controls.Add(Panel34)
        FlowLayoutPanel2.Controls.Add(Panel46)
        FlowLayoutPanel2.Controls.Add(Panel41)
        FlowLayoutPanel2.Controls.Add(Panel45)
        FlowLayoutPanel2.Location = New Point(21, 98)
        FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        FlowLayoutPanel2.Size = New Size(308, 531)
        FlowLayoutPanel2.TabIndex = 1
        ' 
        ' Panel25
        ' 
        Panel25.Controls.Add(Label99)
        Panel25.Controls.Add(Label98)
        Panel25.Controls.Add(Label19)
        Panel25.Controls.Add(sldLutIntens)
        Panel25.Controls.Add(txt_LutIntens)
        Panel25.Location = New Point(3, 3)
        Panel25.Name = "Panel25"
        Panel25.Size = New Size(299, 29)
        Panel25.TabIndex = 3
        ' 
        ' Label99
        ' 
        Label99.AutoSize = True
        Label99.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label99.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label99.Location = New Point(248, 9)
        Label99.Name = "Label99"
        Label99.Size = New Size(13, 11)
        Label99.TabIndex = 57
        Label99.Text = "50"
        ' 
        ' Label98
        ' 
        Label98.AutoSize = True
        Label98.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label98.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label98.Location = New Point(70, 9)
        Label98.Name = "Label98"
        Label98.Size = New Size(9, 11)
        Label98.TabIndex = 45
        Label98.Text = "0"
        ' 
        ' Label19
        ' 
        Label19.AutoSize = True
        Label19.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label19.Location = New Point(3, 7)
        Label19.Name = "Label19"
        Label19.Size = New Size(67, 13)
        Label19.TabIndex = 2
        Label19.Text = "LUT INTENS."
        ' 
        ' sldLutIntens
        ' 
        sldLutIntens.AccentColor = Color.FromArgb(CByte(192), CByte(192), CByte(255))
        sldLutIntens.BackColor = Color.Transparent
        sldLutIntens.Location = New Point(79, 2)
        sldLutIntens.Margin = New Padding(3, 2, 3, 2)
        sldLutIntens.Maximum = 100
        sldLutIntens.Minimum = 0
        sldLutIntens.Name = "sldLutIntens"
        sldLutIntens.Size = New Size(170, 24)
        sldLutIntens.TabIndex = 0
        sldLutIntens.Tag = "LutIntens"
        sldLutIntens.Text = "LutIntens"
        sldLutIntens.Value = 0
        ' 
        ' txt_LutIntens
        ' 
        txt_LutIntens.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_LutIntens.BorderStyle = BorderStyle.None
        txt_LutIntens.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_LutIntens.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_LutIntens.Location = New Point(263, 7)
        txt_LutIntens.Margin = New Padding(3, 2, 3, 2)
        txt_LutIntens.Name = "txt_LutIntens"
        txt_LutIntens.Size = New Size(30, 15)
        txt_LutIntens.TabIndex = 1
        txt_LutIntens.Text = "0"
        txt_LutIntens.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel26
        ' 
        Panel26.Controls.Add(Label111)
        Panel26.Controls.Add(Label100)
        Panel26.Controls.Add(Label20)
        Panel26.Controls.Add(sldDeHaze)
        Panel26.Controls.Add(txt_DeHaze)
        Panel26.Location = New Point(3, 38)
        Panel26.Name = "Panel26"
        Panel26.Size = New Size(299, 29)
        Panel26.TabIndex = 4
        ' 
        ' Label111
        ' 
        Label111.AutoSize = True
        Label111.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label111.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label111.Location = New Point(248, 9)
        Label111.Name = "Label111"
        Label111.Size = New Size(13, 11)
        Label111.TabIndex = 58
        Label111.Text = "50"
        ' 
        ' Label100
        ' 
        Label100.AutoSize = True
        Label100.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label100.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label100.Location = New Point(70, 9)
        Label100.Name = "Label100"
        Label100.Size = New Size(9, 11)
        Label100.TabIndex = 46
        Label100.Text = "0"
        ' 
        ' Label20
        ' 
        Label20.AutoSize = True
        Label20.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label20.Location = New Point(3, 7)
        Label20.Name = "Label20"
        Label20.Size = New Size(52, 13)
        Label20.TabIndex = 2
        Label20.Text = "DE-HAZE"
        ' 
        ' sldDeHaze
        ' 
        sldDeHaze.AccentColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        sldDeHaze.BackColor = Color.Transparent
        sldDeHaze.Location = New Point(79, 2)
        sldDeHaze.Margin = New Padding(3, 2, 3, 2)
        sldDeHaze.Maximum = 100
        sldDeHaze.Minimum = 0
        sldDeHaze.Name = "sldDeHaze"
        sldDeHaze.Size = New Size(170, 24)
        sldDeHaze.TabIndex = 0
        sldDeHaze.Tag = "DeHaze"
        sldDeHaze.Text = "DeHaze"
        sldDeHaze.Value = 0
        ' 
        ' txt_DeHaze
        ' 
        txt_DeHaze.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_DeHaze.BorderStyle = BorderStyle.None
        txt_DeHaze.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_DeHaze.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_DeHaze.Location = New Point(263, 7)
        txt_DeHaze.Margin = New Padding(3, 2, 3, 2)
        txt_DeHaze.Name = "txt_DeHaze"
        txt_DeHaze.Size = New Size(30, 15)
        txt_DeHaze.TabIndex = 1
        txt_DeHaze.Text = "0"
        txt_DeHaze.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel27
        ' 
        Panel27.Controls.Add(Label112)
        Panel27.Controls.Add(Label101)
        Panel27.Controls.Add(Label21)
        Panel27.Controls.Add(sldVignette)
        Panel27.Controls.Add(txt_Vignette)
        Panel27.Location = New Point(3, 73)
        Panel27.Name = "Panel27"
        Panel27.Size = New Size(299, 29)
        Panel27.TabIndex = 5
        ' 
        ' Label112
        ' 
        Label112.AutoSize = True
        Label112.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label112.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label112.Location = New Point(248, 9)
        Label112.Name = "Label112"
        Label112.Size = New Size(13, 11)
        Label112.TabIndex = 58
        Label112.Text = "50"
        ' 
        ' Label101
        ' 
        Label101.AutoSize = True
        Label101.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label101.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label101.Location = New Point(70, 9)
        Label101.Name = "Label101"
        Label101.Size = New Size(9, 11)
        Label101.TabIndex = 46
        Label101.Text = "0"
        ' 
        ' Label21
        ' 
        Label21.AutoSize = True
        Label21.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label21.Location = New Point(3, 7)
        Label21.Name = "Label21"
        Label21.Size = New Size(65, 13)
        Label21.TabIndex = 2
        Label21.Text = "VIGNETTE p"
        ' 
        ' sldVignette
        ' 
        sldVignette.AccentColor = Color.FromArgb(CByte(74), CByte(81), CByte(117))
        sldVignette.BackColor = Color.Transparent
        sldVignette.Location = New Point(79, 2)
        sldVignette.Margin = New Padding(3, 2, 3, 2)
        sldVignette.Maximum = 100
        sldVignette.Minimum = 0
        sldVignette.Name = "sldVignette"
        sldVignette.Size = New Size(170, 24)
        sldVignette.TabIndex = 0
        sldVignette.Tag = "Vignette"
        sldVignette.Text = "Vignette"
        sldVignette.Value = 0
        ' 
        ' txt_Vignette
        ' 
        txt_Vignette.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_Vignette.BorderStyle = BorderStyle.None
        txt_Vignette.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_Vignette.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_Vignette.Location = New Point(263, 7)
        txt_Vignette.Margin = New Padding(3, 2, 3, 2)
        txt_Vignette.Name = "txt_Vignette"
        txt_Vignette.Size = New Size(30, 15)
        txt_Vignette.TabIndex = 1
        txt_Vignette.Text = "0"
        txt_Vignette.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel28
        ' 
        Panel28.Controls.Add(Label113)
        Panel28.Controls.Add(Label102)
        Panel28.Controls.Add(Label22)
        Panel28.Controls.Add(sldVignetteStd)
        Panel28.Controls.Add(txt_VignetteStd)
        Panel28.Location = New Point(3, 108)
        Panel28.Name = "Panel28"
        Panel28.Size = New Size(299, 29)
        Panel28.TabIndex = 6
        ' 
        ' Label113
        ' 
        Label113.AutoSize = True
        Label113.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label113.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label113.Location = New Point(248, 9)
        Label113.Name = "Label113"
        Label113.Size = New Size(13, 11)
        Label113.TabIndex = 58
        Label113.Text = "50"
        ' 
        ' Label102
        ' 
        Label102.AutoSize = True
        Label102.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label102.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label102.Location = New Point(70, 9)
        Label102.Name = "Label102"
        Label102.Size = New Size(9, 11)
        Label102.TabIndex = 46
        Label102.Text = "0"
        ' 
        ' Label22
        ' 
        Label22.AutoSize = True
        Label22.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label22.Location = New Point(3, 7)
        Label22.Name = "Label22"
        Label22.Size = New Size(63, 13)
        Label22.TabIndex = 2
        Label22.Text = "VIGNETTE s"
        ' 
        ' sldVignetteStd
        ' 
        sldVignetteStd.AccentColor = Color.FromArgb(CByte(74), CByte(81), CByte(117))
        sldVignetteStd.BackColor = Color.Transparent
        sldVignetteStd.Location = New Point(79, 2)
        sldVignetteStd.Margin = New Padding(3, 2, 3, 2)
        sldVignetteStd.Maximum = 100
        sldVignetteStd.Minimum = 0
        sldVignetteStd.Name = "sldVignetteStd"
        sldVignetteStd.Size = New Size(170, 24)
        sldVignetteStd.TabIndex = 0
        sldVignetteStd.Tag = "VignetteStd"
        sldVignetteStd.Text = "VignetteStd"
        sldVignetteStd.Value = 0
        ' 
        ' txt_VignetteStd
        ' 
        txt_VignetteStd.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_VignetteStd.BorderStyle = BorderStyle.None
        txt_VignetteStd.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_VignetteStd.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_VignetteStd.Location = New Point(263, 7)
        txt_VignetteStd.Margin = New Padding(3, 2, 3, 2)
        txt_VignetteStd.Name = "txt_VignetteStd"
        txt_VignetteStd.Size = New Size(30, 15)
        txt_VignetteStd.TabIndex = 1
        txt_VignetteStd.Text = "0"
        txt_VignetteStd.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel29
        ' 
        Panel29.Controls.Add(Label114)
        Panel29.Controls.Add(Label103)
        Panel29.Controls.Add(Label23)
        Panel29.Controls.Add(sldSepiaMix)
        Panel29.Controls.Add(txt_SepiaMix)
        Panel29.Location = New Point(3, 143)
        Panel29.Name = "Panel29"
        Panel29.Size = New Size(299, 29)
        Panel29.TabIndex = 7
        ' 
        ' Label114
        ' 
        Label114.AutoSize = True
        Label114.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label114.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label114.Location = New Point(248, 9)
        Label114.Name = "Label114"
        Label114.Size = New Size(13, 11)
        Label114.TabIndex = 58
        Label114.Text = "50"
        ' 
        ' Label103
        ' 
        Label103.AutoSize = True
        Label103.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label103.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label103.Location = New Point(70, 9)
        Label103.Name = "Label103"
        Label103.Size = New Size(9, 11)
        Label103.TabIndex = 46
        Label103.Text = "0"
        ' 
        ' Label23
        ' 
        Label23.AutoSize = True
        Label23.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label23.Location = New Point(3, 7)
        Label23.Name = "Label23"
        Label23.Size = New Size(57, 13)
        Label23.TabIndex = 2
        Label23.Text = "SEPIA MIX"
        ' 
        ' sldSepiaMix
        ' 
        sldSepiaMix.AccentColor = Color.FromArgb(CByte(255), CByte(224), CByte(192))
        sldSepiaMix.BackColor = Color.Transparent
        sldSepiaMix.Location = New Point(79, 2)
        sldSepiaMix.Margin = New Padding(3, 2, 3, 2)
        sldSepiaMix.Maximum = 50
        sldSepiaMix.Minimum = 0
        sldSepiaMix.Name = "sldSepiaMix"
        sldSepiaMix.Size = New Size(170, 24)
        sldSepiaMix.TabIndex = 0
        sldSepiaMix.Tag = "SepiaMix"
        sldSepiaMix.Text = "SepiaMix"
        sldSepiaMix.Value = 0
        ' 
        ' txt_SepiaMix
        ' 
        txt_SepiaMix.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_SepiaMix.BorderStyle = BorderStyle.None
        txt_SepiaMix.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_SepiaMix.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_SepiaMix.Location = New Point(263, 7)
        txt_SepiaMix.Margin = New Padding(3, 2, 3, 2)
        txt_SepiaMix.Name = "txt_SepiaMix"
        txt_SepiaMix.Size = New Size(30, 15)
        txt_SepiaMix.TabIndex = 1
        txt_SepiaMix.Text = "0"
        txt_SepiaMix.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel30
        ' 
        Panel30.Controls.Add(Label115)
        Panel30.Controls.Add(Label104)
        Panel30.Controls.Add(Label24)
        Panel30.Controls.Add(sldNoirFactor)
        Panel30.Controls.Add(txt_NoirFactor)
        Panel30.Location = New Point(3, 178)
        Panel30.Name = "Panel30"
        Panel30.Size = New Size(299, 29)
        Panel30.TabIndex = 8
        ' 
        ' Label115
        ' 
        Label115.AutoSize = True
        Label115.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label115.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label115.Location = New Point(248, 9)
        Label115.Name = "Label115"
        Label115.Size = New Size(13, 11)
        Label115.TabIndex = 58
        Label115.Text = "50"
        ' 
        ' Label104
        ' 
        Label104.AutoSize = True
        Label104.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label104.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label104.Location = New Point(70, 9)
        Label104.Name = "Label104"
        Label104.Size = New Size(9, 11)
        Label104.TabIndex = 46
        Label104.Text = "0"
        ' 
        ' Label24
        ' 
        Label24.AutoSize = True
        Label24.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label24.Location = New Point(3, 7)
        Label24.Name = "Label24"
        Label24.Size = New Size(34, 13)
        Label24.TabIndex = 2
        Label24.Text = "NOIR"
        ' 
        ' sldNoirFactor
        ' 
        sldNoirFactor.AccentColor = Color.Silver
        sldNoirFactor.BackColor = Color.Transparent
        sldNoirFactor.Location = New Point(79, 2)
        sldNoirFactor.Margin = New Padding(3, 2, 3, 2)
        sldNoirFactor.Maximum = 100
        sldNoirFactor.Minimum = 0
        sldNoirFactor.Name = "sldNoirFactor"
        sldNoirFactor.Size = New Size(170, 24)
        sldNoirFactor.TabIndex = 0
        sldNoirFactor.Tag = "NoirFactor"
        sldNoirFactor.Text = "NoirFactor"
        sldNoirFactor.Value = 0
        ' 
        ' txt_NoirFactor
        ' 
        txt_NoirFactor.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_NoirFactor.BorderStyle = BorderStyle.None
        txt_NoirFactor.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_NoirFactor.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_NoirFactor.Location = New Point(263, 7)
        txt_NoirFactor.Margin = New Padding(3, 2, 3, 2)
        txt_NoirFactor.Name = "txt_NoirFactor"
        txt_NoirFactor.Size = New Size(30, 15)
        txt_NoirFactor.TabIndex = 1
        txt_NoirFactor.Text = "0"
        txt_NoirFactor.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel31
        ' 
        Panel31.Controls.Add(Label116)
        Panel31.Controls.Add(Label105)
        Panel31.Controls.Add(Label25)
        Panel31.Controls.Add(sldVivid)
        Panel31.Controls.Add(txt_Vivid)
        Panel31.Location = New Point(3, 213)
        Panel31.Name = "Panel31"
        Panel31.Size = New Size(299, 29)
        Panel31.TabIndex = 9
        ' 
        ' Label116
        ' 
        Label116.AutoSize = True
        Label116.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label116.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label116.Location = New Point(248, 9)
        Label116.Name = "Label116"
        Label116.Size = New Size(13, 11)
        Label116.TabIndex = 58
        Label116.Text = "50"
        ' 
        ' Label105
        ' 
        Label105.AutoSize = True
        Label105.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label105.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label105.Location = New Point(70, 9)
        Label105.Name = "Label105"
        Label105.Size = New Size(9, 11)
        Label105.TabIndex = 46
        Label105.Text = "0"
        ' 
        ' Label25
        ' 
        Label25.AutoSize = True
        Label25.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label25.Location = New Point(3, 7)
        Label25.Name = "Label25"
        Label25.Size = New Size(35, 13)
        Label25.TabIndex = 2
        Label25.Text = "VIVID"
        ' 
        ' sldVivid
        ' 
        sldVivid.AccentColor = Color.FromArgb(CByte(255), CByte(192), CByte(128))
        sldVivid.BackColor = Color.Transparent
        sldVivid.Location = New Point(79, 2)
        sldVivid.Margin = New Padding(3, 2, 3, 2)
        sldVivid.Maximum = 100
        sldVivid.Minimum = 0
        sldVivid.Name = "sldVivid"
        sldVivid.Size = New Size(170, 24)
        sldVivid.TabIndex = 0
        sldVivid.Tag = "Vivid"
        sldVivid.Text = "Vivid"
        sldVivid.Value = 0
        ' 
        ' txt_Vivid
        ' 
        txt_Vivid.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_Vivid.BorderStyle = BorderStyle.None
        txt_Vivid.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_Vivid.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_Vivid.Location = New Point(263, 7)
        txt_Vivid.Margin = New Padding(3, 2, 3, 2)
        txt_Vivid.Name = "txt_Vivid"
        txt_Vivid.Size = New Size(30, 15)
        txt_Vivid.TabIndex = 1
        txt_Vivid.Text = "0"
        txt_Vivid.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel32
        ' 
        Panel32.Controls.Add(Label117)
        Panel32.Controls.Add(Label106)
        Panel32.Controls.Add(Label26)
        Panel32.Controls.Add(sldFilmGrain)
        Panel32.Controls.Add(txt_FilmGrain)
        Panel32.Location = New Point(3, 248)
        Panel32.Name = "Panel32"
        Panel32.Size = New Size(299, 29)
        Panel32.TabIndex = 10
        ' 
        ' Label117
        ' 
        Label117.AutoSize = True
        Label117.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label117.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label117.Location = New Point(248, 9)
        Label117.Name = "Label117"
        Label117.Size = New Size(13, 11)
        Label117.TabIndex = 58
        Label117.Text = "50"
        ' 
        ' Label106
        ' 
        Label106.AutoSize = True
        Label106.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label106.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label106.Location = New Point(70, 9)
        Label106.Name = "Label106"
        Label106.Size = New Size(9, 11)
        Label106.TabIndex = 46
        Label106.Text = "0"
        ' 
        ' Label26
        ' 
        Label26.AutoSize = True
        Label26.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label26.Location = New Point(3, 7)
        Label26.Name = "Label26"
        Label26.Size = New Size(67, 13)
        Label26.TabIndex = 2
        Label26.Text = "FILM GRAIN"
        ' 
        ' sldFilmGrain
        ' 
        sldFilmGrain.AccentColor = Color.Teal
        sldFilmGrain.BackColor = Color.Transparent
        sldFilmGrain.Location = New Point(79, 2)
        sldFilmGrain.Margin = New Padding(3, 2, 3, 2)
        sldFilmGrain.Maximum = 100
        sldFilmGrain.Minimum = 0
        sldFilmGrain.Name = "sldFilmGrain"
        sldFilmGrain.Size = New Size(170, 24)
        sldFilmGrain.TabIndex = 0
        sldFilmGrain.Tag = "FilmGrain"
        sldFilmGrain.Text = "FilmGrain"
        sldFilmGrain.Value = 0
        ' 
        ' txt_FilmGrain
        ' 
        txt_FilmGrain.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_FilmGrain.BorderStyle = BorderStyle.None
        txt_FilmGrain.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_FilmGrain.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_FilmGrain.Location = New Point(263, 7)
        txt_FilmGrain.Margin = New Padding(3, 2, 3, 2)
        txt_FilmGrain.Name = "txt_FilmGrain"
        txt_FilmGrain.Size = New Size(30, 15)
        txt_FilmGrain.TabIndex = 1
        txt_FilmGrain.Text = "0"
        txt_FilmGrain.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel39
        ' 
        Panel39.Controls.Add(Label118)
        Panel39.Controls.Add(Label108)
        Panel39.Controls.Add(PictureBox1)
        Panel39.Controls.Add(Label35)
        Panel39.Controls.Add(sldGrainSize)
        Panel39.Controls.Add(txt_GrainSize)
        Panel39.Location = New Point(3, 283)
        Panel39.Name = "Panel39"
        Panel39.Size = New Size(299, 29)
        Panel39.TabIndex = 13
        ' 
        ' Label118
        ' 
        Label118.AutoSize = True
        Label118.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label118.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label118.Location = New Point(248, 9)
        Label118.Name = "Label118"
        Label118.Size = New Size(13, 11)
        Label118.TabIndex = 58
        Label118.Text = "50"
        ' 
        ' Label108
        ' 
        Label108.AutoSize = True
        Label108.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label108.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label108.Location = New Point(125, 9)
        Label108.Name = "Label108"
        Label108.Size = New Size(9, 11)
        Label108.TabIndex = 46
        Label108.Text = "0"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), Image)
        PictureBox1.BackgroundImageLayout = ImageLayout.Zoom
        PictureBox1.Location = New Point(3, 2)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(25, 25)
        PictureBox1.TabIndex = 3
        PictureBox1.TabStop = False
        ' 
        ' Label35
        ' 
        Label35.AutoSize = True
        Label35.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label35.Location = New Point(45, 8)
        Label35.Name = "Label35"
        Label35.Size = New Size(70, 13)
        Label35.TabIndex = 2
        Label35.Text = "GRAIN   SIZE"
        ' 
        ' sldGrainSize
        ' 
        sldGrainSize.AccentColor = Color.Teal
        sldGrainSize.BackColor = Color.Transparent
        sldGrainSize.Location = New Point(132, 2)
        sldGrainSize.Margin = New Padding(3, 2, 3, 2)
        sldGrainSize.Maximum = 100
        sldGrainSize.Minimum = 0
        sldGrainSize.Name = "sldGrainSize"
        sldGrainSize.Size = New Size(117, 24)
        sldGrainSize.TabIndex = 0
        sldGrainSize.Tag = "GrainSize"
        sldGrainSize.Text = "GrainSize"
        sldGrainSize.Value = 0
        ' 
        ' txt_GrainSize
        ' 
        txt_GrainSize.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_GrainSize.BorderStyle = BorderStyle.None
        txt_GrainSize.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_GrainSize.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_GrainSize.Location = New Point(263, 7)
        txt_GrainSize.Margin = New Padding(3, 2, 3, 2)
        txt_GrainSize.Name = "txt_GrainSize"
        txt_GrainSize.Size = New Size(30, 15)
        txt_GrainSize.TabIndex = 1
        txt_GrainSize.Text = "0"
        txt_GrainSize.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel33
        ' 
        Panel33.Controls.Add(Label119)
        Panel33.Controls.Add(Label107)
        Panel33.Controls.Add(Label27)
        Panel33.Controls.Add(sldSplitToning)
        Panel33.Controls.Add(txt_SplitToning)
        Panel33.Location = New Point(3, 318)
        Panel33.Name = "Panel33"
        Panel33.Size = New Size(299, 29)
        Panel33.TabIndex = 11
        ' 
        ' Label119
        ' 
        Label119.AutoSize = True
        Label119.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label119.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label119.Location = New Point(248, 9)
        Label119.Name = "Label119"
        Label119.Size = New Size(13, 11)
        Label119.TabIndex = 58
        Label119.Text = "50"
        ' 
        ' Label107
        ' 
        Label107.AutoSize = True
        Label107.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label107.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label107.Location = New Point(70, 9)
        Label107.Name = "Label107"
        Label107.Size = New Size(9, 11)
        Label107.TabIndex = 46
        Label107.Text = "0"
        ' 
        ' Label27
        ' 
        Label27.AutoSize = True
        Label27.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label27.Location = New Point(3, 7)
        Label27.Name = "Label27"
        Label27.Size = New Size(62, 13)
        Label27.TabIndex = 2
        Label27.Text = "SPLIT TONE"
        ' 
        ' sldSplitToning
        ' 
        sldSplitToning.AccentColor = Color.Olive
        sldSplitToning.BackColor = Color.Transparent
        sldSplitToning.Location = New Point(79, 2)
        sldSplitToning.Margin = New Padding(3, 2, 3, 2)
        sldSplitToning.Maximum = 100
        sldSplitToning.Minimum = 0
        sldSplitToning.Name = "sldSplitToning"
        sldSplitToning.Size = New Size(170, 24)
        sldSplitToning.TabIndex = 0
        sldSplitToning.Tag = "SplitToning"
        sldSplitToning.Text = "SplitToning"
        sldSplitToning.Value = 0
        ' 
        ' txt_SplitToning
        ' 
        txt_SplitToning.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_SplitToning.BorderStyle = BorderStyle.None
        txt_SplitToning.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_SplitToning.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_SplitToning.Location = New Point(263, 7)
        txt_SplitToning.Margin = New Padding(3, 2, 3, 2)
        txt_SplitToning.Name = "txt_SplitToning"
        txt_SplitToning.Size = New Size(30, 15)
        txt_SplitToning.TabIndex = 1
        txt_SplitToning.Text = "0"
        txt_SplitToning.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel34
        ' 
        Panel34.Controls.Add(Label120)
        Panel34.Controls.Add(Label109)
        Panel34.Controls.Add(Label28)
        Panel34.Controls.Add(sldFade)
        Panel34.Controls.Add(txt_Fade)
        Panel34.Location = New Point(3, 353)
        Panel34.Name = "Panel34"
        Panel34.Size = New Size(299, 29)
        Panel34.TabIndex = 12
        ' 
        ' Label120
        ' 
        Label120.AutoSize = True
        Label120.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label120.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label120.Location = New Point(248, 9)
        Label120.Name = "Label120"
        Label120.Size = New Size(13, 11)
        Label120.TabIndex = 58
        Label120.Text = "50"
        ' 
        ' Label109
        ' 
        Label109.AutoSize = True
        Label109.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label109.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label109.Location = New Point(70, 9)
        Label109.Name = "Label109"
        Label109.Size = New Size(9, 11)
        Label109.TabIndex = 46
        Label109.Text = "0"
        ' 
        ' Label28
        ' 
        Label28.AutoSize = True
        Label28.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label28.Location = New Point(3, 7)
        Label28.Name = "Label28"
        Label28.Size = New Size(33, 13)
        Label28.TabIndex = 2
        Label28.Text = "FADE"
        ' 
        ' sldFade
        ' 
        sldFade.AccentColor = Color.FromArgb(CByte(237), CByte(244), CByte(168))
        sldFade.BackColor = Color.Transparent
        sldFade.Location = New Point(79, 2)
        sldFade.Margin = New Padding(3, 2, 3, 2)
        sldFade.Maximum = 100
        sldFade.Minimum = 0
        sldFade.Name = "sldFade"
        sldFade.Size = New Size(170, 24)
        sldFade.TabIndex = 0
        sldFade.Tag = "Fade"
        sldFade.Text = "Fade"
        sldFade.Value = 0
        ' 
        ' txt_Fade
        ' 
        txt_Fade.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_Fade.BorderStyle = BorderStyle.None
        txt_Fade.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_Fade.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_Fade.Location = New Point(263, 7)
        txt_Fade.Margin = New Padding(3, 2, 3, 2)
        txt_Fade.Name = "txt_Fade"
        txt_Fade.Size = New Size(30, 15)
        txt_Fade.TabIndex = 1
        txt_Fade.Text = "0"
        txt_Fade.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel46
        ' 
        Panel46.Controls.Add(Label124)
        Panel46.Controls.Add(Label121)
        Panel46.Controls.Add(PictureBox6)
        Panel46.Controls.Add(Label47)
        Panel46.Controls.Add(sldFadeTint)
        Panel46.Controls.Add(txt_FadeTint)
        Panel46.Location = New Point(3, 388)
        Panel46.Name = "Panel46"
        Panel46.Size = New Size(299, 29)
        Panel46.TabIndex = 16
        ' 
        ' Label124
        ' 
        Label124.AutoSize = True
        Label124.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label124.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label124.Location = New Point(124, 9)
        Label124.Name = "Label124"
        Label124.Size = New Size(16, 11)
        Label124.TabIndex = 60
        Label124.Text = "-50"
        ' 
        ' Label121
        ' 
        Label121.AutoSize = True
        Label121.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label121.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label121.Location = New Point(248, 9)
        Label121.Name = "Label121"
        Label121.Size = New Size(13, 11)
        Label121.TabIndex = 59
        Label121.Text = "50"
        ' 
        ' PictureBox6
        ' 
        PictureBox6.BackgroundImage = CType(resources.GetObject("PictureBox6.BackgroundImage"), Image)
        PictureBox6.BackgroundImageLayout = ImageLayout.Zoom
        PictureBox6.Location = New Point(3, 2)
        PictureBox6.Name = "PictureBox6"
        PictureBox6.Size = New Size(25, 25)
        PictureBox6.TabIndex = 3
        PictureBox6.TabStop = False
        ' 
        ' Label47
        ' 
        Label47.AutoSize = True
        Label47.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label47.Location = New Point(45, 8)
        Label47.Name = "Label47"
        Label47.Size = New Size(57, 13)
        Label47.TabIndex = 2
        Label47.Text = "FADE TINT"
        ' 
        ' sldFadeTint
        ' 
        sldFadeTint.AccentColor = Color.FromArgb(CByte(237), CByte(244), CByte(168))
        sldFadeTint.BackColor = Color.Transparent
        sldFadeTint.Location = New Point(135, 2)
        sldFadeTint.Margin = New Padding(3, 2, 3, 2)
        sldFadeTint.Maximum = 100
        sldFadeTint.Minimum = 0
        sldFadeTint.Name = "sldFadeTint"
        sldFadeTint.Size = New Size(115, 24)
        sldFadeTint.TabIndex = 0
        sldFadeTint.Tag = "FadeTint"
        sldFadeTint.Text = "FadeTint"
        sldFadeTint.Value = 50
        ' 
        ' txt_FadeTint
        ' 
        txt_FadeTint.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_FadeTint.BorderStyle = BorderStyle.None
        txt_FadeTint.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_FadeTint.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_FadeTint.Location = New Point(263, 7)
        txt_FadeTint.Margin = New Padding(3, 2, 3, 2)
        txt_FadeTint.Name = "txt_FadeTint"
        txt_FadeTint.Size = New Size(30, 15)
        txt_FadeTint.TabIndex = 1
        txt_FadeTint.Text = "0"
        txt_FadeTint.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel41
        ' 
        Panel41.Controls.Add(Label122)
        Panel41.Controls.Add(Label110)
        Panel41.Controls.Add(Label45)
        Panel41.Controls.Add(sldCinematic)
        Panel41.Controls.Add(txt_Cinematic)
        Panel41.Location = New Point(3, 423)
        Panel41.Name = "Panel41"
        Panel41.Size = New Size(299, 29)
        Panel41.TabIndex = 14
        ' 
        ' Label122
        ' 
        Label122.AutoSize = True
        Label122.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label122.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label122.Location = New Point(248, 9)
        Label122.Name = "Label122"
        Label122.Size = New Size(13, 11)
        Label122.TabIndex = 59
        Label122.Text = "50"
        ' 
        ' Label110
        ' 
        Label110.AutoSize = True
        Label110.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label110.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label110.Location = New Point(70, 9)
        Label110.Name = "Label110"
        Label110.Size = New Size(9, 11)
        Label110.TabIndex = 46
        Label110.Text = "0"
        ' 
        ' Label45
        ' 
        Label45.AutoSize = True
        Label45.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label45.Location = New Point(3, 7)
        Label45.Name = "Label45"
        Label45.Size = New Size(62, 13)
        Label45.TabIndex = 2
        Label45.Text = "CINEMATIC"
        ' 
        ' sldCinematic
        ' 
        sldCinematic.AccentColor = Color.FromArgb(CByte(235), CByte(158), CByte(129))
        sldCinematic.BackColor = Color.Transparent
        sldCinematic.Location = New Point(79, 2)
        sldCinematic.Margin = New Padding(3, 2, 3, 2)
        sldCinematic.Maximum = 100
        sldCinematic.Minimum = 0
        sldCinematic.Name = "sldCinematic"
        sldCinematic.Size = New Size(170, 24)
        sldCinematic.TabIndex = 0
        sldCinematic.Tag = "Cinematic"
        sldCinematic.Text = "Cinematic"
        sldCinematic.Value = 0
        ' 
        ' txt_Cinematic
        ' 
        txt_Cinematic.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_Cinematic.BorderStyle = BorderStyle.None
        txt_Cinematic.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_Cinematic.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_Cinematic.Location = New Point(263, 7)
        txt_Cinematic.Margin = New Padding(3, 2, 3, 2)
        txt_Cinematic.Name = "txt_Cinematic"
        txt_Cinematic.Size = New Size(30, 15)
        txt_Cinematic.TabIndex = 1
        txt_Cinematic.Text = "0"
        txt_Cinematic.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel45
        ' 
        Panel45.Controls.Add(Label125)
        Panel45.Controls.Add(Label123)
        Panel45.Controls.Add(PictureBox5)
        Panel45.Controls.Add(Label46)
        Panel45.Controls.Add(sldCineBalance)
        Panel45.Controls.Add(txt_CineBalance)
        Panel45.Location = New Point(3, 458)
        Panel45.Name = "Panel45"
        Panel45.Size = New Size(299, 29)
        Panel45.TabIndex = 15
        ' 
        ' Label125
        ' 
        Label125.AutoSize = True
        Label125.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label125.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label125.Location = New Point(124, 9)
        Label125.Name = "Label125"
        Label125.Size = New Size(16, 11)
        Label125.TabIndex = 61
        Label125.Text = "-50"
        ' 
        ' Label123
        ' 
        Label123.AutoSize = True
        Label123.Font = New Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label123.ForeColor = Color.FromArgb(CByte(242), CByte(200), CByte(193))
        Label123.Location = New Point(248, 9)
        Label123.Name = "Label123"
        Label123.Size = New Size(13, 11)
        Label123.TabIndex = 59
        Label123.Text = "50"
        ' 
        ' PictureBox5
        ' 
        PictureBox5.BackgroundImage = CType(resources.GetObject("PictureBox5.BackgroundImage"), Image)
        PictureBox5.BackgroundImageLayout = ImageLayout.Zoom
        PictureBox5.Location = New Point(3, 2)
        PictureBox5.Name = "PictureBox5"
        PictureBox5.Size = New Size(25, 25)
        PictureBox5.TabIndex = 3
        PictureBox5.TabStop = False
        ' 
        ' Label46
        ' 
        Label46.AutoSize = True
        Label46.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Label46.Location = New Point(45, 8)
        Label46.Name = "Label46"
        Label46.Size = New Size(81, 13)
        Label46.TabIndex = 2
        Label46.Text = "CINE BALANCE"
        ' 
        ' sldCineBalance
        ' 
        sldCineBalance.AccentColor = Color.FromArgb(CByte(235), CByte(158), CByte(129))
        sldCineBalance.BackColor = Color.Transparent
        sldCineBalance.Location = New Point(135, 2)
        sldCineBalance.Margin = New Padding(3, 2, 3, 2)
        sldCineBalance.Maximum = 100
        sldCineBalance.Minimum = 0
        sldCineBalance.Name = "sldCineBalance"
        sldCineBalance.Size = New Size(115, 24)
        sldCineBalance.TabIndex = 0
        sldCineBalance.Tag = "CineBalance"
        sldCineBalance.Text = "CineBalance"
        sldCineBalance.Value = 50
        ' 
        ' txt_CineBalance
        ' 
        txt_CineBalance.BackColor = Color.FromArgb(CByte(52), CByte(52), CByte(52))
        txt_CineBalance.BorderStyle = BorderStyle.None
        txt_CineBalance.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_CineBalance.ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        txt_CineBalance.Location = New Point(263, 7)
        txt_CineBalance.Margin = New Padding(3, 2, 3, 2)
        txt_CineBalance.Name = "txt_CineBalance"
        txt_CineBalance.Size = New Size(30, 15)
        txt_CineBalance.TabIndex = 1
        txt_CineBalance.Text = "0"
        txt_CineBalance.TextAlign = HorizontalAlignment.Center
        ' 
        ' pb_HEXA_READ
        ' 
        pb_HEXA_READ.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        pb_HEXA_READ.BackColor = Color.FromArgb(CByte(32), CByte(32), CByte(32))
        pb_HEXA_READ.Location = New Point(277, 31)
        pb_HEXA_READ.Name = "pb_HEXA_READ"
        pb_HEXA_READ.Size = New Size(22, 22)
        pb_HEXA_READ.TabIndex = 19
        pb_HEXA_READ.TabStop = False
        ' 
        ' Panel4
        ' 
        Panel4.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Panel4.BackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        Panel4.Controls.Add(btn_OrigEdit)
        Panel4.Controls.Add(Label41)
        Panel4.Controls.Add(pb_HEXA_READ)
        Panel4.Controls.Add(Label29)
        Panel4.Controls.Add(txt_HEXA_READ)
        Panel4.Location = New Point(1512, 890)
        Panel4.Margin = New Padding(3, 2, 3, 2)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(382, 68)
        Panel4.TabIndex = 9
        ' 
        ' btn_OrigEdit
        ' 
        btn_OrigEdit.BackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        btn_OrigEdit.CornerRadius = 5
        btn_OrigEdit.FlatAppearance.BorderSize = 0
        btn_OrigEdit.FlatStyle = FlatStyle.Flat
        btn_OrigEdit.Font = New Font("Segoe UI", 9F)
        btn_OrigEdit.ForeColor = Color.FromArgb(CByte(217), CByte(203), CByte(179))
        btn_OrigEdit.GlowColor = Color.FromArgb(CByte(255), CByte(128), CByte(0))
        btn_OrigEdit.HoverGlowIntensity = 0.9F
        btn_OrigEdit.Location = New Point(16, 29)
        btn_OrigEdit.Name = "btn_OrigEdit"
        btn_OrigEdit.NormalGlowIntensity = 0.5F
        btn_OrigEdit.PenWidth = 1
        btn_OrigEdit.Size = New Size(210, 25)
        btn_OrigEdit.TabIndex = 24
        btn_OrigEdit.TabStop = False
        btn_OrigEdit.Text = "EDIT MODE ( EDIT ON )"
        btn_OrigEdit.UseVisualStyleBackColor = False
        ' 
        ' Label41
        ' 
        Label41.AutoSize = True
        Label41.ForeColor = Color.Gray
        Label41.Location = New Point(303, 14)
        Label41.Name = "Label41"
        Label41.Size = New Size(52, 13)
        Label41.TabIndex = 22
        Label41.Text = "C.PICKER"
        ' 
        ' Label29
        ' 
        Label29.AutoSize = True
        Label29.ForeColor = Color.Gray
        Label29.Location = New Point(55, 14)
        Label29.Name = "Label29"
        Label29.Size = New Size(123, 13)
        Label29.TabIndex = 5
        Label29.Text = " 'BEFORE' AND 'AFTER'"
        ' 
        ' viewportMenu
        ' 
        viewportMenu.Items.AddRange(New ToolStripItem() {itemCenter, itemFit, itemOriginal, ToolStripSeparator1, FixPositionToolStripMenuItem, ToolStripSeparator2, ChangeColorCtopToolStripMenuItem})
        viewportMenu.Name = "viewportMenu"
        viewportMenu.Size = New Size(199, 126)
        ' 
        ' itemCenter
        ' 
        itemCenter.Name = "itemCenter"
        itemCenter.Size = New Size(198, 22)
        itemCenter.Text = "Center Screen"
        ' 
        ' itemFit
        ' 
        itemFit.Name = "itemFit"
        itemFit.Size = New Size(198, 22)
        itemFit.Text = "Fit to Screen"
        ' 
        ' itemOriginal
        ' 
        itemOriginal.Name = "itemOriginal"
        itemOriginal.Size = New Size(198, 22)
        itemOriginal.Text = "Original Size (1:1)"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(195, 6)
        ' 
        ' FixPositionToolStripMenuItem
        ' 
        FixPositionToolStripMenuItem.Name = "FixPositionToolStripMenuItem"
        FixPositionToolStripMenuItem.Size = New Size(198, 22)
        FixPositionToolStripMenuItem.Text = "Color: Fix position Area"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(195, 6)
        ' 
        ' ChangeColorCtopToolStripMenuItem
        ' 
        ChangeColorCtopToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {TS_White, TS_Yellow, TS_Red, TS_Blue})
        ChangeColorCtopToolStripMenuItem.Name = "ChangeColorCtopToolStripMenuItem"
        ChangeColorCtopToolStripMenuItem.Size = New Size(198, 22)
        ChangeColorCtopToolStripMenuItem.Text = "Crop: Change color Sel."
        ' 
        ' TS_White
        ' 
        TS_White.Name = "TS_White"
        TS_White.Size = New Size(108, 22)
        TS_White.Text = "White"
        ' 
        ' TS_Yellow
        ' 
        TS_Yellow.Name = "TS_Yellow"
        TS_Yellow.Size = New Size(108, 22)
        TS_Yellow.Text = "Yellow"
        ' 
        ' TS_Red
        ' 
        TS_Red.Name = "TS_Red"
        TS_Red.Size = New Size(108, 22)
        TS_Red.Text = "Red"
        ' 
        ' TS_Blue
        ' 
        TS_Blue.Name = "TS_Blue"
        TS_Blue.Size = New Size(108, 22)
        TS_Blue.Text = "Blue"
        ' 
        ' tmrAnimate
        ' 
        tmrAnimate.Enabled = True
        tmrAnimate.Interval = 33
        ' 
        ' thumbMenu
        ' 
        thumbMenu.Items.AddRange(New ToolStripItem() {DeletePictureFromStripToolStripMenuItem, ToolStripSeparator3, ClearAllStripToolStripMenuItem})
        thumbMenu.Name = "thumbMenu"
        thumbMenu.Size = New Size(204, 54)
        ' 
        ' DeletePictureFromStripToolStripMenuItem
        ' 
        DeletePictureFromStripToolStripMenuItem.Name = "DeletePictureFromStripToolStripMenuItem"
        DeletePictureFromStripToolStripMenuItem.Size = New Size(203, 22)
        DeletePictureFromStripToolStripMenuItem.Text = "Delete Picture from Strip"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(200, 6)
        ' 
        ' ClearAllStripToolStripMenuItem
        ' 
        ClearAllStripToolStripMenuItem.Name = "ClearAllStripToolStripMenuItem"
        ClearAllStripToolStripMenuItem.Size = New Size(203, 22)
        ClearAllStripToolStripMenuItem.Text = "Clear All Strip"
        ' 
        ' F_Stop
        ' 
        AutoScaleDimensions = New SizeF(6F, 13F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(20), CByte(20), CByte(20))
        ClientSize = New Size(1904, 985)
        Controls.Add(panelCenter)
        Controls.Add(Panel4)
        Controls.Add(Panel3)
        Controls.Add(panelHistogram)
        Controls.Add(thumbnailPanel)
        Controls.Add(lblLibrary)
        Controls.Add(StatusBar)
        Controls.Add(Panel1)
        Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        ForeColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        KeyPreview = True
        Margin = New Padding(3, 2, 3, 2)
        Name = "F_Stop"
        StartPosition = FormStartPosition.CenterScreen
        Text = "F-Stop V1.0"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        StatusBar.ResumeLayout(False)
        StatusBar.PerformLayout()
        panelCenter.ResumeLayout(False)
        panelCenter.PerformLayout()
        FlowLayoutPanel3.ResumeLayout(False)
        FlowLayoutPanel3.PerformLayout()
        CType(pnlColorPreview, ComponentModel.ISupportInitialize).EndInit()
        panelHistogram.ResumeLayout(False)
        panelHistogram.PerformLayout()
        tsRGB.ResumeLayout(False)
        tsRGB.PerformLayout()
        CType(picHighlights, ComponentModel.ISupportInitialize).EndInit()
        CType(picShadows, ComponentModel.ISupportInitialize).EndInit()
        CType(glHist, ComponentModel.ISupportInitialize).EndInit()
        Panel3.ResumeLayout(False)
        ModernTabControl1.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        TabPage1.PerformLayout()
        flow_Correction.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        Panel5.ResumeLayout(False)
        Panel5.PerformLayout()
        Panel6.ResumeLayout(False)
        Panel6.PerformLayout()
        Panel7.ResumeLayout(False)
        Panel7.PerformLayout()
        Panel8.ResumeLayout(False)
        Panel8.PerformLayout()
        Panel9.ResumeLayout(False)
        Panel9.PerformLayout()
        Panel10.ResumeLayout(False)
        Panel10.PerformLayout()
        Panel11.ResumeLayout(False)
        Panel11.PerformLayout()
        pnl_3.ResumeLayout(False)
        pnl_3.PerformLayout()
        CType(pbCorrectedHexa_Correct, ComponentModel.ISupportInitialize).EndInit()
        CType(pbHexa_Corect, ComponentModel.ISupportInitialize).EndInit()
        Panel24.ResumeLayout(False)
        Panel24.PerformLayout()
        Panel40.ResumeLayout(False)
        TabPage2.ResumeLayout(False)
        TabPage2.PerformLayout()
        CType(numFontSize, ComponentModel.ISupportInitialize).EndInit()
        FlowLayoutPanel1.ResumeLayout(False)
        Panel12.ResumeLayout(False)
        Panel12.PerformLayout()
        Panel14.ResumeLayout(False)
        Panel14.PerformLayout()
        Panel15.ResumeLayout(False)
        Panel15.PerformLayout()
        Panel16.ResumeLayout(False)
        Panel16.PerformLayout()
        Panel17.ResumeLayout(False)
        Panel17.PerformLayout()
        Panel42.ResumeLayout(False)
        Panel42.PerformLayout()
        CType(PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        Panel43.ResumeLayout(False)
        Panel43.PerformLayout()
        CType(PictureBox3, ComponentModel.ISupportInitialize).EndInit()
        Panel44.ResumeLayout(False)
        Panel44.PerformLayout()
        CType(PictureBox4, ComponentModel.ISupportInitialize).EndInit()
        Panel36.ResumeLayout(False)
        Panel36.PerformLayout()
        Panel13.ResumeLayout(False)
        Panel13.PerformLayout()
        Panel18.ResumeLayout(False)
        Panel18.PerformLayout()
        Panel19.ResumeLayout(False)
        Panel19.PerformLayout()
        Panel20.ResumeLayout(False)
        Panel20.PerformLayout()
        Panel21.ResumeLayout(False)
        Panel21.PerformLayout()
        Panel22.ResumeLayout(False)
        Panel22.PerformLayout()
        TabPage3.ResumeLayout(False)
        TabPage3.PerformLayout()
        FlowLayoutPanel2.ResumeLayout(False)
        Panel25.ResumeLayout(False)
        Panel25.PerformLayout()
        Panel26.ResumeLayout(False)
        Panel26.PerformLayout()
        Panel27.ResumeLayout(False)
        Panel27.PerformLayout()
        Panel28.ResumeLayout(False)
        Panel28.PerformLayout()
        Panel29.ResumeLayout(False)
        Panel29.PerformLayout()
        Panel30.ResumeLayout(False)
        Panel30.PerformLayout()
        Panel31.ResumeLayout(False)
        Panel31.PerformLayout()
        Panel32.ResumeLayout(False)
        Panel32.PerformLayout()
        Panel39.ResumeLayout(False)
        Panel39.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        Panel33.ResumeLayout(False)
        Panel33.PerformLayout()
        Panel34.ResumeLayout(False)
        Panel34.PerformLayout()
        Panel46.ResumeLayout(False)
        Panel46.PerformLayout()
        CType(PictureBox6, ComponentModel.ISupportInitialize).EndInit()
        Panel41.ResumeLayout(False)
        Panel41.PerformLayout()
        Panel45.ResumeLayout(False)
        Panel45.PerformLayout()
        CType(PictureBox5, ComponentModel.ISupportInitialize).EndInit()
        CType(pb_HEXA_READ, ComponentModel.ISupportInitialize).EndInit()
        Panel4.ResumeLayout(False)
        Panel4.PerformLayout()
        viewportMenu.ResumeLayout(False)
        thumbMenu.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents StatusBar As StatusStrip
    Friend WithEvents lblGpuInfo As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents lblFileName As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As ToolStripStatusLabel
    Friend WithEvents lblWH As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel3 As ToolStripStatusLabel
    Friend WithEvents lblSizeMp As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel4 As ToolStripStatusLabel
    Friend WithEvents lblBit As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel5 As ToolStripStatusLabel
    Friend WithEvents lblZoom As ToolStripStatusLabel
    Friend WithEvents txtExifData As RichTextBox
    Friend WithEvents lblLibrary As Label
    Friend WithEvents panelCenter As Panel
    Friend WithEvents thumbnailPanel As FlowLayoutPanel
    Friend WithEvents ToolStripStatusLabel6 As ToolStripStatusLabel
    Friend WithEvents panelHistogram As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents GlControl2 As OpenTK.GLControl.GLControl
    Friend WithEvents viewportMenu As ContextMenuStrip
    Friend WithEvents itemCenter As ToolStripMenuItem
    Friend WithEvents itemFit As ToolStripMenuItem
    Friend WithEvents itemOriginal As ToolStripMenuItem
    Friend WithEvents picShadows As PictureBox
    Friend WithEvents picHighlights As PictureBox
    Friend WithEvents tmrAnimate As Timer
    Friend WithEvents ModernTabControl1 As ModernTabControl
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents sldWhites As ModernSlider
    Friend WithEvents txt_Whites As TextBox
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents sldShadowsLift As ModernSlider
    Friend WithEvents txt_ShadowsLift As TextBox
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents sldHighlights As ModernSlider
    Friend WithEvents txt_Highlights As TextBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents sldContrast As ModernSlider
    Friend WithEvents txt_Contrast As TextBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents sldExposure As ModernSlider
    Friend WithEvents txt_Exposure As TextBox
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Label6 As Label
    Friend WithEvents sldBlacks As ModernSlider
    Friend WithEvents txt_Blacks As TextBox
    Friend WithEvents Panel10 As Panel
    Friend WithEvents Label7 As Label
    Friend WithEvents sldTemp As ModernSlider
    Friend WithEvents txt_Temp As TextBox
    Friend WithEvents Panel11 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents sldTint As ModernSlider
    Friend WithEvents txt_Tint As TextBox
    Friend WithEvents glHist As PictureBox
    Friend WithEvents flow_Correction As FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents Panel12 As Panel
    Friend WithEvents lbl As Label
    Friend WithEvents sldDynCon As ModernSlider
    Friend WithEvents txt_DynCon As TextBox
    Friend WithEvents Panel13 As Panel
    Friend WithEvents Label10 As Label
    Friend WithEvents sldClarity As ModernSlider
    Friend WithEvents txt_Clarity As TextBox
    Friend WithEvents Panel14 As Panel
    Friend WithEvents Label11 As Label
    Friend WithEvents sldStructX As ModernSlider
    Friend WithEvents txt_StructX As TextBox
    Friend WithEvents Panel15 As Panel
    Friend WithEvents Label12 As Label
    Friend WithEvents sldSharpen As ModernSlider
    Friend WithEvents txt_Sharpen As TextBox
    Friend WithEvents Panel16 As Panel
    Friend WithEvents Label13 As Label
    Friend WithEvents sldDefringe As ModernSlider
    Friend WithEvents txt_Defringe As TextBox
    Friend WithEvents Panel17 As Panel
    Friend WithEvents Label14 As Label
    Friend WithEvents sldDenoise As ModernSlider
    Friend WithEvents txt_Denoise As TextBox
    Friend WithEvents Panel18 As Panel
    Friend WithEvents Label15 As Label
    Friend WithEvents sldSaturation As ModernSlider
    Friend WithEvents txt_Saturation As TextBox
    Friend WithEvents Panel19 As Panel
    Friend WithEvents Label16 As Label
    Friend WithEvents sldVibrance As ModernSlider
    Friend WithEvents txt_Vibrance As TextBox
    Friend WithEvents Panel20 As Panel
    Friend WithEvents Label9 As Label
    Friend WithEvents sldRedMix As ModernSlider
    Friend WithEvents txt_RedMix As TextBox
    Friend WithEvents Panel21 As Panel
    Friend WithEvents Label17 As Label
    Friend WithEvents sldGreenMix As ModernSlider
    Friend WithEvents txt_GreenMix As TextBox
    Friend WithEvents Panel22 As Panel
    Friend WithEvents Label18 As Label
    Friend WithEvents sldBlueMix As ModernSlider
    Friend WithEvents txt_BlueMix As TextBox
    Friend WithEvents Panel23 As Panel
    Friend WithEvents FlowLayoutPanel2 As FlowLayoutPanel
    Friend WithEvents Panel25 As Panel
    Friend WithEvents Label19 As Label
    Friend WithEvents sldLutIntens As ModernSlider
    Friend WithEvents txt_LutIntens As TextBox
    Friend WithEvents Panel26 As Panel
    Friend WithEvents Label20 As Label
    Friend WithEvents sldDeHaze As ModernSlider
    Friend WithEvents txt_DeHaze As TextBox
    Friend WithEvents Panel27 As Panel
    Friend WithEvents Label21 As Label
    Friend WithEvents sldVignette As ModernSlider
    Friend WithEvents txt_Vignette As TextBox
    Friend WithEvents Panel28 As Panel
    Friend WithEvents Label22 As Label
    Friend WithEvents sldVignetteStd As ModernSlider
    Friend WithEvents txt_VignetteStd As TextBox
    Friend WithEvents Panel29 As Panel
    Friend WithEvents Label23 As Label
    Friend WithEvents sldSepiaMix As ModernSlider
    Friend WithEvents txt_SepiaMix As TextBox
    Friend WithEvents Panel30 As Panel
    Friend WithEvents Label24 As Label
    Friend WithEvents sldNoirFactor As ModernSlider
    Friend WithEvents txt_NoirFactor As TextBox
    Friend WithEvents Panel31 As Panel
    Friend WithEvents Label25 As Label
    Friend WithEvents sldVivid As ModernSlider
    Friend WithEvents txt_Vivid As TextBox
    Friend WithEvents Panel32 As Panel
    Friend WithEvents Label26 As Label
    Friend WithEvents sldFilmGrain As ModernSlider
    Friend WithEvents txt_FilmGrain As TextBox
    Friend WithEvents Panel33 As Panel
    Friend WithEvents Label27 As Label
    Friend WithEvents sldSplitToning As ModernSlider
    Friend WithEvents txt_SplitToning As TextBox
    Friend WithEvents Panel34 As Panel
    Friend WithEvents Label28 As Label
    Friend WithEvents sldFade As ModernSlider
    Friend WithEvents txt_Fade As TextBox
    Friend WithEvents lblLutName As Label
    Friend WithEvents cmbLut As ComboBox
    Friend WithEvents Panel35 As Panel
    Friend WithEvents Panel36 As Panel
    Friend WithEvents Label30 As Label
    Friend WithEvents sldTextureX As ModernSlider
    Friend WithEvents txt_TextureX As TextBox
    Friend WithEvents Label29 As Label
    Friend WithEvents Label31 As Label
    Friend WithEvents lblStatus_Change As Label
    Friend WithEvents lblCatalog As Label
    Friend WithEvents lbl_Check_Changes As Label
    Friend WithEvents thumbMenu As ContextMenuStrip
    Friend WithEvents DeletePictureFromStripToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents pbExportAll As ProgressBar
    Friend WithEvents tsRGB As ToolStrip
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents btnRed As ToolStripButton
    Friend WithEvents ToolStripLabel2 As ToolStripLabel
    Friend WithEvents btnGreen As ToolStripButton
    Friend WithEvents ToolStripLabel3 As ToolStripLabel
    Friend WithEvents btnBlue As ToolStripButton
    Friend WithEvents ToolStripLabel4 As ToolStripLabel
    Friend WithEvents btnAll As ToolStripButton
    Friend WithEvents btnLuma As ToolStripButton
    Friend WithEvents ToolStripLabel5 As ToolStripLabel
    Friend WithEvents Panel39 As Panel
    Friend WithEvents Label35 As Label
    Friend WithEvents sldGrainSize As ModernSlider
    Friend WithEvents txt_GrainSize As TextBox
    Friend WithEvents pnl_3 As Panel
    Friend WithEvents txtWatermark As TextBox
    Friend WithEvents btnFont As Button
    Friend WithEvents numFontSize As NumericUpDown
    Friend WithEvents lblFontName As Label
    Friend WithEvents btnSelectColor As Button
    Friend WithEvents sldTextOpacity As ModernSlider
    Friend WithEvents lblTextOpacityValue As Label
    Friend WithEvents lbl_XY As Label
    Friend WithEvents sld_FNT_Size As ModernVerticalSlider
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents lbl_RGB As Label
    Friend WithEvents pnlColorPreview As PictureBox
    Friend WithEvents lbl_Hex As Label
    Friend WithEvents FlowLayoutPanel3 As FlowLayoutPanel
    Friend WithEvents txtCorrectedHexa_Correct As TextBox
    Friend WithEvents txtHexa_Corect As TextBox
    Friend WithEvents txt_HEXA_READ As TextBox
    Friend WithEvents pb_HEXA_READ As PictureBox
    Friend WithEvents Label33 As Label
    Friend WithEvents Label34 As Label
    Friend WithEvents lblSwitchCol As Label
    Friend WithEvents Label37 As Label
    Friend WithEvents Label36 As Label
    Friend WithEvents Panel37 As Panel
    Friend WithEvents Panel38 As Panel
    Friend WithEvents lblSmoothnesCorrect As Label
    Friend WithEvents lblHexaCorrect As Label
    Friend WithEvents cb_UseReplace As CheckBox
    Friend WithEvents pbCorrectedHexa_Correct As PictureBox
    Friend WithEvents pbHexa_Corect As PictureBox
    Friend WithEvents sldSmoothnesCorrect As ModernVerticalSlider
    Friend WithEvents sldHexaCorrect As ModernVerticalSlider
    Friend WithEvents Label39 As Label
    Friend WithEvents sldAreaCorrect As ModernVerticalSlider
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents FixPositionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents lblAreaCorrect As Label
    Friend WithEvents Label38 As Label
    Friend WithEvents sldOpacity As ModernVerticalSlider
    Friend WithEvents lblOpacity As Label
    Friend WithEvents Label40 As Label
    Friend WithEvents lblHelpColor As Label
    Friend WithEvents Label41 As Label
    Friend WithEvents lblLUThelp As Label
    Friend WithEvents lbl_Check_for_Original As Label
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ChangeColorCtopToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TS_White As ToolStripMenuItem
    Friend WithEvents TS_Yellow As ToolStripMenuItem
    Friend WithEvents TS_Red As ToolStripMenuItem
    Friend WithEvents TS_Blue As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ClearAllStripToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel42 As Panel
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label42 As Label
    Friend WithEvents sldNoiseLimit As ModernSlider
    Friend WithEvents txt_NoiseLimit As TextBox
    Friend WithEvents Panel43 As Panel
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents Label43 As Label
    Friend WithEvents sldNoiseChroma As ModernSlider
    Friend WithEvents txt_NoiseChroma As TextBox
    Friend WithEvents Panel44 As Panel
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents Label44 As Label
    Friend WithEvents sldNoiseLuma As ModernSlider
    Friend WithEvents txt_NoiseLuma As TextBox
    Friend WithEvents Panel41 As Panel
    Friend WithEvents Label45 As Label
    Friend WithEvents sldCinematic As ModernSlider
    Friend WithEvents txt_Cinematic As TextBox
    Friend WithEvents Panel45 As Panel
    Friend WithEvents PictureBox5 As PictureBox
    Friend WithEvents Label46 As Label
    Friend WithEvents sldCineBalance As ModernSlider
    Friend WithEvents txt_CineBalance As TextBox
    Friend WithEvents Panel46 As Panel
    Friend WithEvents PictureBox6 As PictureBox
    Friend WithEvents Label47 As Label
    Friend WithEvents sldFadeTint As ModernSlider
    Friend WithEvents txt_FadeTint As TextBox
    Friend WithEvents btn_AutoCorrect As GlowButton
    Friend WithEvents btn_Crop As GlowButton
    Friend WithEvents btn_UndoCrop As GlowButton
    Friend WithEvents btn_Remove_Changes As GlowButton
    Friend WithEvents Label49 As Label
    Friend WithEvents Label50 As Label
    Friend WithEvents BtnResetCorrectionSliders As GlowButton
    Friend WithEvents btn_Open As GlowButton
    Friend WithEvents btn_LoadFolder As GlowButton
    Friend WithEvents btn_SaveToCatalog As GlowButton
    Friend WithEvents btn_SaveChanges As GlowButton
    Friend WithEvents btn_ExportJPG As GlowButton
    Friend WithEvents btn_ExportAll As GlowButton
    Friend WithEvents btn_OrigEdit As GlowButton
    Friend WithEvents btn_ResetEnchentment As GlowButton
    Friend WithEvents btn_ResetStylization As GlowButton
    Friend WithEvents Panel24 As Panel
    Friend WithEvents txt_BarrelDistortion As TextBox
    Friend WithEvents Label51 As Label
    Friend WithEvents sldBarrelDistortion As ModernSlider
    Friend WithEvents lblHelpEnhancement As Label
    Friend WithEvents btn_LoadLut As GlowButton
    Friend WithEvents Label52 As Label
    Friend WithEvents Label53 As Label
    Friend WithEvents Label55 As Label
    Friend WithEvents Label54 As Label
    Friend WithEvents Label57 As Label
    Friend WithEvents Label56 As Label
    Friend WithEvents Label59 As Label
    Friend WithEvents Label58 As Label
    Friend WithEvents Label61 As Label
    Friend WithEvents Label60 As Label
    Friend WithEvents Label65 As Label
    Friend WithEvents Label62 As Label
    Friend WithEvents Label66 As Label
    Friend WithEvents Label63 As Label
    Friend WithEvents Label67 As Label
    Friend WithEvents Label64 As Label
    Friend WithEvents Label69 As Label
    Friend WithEvents Label68 As Label
    Friend WithEvents Label71 As Label
    Friend WithEvents Label70 As Label
    Friend WithEvents Label73 As Label
    Friend WithEvents Label72 As Label
    Friend WithEvents Label75 As Label
    Friend WithEvents Label74 As Label
    Friend WithEvents Label77 As Label
    Friend WithEvents Label76 As Label
    Friend WithEvents Label79 As Label
    Friend WithEvents Label78 As Label
    Friend WithEvents Label81 As Label
    Friend WithEvents Label80 As Label
    Friend WithEvents Label82 As Label
    Friend WithEvents Label83 As Label
    Friend WithEvents Label84 As Label
    Friend WithEvents Label85 As Label
    Friend WithEvents Label86 As Label
    Friend WithEvents Label87 As Label
    Friend WithEvents Label88 As Label
    Friend WithEvents Label89 As Label
    Friend WithEvents Label92 As Label
    Friend WithEvents Label90 As Label
    Friend WithEvents Label93 As Label
    Friend WithEvents Label91 As Label
    Friend WithEvents Label94 As Label
    Friend WithEvents Label96 As Label
    Friend WithEvents Label97 As Label
    Friend WithEvents Label95 As Label
    Friend WithEvents Label98 As Label
    Friend WithEvents Label99 As Label
    Friend WithEvents Label100 As Label
    Friend WithEvents Label101 As Label
    Friend WithEvents Label102 As Label
    Friend WithEvents Label103 As Label
    Friend WithEvents Label104 As Label
    Friend WithEvents Label105 As Label
    Friend WithEvents Label106 As Label
    Friend WithEvents Label108 As Label
    Friend WithEvents Label107 As Label
    Friend WithEvents Label111 As Label
    Friend WithEvents Label112 As Label
    Friend WithEvents Label113 As Label
    Friend WithEvents Label114 As Label
    Friend WithEvents Label115 As Label
    Friend WithEvents Label116 As Label
    Friend WithEvents Label117 As Label
    Friend WithEvents Label118 As Label
    Friend WithEvents Label119 As Label
    Friend WithEvents Label120 As Label
    Friend WithEvents Label109 As Label
    Friend WithEvents Label110 As Label
    Friend WithEvents Label124 As Label
    Friend WithEvents Label121 As Label
    Friend WithEvents Label122 As Label
    Friend WithEvents Label125 As Label
    Friend WithEvents Label123 As Label
    Friend WithEvents Label126 As Label
    Friend WithEvents Label127 As Label
    Friend WithEvents lblOwner As Label
    Friend WithEvents Label32 As Label
    Friend WithEvents Panel40 As Panel
    Friend WithEvents btn_UndoRotate As GlowButton
    Friend WithEvents btn_StraightenPic As GlowButton
End Class
