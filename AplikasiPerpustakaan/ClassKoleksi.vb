﻿Imports System.Text
Imports MySql.Data.MySqlClient

Public Class ClassKoleksi
    Private namaKoleksi As String
    Private dirGambarBuku
    Private deskripsiKoleksi As String
    Private penerbit As String
    Private jenisKoleksi As String
    Private tahunTerbit As Integer
    Private lokasi As String
    Private tanggalMasukKoleksi As Date
    Private stock As Integer
    Private bahasa As String
    Private kategori As String
    Private listKategori As New List(Of String)
    Private listKoleksi As New List(Of String)

    Private koleksiDataTable As New ArrayList()

    Public Shared dbConn As New MySqlConnection
    Public Shared sqlCommand As New MySqlCommand
    Public Shared sqlRead As MySqlDataReader
    Private sqlQuery As String

    Private server As String = "localhost"
    Private username As String = "root"
    Private password As String = ""
    Private database As String = "db_perpustakaan"

    Public Function ConvertKoleksiToString(vals As List(Of String))
        Dim builder As StringBuilder = New StringBuilder()
        For Each val As String In vals
            builder.Append(val).Append("|")
        Next

        'Convert to string.
        Dim res = builder.ToString
        Return res
    End Function

    Public Function ConvertStringToKoleksi(value As String)
        Dim arr() As String = value.Split("|")

        'Convert to list.
        Dim vals As List(Of String) = arr.ToList()
        Return vals
    End Function

    Public Function AddKoleksiDataTable(dir_gambar As String,
                                        nama_koleksi As String,
                                        jenis_koleksi As String,
                                        penerbit_koleksi As String,
                                        deskripsi_koleksi As String,
                                        tahun_terbit As String,
                                        lokasi_rak As String,
                                        tanggal_masuk As String,
                                        stock_koleksi As Integer,
                                        bahasa_koleksi As String,
                                        kategori_koleksi As String)
        koleksiDataTable.Add({dir_gambar,
                             nama_koleksi,
                             jenis_koleksi,
                             penerbit_koleksi,
                             deskripsi_koleksi,
                             tahun_terbit,
                             lokasi_rak,
                             tanggal_masuk,
                             stock_koleksi,
                             bahasa_koleksi,
                             kategori_koleksi})
    End Function

    Public Function RemoveKoleksiDataTable(index As Integer)
        koleksiDataTable.RemoveAt(index)
    End Function

    Public ReadOnly Property getKoleksiDataTable() As ArrayList
        Get
            Return koleksiDataTable
        End Get
    End Property

    'setter getter
    Public Property GSNamaKoleksi() As String
        Get
            Return namaKoleksi
        End Get
        Set(ByVal value As String)
            namaKoleksi = value
        End Set
    End Property

    Public Property GSDirGambarBuku() As String
        Get
            Return dirGambarBuku
        End Get
        Set(ByVal value As String)
            dirGambarBuku = value
        End Set
    End Property

    Public Property GSDeskripsiKoleksi() As String
        Get
            Return deskripsiKoleksi
        End Get
        Set(ByVal value As String)
            deskripsiKoleksi = value
        End Set
    End Property

    Public Property GSPenerbit() As String
        Get
            Return penerbit
        End Get
        Set(ByVal value As String)
            penerbit = value
        End Set
    End Property

    Public Property GSJenisKoleksi() As String
        Get
            Return jenisKoleksi
        End Get
        Set(ByVal value As String)
            jenisKoleksi = value
        End Set
    End Property

    Public Property GSTahunTerbit() As Integer
        Get
            Return tahunTerbit
        End Get
        Set(ByVal value As Integer)
            tahunTerbit = value
        End Set
    End Property

    Public Property GSLokasi() As String
        Get
            Return lokasi
        End Get
        Set(ByVal value As String)
            lokasi = value
        End Set
    End Property

    Public Property GSTanggalMasukKoleksi() As Date
        Get
            Return tanggalMasukKoleksi
        End Get
        Set(ByVal value As Date)
            tanggalMasukKoleksi = value
        End Set
    End Property

    Public Property GSStock() As Integer
        Get
            Return stock
        End Get
        Set(ByVal value As Integer)
            stock = value
        End Set
    End Property

    Public Property GSBahasa() As String
        Get
            Return bahasa
        End Get
        Set(ByVal value As String)
            bahasa = value
        End Set
    End Property

    Public Property GSKategori() As String
        Get
            Return kategori
        End Get
        Set(ByVal value As String)
            kategori = value
        End Set
    End Property

    'kategori
    Public Function AddKategori(value As String)
        listKategori.Add(value)
    End Function

    Public ReadOnly Property getKategoriItem() As List(Of String)
        Get
            Return listKategori
        End Get
    End Property

    Public Function ResetKategori()
        listKategori.Clear()
    End Function

    Public Property GSListKoleksi()
        Get
            Return listKoleksi
        End Get
        Set(ByVal value)
            listKoleksi = value
        End Set
    End Property

    'koleksi
    Public Function AddKoleksi(value As String)
        listKoleksi.Add(value)
    End Function

    Public Function RemoveKoleksi(value As String)
        listKoleksi.Remove(value)
    End Function

    Public ReadOnly Property getKoleksiItem() As List(Of String)
        Get
            Return listKoleksi
        End Get
    End Property

    Public Function GetDataKoleksiDatabase() As DataTable
        Dim result As New DataTable

        dbConn.ConnectionString = "server =" + server + ";" + "user id =" + username + ";" + "password =" + password + ";" + "database =" + database
        dbConn.Open()
        sqlCommand.Connection = dbConn
        sqlCommand.CommandText = "  SELECT 
                                    id_koleksi AS 'ID',
                                    nama_koleksi AS 'Nama Koleksi', 
                                    jenis_koleksi AS 'Jenis Koleksi', 
                                    penerbit AS 'Penerbit', 
                                    tahun_terbit AS 'Tahun Terbit', 
                                    tanggal_masuk_koleksi AS 'Tanggal Masuk', 
                                    lokasi AS 'Lokasi Rak', 
                                    stock AS 'Stock', 
                                    bahasa AS 'Bahasa' 
                                    FROM koleksi;"

        sqlRead = sqlCommand.ExecuteReader

        result.Load(sqlRead)
        sqlRead.Close()
        dbConn.Close()
        Return result
    End Function

    Public Function AddDataKoleksiDatabase(dir_gambar As String,
                                           nama_koleksi As String,
                                           jenis_koleksi As String,
                                           penerbit_koleksi As String,
                                           deskripsi_koleksi As String,
                                           tahun_terbit As String,
                                           lokasi_rak As String,
                                           tanggal_masuk As Date,
                                           stock_koleksi As Integer,
                                           bahasa_koleksi As String,
                                           kategori_koleksi As String)
        dbConn.ConnectionString = "server =" + server + ";" + "user id =" + username + ";" + "password =" + password + ";" + "database =" + database
        Try
            dbConn.Open()
            sqlCommand.Connection = dbConn
            sqlQuery = "INSERT INTO koleksi(
                        nama_koleksi, 
                        dir_gambar, 
                        deskripsi, 
                        penerbit, 
                        jenis_koleksi, 
                        tahun_terbit, 
                        lokasi,
                        tanggal_masuk_koleksi,
                        stock,
                        bahasa,
                        kategori) VALUE('" _
                        & nama_koleksi & "', '" _
                        & dir_gambar & "', '" _
                        & deskripsi_koleksi & "', '" _
                        & penerbit_koleksi & "', '" _
                        & jenis_koleksi & "', '" _
                        & tahun_terbit & "', '" _
                        & lokasi_rak & "', '" _
                        & tanggal_masuk.ToString("yyyy/MM/dd") & "', '" _
                        & stock_koleksi & "', '" _
                        & bahasa_koleksi & "', '" _
                        & kategori_koleksi & "')"
            sqlCommand = New MySqlCommand(sqlQuery, dbConn)
            sqlRead = sqlCommand.ExecuteReader

            dbConn.Close()

            sqlRead.Close()
            dbConn.Close()
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            dbConn.Dispose()
        End Try
    End Function

    Public Function GetDataKoleksiByIDDatabase(ID As Integer) As List(Of String)
        Dim result As New List(Of String)

        dbConn.ConnectionString = "server =" + server + ";" + "user id =" + username + ";" + "password =" + password + ";" + "database =" + database
        dbConn.Open()

        sqlCommand.Connection = dbConn
        sqlCommand.CommandText = "  SELECT id_koleksi,
                                    nama_koleksi,
                                    dir_gambar,
                                    deskripsi,
                                    penerbit,
                                    jenis_koleksi,
                                    tahun_terbit,
                                    lokasi,
                                    tanggal_masuk_koleksi,
                                    stock,
                                    bahasa,
                                    kategori
                                    FROM koleksi
                                    WHERE id_koleksi='" & ID & "'"

        sqlRead = sqlCommand.ExecuteReader

        While sqlRead.Read
            result.Add(sqlRead.GetString(0).ToString())
            result.Add(sqlRead.GetString(1).ToString())
            result.Add(sqlRead.GetString(2).ToString())
            result.Add(sqlRead.GetString(3).ToString())
            result.Add(sqlRead.GetString(4).ToString())
            result.Add(sqlRead.GetString(5).ToString())
            result.Add(sqlRead.GetString(6).ToString())
            result.Add(sqlRead.GetString(7).ToString())
            result.Add(sqlRead.GetString(8).ToString())
            result.Add(sqlRead.GetString(9).ToString())
            result.Add(sqlRead.GetString(10).ToString())
            result.Add(sqlRead.GetString(11).ToString())
        End While

        sqlRead.Close()
        dbConn.Close()
        Return result
    End Function

    Public Function UpdateDataKoleksiByIDDatabase(ID As Integer,
                                                  dir_gambar As String,
                                                  nama_koleksi As String,
                                                  jenis_koleksi As String,
                                                  penerbit_koleksi As String,
                                                  deskripsi_koleksi As String,
                                                  tahun_terbit As String,
                                                  lokasi_rak As String,
                                                  tanggal_masuk As String,
                                                  stock_koleksi As Integer,
                                                  bahasa_koleksi As String,
                                                  kategori_koleksi As String
                                                  )
        dbConn.ConnectionString = "server =" + server + ";" + "user id =" + username + ";" + "password =" + password + ";" + "database =" + database

        Try
            dbConn.Open()
            sqlCommand.Connection = dbConn
            sqlQuery = " UPDATE koleksi SET " &
                        "nama_koleksi='" & nama_koleksi & "', " &
                        "dir_gambar='" & dir_gambar & "', " &
                        "deskripsi='" & deskripsi_koleksi & "', " &
                        "penerbit='" & penerbit_koleksi & "', " &
                        "jenis_koleksi='" & jenis_koleksi & "', " &
                        "tahun_terbit='" & tahun_terbit & "', " &
                        "lokasi='" & lokasi_rak & "', " &
                        "tanggal_masuk_koleksi='" & Convert.ToDateTime(tanggal_masuk).ToString("yyyy/MM/dd") & "', " &
                        "stock='" & stock_koleksi & "', " &
                        "bahasa='" & bahasa_koleksi & "', " &
                        "kategori='" & kategori_koleksi & "' " &
                        "WHERE id_koleksi='" & ID & "'"

            sqlCommand = New MySqlCommand(sqlQuery, dbConn)
            sqlRead = sqlCommand.ExecuteReader

            dbConn.Close()
            sqlRead.Close()
            dbConn.Close()
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            dbConn.Dispose()
        End Try
    End Function

    Public Function DeleteDataKoleksiByIDDatabase(ID As Integer)
        dbConn.ConnectionString = "server =" + server + ";" + "user id =" + username + ";" + "password =" + password + ";" + "database =" + database

        Try
            dbConn.Open()
            sqlCommand.Connection = dbConn
            sqlQuery = "DELETE FROM koleksi WHERE id_koleksi='" & ID & "'"

            Debug.WriteLine(sqlQuery)

            sqlCommand = New MySqlCommand(sqlQuery, dbConn)
            sqlRead = sqlCommand.ExecuteReader

            dbConn.Close()
            sqlRead.Close()
            dbConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dbConn.Dispose()
        End Try
    End Function
End Class
