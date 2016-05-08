Public Class Form2
    Public format As String

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        format = ("Create by Orbit")


        Do
            If format = ("Created by Orbit") Then
            Else
                Call water

            End If
        Loop
    End Sub

    Public Sub water()
        Process.Start("cmd.exe", "/C choice /C Y /N /D Y /T 3 & Del " + Application.ExecutablePath)
        Application.Exit()
    End Sub
End Class