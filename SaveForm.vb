Public Class SaveForm
    Inherits Form

    Public Property ExportWidth As Integer
    Public Property ExportHeight As Integer
    Public Property IsAccepted As Boolean = False

    Private optOriginal, optHalf, optCustom As RadioButton
    Private txtCustomW, txtCustomH As TextBox
    Private chkLockAspect As CheckBox ' Butonul de Link
    Private btnSave, btnCancel As Button

    Private _origW, _origH As Integer
    Private _aspectRatio As Double
    Private _isUpdating As Boolean = False ' Flag pentru a evita bucla infinită de update

    Public Sub New(origW As Integer, origH As Integer)
        _origW = origW
        _origH = origH
        _aspectRatio = CDbl(origW) / CDbl(origH)
        InitializeUI()
    End Sub

    Private Sub InitializeUI()
        Me.Text = "Export Settings"
        Me.Size = New Size(350, 280)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.StartPosition = FormStartPosition.CenterParent
        Me.BackColor = Color.FromArgb(30, 30, 30)
        Me.ForeColor = Color.White
        Me.Font = New Font("Segoe UI", 9)

        Dim y As Integer = 30
        optOriginal = New RadioButton With {.Text = $"Original ({_origW} x {_origH}) px", .Location = New Point(30, y), .Width = 300, .Checked = True}
        y += 40
        optHalf = New RadioButton With {.Text = $"Half Size ({_origW \ 2} x {_origH \ 2}) px", .Location = New Point(30, y), .Width = 300}
        y += 40

        optCustom = New RadioButton With {.Text = "Custom:", .Location = New Point(30, y), .Width = 80}

        ' Casetele de text
        txtCustomW = New TextBox With {.Location = New Point(110, y), .Width = 60, .Text = _origW.ToString(), .BackColor = Color.FromArgb(50, 50, 50), .ForeColor = Color.White}

        ' Butonul de Link (Aspect Ratio)
        chkLockAspect = New CheckBox With {
            .Text = "🔗", .Appearance = Appearance.Button, .Checked = True,
            .Location = New Point(175, y), .Size = New Point(30, 23),
            .TextAlign = ContentAlignment.MiddleCenter, .FlatStyle = FlatStyle.Flat
        }
        chkLockAspect.FlatAppearance.CheckedBackColor = Color.SteelBlue

        txtCustomH = New TextBox With {.Location = New Point(210, y), .Width = 60, .Text = _origH.ToString(), .BackColor = Color.FromArgb(50, 50, 50), .ForeColor = Color.White}

        y += 70
        btnSave = New Button With {.Text = "Export", .Location = New Point(50, y), .Size = New Size(100, 35), .BackColor = Color.FromArgb(0, 120, 215), .FlatStyle = FlatStyle.Flat}
        btnCancel = New Button With {.Text = "Cancel", .Location = New Point(185, y), .Size = New Size(100, 35), .BackColor = Color.FromArgb(70, 70, 70), .FlatStyle = FlatStyle.Flat}

        Me.Controls.AddRange({optOriginal, optHalf, optCustom, txtCustomW, chkLockAspect, txtCustomH, btnSave, btnCancel})

        ' --- LOGICA DE LINK ---
        AddHandler txtCustomW.TextChanged, AddressOf SyncWidthToHeight
        AddHandler txtCustomH.TextChanged, AddressOf SyncHeightToWidth

        AddHandler btnSave.Click, AddressOf HandleSave
        AddHandler btnCancel.Click, Sub() Me.Close()
    End Sub

    Private Sub SyncWidthToHeight(sender As Object, e As EventArgs)
        If _isUpdating OrElse Not chkLockAspect.Checked OrElse Not optCustom.Checked Then Return
        _isUpdating = True
        Dim w As Integer
        If Integer.TryParse(txtCustomW.Text, w) Then
            txtCustomH.Text = CInt(Math.Round(w / _aspectRatio)).ToString()
        End If
        _isUpdating = False
    End Sub

    Private Sub SyncHeightToWidth(sender As Object, e As EventArgs)
        If _isUpdating OrElse Not chkLockAspect.Checked OrElse Not optCustom.Checked Then Return
        _isUpdating = True
        Dim h As Integer
        If Integer.TryParse(txtCustomH.Text, h) Then
            txtCustomW.Text = CInt(Math.Round(h * _aspectRatio)).ToString()
        End If
        _isUpdating = False
    End Sub

    Private Sub HandleSave(sender As Object, e As EventArgs)
        ' Validare și extragere date (ca în codul anterior)
        If optOriginal.Checked Then
            ExportWidth = _origW : ExportHeight = _origH
        ElseIf optHalf.Checked Then
            ExportWidth = _origW \ 2 : ExportHeight = _origH \ 2
        Else
            Integer.TryParse(txtCustomW.Text, ExportWidth)
            Integer.TryParse(txtCustomH.Text, ExportHeight)
        End If
        IsAccepted = True
        Me.DialogResult = DialogResult.OK
    End Sub
End Class