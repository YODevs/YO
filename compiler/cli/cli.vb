﻿Imports System.Runtime.InteropServices

''' <summary>
''' <en>
''' interface appearance changes such as font and sizes and waiting for new commands can be performed in this class.
''' </en>
''' <fa>
''' تغییرات ظاهری از جمله فونت و سایز ها و انتظار برای دستورات جدید در این کلاس انجام می گیرد
''' </fa>
''' </summary>
Public Class cli

    ''' <summary>
    ''' <en>
    ''' interface appearance changes such as font and other features of console can be performed in this function.
    ''' </en>
    ''' <fa>
    ''' تغییرات ظاهری از جمله فونت و دیگر خاصیت های کنسول در این تابع انجام می گیرد.
    ''' </fa>
    ''' </summary>
    Friend Shared Sub init_cli(displayinitcommand As Boolean)
        If Console.IsOutputRedirected = False Then
            'Change defualt font to Consolas font.
            Dim consolefontinfo As CONSOLE_FONT_INFO_EX = New CONSOLE_FONT_INFO_EX()
            consolefontinfo.cbSize = CUInt(Marshal.SizeOf(consolefontinfo))
            consolefontinfo.nFont = 16
            consolefontinfo.dwFontSize.X = 0
            consolefontinfo.dwFontSize.Y = 16
            consolefontinfo.FontWeight = 700
            consolefontinfo.FaceName = "Consolas"
            SetCurrentConsoleFontEx(GetStdHandle(StdHandle.OutputHandle), True, consolefontinfo)
            Console.BufferHeight = 370
        End If
        Console.Title = conrex.TITLE
        If displayinitcommand Then display_init_command()
    End Sub

    Private Shared Sub display_init_command()
        Console.Write(constcli.maincohelp)
    End Sub

    Public Shared Sub goto_cmdln(args() As String)
        Console.Write(vbCrLf)
        While True
            interactioncmd.get_interaction(args)
            parseargs.parse_data(args)
        End While
    End Sub

    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Shared Function SetCurrentConsoleFontEx(ByVal ConsoleOutput As IntPtr, ByVal MaximumWindow As Boolean, ByRef ConsoleCurrentFontEx As CONSOLE_FONT_INFO_EX) As Int32

    End Function
    Private Enum StdHandle
        OutputHandle = -11
    End Enum

    <DllImport("kernel32")>
    Private Shared Function GetStdHandle(ByVal index As StdHandle) As IntPtr

    End Function
    Private Shared ReadOnly INVALID_HANDLE_VALUE As IntPtr = New IntPtr(-1)


    <StructLayout(LayoutKind.Sequential)>
    Public Structure COORD
        Public X As Short
        Public Y As Short

        Public Sub New(ByVal X As Short, ByVal Y As Short)
            Me.X = X
            Me.Y = Y
        End Sub
    End Structure

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
    Public Structure CONSOLE_FONT_INFO_EX
        Public cbSize As UInteger
        Public nFont As UInteger
        Public dwFontSize As COORD
        Public FontFamily As Integer
        Public FontWeight As Integer
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)>
        Public FaceName As String
    End Structure

End Class
