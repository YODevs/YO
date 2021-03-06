﻿Imports System.IO

Public Class crproj

    Public Structure _projstruct
        Dim assemblyname As String
        Dim typeproject As String
        Dim srcpath As String
        Dim createbatchfile As Boolean
        Dim targetframework As String
    End Structure

    Dim yoda As YODA.YODA_Format
    Dim proj As _projstruct
    Dim typesitem As ArrayList
    Public Sub New()
        yoda = New YODA.YODA_Format
        proj = New _projstruct
        typesitem = New ArrayList
        typesitem.Add("Console [.exe]")
        typesitem.Add("Library [.dll]")
    End Sub

    Public Sub init_project()
        proj.assemblyname = get_assembly_name()
        proj.typeproject = get_type_of_project()
        'proj.targetframework = set_target_framwork()
        proj.createbatchfile = set_batch_file()
        create_prerequisites()
    End Sub

    Private Sub create_prerequisites()
        Try
            Dim path As String = conrex.ENVCURDIR & "\" & proj.assemblyname
            If Directory.Exists(path) Then
                dserr.set_error("Create prerequisites", path & " - This path already exists.")
                Return
            End If
            Directory.CreateDirectory(path)

            Directory.CreateDirectory(path & "\target\debug")
            Directory.CreateDirectory(path & "\target\release")
            Directory.CreateDirectory(path & "\src")
            Directory.CreateDirectory(path & "\assets")

            If proj.typeproject = "console" Then
                File.WriteAllText(path & "\src\main.yo", conrex.MAINDEFCODE)
            ElseIf proj.typeproject = "library" Then
                File.WriteAllText(path & "\src\main.yo", conrex.MAINLIBDEFCODE)
            End If

            File.WriteAllText(path & "\labra.yoda", get_labra_setting())
            If proj.createbatchfile Then File.WriteAllText(path & "\build & run.bat", conrex.BATCHFILESTR)
            Console.Clear()
            Dim peconsolecolor As Int16 = Console.ForegroundColor
            Console.ForegroundColor = System.ConsoleColor.DarkGreen
            Console.Write(proj.assemblyname & " - The project was created successfully.")
            Console.ForegroundColor = peconsolecolor

            Console.Write(vbLf & "# Select the next task: " & vbCr)
            Select Case YOOrderList.YOList.ShowMenu("!['Show folder','Continue','Exit']")
                Case "Show folder"
                    Process.Start(path)
                Case "Contiune"
                    Return
                Case "Exit"
                    Environment.Exit(1)
            End Select
        Catch ex As Exception
            dserr.set_error("Create prerequisites", ex.Message)
        End Try
    End Sub

    Private Function get_labra_setting() As String
        Dim key, value As ArrayList
        key = New ArrayList
        value = New ArrayList

        key.Add("assemblyname")
        value.Add(proj.assemblyname)

        key.Add("outputtype")
        value.Add(proj.typeproject)

        key.Add("sourcepath")
        value.Add("\src")

        key.Add("assetspath")
        value.Add("\assets")

        ' key.Add("targetframework")
        'value.Add(proj.targetframework)

        Return yoda.WriteYODA_Map(key, value, False)
    End Function

    Private Function get_assembly_name() As String
        Dim checkit As Boolean = True
        Dim asmname As String = String.Empty
        While checkit
            If asmname = String.Empty Then
                Console.Write("# Assembly name [ex:myapp]: ")
            Else
                Console.Write(vbLf & "# Assembly name [ex:myapp]: ")
            End If
            asmname = Console.ReadLine.Trim
            check_assembly_name(checkit, asmname)
        End While
        Return asmname
    End Function
    Private Sub check_assembly_name(ByRef continueloop As Boolean, asmname As String)
        Dim errtext As String = String.Empty
        If asmname = Nothing Then
            errtext = "Assembly name can not be empty, write a custom name like 'myfirstapp'."
        ElseIf Asc(asmname(0).ToString.ToUpper) < 65 OrElse Asc(asmname(0).ToString.ToUpper) > 90 Then
            errtext = "Assembly name must start with letters , [a-z,A-Z]."
        Else
            For index = 0 To asmname.Length - 1
                If Asc(asmname(index).ToString.ToUpper) < 65 OrElse Asc(asmname(index).ToString.ToUpper) > 90 Then
                    If IsNumeric(asmname(index)) OrElse asmname(index) = "_" Then
                        Continue For
                    End If
                    errtext = "[ " & asmname(index) & " ]- The assembly name can be a combination of letters, numbers and the character '_'."
                    Exit For
                End If
            Next
        End If

        If errtext <> conrex.NULL Then
            dserr.set_error("Assembly name", errtext)
        Else
            continueloop = False
        End If
    End Sub
    Private Function get_type_of_project() As String
        Console.Write(vbLf & "# Select project type:")
        Dim menudata As String = yoda.WriteYODA(typesitem)
        Dim tproj As String = YOOrderList.YOList.ShowMenu(menudata)
        tproj = tproj.Remove(tproj.IndexOf("[")).Trim.ToLower
        Return tproj
    End Function
    Private Function set_target_framwork() As String
        Dim pathlist As New ArrayList
        Dim namelist As New ArrayList
        For index = 0 To Microsoft.Build.Utilities.TargetDotNetFrameworkVersion.VersionLatest
            Dim pathdotnetframework As String = Microsoft.Build.Utilities.ToolLocationHelper.GetPathToDotNetFrameworkFile("ilasm.exe", index)
            If pathdotnetframework <> String.Empty AndAlso File.Exists(pathdotnetframework) Then
                pathlist.Add(pathdotnetframework)
                namelist.Add([Enum].GetName(GetType(Microsoft.Build.Utilities.TargetDotNetFrameworkVersion), index))
            End If
        Next
        Console.Write(vbLf & "# Select target framework:")
        Dim menudata As String = yoda.WriteYODA(namelist)
        Dim snetframework As String = YOOrderList.YOList.ShowMenu(menudata)
        Return snetframework
    End Function
    Private Function set_batch_file() As Boolean
        Console.Write(vbLf & "# Need a bash file to compile and run fast?[y/N] > ")
        Select Case Console.ReadKey.KeyChar.ToString.ToLower
            Case "y"
                Console.Write(vbLf)
                Return True
            Case Else
                Console.Write(vbLf)
                Return False
        End Select
    End Function
End Class
