Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient

Public Class ConnectionMananger
    Implements IDisposable

    ''''stringa di connessione usata da tutte le funzioni
    Private _connectionString As String = String.Empty
    ''''tabella degli sqlParameter da aggiungere al command. La key è il ParameterName
    Private _parameterList As New Hashtable



    ''''connection esterna che sopravvive alla chiusura della funzione. Usata per i DataReader
    Private _connection As SqlConnection
    Private _dr As DbDataReader

    Public Sub New()
        Me.ClearParameters()
        Dim connectionStringMaster As String = System.Configuration.ConfigurationManager.ConnectionStrings("MyConnection").ToString


        'testo e setto la connectionString Master
        If TestConnectionString(connectionStringMaster) Then
            Me._connectionString = connectionStringMaster
        Else
            Throw New Exception("Invalid Connection String, can not connect to database")
        End If
    End Sub

    Public Sub New(ByVal dbName As String)
        Me.ClearParameters()
        Dim connectionStringMaster As String = System.Configuration.ConfigurationManager.ConnectionStrings("MyConnection").ToString

        connectionStringMaster = connectionStringMaster.Replace("[DBNAME]", dbName)

        'testo e setto la connectionString Master
        If TestConnectionString(connectionStringMaster) Then
            Me._connectionString = connectionStringMaster
        Else
            Throw New Exception("Invalid Connection String, can not connect to database")
        End If
    End Sub

    Public Sub New(ByVal IsExternal As Boolean, ByVal connectionStringName As String)
        Me.ClearParameters()
        Dim connectionStringMaster As String = System.Configuration.ConfigurationManager.ConnectionStrings(connectionStringName).ToString


        'testo e setto la connectionString Master
        If TestConnectionString(connectionStringMaster) Then
            Me._connectionString = connectionStringMaster
        Else
            Throw New Exception("Invalid Connection String, can not connect to database")
        End If
    End Sub




    Private Sub OpenConnection(ByRef connection As SqlConnection)
        If Not connection.State = ConnectionState.Open Then
            connection.Open()
        End If
    End Sub

    Public Sub CloseConnection(Optional ByRef connection As SqlConnection = Nothing)
        If Not _dr Is Nothing Then
            If Not _dr.IsClosed Then
                _dr.Close()
            End If
        End If

        ''''se non passo la connection da chiudere chiudo quella interna
        If connection Is Nothing Then
            connection = _connection
        End If

        If Not connection Is Nothing Then
            If connection.State = ConnectionState.Open Or connection.State = ConnectionState.Broken Then
                connection.Close()
            End If
            connection.Dispose()
        End If
    End Sub

    Public Function TestConnectionString(ByVal connectionString As String) As Boolean
        Using connection As New SqlConnection(connectionString)
            Try
                Me.OpenConnection(connection)
                Return True
            Catch ex As Exception
                Return False
            Finally
                Me.CloseConnection(connection)
            End Try
        End Using
    End Function





    Public Sub ClearParameters()
        _parameterList.Clear()
    End Sub

    Public Sub AddOrReplaceParameter(ByVal name As String,
                                     ByVal value As Object,
                                     Optional ByVal sqlDbType As System.Data.SqlDbType = SqlDbType.NVarChar,
                                     Optional ByVal direction As System.Data.ParameterDirection = ParameterDirection.Input)
        Dim sqlParameter As New SqlParameter(name, value)
        sqlParameter.Direction = direction
        sqlParameter.SqlDbType = sqlDbType

        Select Case sqlParameter.SqlDbType
            Case Data.SqlDbType.Char,
                Data.SqlDbType.NChar,
                Data.SqlDbType.NVarChar,
                Data.SqlDbType.VarChar

                sqlParameter.Size = -1
        End Select

        If Not _parameterList.ContainsKey(name) Then
            _parameterList.Add(name, sqlParameter)
        Else
            _parameterList(name) = sqlParameter
        End If
    End Sub

    Public Function GetParameterValue(ByVal name As String) As Object
        If _parameterList.ContainsKey(name) Then
            Return DirectCast(_parameterList(name), SqlParameter).Value
        Else
            Return Nothing
        End If
    End Function





    ''' <summary>
    ''' ritorna il rowcount
    ''' </summary>
    ''' <param name="queryString"></param>
    ''' <param name="commandType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExecuteNonQuery(ByVal queryString As String, Optional ByVal commandType As CommandType = System.Data.CommandType.Text) As Integer
        If Not String.IsNullOrEmpty(_connectionString) And Not String.IsNullOrEmpty(queryString) Then
            Using connection As New SqlConnection(_connectionString)
                Dim command As New SqlCommand(queryString, connection)
                command.CommandType = commandType

                For Each de As DictionaryEntry In _parameterList
                    command.Parameters.Add(DirectCast(de.Value, SqlParameter))
                Next

                Me.OpenConnection(connection)
                Dim rowCount As Integer = command.ExecuteNonQuery()
                'disassocio la lista di parametri dal command
                command.Parameters.Clear()
                command.Dispose()
                Return rowCount
            End Using
        Else
            Return 0
        End If
    End Function

    Public Function GetScalar(ByVal queryString As String, Optional ByVal commandType As CommandType = System.Data.CommandType.Text) As Object
        If Not String.IsNullOrEmpty(_connectionString) And Not String.IsNullOrEmpty(queryString) Then
            Using connection As New SqlConnection(_connectionString)
                Dim command As New SqlCommand(queryString, connection)
                command.CommandType = commandType
                For Each de As DictionaryEntry In _parameterList
                    command.Parameters.Add(DirectCast(de.Value, SqlParameter))
                Next

                Me.OpenConnection(connection)
                Dim returnValue As Object = command.ExecuteScalar()
                'disassocio la lista di parametri dal command
                command.Parameters.Clear()
                command.Dispose()

                Return returnValue
            End Using
        Else
            Return Nothing
        End If
    End Function

    Public Function GetDataSet(ByVal queryString As String, Optional ByVal commandType As CommandType = System.Data.CommandType.Text) As DataSet
        If Not String.IsNullOrEmpty(_connectionString) And Not String.IsNullOrEmpty(queryString) Then
            Using connection As New SqlConnection(_connectionString)
                Dim command As New SqlCommand(queryString, connection)
                command.CommandType = commandType
                For Each de As DictionaryEntry In _parameterList
                    command.Parameters.Add(DirectCast(de.Value, SqlParameter))
                Next

                Dim adapter As New SqlDataAdapter(command)
                Dim dataset As New DataSet()
                adapter.Fill(dataset)
                'disassocio la lista di parametri dal command
                command.Parameters.Clear()
                command.Dispose()
                adapter.Dispose()

                Return dataset
            End Using
        Else
            Return Nothing
        End If
    End Function

    Public Function GetDataReader(ByVal queryString As String, Optional ByVal commandType As CommandType = System.Data.CommandType.Text) As DbDataReader
        If Not String.IsNullOrEmpty(_connectionString) And Not String.IsNullOrEmpty(queryString) Then
            '''''uso la connection esterna altrimenti si eliminerebbe alla fine del metodo rendendo
            ''''' inutilizzabile i DataReader
            _connection = New SqlConnection(_connectionString)

            Dim command As New SqlCommand(queryString, _connection)
            command.CommandTimeout = 1800
            command.CommandType = commandType
            For Each de As DictionaryEntry In _parameterList
                command.Parameters.Add(DirectCast(de.Value, SqlParameter))
            Next

            Me.OpenConnection(_connection)
            _dr = command.ExecuteReader()
            'disassocio la lista di parametri dal command
            command.Parameters.Clear()
            command.Dispose()

            Return _dr
        Else
            Return Nothing
        End If
    End Function


    ''' <summary>
    ''' ritorna  la query sottoforma di stringa con eventuali parametri popolati
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <remarks></remarks>
    Public Function DebugQuery(ByVal sql As String) As String

        Dim _debug As String = ""
        Dim _size As Integer = 0

        For Each de As DictionaryEntry In _parameterList

            Dim oSqlParameter As SqlParameter = DirectCast(de.Value, SqlParameter)

            Select Case oSqlParameter.SqlDbType
                Case Data.SqlDbType.Char,
                    Data.SqlDbType.NChar,
                    Data.SqlDbType.NVarChar,
                    Data.SqlDbType.VarChar

                    If oSqlParameter.Size = -1 Then
                        _debug &= "DECLARE @" & de.Key & " as " & oSqlParameter.SqlDbType.ToString & "(MAX)  = '" & oSqlParameter.Value & "'<br />"
                    Else
                        _debug &= "DECLARE @" & de.Key & " as " & oSqlParameter.SqlDbType.ToString & "(" & oSqlParameter.Size & ")  = '" & oSqlParameter.Value & "'<br />"
                    End If
                Case Else
                    _debug &= "DECLARE @" & de.Key & " as " & oSqlParameter.SqlDbType.ToString & " = " & oSqlParameter.Value & "<br />"
            End Select


        Next

        _debug &= sql

        Return _debug

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' dispose managed state (managed objects).
                Me.CloseConnection()
                Me._parameterList.Clear()
                Me._parameterList = Nothing
            End If

            ' free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
