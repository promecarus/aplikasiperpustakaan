﻿Public Class FormPerpustakaan
    Public Shared dataKoleksi As ClassKoleksi
    Public Shared listDataKoleksi As List(Of String)
    Public Shared selectedTableKoleksi
    Public Shared selectedTableKoleksiNama

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        dataKoleksi = New ClassKoleksi
        UpdateTableDataArrayList()
    End Sub

    Private Sub FormPerpustakaan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        'UpdateTableDataArrayList()
        ReloadDataTableDatabase()
        BiarGaBiru()
    End Sub

    Private Sub ReloadDataTableDatabase()
        DataGridViewKoleksi.DataSource = dataKoleksi.GetDataKoleksiDatabase
    End Sub

    Public Sub tambahKoleksi(nama As String)
        ListBoxKoleksi.Items.Add(nama)
    End Sub

    Private Sub TambahKoleksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TambahKoleksiToolStripMenuItem.Click
        FormTambahKoleksi.Show()
        Me.Hide()
    End Sub

    Private Sub ToolStripButtonPlus_Click(sender As Object, e As EventArgs) Handles ToolStripButtonPlus.Click
        FormTambahKoleksi.Show()
        Me.Hide()
    End Sub

    Private Sub ToolStripButtonMinus_Click(sender As Object, e As EventArgs) Handles ToolStripButtonMinus.Click
        If String.IsNullOrEmpty(ListBoxKoleksi.SelectedItem) Then
            MessageBox.Show("Pilih koleksi yang ingin dihapus")
        Else
            FormHapusKoleksi.LabelKoleksi.Text = ListBoxKoleksi.SelectedItem.ToString
            Me.Hide()
            FormHapusKoleksi.Show()
        End If
    End Sub

    Friend Function BlankImage() As Image
        Dim oBM As New Bitmap(1, 1)
        oBM.SetPixel(0, 0, Color.Transparent)
        Return oBM
    End Function

    Public Sub UpdateTableDataArrayList()
        'DataGridViewKoleksi.Rows.Clear()
        For Each rowKoleksi In dataKoleksi.getKoleksiDataTable()
            Dim dataTable = {
                If(rowKoleksi(0) IsNot Nothing, Image.FromFile(rowKoleksi(0)), BlankImage()),
                rowKoleksi(1),
                rowKoleksi(2),
                rowKoleksi(3),
                rowKoleksi(4),
                rowKoleksi(5),
                rowKoleksi(6),
                rowKoleksi(7),
                rowKoleksi(8),
                rowKoleksi(9),
                rowKoleksi(10)
            }
            DataGridViewK.Rows.Add(dataTable)
        Next
    End Sub

    Private Sub DataGridViewKoleksi_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewKoleksi.CellClick
        'selectedTableKoleksi = DataGridViewKoleksi.CurrentRow.Index
        Try
            Dim index As Integer = e.RowIndex
            Dim selectedRow As DataGridViewRow
            selectedRow = DataGridViewKoleksi.Rows(index)

            selectedTableKoleksi = selectedRow.Cells(0).Value
            selectedTableKoleksiNama = selectedRow.Cells(1).Value
        Catch ex As Exception
            MessageBox.Show("Fitur sorting belum tersedia!")
        End Try
    End Sub

    Private Sub ButtonShow_Click(sender As Object, e As EventArgs) Handles ButtonShow.Click
        Try
            Dim dataSelected As List(Of String) = dataKoleksi.GetDataKoleksiByIDDatabase(selectedTableKoleksi)

            dataKoleksi.GSNamaKoleksi = dataSelected(1)
            dataKoleksi.GSDirGambarBuku = dataSelected(2)
            dataKoleksi.GSDeskripsiKoleksi = dataSelected(3)
            dataKoleksi.GSPenerbit = dataSelected(4)
            dataKoleksi.GSJenisKoleksi = dataSelected(5)
            dataKoleksi.GSTahunTerbit = dataSelected(6)
            dataKoleksi.GSLokasi = dataSelected(7)
            dataKoleksi.GSTanggalMasukKoleksi = dataSelected(8)
            dataKoleksi.GSStock = dataSelected(9)
            dataKoleksi.GSBahasa = dataSelected(10)
            Dim data_koleksi As List(Of String) = dataKoleksi.ConvertStringToKoleksi(dataSelected(11))

            For Each info_tambah In data_koleksi
                dataKoleksi.AddKategori(info_tambah)
            Next

            Dim infoTambah = New FormInfoTambahKoleksi()
            infoTambah.Show()
        Catch ex As Exception
            MessageBox.Show("Pilih row terlebih dahulu!")
        End Try
    End Sub

    Private Sub FormPerpustakaan_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        FormLogin.Show()
    End Sub

    Private Sub ButtonUpdate_Click(sender As Object, e As EventArgs) Handles ButtonUpdate.Click
        Try
            Dim selectedKoleksi As List(Of String) = dataKoleksi.GetDataKoleksiByIDDatabase(selectedTableKoleksi)

            dataKoleksi.GSNamaKoleksi = selectedKoleksi(1)
            dataKoleksi.GSDirGambarBuku = selectedKoleksi(2)
            dataKoleksi.GSDeskripsiKoleksi = selectedKoleksi(3)
            dataKoleksi.GSPenerbit = selectedKoleksi(4)
            dataKoleksi.GSJenisKoleksi = selectedKoleksi(5)
            dataKoleksi.GSTahunTerbit = selectedKoleksi(6)
            dataKoleksi.GSLokasi = selectedKoleksi(7)
            dataKoleksi.GSTanggalMasukKoleksi = selectedKoleksi(8)
            dataKoleksi.GSStock = selectedKoleksi(9)
            dataKoleksi.GSBahasa = selectedKoleksi(10)
            Dim data_kategori As List(Of String) = dataKoleksi.ConvertStringToKoleksi(selectedKoleksi(11))

            For Each info_kategori In data_kategori
                dataKoleksi.AddKategori(info_kategori)
            Next

            Dim formUpdate = New FormUpdateKoleksi()
            formUpdate.Show()
            Me.Hide()
        Catch ex As Exception
            MessageBox.Show("Pilih row terlebih dahulu!")
        End Try
    End Sub

    Private Sub ButtonRemove_Click(sender As Object, e As EventArgs) Handles ButtonRemove.Click
        Try
            Dim selectedKoleksi As List(Of String) = dataKoleksi.GetDataKoleksiByIDDatabase(selectedTableKoleksi)
            dataKoleksi.GSNamaKoleksi = selectedKoleksi(1)

            Me.Hide()
            Dim formHapusKoleksi = New FormHapusKoleksi()
            formHapusKoleksi.Show()
        Catch ex As Exception
            MessageBox.Show("Pilih row terlebih dahulu!")
        End Try
    End Sub

    Private Sub BiarGaBiru()
        DataGridViewKoleksi.ClearSelection()
    End Sub
End Class
