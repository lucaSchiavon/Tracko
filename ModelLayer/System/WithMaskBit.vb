Public Class WithMaskBit

    Protected Function IsBitActive(ByVal Numero As Integer, ByVal IndexBit As Integer) As Boolean

        Dim MaskBit As String = Convert.ToString(Numero, 2)
        Dim arrayChar As Char() = MaskBit.ToCharArray
        Array.Reverse(arrayChar)
        MaskBit = New String(arrayChar)

        If IndexBit > MaskBit.Length - 1 Then
            Return False
        End If

        If MaskBit(IndexBit) = "1" Then
            Return True
        End If

        Return False
    End Function

    Protected Function GetIntMaskBit(ByVal oList As List(Of Boolean)) As Integer

        Dim _Number As Integer = 0

        For i As Integer = 0 To oList.Count - 1
            If oList(i) Then
                _Number += Math.Pow(2, i)
            End If
        Next

        Return _Number

    End Function

End Class
