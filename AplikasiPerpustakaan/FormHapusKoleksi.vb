﻿Public Class FormHapusKoleksi
    Private Sub ButtonHapus_Click(sender As Object, e As EventArgs) Handles ButtonHapus.Click
        'MessageBox.Show(LabelKoleksi.Text.ToString)
        FormPerpustakaan.ListBoxKoleksi.Items.Remove(LabelKoleksi.Text.ToString)
        Me.Close()
        FormPerpustakaan.Show()
    End Sub

    Private Sub FormHapusKoleksi_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        FormPerpustakaan.Show()
    End Sub
End Class