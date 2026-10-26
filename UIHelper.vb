Imports System.Windows.Documents

Public Module UIHelper
    ' We add "targetHandler" as a parameter so the slider knows where to send the data
    Public Function CreateSlider(tag As String, min As Integer, max As Integer, startVal As Integer, labelName As String, targetHandler As EventHandler) As Panel
        ' 1. Panel setup (Width 289 to match your frame)
        Dim pnl As New Panel With {.Height = 50, .Width = 289, .Dock = DockStyle.Top, .BackColor = Color.FromArgb(32, 32, 32)}

        ' 2. Label (Left Side) - Using tag as identifier for internal logic
        Dim lblName As New Label With {
            .Text = labelName, .ForeColor = Color.WhiteSmoke, .Font = New Font("Segoe UI", 8),
            .Location = New Point(10, 5), .AutoSize = True, .Tag = tag
        }

        ' 3. ModernSlider (Center) - Width adjusted to leave room on the right
        Dim ms As New ModernSlider With {
            .Minimum = min, .Maximum = max, .Value = startVal, .Tag = tag,
            .Location = New Point(10, 25), .Width = 200 ' Reduced from 220 to fit TextBox
        }

        ' 4. txtVal (Right Side) - Now positioned at the end of the slider
        Dim txtVal As New TextBox With {
            .Name = "txt_" & tag, .Text = "0.00", .Width = 50, .Height = 18,
            .BackColor = Color.FromArgb(45, 45, 45), .ForeColor = Color.Cyan,
            .BorderStyle = BorderStyle.FixedSingle, .TextAlign = HorizontalAlignment.Center,
            .Location = New Point(ms.Right + 10, 22), .ReadOnly = True
        }

        ' 5. Rail colors based on Tag
        Select Case tag
            Case "Exposure", "Whites" : ms.AccentColor = Color.White
            Case "Temp" : ms.AccentColor = Color.Orange
            Case "Tint" : ms.AccentColor = Color.Magenta
            Case Else : ms.AccentColor = Color.DodgerBlue
        End Select

        ' 6. Event Linking
        AddHandler ms.Scroll, targetHandler
        ' AddHandler ms.MouseDown, AddressOf GlobalSlider_MouseDown

        ' 7. Add Controls
        pnl.Controls.Add(lblName)
        pnl.Controls.Add(ms)
        pnl.Controls.Add(txtVal)

        Return pnl
    End Function



End Module
