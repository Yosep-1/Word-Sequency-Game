Public Class Form1
    ' Untuk Icon Profile
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Dim ofd As New OpenFileDialog
        ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
        If ofd.ShowDialog() = DialogResult.OK Then
            PictureBox1.Image = Image.FromFile(ofd.FileName)
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        End If
    End Sub
    ' Untuk Icon Silang
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        ' Kode konfirmasi keluar
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to exit?", "Exit Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        ' Jika user memilih Yes, aplikasi tertutup
        If result = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    ' List untuk menyimpan urutan kata yang akan dikirim ke halaman game
    Public Shared UrutanKata As New List(Of String)

    ' Kumpulan database kata (bisa bulan, hari, atau benda)
    Dim databaseKata As String() = {"January", "February", "March", "April", "May", "June",
                                    "July", "August", "September", "October", "November", "December"}

    ' Daftar pilihan warna yang kontras 
    Dim daftarWarna As Color() = {Color.DarkSlateBlue, Color.Firebrick, Color.DarkGreen, Color.SaddleBrown,
                                  Color.Teal, Color.DarkMagenta, Color.DarkSlateGray}

    ' Sub untuk mengacak kata ke Label
    Sub AcakKata()
        UrutanKata.Clear()

        Dim rand As New Random()
        Dim daftarLabel As Label() = {lblKata1, lblKata2, lblKata3, lblKata4, lblKata5, lblKata6, lblKata7}

        ' 1. Acak urutan kata dari database
        Dim kataAcak = databaseKata.OrderBy(Function() rand.Next()).ToList()

        ' 2. Acak urutan warna dari daftarWarna
        Dim warnaAcak = daftarWarna.OrderBy(Function() rand.Next()).ToList()

        For i As Integer = 0 To daftarLabel.Length - 1
            ' Pasang kata acak
            daftarLabel(i).Text = kataAcak(i)

            ' Pasang warna acak (ambil dari list warnaAcak)
            daftarLabel(i).ForeColor = warnaAcak(i)

            ' Simpan kata ke list shared untuk dipanggil di FormGame nanti
            UrutanKata.Add(kataAcak(i))
        Next
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AcakKata()
    End Sub

    'ganti kata tiap kali form muncul 
    Private Sub Form1_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        AcakKata()
    End Sub

    ' Tombol PLAY untuk pindah ke halaman tantangan
    Private Sub btnPlay_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FormGame.Show()
        Me.Hide()
    End Sub

End Class
