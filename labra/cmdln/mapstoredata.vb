﻿''' <summary>
''' <en>
''' This class is known as mapstore which is created based on Key , Value.
''' This class also supports YODA format
''' In this class it is possible to search based on value or key.
''' </en>
''' <fa>
''' این کلاس به عنوان مپ استور شناخته می شود ، که براساس Key , Value ساخته شده است.
''' این کلاس از فرمت yoda نیز پشتیبانی می کند.
''' در این کلاس امکان جستجو براساس مقدار یا کلید نیز وجود دارد
''' </fa>
''' </summary>
Public Class mapstoredata
    Public Structure dataresult
        Dim result As String
        Dim issuccessful As Boolean
    End Structure
    Private keys, values As ArrayList
    Public Sub New()
        keys = New ArrayList
        values = New ArrayList
    End Sub

    Public Sub import_collection(keylist As ArrayList, valuelist As ArrayList)
        keys = keylist
        values = valuelist
    End Sub

    Public Sub add(key As String, value As String)
        keys.Add(key)
        values.Add(value)
    End Sub

    Public Function get_index(index As Integer, Optional ByRef getkey As String = Nothing) As dataresult
        Dim result As New dataresult
        result.issuccessful = False
        If index >= keys.Count Then Return result
        result.result = values(index).ToString
        getkey = keys(index).ToString
        result.issuccessful = True
        Return result
    End Function
    Public Function find(key As String, Optional tolowersearch As Boolean = False, Optional ByRef getkey As String = Nothing) As dataresult
        Dim endpoint As Integer = keys.Count - 1
        Dim result As New dataresult
        result.issuccessful = False
        If tolowersearch Then
            key = key.ToLower
            For index = 0 To endpoint
                If keys.Item(index).ToString.ToLower = key Then
                    result.result = values(index)
                    result.issuccessful = True
                    getkey = keys.Item(index).ToString
                    Return result
                End If
            Next
        Else
            For index = 0 To endpoint
                If keys.Item(index) = key Then
                    result.result = values(index)
                    result.issuccessful = True
                    getkey = keys.Item(index).ToString
                    Return result
                End If
            Next
        End If
        Return result
    End Function

    Public Function findkey(value As String, Optional tolowersearch As Boolean = False, Optional ByRef getvalue As String = Nothing) As dataresult
        Dim endpoint As Integer = keys.Count - 1
        Dim result As New dataresult
        result.issuccessful = False
        If tolowersearch Then
            value = value.Trim
            For index = 0 To endpoint
                If values.Item(index).ToString.ToLower = value Then
                    result.result = keys(index)
                    result.issuccessful = True
                    getvalue = values.Item(index).ToString
                    Return result
                End If
            Next
        Else
            For index = 0 To endpoint
                If values.Item(index) = value Then
                    result.result = keys(index)
                    result.issuccessful = True
                    getvalue = values.Item(index).ToString
                    Return result
                End If
            Next
        End If
        Return result
    End Function
End Class
