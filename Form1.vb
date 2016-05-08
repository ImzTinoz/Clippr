Imports System.IO
Imports System.Text

Public Class Form1
    'Created By Orbit

    Public loc As String
    Public rndm As String
    Public zghg As String

    Sub New()
        InitializeComponent()
        loc = SaveLocation()



    End Sub

    Public Function SaveLocation()
        Dim fileName = Path.GetRandomFileName()
        Return fileName

    End Function




    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form2.Hide()
        Dim area As Rectangle
        Dim capture As System.Drawing.Bitmap
        Dim graph As Graphics
        area = Form2.Bounds
        capture = New System.Drawing.Bitmap(area.Width, area.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        graph = Graphics.FromImage(capture)
        graph.CopyFromScreen(area.X, area.Y, 0, 0, area.Size, CopyPixelOperation.SourceCopy)
        PictureBox1.Image = capture


    End Sub





    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Form2.Show()
    End Sub



    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        ' Save location defined below

        If (Not System.IO.Directory.Exists("C:\Clippr\Saves\")) Then
            System.IO.Directory.CreateDirectory("C:\Clippr\Saves\")
        End If

        If Not PictureBox1.Image Is Nothing Then

            PictureBox1.Image.Save("C:\Clippr\Saves\" + loc + ".png", System.Drawing.Imaging.ImageFormat.Png)
        Else
            MessageBox.Show("Imagebox is empty!")
            Application.Restart()
        End If


        Call FTPUpload()


    End Sub


    Sub FTPUpload()
        ' Set file location on server
        Dim request As System.Net.FtpWebRequest = DirectCast(System.Net.WebRequest.Create("ftp://serverip" + loc + ".png"), System.Net.FtpWebRequest)
        'Set Credentials here
        request.Credentials = New System.Net.NetworkCredential("username", "password")

        request.Method = System.Net.WebRequestMethods.Ftp.UploadFile


        'Choose the file to upload
        Dim file() As Byte = System.IO.File.ReadAllBytes("C:\Clippr\Saves\" + loc + ".png")


        Dim strz As System.IO.Stream = request.GetRequestStream()

        strz.Write(file, 0, file.Length)

        strz.Close()

        strz.Dispose()


        Call Upload()

    End Sub
    'Opens the new image in the default web browser
    Sub Upload()
        Dim webAddress As String = ("location of website" + loc + ".png")
        Process.Start(webAddress)

        Call CleanUp()

    End Sub

    Sub CleanUp()
        Application.Restart()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
