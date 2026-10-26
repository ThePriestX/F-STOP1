Public Class SaveMulti
    Inherits Form

    Public Property Raport_Scale As Integer
    Public Property IsAccepted As Boolean = False

    Private optOriginal, optHalf, optThird As RadioButton
    Private btnSave, btnCancel As Button
    Private _isUpdating As Boolean = False ' Flag pentru a evita bucla infinită de update

    Public Sub New()
        InitializeUI()
    End Sub

    Private Sub InitializeUI()
        Me.Text = "Group-Export Settings"
        Me.Size = New Size(350, 280)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.StartPosition = FormStartPosition.CenterParent
        Me.BackColor = Color.FromArgb(30, 30, 30)
        Me.ForeColor = Color.White
        Me.Font = New Font("Segoe UI", 9)

        Dim y As Integer = 30
        optOriginal = New RadioButton With {.Text = $"Original Size for All", .Location = New Point(30, y), .Width = 300, .Checked = True}
        y += 40
        optHalf = New RadioButton With {.Text = $"Half Size for All", .Location = New Point(30, y), .Width = 300}
        y += 40
        optThird = New RadioButton With {.Text = $"One-Third Size for All", .Location = New Point(30, y), .Width = 300}
        y += 70
        btnSave = New Button With {.Text = "Export", .Location = New Point(50, y), .Size = New Size(100, 35), .BackColor = Color.FromArgb(0, 120, 215), .FlatStyle = FlatStyle.Flat}
        btnCancel = New Button With {.Text = "Cancel", .Location = New Point(185, y), .Size = New Size(100, 35), .BackColor = Color.FromArgb(70, 70, 70), .FlatStyle = FlatStyle.Flat}

        Me.Controls.AddRange({optOriginal, optHalf, optThird, btnSave, btnCancel})

        ' --- LOGICA DE LINK ---
        AddHandler btnSave.Click, AddressOf HandleSave
        AddHandler btnCancel.Click, Sub() Me.Close()
    End Sub

    Private Sub HandleSave(sender As Object, e As EventArgs)
        ' Validare și extragere date (ca în codul anterior)
        If optOriginal.Checked Then
            Raport_Scale = 1
        ElseIf optHalf.Checked Then
            Raport_Scale = 2
        Else
            Raport_Scale = 3
        End If
        IsAccepted = True
        Me.DialogResult = DialogResult.OK
    End Sub
End Class