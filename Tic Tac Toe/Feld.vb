Public Class Feld
    Public Besitzer = 0
    Public Button

    Public Sub Create(name, x, y, breite, hoehe)
        Dim button = New Button
        With button
            .Parent = Form1
            .Parent.Controls.Add(button)

            .Name = name
            .Location = New Point(x, y)
            .Size = New Size(breite, hoehe)
            .FlatStyle = FlatStyle.Flat
            .BackColor = SystemColors.ButtonFace
            .Enabled = False
        End With
        Me.Button = button
    End Sub

    Public Sub Farbe()
        If Me.Besitzer = 1 Then
            Me.Button.backcolor = Color.Aqua
        ElseIf Me.Besitzer = 2 Then
            Me.Button.backcolor = Color.Red
        Else
            Me.Button.backcolor = SystemColors.ButtonFace
        End If
    End Sub
End Class
