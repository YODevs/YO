﻿Public Class execln
    Friend Shared Sub nv_check_command(result As parseargs._parseresult)
        If result.command = Nothing Or result.command = conrex.SPACE Then
            Return
        End If
        Dim getindex As Int16 = cmdlnproc.check_master_key(result.command)
        If getindex = -1 Then
            'Set Error , command not found .
            dserr.set_error("Command Error", "'" & result.command & "' - Command not found." & vbLf & "This command may be used in updated versions of Labra.")
            Return
        End If

        If cmdlnproc.cmd(getindex).withargs = False AndAlso result.args.Count > 0 Then
            dserr.set_error("Parameter Error", "'" & result.command & "' - This command does not support any parameters, enter this command without entering a parameter.")
            Return
        ElseIf cmdlnproc.cmd(getindex).maxargs < result.args.Count Then
            dserr.set_error("Parameter Error", "'" & result.command & "' - The number of parameters of this command is too much.")
            Return
        End If

        Dim cmdexeclninstance As New execln
        If cmdlnproc.cmd(getindex).withargs = False Then
            CallByName(cmdexeclninstance, "rp_" & result.command, CallType.Method, Nothing)
        Else
            CallByName(cmdexeclninstance, "rp_" & result.command, CallType.Method, result.args)
        End If
    End Sub
    Public Sub New()
    End Sub
    Public Sub rp_test()
        Dim peconsolecolor As Int16 = Console.ForegroundColor
        Console.ForegroundColor = System.ConsoleColor.DarkGreen
        Console.Write("Bravo !!!!! Labra is ready to execute your commands.
You can type 'Help' to view commands.")
        Console.ForegroundColor = peconsolecolor
    End Sub

    Public Sub rp_exit()
        Environment.Exit(1)
    End Sub

    Public Sub rp_new()
        Dim projcreator As New crproj()
        projcreator.init_project()
    End Sub
    Public Sub rp_version()
        Console.Write(conrex.VER)
    End Sub

    Public Sub rp_add()
        Console.Write(vbLf & "# Select an item: " & vbCr)
        Select Case YOOrderList.YOList.ShowMenu("!['Class File [.yo]','New Folder','Custom File']")
            Case "New Folder"

            Case "Class File [.yo]"

            Case "Custom File"

        End Select
    End Sub

End Class
