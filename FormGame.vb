Public Class FormGame
    Dim urutanUser As New List(Of String)
    Dim sisaWaktu As Integer = 8
    Dim gameAktif As Boolean = False
    Dim gameSelesai As Boolean = False
    Dim rand As New Random()

    ' Warna yang kontras dan jelas
    Dim daftarWarna As Color() = {Color.DarkBlue, Color.Firebrick, Color.DarkGreen, Color.SaddleBrown, Color.Teal}

    Private Sub FormGame_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Interval = 1000
        Timer1.Enabled = False
        lblCountdown.Text = "⌛ 8"
        Label5.Text = "0"
        ResetGame()
    End Sub

    Sub ResetGame()
        gameAktif = False
        gameSelesai = False
        urutanUser.Clear()
        sisaWaktu = 8
        lblCountdown.Text = "⌛ 8"
        Button1.Enabled = True

        Dim labelsBulan As Label() = {Label6, Label7, Label8, Label9, Label10, Label11, Label12, Label13, Label14, Label15, Label16, Label17}

        Dim cbs As CheckBox() = {CheckBox1, CheckBox2, CheckBox3, CheckBox4, CheckBox5, CheckBox6, CheckBox7, CheckBox8, CheckBox9, CheckBox10, CheckBox11, CheckBox12}
        For Each cb In cbs
            cb.Checked = False
            cb.ForeColor = Color.Black
        Next
        Me.BackColor = SystemColors.Control
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If gameSelesai Then
            MessageBox.Show("Game over! Please click 'New Game' to play again.", "Notice")
            Exit Sub
        End If

        ' Cek Data Form1
        If Form1.UrutanKata.Count = 0 Then
            MessageBox.Show("Data soal dari Form1 kosong!", "Error")
            Exit Sub
        End If

        gameAktif = True
        Timer1.Start()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim res As DialogResult = MessageBox.Show("Do you want to play again?", "New Game", MessageBoxButtons.YesNo)
        If res = DialogResult.Yes Then
            Timer1.Stop()
            Form1.Show()
            Me.Close()
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        sisaWaktu -= 1
        lblCountdown.Text = "⌛ " & sisaWaktu

        Me.BackColor = Color.FromArgb(rand.Next(150, 255), rand.Next(150, 255), rand.Next(150, 255))

        ' Warna Tulisan Bulan Random
        Dim cbs As CheckBox() = {CheckBox1, CheckBox2, CheckBox3, CheckBox4, CheckBox5, CheckBox6, CheckBox7, CheckBox8, CheckBox9, CheckBox10, CheckBox11, CheckBox12}
        For Each cb In cbs
            cb.ForeColor = daftarWarna(rand.Next(daftarWarna.Length))
        Next

        If sisaWaktu <= 0 Then
            Timer1.Stop()
            gameAktif = False
            gameSelesai = True
            MessageBox.Show("Time's Up!", "Game Over")
            HitungSkor()
        End If
    End Sub

    Private Sub CheckBox_Click(sender As Object, e As EventArgs) Handles _
        CheckBox1.Click, CheckBox2.Click, CheckBox3.Click, CheckBox4.Click, CheckBox5.Click, CheckBox6.Click,
        CheckBox7.Click, CheckBox8.Click, CheckBox9.Click, CheckBox10.Click, CheckBox11.Click, CheckBox12.Click

        If Not gameAktif Then
            ' Jika game belum PLAY, jangan kasih centang
            DirectCast(sender, CheckBox).Checked = False
            Exit Sub
        End If
    End Sub

    Sub HitungSkor()
        Dim benar As Integer = 0

        ' 1. Masukkan semua CheckBox ke dalam array sesuai urutan di form
        Dim cbs As CheckBox() = {CheckBox1, CheckBox2, CheckBox3, CheckBox4, CheckBox5, CheckBox6, CheckBox7, CheckBox8, CheckBox9, CheckBox10, CheckBox11, CheckBox12}

        ' 2. Daftar nama bulan 
        Dim namaBulan As String() = {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"}

        ' 3. Lakukan pengecekan
        For i As Integer = 0 To cbs.Length - 1
            ' Jika CheckBox ke-i dicentang oleh user
            If cbs(i).Checked = True Then
                Dim bulanTerpilih As String = namaBulan(i).ToLower().Trim()

                ' Cek apakah bulan tersebut ada dalam daftar soal dari Form1
                For Each bulanSoal In Form1.UrutanKata
                    If bulanTerpilih = bulanSoal.ToLower().Trim() Then
                        benar += 1
                        Exit For
                    End If
                Next
            End If
        Next

        ' 4. Hitung Nilai Akhir
        Dim nilai As Integer = 0
        Dim totalSoal As Integer = Form1.UrutanKata.Count

        If totalSoal > 0 Then
            ' Rumus: (Jumlah Benar / Total Soal) * 100
            nilai = CInt((benar / totalSoal) * 100)
        End If

        ' 5. Tampilkan ke Label Skor (Label5)
        Label5.Text = nilai.ToString()
        Label5.Refresh()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Form1.Show()
        Me.Close()
    End Sub
End Class