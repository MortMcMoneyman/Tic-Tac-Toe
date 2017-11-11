Public Class Form1
    Public Spielbrett(0, 0) As Feld
    Dim feldAnzahl As Integer
    Dim aktSpieler As Integer = 1
    Dim Punkte1 = 0, Punkte2 = 0

    Dim Anzeige1, Anzeige2 As New Button
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Setup(3, 3)
        feldAnzahl = Spielbrett.Length
        Anzeige(75)
        Anzeige1.BackColor = Color.Aqua

    End Sub

    Sub Mouse_Click(sender As Object, e As EventArgs) Handles Me.Click
        For Each i In Spielbrett
            If i.Button.Bounds.contains(Me.PointToClient(MousePosition)) Then
                If i.Besitzer = 0 Then
                    i.Besitzer = aktSpieler
                    i.Farbe()
                    feldAnzahl -= 1
                Else Exit Sub
                End If
            End If
        Next

        'Überprüfen ob jmd gewonnen hat
        If Spielende(aktSpieler) Then
            If aktSpieler = 1 Then
                Anzeige1.Text = "Gewinner"
                Punkte1 += 1
                aktSpieler = 2
            ElseIf aktSpieler = 2 Then
                Anzeige2.Text = "Gewinner"
                Punkte2 += 1
                aktSpieler = 1
            End If

            Neustart()
        End If

        'Unentschieden
        If feldAnzahl = 0 Then
            Anzeige1.Text = "Unentschieden"
            Anzeige1.BackColor = Color.Aqua
            Anzeige2.Text = "Unentschieden"
            Anzeige2.BackColor = Color.Red

            Neustart()
            Exit Sub
        End If

        'Spielerwechsel
        If aktSpieler = 1 Then
            aktSpieler = 2
            Anzeige1.BackColor = SystemColors.ButtonFace
            Anzeige2.BackColor = Color.Red
        Else
            aktSpieler = 1
            Anzeige1.BackColor = Color.Aqua
            Anzeige2.BackColor = SystemColors.ButtonFace
        End If
    End Sub

    Sub Minimax()

    End Sub

    Sub Neustart()
        Application.DoEvents()
        Threading.Thread.Sleep(1000)

        For Each i As Feld In Spielbrett
            i.Besitzer = 0
            i.Farbe()
        Next
        feldAnzahl = Spielbrett.Length

        aktSpieler = 1
        Anzeige1.Text = Punkte1
        Anzeige2.Text = Punkte2
        Anzeige1.BackColor = Color.Aqua
        Anzeige2.BackColor = SystemColors.ButtonFace
    End Sub

    Function Spielende(spieler)
        Spielende = False
        If Spielbrett(0, 0).Besitzer = spieler Then
            If Spielbrett(0, 1).Besitzer = spieler And Spielbrett(0, 2).Besitzer = spieler Then Return True 'erste Zeile
            If Spielbrett(1, 0).Besitzer = spieler And Spielbrett(2, 0).Besitzer = spieler Then Return True 'erste Spalte
            If Spielbrett(1, 1).Besitzer = spieler And Spielbrett(2, 2).Besitzer = spieler Then Return True 'diagonal Oben, Links - Unten, Rechts
        End If

        If Spielbrett(1, 1).Besitzer = spieler Then
            If Spielbrett(0, 1).Besitzer = spieler And Spielbrett(2, 1).Besitzer = spieler Then Return True 'zweite Zeile
            If Spielbrett(1, 0).Besitzer = spieler And Spielbrett(1, 2).Besitzer = spieler Then Return True 'zweite Spalte
        End If

        If Spielbrett(2, 2).Besitzer = spieler Then
            If Spielbrett(0, 2).Besitzer = spieler And Spielbrett(1, 2).Besitzer = spieler Then Return True 'dritte Zeile
            If Spielbrett(2, 0).Besitzer = spieler And Spielbrett(2, 1).Besitzer = spieler Then Return True 'dritte Spalte
        End If

        If Spielbrett(2, 0).Besitzer = spieler Then
            If Spielbrett(1, 1).Besitzer = spieler And Spielbrett(0, 2).Besitzer = spieler Then Return True 'diagonal Unten, Links - Oben, Rechts
        End If
    End Function

    '========= Setup ========='
    Sub Setup(hrztAnzahl, vertAnzahl)
        Dim positionX = 0, positionY = 0
        Dim breite = Me.ClientSize.Width / hrztAnzahl
        Dim hoehe = Me.ClientSize.Height / vertAnzahl

        vertAnzahl -= 1
        hrztAnzahl -= 1
        ReDim Spielbrett(vertAnzahl, hrztAnzahl)
        For i As Integer = 0 To vertAnzahl
            For j As Integer = 0 To hrztAnzahl
                Spielbrett(i, j) = New Feld
                Spielbrett(i, j).Create(CStr(i & ", " & j), positionX, positionY, breite, hoehe)

                positionX += breite
            Next j

            positionX = 0
            positionY += hoehe
        Next i
    End Sub

    Sub Anzeige(hoehe)
        'Dim Anzeige1, Anzeige2 As New Button

        Me.Size = New Size(Me.Width, Me.Height + hoehe)
        'Anzeige1.Create("Anzeige1", 0, Me.ClientSize.Height - hoehe, Me.ClientSize.Width / 2, hoehe)
        'Anzeige2.Create("Anzeige2", Me.ClientSize.Width / 2, Me.ClientSize.Height - hoehe, Me.ClientSize.Width / 2, hoehe)

        With Anzeige1
            .Parent = Me
            .Parent.Controls.Add(Anzeige1)

            .Name = "Anzeige1"
            .Location = New Point(0, Me.ClientSize.Height - hoehe)
            .Size = New Size(Me.ClientSize.Width / 2, hoehe)
            .FlatStyle = FlatStyle.Flat
            .BackColor = SystemColors.ButtonFace
            .Enabled = False

            .Text = Punkte2
        End With

        With Anzeige2
            .Parent = Me
            .Parent.Controls.Add(Anzeige2)

            .Name = "Anzeige2"
            .Location = New Point(Me.ClientSize.Width / 2, Me.ClientSize.Height - hoehe)
            .Size = New Size(Me.ClientSize.Width / 2, hoehe)
            .FlatStyle = FlatStyle.Flat
            .BackColor = SystemColors.ButtonFace
            .Enabled = False

            .Text = Punkte1
        End With

    End Sub
End Class
